using System;
using System.Threading.Tasks;
using ClArc.Async.Core;

namespace ClArc.Tests.Async
{
    public class ThrowsExceptionInteractor : IInputPort<InputData, OutputData, Task<OutputData>>
    {


        public  Task<OutputData> Handle(InputData request)
        {
            throw new NotImplementedException();
        }
    }
}