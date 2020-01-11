using System;
using ClArc.Builder;
using ClArc.Tests.Async;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClArc.Tests
{
    [TestClass]
    public class AsyncUseCaseBusTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestThrowsException()
        {
            var serviceCollection = new ServiceCollection();
            var busBuilder = new AsyncUseCaseBusBuilder(serviceCollection);
            busBuilder.RegisterUseCase<InputData, ThrowsExceptionInteractor>();
            var bus = busBuilder.Build();
            var request = new InputData();
            try
            {
                bus.Handle(request);
                Assert.Fail();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [TestMethod]
        public void TestNormal()
        {
            var request = new InputData();
            var serviceCollection = new ServiceCollection();
            var busBuilder = new AsyncUseCaseBusBuilder(serviceCollection);
            busBuilder.RegisterUseCase<InputData, NormalInteractor>();
            var bus = busBuilder.Build();
            bus.Handle(request);
        }

        [TestMethod]
        public void TestDefinedInterface()
        {
            var serviceCollection = new ServiceCollection();
            var busBuilder = new AsyncUseCaseBusBuilder(serviceCollection);
            busBuilder.RegisterUseCase<InputData, DefinedInterfaceInteractor>();
            var bus = busBuilder.Build();
            var request = new InputData();
            bus.Handle(request);
        }
    }
}