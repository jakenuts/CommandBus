// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NonUniqueUsernameException.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   The non unique username exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus.Example.Users
{
    using System;

    /// <summary>
    /// The non unique username exception.
    /// </summary>
    public class NonUniqueUsernameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NonUniqueUsernameException"/> class.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        public NonUniqueUsernameException(string username)
            : base(string.Format("'{0}' is not a unique username", username))
        {
        }
    }
}