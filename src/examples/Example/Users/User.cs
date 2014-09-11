// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the User type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus.Example.Users
{
    using System;

    /// <summary>
    /// Defines the User type.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        public User(string username)
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
            Username = username;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string Username { get; private set; }
    }
}