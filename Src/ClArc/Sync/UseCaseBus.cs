using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClArc.Sync.Core;
using ClArc.Sync.Invoker;

namespace ClArc.Sync
{
    public class UseCaseBus
    {
        private readonly Dictionary<Type, Type> handlerTypes = new Dictionary<Type, Type>();
        private readonly ConcurrentDictionary<Type, IUseCaseInvoker> invokers = new ConcurrentDictionary<Type, IUseCaseInvoker>();
        private IUseCaseInvokerFactory invokerFactory;

        private IServiceProvider provider;

        internal UseCaseBus()
        {
        }

        public TResponse Handle<TResponse>(IRequest<TResponse> request)
            where TResponse : IResponse
        {
            var invoker = Invoker(request);
            return invoker.Invoke(request);
        }

        public async Task<TResponse> HandleAync<TResponse>(IRequest<TResponse> request)
            where TResponse : IResponse
        {
            var invoker = Invoker(request);
            var result = await Task.Run(() => invoker.Invoke(request));
            return result;
        }

        internal void Setup(IServiceProvider provider, IUseCaseInvokerFactory invokerFactory)
        {
            this.provider = provider;
            this.invokerFactory = invokerFactory;
        }

        internal void Register<TRequest, TUseCase>()
            where TRequest : IRequest<IResponse>
            where TUseCase : IUseCase<TRequest, IResponse>
        {
            handlerTypes.Add(typeof(TRequest), typeof(TUseCase));
        }

        private IUseCaseInvoker Invoker<TResponse>(IRequest<TResponse> request)
            where TResponse : IResponse
        {
            var requestType = request.GetType();
            if (invokers.TryGetValue(requestType, out var searchedInvoker)) return searchedInvoker;

            if (!handlerTypes.TryGetValue(requestType, out var handlerType)) throw new Exception($"No registered any usecase for this request(RequestType : {request.GetType().Name}");

            var invoker = invokers.GetOrAdd(requestType, _ =>
            {
                var handlerInstance = provider.GetService(handlerType);
                return invokerFactory.Generate(handlerType, handlerInstance.GetType(), provider);
            });

            return invoker;
        }
    }
}