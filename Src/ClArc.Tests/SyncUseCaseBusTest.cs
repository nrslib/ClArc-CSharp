using System;
using System.Threading.Tasks;
using ClArc.Builder;
using ClArc.Tests.Module;
using ClArc.Tests.Sync;
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
            var serviceRegistration = new TestServiceRegistration();
            var busBuilder = new SyncUseCaseBusBuilder(serviceRegistration);
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
        public async Task TestAsync()
        {
            var serviceRegistration = new TestServiceRegistration();
            var busBuilder = new SyncUseCaseBusBuilder(serviceRegistration);
            busBuilder.RegisterUseCaseAsync<InputData, AsyncInteractor, OutputData>();
            var bus = busBuilder.Build();
            var request = new InputData();
            var response = await bus.HandleAync(request);
        }

        [TestMethod]
        public async Task TestAsyncVoid()
        {
            var serviceRegistration = new TestServiceRegistration();
            var busBuilder = new SyncUseCaseBusBuilder(serviceRegistration);
            busBuilder.RegisterUseCaseAsyncVoidOutput<InputDataVoidOutput, AsyncVoidInteractor>();
            var bus = busBuilder.Build();
            var request = new InputDataVoidOutput();
            await bus.HandleAyncVoidOutput(request);
        }
        [TestMethod]
        public void TestNormal()
        {
            var serviceRegistration = new TestServiceRegistration();
            var busBuilder = new SyncUseCaseBusBuilder(serviceRegistration);
            busBuilder.RegisterUseCase<InputData, NormalInteractor>();
            var bus = busBuilder.Build();
            var request = new InputData();
            var response = bus.Handle(request);
        }

        [TestMethod]
        public void TestSyncVoid()
        {
            var serviceRegistration = new TestServiceRegistration();
            var busBuilder = new SyncUseCaseBusBuilder(serviceRegistration);
            busBuilder.RegisterUseCaseVoidOutput<InputDataVoidOutput, VoidInteractor>();
            var bus = busBuilder.Build();
            var request = new InputDataVoidOutput();
            bus.HandleVoidOutput(request);
        }

        [TestMethod]
        public void TestDefinedInterface()
        {
            var serviceRegistration = new TestServiceRegistration();
            var busBuilder = new SyncUseCaseBusBuilder(serviceRegistration);
            busBuilder.RegisterUseCase<InputData, DefinedInterfaceInteractor>();
            var bus = busBuilder.Build();
            var request = new InputData();
            var response = bus.Handle(request);
        }
    }
}