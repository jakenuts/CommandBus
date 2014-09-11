// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterUserCommand.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the RegisterUserCommand type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus.Example.Commands
{
    /// <summary>
    /// Defines the RegisterUserCommand type.
    /// </summary>
    public class RegisterUserCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserCommand"/> class.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        public RegisterUserCommand(string username)
        {
            Username = username;
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string Username { get; private set; }
    }
}