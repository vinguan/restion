using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Restion.Constants;
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
        /// Dictionary of form url encoded
        /// </summary>
        private IDictionary<string, string> _formUrlEncoded;

        /// <summary>
        /// Dictionary of form url encoded
        /// </summary>
        private IList<Tuple<string, object>> _formData;

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
        /// Gets or sets the base url for the request
        /// </summary>
        public string BaseUrl { get; set; }

        #endregion Public Properties

        #region Private Properties
        /// <summary>
        /// Gets the parameters
        /// </summary>
        private IDictionary<string, string> Parameters
        {
            get { return _parameters ?? (_parameters = new Dictionary<string, string>()); }
        }

        /// <summary>
        /// Gets the headers
        /// </summary>
        private IDictionary<string, string> Headers
        {
            get { return _headers ?? (_headers = new Dictionary<string, string>()); }
        }

        /// <summary>
        /// Dictionary of form url encoded parameters
        /// </summary>
        private IDictionary<string, string> FormUrlEncoded
        {
            get { return _formUrlEncoded ?? (_formUrlEncoded = new Dictionary<string, string>()); }
        }

        /// <summary>
        /// Dictionary of form data
        /// </summary>
        private IList<Tuple<string, object>> FormData
        {
            get { return _formData ?? (_formData = new List<Tuple<string, object>>()); }
        }

        #endregion Private Properties

        #region Internal Properties
        /// <summary>
        /// Gets or sets the <see cref="ISerialiazer"/> implementation for this request
        /// </summary>
        internal ISerialiazer Serialiazer { get; set; }
        #endregion 

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="RestionRequest"/> class
        /// </summary>
        public RestionRequest()
        {
            //Sets default httpMethod
            Method = HttpMethod.Get;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RestionRequest"/> class using a base url
        /// </summary>
        /// <param name="url">Base url of the request</param>
        public RestionRequest(string url) : this()
        {
            _url = url;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Sets the content of the request
        /// </summary>
        /// <param name="content">The content of the request</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestionRequest"/></returns>
        public IRestionRequest WithContent(object content)
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

            if (Parameters.ContainsKey(parameterKey))
                throw new ArgumentException("There is already a paramater with this key");

            Parameters.Add(parameterKey, parameterValue);

            return this;
        }

        /// <summary>
        /// Adds a header in the request
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

            if (Headers.ContainsKey(headerKey))
                throw new ArgumentException("There is already a paramater with this key");

            Headers.Add(headerKey, headerValue);

            return this;
        }

        /// <summary>
        /// Adds a form url enconded into the request
        /// </summary>
        /// <param name="formUrlKey">String with the form url key</param>
        /// <param name="formUrlValue">String with the  form url value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestionRequest"/></returns>
        public IRestionRequest AddFormUrl(string formUrlKey, string formUrlValue)
        {
            if (string.IsNullOrWhiteSpace(formUrlKey))
                throw new ArgumentNullException("formUrlKey");

            if (string.IsNullOrWhiteSpace(formUrlValue))
                throw new ArgumentNullException("formUrlValue");

            if (FormUrlEncoded.ContainsKey(formUrlValue))
                throw new ArgumentException("There is already a paramater with this key");

            FormUrlEncoded.Add(formUrlKey, formUrlValue);

            return this;
        }

        /// <summary>
        /// Adds a form data into the request
        /// </summary>
        /// <param name="formDataKey">String with the form data key</param>
        /// <param name="formDataValue">String with the form data value</param>
        /// <returns>An instance of a concrete implmentation of <see cref="IRestionRequest"/></returns>
        public IRestionRequest AddFormData(string formDataKey, object formDataValue)
        {
            if (string.IsNullOrWhiteSpace(formDataKey))
                throw new ArgumentNullException("formDataKey");

            if (formDataValue == null)
                throw new ArgumentNullException("formDataValue");

            if (FormData.Any(predicate => predicate.Item1 == formDataKey))
                throw new ArgumentException("There is already a paramater with this key");

            FormData.Add(new Tuple<string, object>(formDataKey, formDataValue));

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
        public async Task<HttpRequestMessage> BuildHttpRequestMessageAsync()
        {
            var httpRequestMessage = new HttpRequestMessage { Method = Method };

            #region Headers
            foreach (var header in Headers)
            {
                httpRequestMessage.Headers.Add(header.Key, header.Value);
            }
            #endregion Headers

            #region Parameters

            var parametersUrl = Parameters.ToQueryString();

            var uri = new Uri(BaseUrl + _url + parametersUrl);

            httpRequestMessage.RequestUri = uri;

            #endregion Parameters

            #region Content

            //Default Encoding
            if (_encoding == null)
            {
                _encoding = Encoding.UTF8;
            }

            //If the request is get method there is no content
            if (Method == HttpMethod.Get)
                return httpRequestMessage;

            //application/x-www-form-urlencoded
            if (!_formUrlEncoded.IsNullOrEmpty())
            {
                httpRequestMessage.Content = new FormUrlEncodedContent(_formUrlEncoded);

                return httpRequestMessage;
            }

            //String Content
            if (_content != null)
            {
                var contentSerialized = await Serialiazer.SerializeAsync(_content);

                if (Serialiazer is JsonNetSerializer)
                {
                    if (string.IsNullOrWhiteSpace(_mediaType))
                    {
                        _mediaType = MediaTypes.ApplicationJson;
                    }
                }

                httpRequestMessage.Content = new StringContent(contentSerialized, _encoding, _mediaType);

                httpRequestMessage.Content.Headers.ContentLength = contentSerialized.Length;

                return httpRequestMessage;
            }

            //Multipart/form-data
            if (!_formData.IsNullOrEmpty())
            {
                var formDataMultipartFormData = new MultipartFormDataContent();

                foreach (var data in _formData)
                {
                    var bytes = data.Item2 as byte[];
                    if (bytes != null)
                    {
                        formDataMultipartFormData.Add(new ByteArrayContent(bytes), data.Item1);
                    }
                    else if (data.Item2 is Stream)
                    {
                        formDataMultipartFormData.Add(new StreamContent((Stream)data.Item2), data.Item1);
                    }
                    else
                    {
                        formDataMultipartFormData.Add(new StringContent(data.Item2.ToString()), data.Item1);
                    }
                }

                httpRequestMessage.Content = formDataMultipartFormData;

                return httpRequestMessage;
            }

            #endregion Content

            return httpRequestMessage;
        }

        #endregion Public Methods

        #region IDisposable
        /// <summary>
        /// Disposes the <see cref="RestionRequest"/>
        /// </summary>
        public void Dispose()
        {
            _parameters = null;

            _headers = null;

            _url = null;

            _encoding = null;

            _mediaType = null;

            Method = null;

            Serialiazer = null;

            BaseUrl = null;

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose for derived classes
        /// </summary>
        /// <param name="disposing">If it is disposing</param>
        protected virtual void Dispose(bool disposing)
        {

        }

        #endregion IDisposable
    }
}
