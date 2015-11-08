using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Restion.Serialization;

namespace Restion
{
    /// <summary>
    /// Represents the contracts to send a rest request that will be executed by <see cref="IRestionClient"/>
    /// </summary>
    public interface IRestionRequest : IDisposable
    {
        #region Properties
        /// <summary>
        /// Gets the HttpMethod for the http request.
        /// </summary>
        HttpMethod Method { get; }

        /// <summary>
        /// Gets or sets the <see cref="ISerialiazer"/> implementation for this request
        /// </summary>
        ISerialiazer Serialiazer { get; set;}

        /// <summary>
        /// Gets or sets the base url for the request
        /// </summary>
        string BaseUrl { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <typeparam name="T">type to be serialized</typeparam>
        /// <param name="content">The content of the request</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionRequest"/></returns>
        IRestionRequest WithContent<T>(T content);

        /// <summary>
        /// Sets the enconding of the request
        /// </summary>
        /// <param name="encoding"><see cref="Encoding"/> of the request</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionRequest"/></returns>
        IRestionRequest WithContentEnconding(Encoding encoding);

        /// <summary>
        /// Sets the media type of the request
        /// </summary>
        /// <param name="mediaType">String with the media type</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionRequest"/></returns>
        IRestionRequest WithContentMediaType(string mediaType);

        /// <summary>
        /// Sets the <see cref="HttpMethod"/> of the request
        /// </summary>
        /// <param name="method">An instance of<see cref="HttpMethod"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionRequest"/></returns>
        IRestionRequest WithHttpMethod(HttpMethod method);

        /// <summary>
        /// Adds a parameter to the url of the request
        /// </summary>
        /// <param name="parameterKey">String with the parameter name</param>
        /// <param name="parameterValue">String with the parameter value</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionRequest"/></returns>
        IRestionRequest AddParameter(string parameterKey, string parameterValue);

        /// <summary>
        /// Adds a header ont the request
        /// </summary>
        /// <param name="headerKey">String with the header name</param>
        /// <param name="headerValue">String with the header value</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionRequest"/></returns>
        IRestionRequest AddHeader(string headerKey, string headerValue);

        /// <summary>
        /// Builds asynchronously a <see cref="HttpRequestMessage"/> based on this <see cref="IRestionRequest"/>
        /// </summary>
        /// <returns>The <see cref="HttpRequestMessage"/> built</returns>
        Task<HttpRequestMessage> GetHttpRequestMessageAsync();
        #endregion Methods
    }
}