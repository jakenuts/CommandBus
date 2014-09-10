// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandHandler{T}.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the ICommandHandler{T} type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus
{
    /// <summary>
    /// Defines the ICommandHandler{T} type.
    /// </summary>
    /// <typeparam name="T">
    /// The command type.
    /// </typeparam>
    public interface ICommandHandler<in T> : ICommandHandler
    {
        /// <summary>
        /// Handle a command.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The result.
        /// </returns>
        object Handle(T command, ICommandContext context);
    }
}