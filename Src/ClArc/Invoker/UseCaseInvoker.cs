using System;
using System.Reflection;
using System.Threading.Tasks;
using ClArc.Core;

namespace ClArc.Invoker
{
    internal class UseCaseInvoker : IUseCaseInvoker
    {
        private readonly MethodInfo handleMethod; 
        private readonly IServiceProvider provider;
        private readonly Type usecaseType;

        public UseCaseInvoker(Type usecaseType, Type implementsType, IServiceProvider provider)
        {
            this.usecaseType = usecaseType;
            this.provider = provider;

            handleMethod = implementsType.GetMethod("Handle");
        }

        public TResponse Invoke<TResponse>(IInputData<TResponse> inputData)
            where TResponse : IOutputData
        {
            var instance = provider.GetService(usecaseType);

            object responseObject;
            try
            {
                responseObject = handleMethod.Invoke(instance, new object[] { inputData });
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            var response = (TResponse)responseObject;

            return response;
        }
        public async Task<TResponse> InvokeAsync<TResponse>(IInputData<TResponse> inputData)
    where TResponse : IOutputData
        {
            var instance = provider.GetService(usecaseType);

            Task<TResponse> responseObject;
            try
            {
                responseObject = (Task<TResponse>)handleMethod.Invoke(instance, new object[] { inputData });
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            return await responseObject;
        }

        public async Task InvokeAsyncVoidOutput(IInputDataVoidOutput inputData)
        {
            var instance = provider.GetService(usecaseType);

            Task responseObject;
            try
            {
                responseObject = (Task)handleMethod.Invoke(instance, new object[] { inputData });
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
            await responseObject;
            return;
        }

        public void InvokeVoidOutput(IInputDataVoidOutput inputData)
        {
            var instance = provider.GetService(usecaseType);

            try
            {
                handleMethod.Invoke(instance, new object[] { inputData });
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            return;
        }
    }
}