using ClArc.Async;
using ClArc.Async.Core;
using ClArc.Async.Invoker;
using ClArc.Dependency;
using System.Threading.Tasks;

namespace ClArc.Builder
{
    public class AsyncUseCaseBusBuilder
    {
        private readonly UseCaseBus bus = new UseCaseBus();
        private readonly IServiceRegistration services;

        public AsyncUseCaseBusBuilder(IServiceRegistration services)
        {
            this.services = services;
        }

        /// <summary>
        /// For change invoker
        /// </summary>
        public IUseCaseInvokerFactoryAsync UseCaseInvokerFactory { get; set; } = new UseCaseInvokerDefaultFactoryAsync();

        public UseCaseBus Build()
        {
            var provider = services.BuildServiceProvider();
            bus.Setup(provider, UseCaseInvokerFactory);
            return bus;
        }

        public void RegisterUseCase<TRequest, TImplement, TOutputData>()
            where TOutputData : IOutputDataAsync
            where TRequest : IInputData<TOutputData>
            where TImplement : class, IInputPort<TRequest, TOutputData, Task<TOutputData>>
        {
            services.AddTransient<TImplement>();
            bus.Register<TRequest, TImplement, TOutputData>();
        }
    }
}