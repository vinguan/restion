using System.Threading.Tasks;

namespace Restion.Deserialization
{
    /// <summary>
    /// Represents the contracts for Deserialization
    /// </summary>
    public interface IDeserialiazer
    {
        #region Properties

        /// <summary>
        /// Gets or sets the root element for deserialization
        /// </summary>
        string RootElement { get; set; }

        /// <summary>
        /// Gets or sets the date format for deserialization
        /// </summary>
        string DateFormat { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Deserializes <see cref="T"/> asynchronously based on a string value
        /// </summary>
        /// <typeparam name="T">Type to be deserialized</typeparam>
        /// <param name="value">String to deserialize</param>
        /// <returns>A instance of <see cref="T"/> deserialized</returns>
        Task<T> DeserializeAsync<T>(string value) where T : class;

        #endregion Methods
    }
}