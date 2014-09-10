// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandBus.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the CommandBus type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus
{
    /// <summary>
    /// Defines the CommandBus type.
    /// </summary>
    public class CommandBus : ICommandBus
    {
        /// <summary>
        /// The dispatcher.
        /// </summary>
        private readonly Dispatcher _dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBus"/> class.
        /// </summary>
        /// <param name="dispatcher">
        /// The dispatcher.
        /// </param>
        public CommandBus(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

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
        public IDispatchResult Dispatch<TCommand>(TCommand command)
        {
            try
            {
                var result = _dispatcher.Dispatch(command);

                return new DispatchResult(result);
            }
            catch (System.Exception exception)
            {
                return new DispatchResult(exception);
            }
        }
    }
}