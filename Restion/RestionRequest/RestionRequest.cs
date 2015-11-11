using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Restion.Extensions;
using Restion.Serialization;

namespace Restion
{
    /// <summary>
    /// Represents a default the implementation of <see cref="IRestionClient"/> 
    /// </summary>
    public class RestionRequest : IRestionRequest
    {
        #region Fields
        /// <summary>
        /// Content of the request
        /// </summary>
        private object _content;

        /// <summary>
        /// Dictionary of parameters
        /// </summary>
        private IDictionary<string, string> _parameters;

        /// <summary>
        /// Dictionary of headers
        /// </summary>
        private IDictionary<string, string> _headers;

        /// <summary>
        /// Url of the request
        /// </summary>
        private string _url;

        /// <summary>
        /// Enconding of the request
        /// </summary>
        private Encoding _encoding;

        /// <summary>
        /// Media-Type for the request
        /// </summary>
        private string _mediaType;

        #endregion Fields

        #region Public Properties
        /// <summary>
        /// Gets the HttpMethod for the http request.
        /// </summary>
        public HttpMethod Method { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="ISerialiazer"/> implementation for this request
        /// </summary>
        public ISerialiazer Serialiazer { get; set; }

        /// <summary>
        /// Gets or sets the base url for the request
        /// </summary>
        public string BaseUrl { get; set; }

        #endregion Public Properties

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="RestionRequest"/> class
        /// </summary>
        /// <param name="parameters">A dictionary for the parameters</param>
        /// <param name="headers">A dictionary for the headers</param>
        public RestionRequest(IDictionary<string, string> parameters,
                              IDictionary<string, string> headers)
        {
            _parameters = parameters;
            _headers = headers;

            //Sets default httpMethod
            Method = HttpMethod.Get;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RestionRequest"/> class using a base url
        /// </summary>
        /// <param name="url">Base url of the request</param>
        public RestionRequest(string url) : this(new Dictionary<string, string>(),
                                                 new Dictionary<string, string>())
        {
            _url = url;

            _mediaType = "";
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <typeparam name="T">type to be serialized</typeparam>
        /// <param name="content">The content of the request</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestionRequest"/></returns>
        public IRestionRequest WithContent<T>(T content)
        {
            _content = content;

            return this;
        }

        /// <summary>
        /// Sets the enconding of the request
        /// </summary>
        /// <param name="encoding"><see cref="Encoding"/> of the request</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestionRequest"/></returns>
        public IRestionRequest WithContentEnconding(Encoding encoding)
        {
            _encoding = encoding;

            return this;
        }

        /// <summary>
        /// Sets the media type of the request
        /// </summary>
        /// <param name="mediaType">String with the media type</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestionRequest"/></returns>
        public IRestionRequest WithContentMediaType(string mediaType)
        {
            _mediaType = mediaType;

            return this;
        }

        /// <summary>
        /// Adds a parameter to the url of the request
        /// </summary>
        /// <param name="parameterKey">String with the parameter name</param>
        /// <param name="parameterValue">String with the parameter value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestionRequest"/></returns>
        public IRestionRequest AddParameter(string parameterKey, string parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterKey))
                throw new ArgumentNullException("parameterKey");

            if (string.IsNullOrWhiteSpace(parameterValue))
                throw new ArgumentNullException("parameterValue");

            if (_parameters.ContainsKey(parameterKey))
                throw new ArgumentException("There is already a paramater with this key");

            _parameters.Add(parameterKey, parameterValue);

            return this;
        }

        /// <summary>
        /// Adds a header ont the request
        /// </summary>
        /// <param name="headerKey">String with the header name</param>
        /// <param name="headerValue">String with the header value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestionRequest"/></returns>
        public IRestionRequest AddHeader(string headerKey, string headerValue)
        {
            if (string.IsNullOrWhiteSpace(headerKey))
                throw new ArgumentNullException("headerKey");

            if (string.IsNullOrWhiteSpace(headerValue))
                throw new ArgumentNullException("headerValue");

            if (_parameters.ContainsKey(headerKey))
                throw new ArgumentException("There is already a paramater with this key");

            _headers.Add(headerKey, headerValue);

            return this;
        }

        /// <summary>
        /// Sets the <see cref="HttpMethod"/> of the request
        /// </summary>
        /// <param name="method">An instance of<see cref="HttpMethod"/></param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestionRequest"/></returns>
        public IRestionRequest WithHttpMethod(HttpMethod method)
        {
            Method = method;

            return this;
        }

        /// <summary>
        /// Builds asynchronously a <see cref="HttpRequestMessage"/> based on this <see cref="IRestionRequest"/>
        /// </summary>
        /// <returns>The <see cref="HttpRequestMessage"/> built</returns>
        public async Task<HttpRequestMessage> GetHttpRequestMessageAsync()
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage { Method = Method };

                #region Headers
                foreach (var header in _headers)
                {
                    httpRequestMessage.Headers.Add(header.Key, header.Value);
                }
                #endregion Headers

                #region Parameters

                var parametersUrl = _parameters.ToQueryString();

                var uri = new Uri(BaseUrl + _url + parametersUrl);

                httpRequestMessage.RequestUri = uri;

                #endregion Parameters

                #region Content

                if (Method == HttpMethod.Get || Method == HttpMethod.Delete) 
                    return httpRequestMessage;

                var contentSerialized = await Serialiazer.SerializeAsync(_content);

                httpRequestMessage.Content = new StringContent(contentSerialized, _encoding, _mediaType);

                #endregion Content

                return httpRequestMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        #endregion Public Methods

        #region IDisposable
        /// <summary>
        /// Disposes the <see cref="RestionRequest"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose for derived classes
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _parameters = null;

                _headers = null;

                _url = null;

                _encoding = null;

                _mediaType = null;

                Method = null;

                Serialiazer = null;

                BaseUrl = null;
            }
        }

        #endregion IDisposable
    }
}
