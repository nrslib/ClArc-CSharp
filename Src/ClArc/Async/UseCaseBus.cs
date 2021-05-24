using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClArc.Async.Core;
using ClArc.Async.Invoker;

namespace ClArc.Async
{
    public class UseCaseBus
    {
        private readonly Dictionary<Type, Type> handlerTypes = new Dictionary<Type, Type>();
        private readonly ConcurrentDictionary<Type, IUseCaseInvokerAsync> invokers = new ConcurrentDictionary<Type, IUseCaseInvokerAsync>();
        private IUseCaseInvokerFactoryAsync invokerFactory;

        private IServiceProvider provider;


        internal UseCaseBus()
        {
        }

        public void Setup(IServiceProvider provider, IUseCaseInvokerFactoryAsync invokerFactory)
        {
            this.provider = provider;
            this.invokerFactory = invokerFactory;
        }

        internal void Register<TRequest, TUseCase>()
            where TRequest : IInputData<IOutputDataAsync>
            where TUseCase : IInputPort<TRequest, IOutputDataAsync>
        {
            handlerTypes.Add(typeof(TRequest), typeof(TUseCase));
        }

        public Task<TResponse> Handle<TResponse>(IInputData<TResponse> inputData) where TResponse : IOutputDataAsync
        {
            var invoker = Invoker(inputData);
            return invoker.Invoke(inputData);
        }

        public async Task<TResponse> HandleAync<TResponse>(IInputData<TResponse> inputData) where TResponse : IOutputDataAsync
        {
            var invoker = Invoker(inputData);
            return await Task.Run(() => invoker.Invoke(inputData));
        }

        private IUseCaseInvokerAsync Invoker<TResponse>(IInputData<TResponse> inputData)
            where TResponse : IOutputDataAsync
        {
            var requestType = inputData.GetType();
            if (invokers.TryGetValue(requestType, out var searchedInvoker)) return searchedInvoker;

            if (!handlerTypes.TryGetValue(requestType, out var handlerType)) throw new Exception($"No registered any usecase for this inputData(RequestType : {inputData.GetType().Name}");

            var invoker = invokers.GetOrAdd(requestType, _ =>
            {
                var handlerInstance = provider.GetService(handlerType);
                return invokerFactory.Generate(handlerType, handlerInstance.GetType(), provider);
            });

            return invoker;
        }
    }
}