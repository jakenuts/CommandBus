// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterUserCommandHandler.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the RegisterUserCommandHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus.Example.CommandHandlers
{
    using System.Collections.Generic;

    using Kumiko.CommandBus.Example.Commands;
    using Kumiko.CommandBus.Example.Users;

    /// <summary>
    /// Defines the RegisterUserCommandHandler type.
    /// </summary>
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        /// <summary>
        /// The registered users.
        /// </summary>
        private static readonly HashSet<string> RegisteredUsers = new HashSet<string>();

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
        public object Handle(RegisterUserCommand command, ICommandContext context)
        {
            if (RegisteredUsers.Contains(command.Username))
            {
                throw new NonUniqueUsernameException(command.Username);
            }

            var user = new User(command.Username);

            RegisteredUsers.Add(user.Username);

            return user;
        }
    }
}