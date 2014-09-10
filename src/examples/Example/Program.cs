// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus.Example
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kumiko.CommandBus;

    public class Program
    {
        public static void Main()
        {
            var commandHandlerInstantiater = new BasicCommandHandlerInstantiater();

            commandHandlerInstantiater.AddHandlerFor<Ping>(() => new PingCommandHandler());

            var dispatcher = new Dispatcher(commandHandlerInstantiater);
            var commandBus = new CommandBus(dispatcher);

            commandBus.Dispatch(new Ping())
                .On<Pong>(pong => Console.WriteLine("Pong from {0}!", pong.MachineName))
                .On<Pong>(pong => Console.WriteLine("Omfg I got a pong!"));

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }

    internal class BasicCommandHandlerInstantiater : IInstantiateCommandHandlers
    {
        private readonly Dictionary<Type, List<Func<object>>> _handlersForTypes = new Dictionary<Type, List<Func<object>>>();

        public IEnumerable<ICommandHandler> Instantiate<TCommand>()
        {
            List<Func<object>> handlers;

            if (_handlersForTypes.TryGetValue(typeof(TCommand), out handlers))
            {
                return handlers.Select(x => x.Invoke()).Cast<ICommandHandler<TCommand>>().ToList();
            }

            return null;
        }

        public void AddHandlerFor<TCommand>(Func<object> creator)
        {
            List<Func<object>> handlers;

            if (_handlersForTypes.TryGetValue(typeof(TCommand), out handlers))
            {
                handlers.Add(creator);
            }
            else
            {
                handlers = new List<Func<object>>()
                {
                    creator
                };

                _handlersForTypes[typeof(TCommand)] = handlers;
            }
        }
    }

    internal class PingCommandHandler : ICommandHandler<Ping>
    {
        public object Handle(Ping command, ICommandContext context)
        {
            return new Pong(Environment.MachineName);
        }
    }

    internal class Ping
    {
    }

    internal class Pong
    {
        public Pong(string machineName)
        {
            MachineName = machineName;
        }

        public string MachineName { get; private set; }
    }
}
