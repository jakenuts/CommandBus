// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapCommandHandlerCreator.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the StructureMapCommandHandlerCreator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus.StructureMap
{
    using System.Collections.Generic;
    using System.Linq;

    using global::StructureMap;

    /// <summary>
    /// Defines the StructureMapCommandHandlerCreator type.
    /// </summary>
    public class StructureMapCommandHandlerCreator : ICommandHandlerCreator
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapCommandHandlerCreator"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public StructureMapCommandHandlerCreator(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Create the command handlers for a given command type.
        /// </summary>
        /// <typeparam name="TCommand">
        /// The command type.
        /// </typeparam>
        /// <returns>
        /// The created command handlers.
        /// </returns>
        public IEnumerable<ICommandHandler> Create<TCommand>()
        {
            var commandHandlers = _container.GetAllInstances<ICommandHandler<TCommand>>()
                .Cast<ICommandHandler>()
                .ToList();

            return commandHandlers;
        }
    }
}