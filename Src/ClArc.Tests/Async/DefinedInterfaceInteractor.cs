using ClArc.Async.Core;

namespace ClArc.Tests.Async
{
    public class DefinedInterfaceInteractor : IDefinedInterfaceInputPort
    {
        public void Handle(InputData inputData)
        {
        }
    }

    public interface IDefinedInterfaceInputPort : IInputPort<InputData>
    {
    }
}