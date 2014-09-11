// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dispatcher.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the Dispatcher type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the Dispatcher type.
    /// </summary>
    public class Dispatcher : IDispatcher
    {
        /// <summary>
        /// The types to dispatch cache.
        /// </summary>
        private readonly IDictionary<Type, Type[]> _typesToDispatchCache = new Dictionary<Type, Type[]>();

        /// <summary>
        /// The instantiators.
        /// </summary>
        private readonly IDictionary<Type, MethodInfo> _instantiators = new Dictionary<Type, MethodInfo>();

        /// <summary>
        /// The dispatcher methods.
        /// </summary>
        private readonly Dictionary<Type, MethodInfo> _dispatcherMethods = new Dictionary<Type, MethodInfo>();

        /// <summary>
        /// The instantiate command handlers.
        /// </summary>
        private readonly ICommandHandlerCreator _commandHandlerCreator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dispatcher"/> class.
        /// </summary>
        /// <param name="commandHandlerCreator">
        /// The instantiate command handlers.
        /// </param>
        public Dispatcher(ICommandHandlerCreator commandHandlerCreator)
        {
            _commandHandlerCreator = commandHandlerCreator;
        }

        /// <summary>
        /// Dispatch a command against all registered command shandlers.
        /// </summary>
        /// <typeparam name="TCommand">
        /// The command type.
        /// </typeparam>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <returns>
        /// The dispatch result.
        /// </returns>
        public object Dispatch<TCommand>(TCommand command)
        {
            var types = GetCommandTypes(typeof(TCommand));

            var handlers = types.SelectMany(CreateHandlers).Distinct();

            object result = null;

            foreach (var handler in handlers)
            {
                var handlerType = handler.GetType();

                foreach (var typeToDispatch in GetTypesToDispatchToThisHandler(types, handlerType))
                {
                    var context = new CommandContext();

                    try
                    {
                        result = GetDispatcherMethod(typeToDispatch).Invoke(this, new object[] { command, context, handler });
                    }
                    catch (TargetInvocationException targetInvocationException)
                    {
                        var exception = targetInvocationException.InnerException;

                        throw exception;
                    }

                    if (context.Abort)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        private static IEnumerable<Type> GetTypesToDispatchToThisHandler(IEnumerable<Type> typesToDispatch, Type handlerType)
        {
            var interfaces = handlerType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                .Select(i => i.GetGenericArguments()[0]);

            return interfaces.Intersect(typesToDispatch).ToArray();
        }

        private static Type[] GetTypes(Type type)
        {
            var types = new HashSet<Type>();

            while (type != null)
            {
                types.Add(type);

                foreach (var interfaceType in type.GetInterfaces())
                {
                    types.Add(interfaceType);
                }

                type = type.BaseType;
            }

            return types.ToArray();
        }

        MethodInfo GetDispatcherMethod(Type typeToDispatch)
        {
            MethodInfo method;

            if (_dispatcherMethods.TryGetValue(typeToDispatch, out method))
            {
                return method;
            }

            method = GetType().GetMethod("DispatchToHandler", BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(typeToDispatch);

            _dispatcherMethods[typeToDispatch] = method;

            return method;
        }

        private object DispatchToHandler<TCommand>(TCommand command, ICommandContext context, ICommandHandler<TCommand> handler)
        {
            return handler.Handle(command, context);
        }

        private IEnumerable<ICommandHandler> CreateHandlers(Type commandType)
        {
            var instantiator = GetInstantiator(commandType);

            var handlers = ((IEnumerable<ICommandHandler>)instantiator.Invoke(_commandHandlerCreator, new object[0])) ?? new ICommandHandler[0];

            return handlers;
        }

        private MethodInfo GetInstantiator(Type type)
        {
            MethodInfo instantiator;

            if (_instantiators.TryGetValue(type, out instantiator))
            {
                return instantiator;
            }

            instantiator = _commandHandlerCreator.GetType()
                .GetMethod("Create")
                .MakeGenericMethod(type);

            _instantiators[type] = instantiator;

            return instantiator;
        }

        private Type[] GetCommandTypes(Type type)
        {
            Type[] typesToDispatch;

            if (_typesToDispatchCache.TryGetValue(type, out typesToDispatch))
            {
                return typesToDispatch;
            }

            typesToDispatch = GetTypes(type);

            _typesToDispatchCache[type] = typesToDispatch;

            return typesToDispatch;
        }
    }
}