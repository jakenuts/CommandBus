// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInstantiateCommandHandlers.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the IInstantiateCommandHandlers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IInstantiateCommandHandlers type.
    /// </summary>
    public interface IInstantiateCommandHandlers
    {
        /// <summary>
        /// Instantiate the command handlers for a given command type.
        /// </summary>
        /// <typeparam name="TCommand">
        /// The command type.
        /// </typeparam>
        /// <returns>
        /// The instantiated command handlers.
        /// </returns>
        IEnumerable<ICommandHandler> Instantiate<TCommand>();
    }
}