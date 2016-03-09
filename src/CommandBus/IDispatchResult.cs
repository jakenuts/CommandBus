// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDispatchResult.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the IDispatchResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Kumiko.CommandBus
{
    /// <summary>
    ///     Defines the IDispatchResult type.
    /// </summary>
    public interface IDispatchResult
    {
        /// <summary>
        ///     Get the result data as a given type.
        /// </summary>
        /// <typeparam name="T">
        ///     The data type.
        /// </typeparam>
        /// <returns>
        ///     The strongly typed data.
        /// </returns>
        T DataAs<T>();

        /// <summary>
        ///     Check if the result is of a given type.
        /// </summary>
        /// <typeparam name="T">
        ///     The data type.
        /// </typeparam>
        /// <returns>
        ///     <c>true</c> if data is of given type; otherwise, <c>false</c>.
        /// </returns>
        bool IsData<T>();

        /// <summary>
        ///     Handle the result if it is of given type.
        /// </summary>
        /// <typeparam name="T">
        ///     The result type to handle.
        /// </typeparam>
        /// <param name="handler">
        ///     The handler.
        /// </param>
        /// <returns>
        ///     The dispatch result.
        /// </returns>
        IDispatchResult On<T>(Action<T> handler);

        /// <summary>
        ///     Handle the result if it is of given type and return
        ///     a new dispatch result.
        /// </summary>
        /// <typeparam name="T">
        ///     The result type to handle.
        /// </typeparam>
        /// <param name="handler">
        ///     The handler.
        /// </param>
        /// <returns>
        ///     The dispatch result.
        /// </returns>
        IDispatchResult On<T>(Func<T, IDispatchResult> handler);
    }
}