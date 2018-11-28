using System;

namespace ClArc.Async.Invoker
{
    public interface IUseCaseInvokerFactory
    {
        IUseCaseInvoker Generate(Type usecaseType, Type implementsType, IServiceProvider provider);
    }
}