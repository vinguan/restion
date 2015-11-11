using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Restion.Deserialization
{
    internal class XmlDeserializer : IDeserialiazer
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
        /// Deserializes <see cref="T"/> asynchronously based on a string value
        /// </summary>
        /// <typeparam name="T">Type to be deserialized</typeparam>
        /// <param name="value">String to deserialize</param>
        /// <returns>A instance of <see cref="T"/> deserialized</returns>
        public Task<T> DeserializeAsync<T>(string value) where T : class
        {
            return Task.Factory.StartNew(() =>
            {
                var xmlDeserializer = new XmlSerializer(typeof(T));

                using (var stringReader = new StringReader(value))
                {
                    using (var xmlReader = XmlReader.Create(stringReader))
                    {
                        if (xmlDeserializer.CanDeserialize(xmlReader))
                        {
                            return (T)xmlDeserializer.Deserialize(xmlReader);
                        }

                        return null;
                    }
                }
            });
        }
        #endregion Public Methods
    }
}