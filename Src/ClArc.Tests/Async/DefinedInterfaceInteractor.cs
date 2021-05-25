using ClArc.Async.Core;
using System.Threading.Tasks;

namespace ClArc.Tests.Async
{
    public class DefinedInterfaceInteractor : IDefinedInterfaceInputPort
    {
        public async Task<OutputData> Handle(InputData inputData)
        {
            return await Task.FromResult(new OutputData());
        }
    }

    public interface IDefinedInterfaceInputPort : IInputPort<InputData, OutputData, Task<OutputData>>
    {
    }
}