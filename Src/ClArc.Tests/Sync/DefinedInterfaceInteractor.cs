using ClArc.Sync.Core;

namespace ClArc.Tests.Sync
{
    public class DefinedInterfaceInteractor : IDefinedInterfaceInputPort
    {
        public OutputData Handle(InputData inputData)
        {
            return new OutputData();
        }
    }

    public interface IDefinedInterfaceInputPort : IInputPort<InputData, OutputData>
    {
    }
}