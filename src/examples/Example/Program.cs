// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus.Example
{
    using System;

    using Kumiko.CommandBus;
    using Kumiko.CommandBus.Example.Commands;
    using Kumiko.CommandBus.Example.Users;
    using Kumiko.CommandBus.StructureMap;

    using global::StructureMap;

    /// <summary>
    /// Defines the Program type.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Start the command bus example.
        /// </summary>
        public static void Main()
        {
            // 1. Setup the StructureMap container.
            var container = new Container(configuration =>
            {
                configuration.For<ICommandHandlerCreator>().Use<StructureMapCommandHandlerCreator>();
                configuration.For<IDispatcher>().Use<Dispatcher>();
                configuration.For<ICommandBus>().Use<CommandBus>();

                configuration.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<Program>();
                    scanner.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                });
            });

            // 2. Create the command bus.
            var commandBus = container.GetInstance<ICommandBus>();

            // 3. Register a new user
            var user = commandBus.Dispatch(new RegisterUserCommand("username"))
                .On<User>(createdUser => Console.WriteLine("User '{0}' named '{1}' was created", createdUser.Id, createdUser.Username))
                .DataAs<User>();

            // 4. Register an existing user and catching the exception.
            commandBus.Dispatch(new RegisterUserCommand(user.Username))
                .On<NonUniqueUsernameException>(exception => Console.WriteLine(exception.Message));

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}