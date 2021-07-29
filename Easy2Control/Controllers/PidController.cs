using System;

namespace Easy2Control
{
    public class PidController : Controller
    {
        public PidController(
            IObservable<double> setpoints,
            IObservable<double> actuals,
            TimeSpan sampleRate,
            double pCoefficient,
            double iCoefficient,
            TimeSpan iTimeConstant,
            double dCoefficient,
            TimeSpan dTimeConstant
            ) :
            base(
                setpoints,
                actuals,
                sampleRate,
                new ITerm[] {
                    new ProportionalTerm(pCoefficient),
                    new IntegralTerm(iCoefficient, iTimeConstant, sampleRate),
                    new DerivativeTerm(dCoefficient, dTimeConstant, sampleRate)
                })
        {
        }
    }
}
