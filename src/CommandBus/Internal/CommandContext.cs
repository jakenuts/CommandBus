// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandContext.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the CommandContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus.Internal
{
    /// <summary>
    ///     Defines the CommandContext type.
    /// </summary>
    public class CommandContext : ICommandContext
    {
        /// <summary>
        ///     Gets or sets a value indicating whether abort.
        /// </summary>
        public bool Abort { get; set; }
    }
}