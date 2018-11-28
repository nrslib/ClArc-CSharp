using System;
using System.Reflection;
using ClArc.Sync.Core;

namespace ClArc.Sync.Invoker
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

        public TResponse Invoke<TResponse>(IRequest<TResponse> request)
            where TResponse : IResponse
        {
            var instance = provider.GetService(usecaseType);

            object responseObject;
            try
            {
                responseObject = handleMethod.Invoke(instance, new object[] {request});
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            var response = (TResponse) responseObject;

            return response;
        }
    }
}