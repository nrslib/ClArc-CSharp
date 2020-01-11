using ClArc.Async.Core;

namespace ClArc.Async.Invoker
{
    public interface IUseCaseInvoker
    {
        void Invoke(IInputData inputData);
    }
}