using System;

namespace Easy2Control
{
    internal readonly struct Sample
    {
        public Sample(double setpoint, double actual, DateTimeOffset timeStamp) : this()
        {
            Setpoint = setpoint;
            Actual = actual;
            TimeStamp = timeStamp;
        }

        public double Setpoint { get; }

        public double Actual { get; }

        public DateTimeOffset TimeStamp { get; }
    }
}
