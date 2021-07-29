using System;

namespace Easy2Control
{
    public class PiController : Controller
    {
        public PiController(
            IObservable<double> setpoints,
            IObservable<double> actuals,
            TimeSpan sampleRate,
            double pCoefficient,
            double iCoefficient,
            TimeSpan iTimeConstant
            ) :
            base(
                setpoints,
                actuals,
                sampleRate,
                new ITerm[] {
                    new ProportionalTerm(pCoefficient),
                    new IntegralTerm(iCoefficient, iTimeConstant, sampleRate)
                })
        {
        }
    }
}
