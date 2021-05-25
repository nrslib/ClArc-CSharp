using ClArc.Core;

namespace ClArc.Tests.Sync
{
    public class NormalInteractor : IInputPort<InputData, OutputData>
    {
        public OutputData Handle(InputData inputData)
        {
            return new OutputData();
        }
    }
}