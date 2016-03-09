using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using CommandBus.Autofac;
using Kumiko.CommandBus.Example.Commands;
using Kumiko.CommandBus.Example.Users;
using Kumiko.CommandBus.Internal;

namespace Kumiko.CommandBus.Example
{
    public class AutofacExample
    {
        public static async Task Go()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AutofacCommandHandlerCreator>().As<ICommandHandlerCreator>();
            builder.RegisterType<Internal.CommandBus>().As<ICommandBus>().SingleInstance();
            builder.RegisterType<Dispatcher>().As<IDispatcher>().SingleInstance();

            var commandHandlerType = typeof (ICommandHandler);

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => commandHandlerType.IsAssignableFrom(t))
                .AsImplementedInterfaces();

            var container = builder.Build();


            if (container.Resolve<ICommandHandler<RegisterUserCommand>>() == null)
                throw new Exception("Registration error");


            // 2. Create the command bus.
            var commandBus = container.Resolve<ICommandBus>();

            // 3. Register a new user
            var user = (await commandBus.Dispatch(new RegisterUserCommand("username")))
                .On<User>(
                    createdUser =>
                        Console.WriteLine("'{0}' named '{1}' was created", createdUser.Id, createdUser.Username))
                .DataAs<User>();

            // 4. Register an existing user and catching the exception.
            (await commandBus.Dispatch(new RegisterUserCommand(user.Username)))
                .On<NonUniqueUsernameException>(exception => Console.WriteLine(exception.Message));
        }
    }
}