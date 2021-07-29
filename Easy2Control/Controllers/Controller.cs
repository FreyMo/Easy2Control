using Easy2Control.Controllers;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Easy2Control
{
    public abstract class Controller : IController, IDisposable
    {
        private readonly ITerm[] terms;

        private readonly BehaviorSubject<double> actualSubject = new(0);
        private readonly BehaviorSubject<double> setpointSubject = new(0);
        private readonly BehaviorSubject<double> feedback = new(0);

        private readonly IObservable<Timestamped<long>> timer;
        private readonly IDisposable timerSubscription;
        private readonly IDisposable actualsSubscription;
        private readonly IDisposable setpointsSubscription;

        private bool isDisposed;

        internal Controller(IObservable<double> setpoints, IObservable<double> actuals, TimeSpan sampleRate, ITerm[] terms)
        {
            this.terms = terms;
            this.timer = Observable.Interval(sampleRate).Timestamp();

            timerSubscription = timer.Subscribe(Run);
            actualsSubscription = actuals.Subscribe(x => actualSubject.OnNext(x));
            setpointsSubscription = setpoints.Subscribe(x => setpointSubject.OnNext(x));
        }

        private void Run(Timestamped<long> sample)
        {
            var setpoint = setpointSubject.MostRecent(0).First();
            var actual = actualSubject.MostRecent(0).First();

            var derivations = terms.Select(term => term.Feed(new(setpoint, actual, sample.Timestamp)));

            feedback.OnNext(derivations.Sum());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    timerSubscription?.Dispose();
                    actualsSubscription?.Dispose();
                    setpointsSubscription?.Dispose();
                    actualSubject?.Dispose();
                    setpointSubject?.Dispose();
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public IObservable<double> Feedback => feedback.AsObservable();
    }
}
