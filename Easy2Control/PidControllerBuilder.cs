using System;

namespace Easy2Control
{
    public class PidControllerBuilder
    {
        private IObservable<double> setpoints;
        private IObservable<double> actuals;
        private TimeSpan sampleRate;
        private double pCoefficient;
        private double iCoefficient;
        private TimeSpan iTimeConstant;
        private double dCoefficient;
        private TimeSpan dTimeConstant;

        public PidControllerBuilder UseSetpoints(IObservable<double> setpoints)
        {
            this.setpoints = setpoints;

            return this;
        }

        public PidControllerBuilder UseActuals(IObservable<double> actuals)
        {
            this.actuals = actuals;

            return this;
        }

        public PidControllerBuilder SampleRate(TimeSpan sampleRate)
        {
            this.sampleRate = sampleRate;

            return this;
        }

        public PidControllerBuilder Proportional(double coefficient)
        {
            pCoefficient = coefficient;

            return this;
        }

        public PidControllerBuilder Integral(double coefficient, TimeSpan timeConstant)
        {
            iCoefficient = coefficient;
            iTimeConstant = timeConstant;

            return this;
        }

        public PidControllerBuilder Derivative(double coefficient, TimeSpan timeConstant)
        {
            dCoefficient = coefficient;
            dTimeConstant = timeConstant;

            return this;
        }

        public PidController Build()
        {
            return new PidController(setpoints, actuals, sampleRate, pCoefficient, iCoefficient, iTimeConstant, dCoefficient, dTimeConstant);
        }
    }
}
