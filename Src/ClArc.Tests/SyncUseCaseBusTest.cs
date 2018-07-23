using System;
using ClArc.Builder;
using ClArc.Tests.Sync;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClArc.Tests
{
    [TestClass]
    public class SyncUseCaseBusTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestThrowsException()
        {
            var serviceCollection = new ServiceCollection();
            var busBuilder = new SyncUseCaseBusBuilder(serviceCollection);
            busBuilder.RegisterUseCase<Request, ThrowsExceptionInteractor>();
            var bus = busBuilder.Build();
            var request = new Request();
            try
            {
                var response = bus.Handle(request);
                Assert.Fail();
            } catch (Exception)
            {
                // ignored
            }
        }

        [TestMethod]
        public void TestNormal()
        {
            var serviceCollection = new ServiceCollection();
            var busBuilder = new SyncUseCaseBusBuilder(serviceCollection);
            busBuilder.RegisterUseCase<Request, NormalInteractor>();
            var bus = busBuilder.Build();
            var request = new Request();
            var response = bus.Handle(request);
        }

        [TestMethod]
        public void TestDefinedInterface()
        {
            var serviceCollection = new ServiceCollection();
            var busBuilder = new SyncUseCaseBusBuilder(serviceCollection);
            busBuilder.RegisterUseCase<Request, DefinedInterfaceInteractor>();
            var bus = busBuilder.Build();
            var request = new Request();
            var response = bus.Handle(request);
        }
    }
}
