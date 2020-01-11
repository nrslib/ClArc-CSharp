using ClArc.Async;
using ClArc.Async.Core;
using ClArc.Async.Invoker;
using Microsoft.Extensions.DependencyInjection;

namespace ClArc.Builder
{
    public class AsyncUseCaseBusBuilder
    {
        private readonly UseCaseBus bus = new UseCaseBus();
        private readonly IServiceCollection services;

        public AsyncUseCaseBusBuilder(IServiceCollection services)
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
            where TRequest : IInputData
            where TImplement : class, IInputPort<TRequest>
        {
            services.AddTransient<TImplement>();
            bus.Register<TRequest, TImplement>();
        }
    }
}