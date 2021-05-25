using ClArc.Sync.Core;
using System.Threading.Tasks;

namespace ClArc.Sync.Invoker
{
    public interface IUseCaseInvoker
    {
        TResponse Invoke<TResponse>(IInputData<TResponse> inputData) where TResponse : IOutputData;
        void InvokeVoidOutput(IInputDataVoidOutput inputData);
        Task<TResponse> InvokeAsync<TResponse>(IInputData<TResponse> inputData) where TResponse : IOutputData;
        Task InvokeAsyncVoidOutput(IInputDataVoidOutput inputData);
    }
}