> Please note: This repo is still work in progress

# Easy2Control

`Easy2Control` is a library that contains a simple implementation for [process controllers](https://en.wikipedia.org/wiki/Control_theory) based on [Reactive Extensions for .NET](https://www.nuget.org/packages/System.Reactive/).

The most prominent example is a [PID Controller](https://en.wikipedia.org/wiki/PID_controller) that is used to regulate a process variable (e.g. the room temperature) to a specific setpoint (e.g. a comfortable 21° C). It uses a [proportional](https://en.wikipedia.org/wiki/Proportional_control), an [integral](https://en.wikipedia.org/wiki/Integral) and a [derivative](https://en.wikipedia.org/wiki/Derivative) term to provide feedback in the control loop. `Easy2Control` takes care of the heavy lifting and let's you focus on your domain!

See how it works in the [samples](./Easy2Control.Samples/Program.cs).

## Available controllers

* P Controller
* PI Controller
* PID Controller

## Easy2Setup

```csharp
// Declare your observables to feed actual values and setpoints into the controller

...

var builder = new PidControllerBuilder()
    .SampleRate(TimeSpan.FromMilliseconds(25))
    .UseActuals(actuals)
    .UseSetpoints(setpoints)
    .Proportional(5)
    .Integral(1.0, TimeSpan.FromSeconds(1.0))
    .Derivative(2.0, TimeSpan.FromMilliseconds(200));

using (var pidController = builder.Build())
using (pidController.Feedback.Subscribe(Regulate))
{
    // Do something in the meantime
}

private void Regulate(double feedback)
{
    // Control your hardware
}
```

See, it's so Easy2Control!