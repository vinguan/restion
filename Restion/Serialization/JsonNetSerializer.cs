using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Restion.Serialization
{
    /// <summary>
    /// Represents the Json.NET implementation of <see cref="ISerialiazer"/> 
    /// </summary>
    public class JsonNetSerializer : ISerialiazer
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the root element for serialization
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Gets or sets the date format for deserialization
        /// </summary>
        public string DateFormat { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Serializes using Json.NET asynchronously based on a string value
        /// </summary>
        /// <typeparam name="T">The type to be Serialized</typeparam>
        /// <param name="obj">Instance of the object</param>
        /// <returns>The object serialized into a string</returns>
        public Task<string> SerializeAsync<T>(T obj) where T : class
        {
            return Task.Factory.StartNew(() =>
            {
                if (DateFormat != null)
                {
                    var jsonNetSerializerSetting = new JsonSerializerSettings {DateFormatString = DateFormat};

                    return JsonConvert.SerializeObject(obj, jsonNetSerializerSetting);
                }

                return JsonConvert.SerializeObject(obj);
                
            });
        }

        #endregion Public Methods
    }
}
