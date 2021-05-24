using System;
using System.Reflection;
using System.Threading.Tasks;
using ClArc.Async.Core;

namespace ClArc.Async.Invoker
{
    internal class UseCaseInvoker : IUseCaseInvokerAsync
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

        public async Task<TResponse> Invoke<TResponse>(IInputData<TResponse> inputData) where TResponse : IOutputDataAsync
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


        //public Task<TResponse> async Invoke<TResponse>(IInputData<TResponse> inputData)
        //    where TResponse : IOutputDataAsync
        //{
        //    var instance = provider.GetService(usecaseType);

        //    try
        //    {
        //        handleMethod.Invoke(instance, new object[] {inputData});
        //    }
        //    catch (TargetInvocationException e)
        //    {
        //        throw e.InnerException;
        //    }
        //}
    }
}