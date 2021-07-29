namespace Easy2Control
{
    internal interface ITerm
    {
        double Coefficient { get; }

        double Feed(Sample sample);
    }
}