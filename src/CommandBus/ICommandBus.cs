// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandBus.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the ICommandBus type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus
{
    /// <summary>
    /// Defines the ICommandBus type.
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Dispatch a command to the command bus.
        /// </summary>
        /// <typeparam name="TCommand">
        /// The command type.
        /// </typeparam>
        /// <param name="command">
        /// The command to dispatch.
        /// </param>
        /// <returns>
        /// The result of the dispatch operation.
        /// </returns>
        IDispatchResult Dispatch<TCommand>(TCommand command);
    }
}