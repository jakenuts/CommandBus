using System.Collections.Generic;
using Autofac;
using Kumiko.CommandBus;

namespace CommandBus.Autofac
{
    /// <summary>
    ///     Defines the StructureMapCommandHandlerCreator type.
    /// </summary>
    public class AutofacCommandHandlerCreator : ICommandHandlerCreator
    {
        private readonly IComponentContext _componentContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutofacCommandHandlerCreator" /> class.
        /// </summary>
        /// <param name="componentContext">
        ///     The container.
        /// </param>
        public AutofacCommandHandlerCreator(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        /// <summary>
        ///     Create the command handlers for a given command type.
        /// </summary>
        /// <typeparam name="TCommand">
        ///     The command type.
        /// </typeparam>
        /// <returns>
        ///     The created command handlers.
        /// </returns>
        public IEnumerable<ICommandHandler> Create<TCommand>()
        {
            return _componentContext.Resolve<IEnumerable<ICommandHandler<TCommand>>>();
        }
    }
}