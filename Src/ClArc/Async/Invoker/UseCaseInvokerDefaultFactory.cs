using System;

namespace ClArc.Async.Invoker
{
    public class UseCaseInvokerDefaultFactoryAsync :     IUseCaseInvokerFactoryAsync
    {
        public IUseCaseInvokerAsync Generate(Type usecaseType, Type implementsType, IServiceProvider provider)
        {
            return new UseCaseInvoker(usecaseType, implementsType, provider);
        }
    }
}