using ClArc.Async.Core;
using System.Threading.Tasks;

namespace ClArc.Tests.Async
{
    public class NormalInteractor : IInputPort<InputData,OutputData>
    {
        public async Task<OutputData> Handle(InputData inputData)
        {
            return null;
        }
    }
}