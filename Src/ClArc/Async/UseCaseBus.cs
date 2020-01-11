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
        private readonly ConcurrentDictionary<Type, IUseCaseInvoker> invokers = new ConcurrentDictionary<Type, IUseCaseInvoker>();
        private IUseCaseInvokerFactory invokerFactory;

        private IServiceProvider provider;


        internal UseCaseBus()
        {
        }

        public void Setup(IServiceProvider provider, IUseCaseInvokerFactory invokerFactory)
        {
            this.provider = provider;
            this.invokerFactory = invokerFactory;
        }

        public void Register<TRequest, TUseCase>()
            where TRequest : IInputData
            where TUseCase : IInputPort<TRequest>
        {
            handlerTypes.Add(typeof(TRequest), typeof(TUseCase));
        }

        public void Handle(IInputData inputData)
        {
            var invoker = Invoker(inputData);
            invoker.Invoke(inputData);
        }

        public async Task HandleAync(IInputData inputData)
        {
            var invoker = Invoker(inputData);
            await Task.Run(() => invoker.Invoke(inputData));
        }

        private IUseCaseInvoker Invoker(IInputData inputData)
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