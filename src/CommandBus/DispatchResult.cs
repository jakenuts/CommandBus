// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DispatchResult.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the DispatchResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus
{
    using System;

    /// <summary>
    /// Defines the DispatchResult type.
    /// </summary>
    public class DispatchResult : IDispatchResult
    {
        /// <summary>
        /// The result data.
        /// </summary>
        private readonly object _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchResult"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public DispatchResult(object data)
        {
            _data = data;
        }

        /// <summary>
        /// Get the result data as a given type.
        /// </summary>
        /// <typeparam name="T">
        /// The data type.
        /// </typeparam>
        /// <returns>
        /// The strongly typed data.
        /// </returns>
        public T DataAs<T>()
        {
            if (IsData<T>())
            {
                return (T)_data;
            }

            var typeOfData = _data == null ? "(null)" : _data.GetType().ToString();

            throw new InvalidCastException(string.Format("'{0}' can not be casted to '{1}'", typeOfData, typeof(T)));
        }

        /// <summary>
        /// Check if the result is of a given type.
        /// </summary>
        /// <typeparam name="T">
        /// The data type.
        /// </typeparam>
        /// <returns>
        /// <c>true</c> if data is of given type; otherwise, <c>false</c>.
        /// </returns>
        public bool IsData<T>()
        {
            return _data is T;
        }

        /// <summary>
        /// Handle the result if it is of given type.
        /// </summary>
        /// <typeparam name="T">
        /// The result type to handle.
        /// </typeparam>
        /// <param name="handler">
        /// The handler.
        /// </param>
        /// <returns>
        /// The dispatch result.
        /// </returns>
        public IDispatchResult On<T>(Action<T> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            if (IsData<T>())
            {
                handler(DataAs<T>());
            }

            return this;
        }

        /// <summary>
        /// Handle the result if it is of given type and return
        /// a new dispatch result.
        /// </summary>
        /// <typeparam name="T">
        /// The result type to handle.
        /// </typeparam>
        /// <param name="handler">
        /// The handler.
        /// </param>
        /// <returns>
        /// The dispatch result.
        /// </returns>
        public IDispatchResult On<T>(Func<T, IDispatchResult> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            return IsData<T>() ? handler(DataAs<T>()) ?? this : this;
        }
    }
}