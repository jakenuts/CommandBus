// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandHandlerCreator.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the ICommandHandlerCreator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Kumiko.CommandBus
{
    /// <summary>
    ///     Defines the ICommandHandlerCreator type.
    /// </summary>
    public interface ICommandHandlerCreator
    {
        /// <summary>
        ///     Create the command handlers for a given command type.
        /// </summary>
        /// <typeparam name="TCommand">
        ///     The command type.
        /// </typeparam>
        /// <returns>
        ///     The created command handlers.
        /// </returns>
        IEnumerable<ICommandHandler> Create<TCommand>();
    }
}