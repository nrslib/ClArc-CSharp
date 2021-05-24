using ClArc.Async.Core;
using System.Threading.Tasks;

namespace ClArc.Async.Invoker
{
    public interface IUseCaseInvokerAsync
    {
        Task<TResponse> Invoke<TResponse>(IInputData<TResponse> inputData) where TResponse : IOutputDataAsync;
    }
}