namespace Restion
{
    /// <summary>
    /// Represents the contracts for the options for the RestionClient
    /// </summary>
    public interface IRestionClientOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether its going to be allowed raw content on the response
        /// </summary>
        bool AllowRawContent { get; }

        /// <summary>
        /// Gets or sets the date format to be used in serialization and deserialization
        /// </summary>
        string DateFormat { get; }
    }
}