using System;

namespace ClArc.Async.Invoker
{
    public interface IUseCaseInvokerFactoryAsync
    {
        IUseCaseInvokerAsync Generate(Type usecaseType, Type implementsType, IServiceProvider provider);
    }
}