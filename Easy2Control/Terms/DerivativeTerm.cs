using System;
using System.Collections.Generic;

namespace Easy2Control
{
    internal class DerivativeTerm : Term
    {
        private readonly TimeSpan timeConstant;
        private readonly Queue<Sample> samples = new();

        public DerivativeTerm(double coefficient, TimeSpan timeConstant, TimeSpan sampleRate) : base(coefficient)
        {
            if (timeConstant < sampleRate)
            {
                throw new ArgumentException("Time constant for derivative term must be greater than the sample rate.");
            }

            this.timeConstant = timeConstant;
        }

        public override double Feed(Sample sample)
        {
            UpdateSamples(sample);

            // Calculate linear function between the two most appropriate actual values
            // get y from x, then calculate difference from y to latest sample
            // deal with edge cases
            
            // TODO: RETURN REAL VALUE
            return Coefficient * 0;
        }

        private void UpdateSamples(Sample sample)
        {
            samples.Enqueue(sample);

            while (samples.Peek().TimeStamp < sample.TimeStamp - 2 * timeConstant)
            {
                _ = samples.Dequeue();
            }
        }
    }
}
