namespace Easy2Control
{
    internal class ProportionalTerm : Term
    {
        public ProportionalTerm(double coefficient) : base(coefficient)
        {
        }

        public override double Feed(Sample sample)
        {
            return Coefficient * (sample.Setpoint - sample.Actual);
        }
    }
}
