using ClArc.Sync;
using ClArc.Sync.Core;
using ClArc.Sync.Invoker;
using Microsoft.Extensions.DependencyInjection;

namespace ClArc.Builder
{
    public class SyncUseCaseBusBuilder
    {
        private readonly UseCaseBus bus = new UseCaseBus();
        private readonly IServiceCollection services;

        public SyncUseCaseBusBuilder(IServiceCollection services)
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
    }
}