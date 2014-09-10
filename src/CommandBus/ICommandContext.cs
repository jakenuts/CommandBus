// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandContext.cs" company="Kumiko">
//   Copyright © Kumiko 2014
// </copyright>
// <summary>
//   Defines the ICommandContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kumiko.CommandBus
{
    /// <summary>
    /// Defines the ICommandContext type.
    /// </summary>
    public interface ICommandContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether abort.
        /// </summary>
        bool Abort { get; set; }
    }
}