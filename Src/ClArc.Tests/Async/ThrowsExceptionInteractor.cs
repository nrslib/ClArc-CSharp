using System;
using ClArc.Async.Core;

namespace ClArc.Tests.Async
{
    public class ThrowsExceptionInteractor : IInputPort<InputData>
    {
        public void Handle(InputData inputData)
        {
            throw new Exception();
        }
    }
}