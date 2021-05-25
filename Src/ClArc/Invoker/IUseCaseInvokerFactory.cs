using System;

namespace ClArc.Invoker
{
    public interface IUseCaseInvokerFactory
    {
        IUseCaseInvoker Generate(Type usecaseType, Type implementsType, IServiceProvider provider);
    }
}