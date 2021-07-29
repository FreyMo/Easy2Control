using System;

namespace Easy2Control
{
    internal abstract class Term : ITerm
    {
        protected Term(double coefficient)
        {
            if (coefficient <= 0) throw new ArgumentException("Coefficient must be greater than 0.");

            Coefficient = coefficient;
        }

        public double Coefficient { get; }

        public abstract double Feed(Sample sample);
    }
}
