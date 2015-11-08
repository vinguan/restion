using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Restion.Deserialization
{
    /// <summary>
    /// Represents the Json.NET implementation of <see cref="IDeserialiazer"/> 
    /// </summary>
    public class JsonNetDeserializer : IDeserialiazer
    {
        #region Public Properties
        
        /// <summary>
        /// Gets or sets the root element for deserialization
        /// </summary>
        public string RootElement { get; set; }
        
        /// <summary>
        /// Gets or sets the date format for deserialization
        /// </summary>
        public string DateFormat { get; set; }

        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Deserializes <see cref="T"/> asynchronously using Json.NET based on a string value
        /// </summary>
        /// <typeparam name="T">Type to be deserialized</typeparam>
        /// <param name="value">String to deserialize</param>
        /// <returns>A instance of <see cref="T"/> deserialized</returns>
        public Task<T> DeserializeAsync<T>(string value) where T : class
        {
            return Task.Factory.StartNew(() =>
            {
                if (DateFormat != null)
                {
                    var jsonNetSerializerSetting = new JsonSerializerSettings { DateFormatString = DateFormat };

                    return JsonConvert.DeserializeObject<T>(value, jsonNetSerializerSetting);
                }

                return JsonConvert.DeserializeObject<T>(value);
            } );
        }
        #endregion Public Methods
    }
}
