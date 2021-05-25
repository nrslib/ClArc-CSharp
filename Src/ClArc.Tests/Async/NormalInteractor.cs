using ClArc.Async.Core;
using System.Threading.Tasks;

namespace ClArc.Tests.Async
{
    public class NormalInteractor : IInputPort<InputData,OutputData,Task<OutputData>>
    {
        public async Task<OutputData> Handle(InputData inputData)
        {
            return await Task.FromResult(new OutputData());
        }
    }
}