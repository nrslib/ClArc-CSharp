using System;

namespace ClArc.Invoker
{
    public class UseCaseInvokerDefaultFactory : IUseCaseInvokerFactory
    {
        public IUseCaseInvoker Generate(Type usecaseType, Type implementsType, IServiceProvider provider)
        {
            return new UseCaseInvoker(usecaseType, implementsType, provider);
        }
    }
}