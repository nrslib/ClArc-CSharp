using System;

namespace ClArc.Dependency
{
    public interface IServiceRegistration
    {
        void AddTransient<T>() where T : class;
        IServiceProvider BuildServiceProvider();
    }
}
