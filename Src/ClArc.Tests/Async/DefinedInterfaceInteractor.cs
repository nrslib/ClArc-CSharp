using ClArc.Async.Core;
using System.Threading.Tasks;

namespace ClArc.Tests.Async
{
    public class DefinedInterfaceInteractor : IDefinedInterfaceInputPort
    {
        public async Task<OutputData> Handle(InputData inputData)
        {
            return new OutputData();
        }
    }

    public interface IDefinedInterfaceInputPort : IInputPort<InputData, OutputData>
    {
    }
}