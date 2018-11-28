using System;

namespace ClArc.Sync.Invoker
{
    public interface IUseCaseInvokerFactory
    {
        IUseCaseInvoker Generate(Type usecaseType, Type implementsType, IServiceProvider provider);
    }
}