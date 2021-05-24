using ClArc.Dependency;
using ClArc.Sync;
using ClArc.Sync.Core;
using ClArc.Sync.Invoker;

namespace ClArc.Builder
{
    public class SyncUseCaseBusBuilder
    {
        private readonly UseCaseBus bus = new UseCaseBus();
        private readonly IServiceRegistration services;

        public SyncUseCaseBusBuilder(IServiceRegistration services)
        {
            this.services = services;
        }

        /// <summary>
        /// For change invoker
        /// </summary>
        public IUseCaseInvokerFactory UseCaseInvokerFactory { get; set; } = new UseCaseInvokerDefaultFactory();

        public UseCaseBus Build()
        {
            var provider = services.BuildServiceProvider();
            bus.Setup(provider, UseCaseInvokerFactory);
            return bus;
        }

        public void RegisterUseCase<TRequest, TImplement>()
            where TRequest : IInputData<IOutputData>
            where TImplement : class, IInputPort<TRequest, IOutputData>
        {
            services.AddTransient<TImplement>();
            bus.Register<TRequest, TImplement>();
        }
        public void RegisterUseCaseAsync<TRequest, TImplement>()
    where TRequest : IInputData<IOutputData>
    where TImplement : class, IInputPortAsync<TRequest, IOutputData>
        {
            services.AddTransient<TImplement>();
            bus.RegisterAsync<TRequest, TImplement>();
        }
    }
}