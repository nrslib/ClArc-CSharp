using System;
using System.Reflection;
using ClArc.Async.Core;

namespace ClArc.Async.Invoker
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

        public void Invoke(IInputData inputData)
        {
            var instance = provider.GetService(usecaseType);

            try
            {
                handleMethod.Invoke(instance, new object[] {inputData});
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}