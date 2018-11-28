using ClArc.Sync.Core;

namespace ClArc.Sync.Invoker
{
    public interface IUseCaseInvoker
    {
        TResponse Invoke<TResponse>(IRequest<TResponse> request) where TResponse : IResponse;
    }
}