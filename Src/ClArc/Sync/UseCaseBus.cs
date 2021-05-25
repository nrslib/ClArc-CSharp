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

        public TResponse Handle<TResponse>(IInputData<TResponse> inputData)
            where TResponse : IOutputData
        {
            var invoker = Invoker(inputData);
            return invoker.Invoke(inputData);
        }

        public async Task<TResponse> HandleAync<TResponse>(IInputData<TResponse> inputData)
            where TResponse : IOutputData
        {
            var invoker = Invoker(inputData);
            var result = await invoker.InvokeAsync(inputData);
            return result;
        }

        internal void Setup(IServiceProvider provider, IUseCaseInvokerFactory invokerFactory)
        {
            this.provider = provider;
            this.invokerFactory = invokerFactory;
        }

        internal void Register<TRequest, TUseCase>()
            where TRequest : IInputData<IOutputData>
            where TUseCase : IInputPort<TRequest, IOutputData>
        {
            handlerTypes.Add(typeof(TRequest), typeof(TUseCase));
        }
        internal void RegisterAsync<TRequest, TUseCase>()
    where TRequest : IInputData<IOutputData>
    where TUseCase : IInputPortAsync<TRequest, IOutputData>
        {
            handlerTypes.Add(typeof(TRequest), typeof(TUseCase));
        }

        private IUseCaseInvoker Invoker<TResponse>(IInputData<TResponse> inputData)
            where TResponse : IOutputData
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
        private IUseCaseInvoker InvokerAsync<TResponse>(IInputData<TResponse> inputData)
    where TResponse : IOutputData
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