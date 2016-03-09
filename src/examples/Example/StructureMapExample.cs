using System;
using System.Threading.Tasks;
using Kumiko.CommandBus.Example.Commands;
using Kumiko.CommandBus.Example.Users;
using Kumiko.CommandBus.Internal;
using Kumiko.CommandBus.StructureMap;
using StructureMap;

namespace Kumiko.CommandBus.Example
{
    public class StructureMapExample
    {
        public static async Task Go()
        {
            // 1. Setup the StructureMap container.
            var container = new Container(configuration =>
            {
                configuration.For<ICommandHandlerCreator>().Use<StructureMapCommandHandlerCreator>();
                configuration.For<IDispatcher>().Use<Dispatcher>();
                configuration.For<ICommandBus>().Use<Internal.CommandBus>();

                configuration.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<Program>();
                    scanner.ConnectImplementationsToTypesClosing(typeof (ICommandHandler<>));
                });
            });


            if (container.GetInstance<ICommandHandler<RegisterUserCommand>>() == null)
                throw new Exception("Registration error");


            // 2. Create the command bus.
            var commandBus = container.GetInstance<ICommandBus>();

            // 3. Register a new user
            var user = (await commandBus.Dispatch(new RegisterUserCommand("username")))
                .On<User>(
                    createdUser =>
                        Console.WriteLine("[StructureMap] User '{0}' named '{1}' was created", createdUser.Id,
                            createdUser.Username))
                .DataAs<User>();

            // 4. Register an existing user and catching the exception.
            (await commandBus.Dispatch(new RegisterUserCommand(user.Username)))
                .On<NonUniqueUsernameException>(exception => Console.WriteLine(exception.Message));
        }
    }
}