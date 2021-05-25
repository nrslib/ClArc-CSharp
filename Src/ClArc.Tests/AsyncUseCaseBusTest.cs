using System;
using System.Threading.Tasks;
using ClArc.Builder;
using ClArc.Tests.Async;
using ClArc.Tests.Module;
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
        public async Task TestThrowsException()
        {
            var serviceRegistration = new TestServiceRegistration();
            var busBuilder = new AsyncUseCaseBusBuilder(serviceRegistration);
            busBuilder.RegisterUseCase<InputData, ThrowsExceptionInteractor, OutputData>();
            var bus = busBuilder.Build();
            var request = new InputData();
            try
            {
                var result = await bus.Handle(request);
                Assert.Fail();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [TestMethod]
        public async Task TestNormal()
        {
            var request = new InputData();
            var serviceRegistration = new TestServiceRegistration();
            var busBuilder = new AsyncUseCaseBusBuilder(serviceRegistration);
            busBuilder.RegisterUseCase<InputData, NormalInteractor, OutputData>();
            var bus = busBuilder.Build();
            var response = await bus.Handle(request);
        }

        [TestMethod]
        public async Task TestDefinedInterface()
        {
            var serviceCollection = new ServiceCollection();
            var serviceRegistration = new TestServiceRegistration();
            var busBuilder = new AsyncUseCaseBusBuilder(serviceRegistration);
            var bus = busBuilder.Build();
            var request = new InputData();
            var response = await bus.Handle(request);
        }
    }
}