// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandBus.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the CommandBus type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Kumiko.CommandBus.Internal
{
    /// <summary>
    ///     Defines the CommandBus type.
    /// </summary>
    public class CommandBus : ICommandBus
    {
        /// <summary>
        ///     The dispatcher.
        /// </summary>
        private readonly IDispatcher _dispatcher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBus" /> class.
        /// </summary>
        /// <param name="dispatcher">
        ///     The dispatcher.
        /// </param>
        public CommandBus(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        ///     Dispatch a command to the command bus.
        /// </summary>
        /// <typeparam name="TCommand">
        ///     The command type.
        /// </typeparam>
        /// <param name="command">
        ///     The command to dispatch.
        /// </param>
        /// <returns>
        ///     The result of the dispatch operation.
        /// </returns>
        public async Task<IDispatchResult> Dispatch<TCommand>(TCommand command)
        {
            try
            {
                var result = await _dispatcher.Dispatch(command);

                return new DispatchResult(result);
            }
            catch (Exception exception)
            {
                return new DispatchResult(exception);
            }
        }
    }
}