using System;

namespace Easy2Control
{
    public class PController : Controller
    {
        public PController(
            IObservable<double> setpoints,
            IObservable<double> actuals,
            TimeSpan sampleRate,
            double pCoefficient) :
            base(
                setpoints,
                actuals,
                sampleRate,
                new [] { new ProportionalTerm(pCoefficient) }
            )
        {
        }
    }
}
