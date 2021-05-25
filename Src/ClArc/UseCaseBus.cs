using ClArc.Core;
using ClArc.Invoker;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ClArc
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

        public async Task HandleAyncVoidOutput(IInputDataVoidOutput inputData)
        {
            var invoker = InvokerVoidOutput(inputData);
            await invoker.InvokeAsyncVoidOutput(inputData);
            return;
        }

        public void HandleVoidOutput(IInputDataVoidOutput inputData)
        {
            var invoker = InvokerVoidOutput(inputData);
            invoker.InvokeVoidOutput(inputData);
            return;
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
        internal void RegisterVoidOutput<TRequest, TUseCase>()
    where TRequest : IInputDataVoidOutput
    where TUseCase : IInputPortVoidOutput<TRequest>
        {
            handlerTypes.Add(typeof(TRequest), typeof(TUseCase));
        }
        internal void RegisterAsync<TRequest, TUseCase, TOutputData>()
            where TOutputData : IOutputData
            where TRequest : IInputData<TOutputData>
            where TUseCase : IInputPortAsync<TRequest, TOutputData, Task<TOutputData>>
        {
            handlerTypes.Add(typeof(TRequest), typeof(TUseCase));
        }

        internal void RegisterAsyncVoidOutput<TRequest, TUseCase>()
    where TRequest : IInputDataVoidOutput
    where TUseCase : IInputPortAsyncVoidOutput<TRequest>
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
        private IUseCaseInvoker InvokerVoidOutput(IInputDataVoidOutput inputData)
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
        private IUseCaseInvoker InvokerAsyncVoidOutput(IInputDataVoidOutput inputData)
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