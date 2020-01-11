using ClArc.Sync.Core;

namespace ClArc.Sync.Invoker
{
    public interface IUseCaseInvoker
    {
        TResponse Invoke<TResponse>(IInputData<TResponse> inputData) where TResponse : IOutputData;
    }
}