using System;

namespace Easy2Control.Controllers
{
    public interface IController
    {
        IObservable<double> Feedback { get; }
    }
}