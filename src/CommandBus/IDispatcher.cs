// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDispatcher.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the IDispatcher type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Kumiko.CommandBus
{
    /// <summary>
    ///     Defines the IDispatcher type.
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        ///     Dispatch a command against all registered command shandlers.
        /// </summary>
        /// <typeparam name="TCommand">
        ///     The command type.
        /// </typeparam>
        /// <param name="command">
        ///     The command.
        /// </param>
        /// <returns>
        ///     The dispatch result.
        /// </returns>
        Task<object> Dispatch<TCommand>(TCommand command);
    }
}