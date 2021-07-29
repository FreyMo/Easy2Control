using System;
using System.Collections.Generic;
using System.Linq;

namespace Easy2Control
{
    internal class IntegralTerm : Term
    {
        private readonly TimeSpan timeConstant;
        private readonly Queue<Sample> samples = new();

        public IntegralTerm(double coefficient, TimeSpan timeConstant, TimeSpan sampleRate) : base(coefficient)
        {
            if (timeConstant < 2 * sampleRate)
            {
                throw new ArgumentException("Time constant for integral term must be greater than twice the sample rate.");
            }

            this.timeConstant = timeConstant;
        }

        public override double Feed(Sample sample)
        {
            UpdateSamples(sample);

            var relevantSamples = samples.ToArray();
            var error = (sample.Setpoint - relevantSamples.Sum(s => s.Actual) / relevantSamples.Length);

            return Coefficient * error;
        }

        private void UpdateSamples(Sample sample)
        {
            samples.Enqueue(sample);

            while (samples.Peek().TimeStamp < sample.TimeStamp - timeConstant)
            {
                _ = samples.Dequeue();
            }
        }
    }
}
