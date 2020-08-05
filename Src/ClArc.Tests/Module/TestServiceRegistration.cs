using System;
using ClArc.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace ClArc.Tests.Module
{
    class TestServiceRegistration : IServiceRegistration
    {
        private readonly ServiceCollection collection = new ServiceCollection();

        public void AddTransient<T>() where T : class
        {
            collection.AddTransient<T>();
        }

        public IServiceProvider BuildServiceProvider()
        {
            return collection.BuildServiceProvider();
        }
    }
}
