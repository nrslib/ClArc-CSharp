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
            busBuilder.RegisterUseCase<InputData, ThrowsExceptionInteractor>();
            var bus = busBuilder.Build();
            var request = new InputData();
            try
            {
                var response = bus.Handle(request);
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
            var serviceCollection = new ServiceCollection();
            var busBuilder = new SyncUseCaseBusBuilder(serviceCollection);
            busBuilder.RegisterUseCase<InputData, NormalInteractor>();
            var bus = busBuilder.Build();
            var request = new InputData();
            var response = bus.Handle(request);
        }

        [TestMethod]
        public void TestDefinedInterface()
        {
            var serviceCollection = new ServiceCollection();
            var busBuilder = new SyncUseCaseBusBuilder(serviceCollection);
            busBuilder.RegisterUseCase<InputData, DefinedInterfaceInteractor>();
            var bus = busBuilder.Build();
            var request = new InputData();
            var response = bus.Handle(request);
        }
    }
}