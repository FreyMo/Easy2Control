using System;
using System.Reactive.Linq;
using System.Threading;

namespace Easy2Control.Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            var setpoints = Observable.Return(0.9);
            var actuals = Observable.Return(0.89).Concat(Observable.Interval(TimeSpan.FromMilliseconds(1500)).Select(counter =>
            {
                return counter switch
                {
                    <= 5 => 1.0,
                    > 5 and <= 10 => 1.6,
                    > 10 and <= 15 => 0.7,
                    _ => 3
                };
            }));

            var builder = new PidControllerBuilder()
                .SampleRate(TimeSpan.FromMilliseconds(25))
                .UseActuals(actuals)
                .UseSetpoints(setpoints)
                .Proportional(5)
                .Integral(1.0, TimeSpan.FromSeconds(1.5))
                .Derivative(2.0, TimeSpan.FromMilliseconds(200));

            using (var pidController = builder.Build())
            using (pidController.Feedback.Sample(TimeSpan.FromMilliseconds(100)).Subscribe(Output))
            using (actuals.Subscribe(x => Console.WriteLine($"Actual: {x:N3}, Deviation: {x - 0.9:N3}")))
            {
                while (true)
                {
                    Thread.Sleep(200);

                    if (Console.KeyAvailable)
                    {
                        break;
                    }
                }
            }
        }

        private static void Output(double x)
        {
            Console.WriteLine($"Feedback:\t{x:N3}");
        }
    }
}
