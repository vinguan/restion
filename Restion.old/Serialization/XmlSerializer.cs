using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Restion.Serialization
{
    /// <summary>
    /// Represents the JsonNET implementation of <see cref="ISerialiazer"/> 
    /// </summary>
    internal class XmlSerializer : ISerialiazer
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
        /// Serializes <see cref="T"/> asynchronously based on a string value
        /// </summary>
        /// <typeparam name="T">The type to be Serialized</typeparam>
        /// <param name="obj">Instance of the <see cref="T"/></param>
        /// <returns>The <see cref="T"/> serialized into a string</returns>
        public Task<string> SerializeAsync<T>(T obj) where T : class
        {
            return Task.Factory.StartNew(() =>
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof (T));

                using (var stringWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(stringWriter))
                {
                    xmlSerializer.Serialize(xmlWriter, obj);

                    return stringWriter.ToString(); 
                }
            });
        }
        #endregion Public Methods
    }
}
