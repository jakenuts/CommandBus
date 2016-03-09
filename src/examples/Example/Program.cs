// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Kumiko.CommandBus.Example.CommandHandlers;

namespace Kumiko.CommandBus.Example
{
    /// <summary>
    ///     Defines the Program type.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Start the command bus example.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("==== [StructureMap] ====");

            try
            {
                Task.Factory.StartNew(StructureMapExample.Go).Wait();
            }
            catch (Exception ex1)
            {
                Console.WriteLine("Exception - " + ex1.Message);
            }

            RegisterUserCommandHandler.ResetUsers();

            Console.WriteLine("==== [Autofac] ====");

            try
            {
                Task.Factory.StartNew(AutofacExample.Go).Wait();
            }
            catch (Exception ex1)
            {
                Console.WriteLine("Exception - " + ex1.Message);
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}