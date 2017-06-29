using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Restion.Deserialization;
using Restion.Serialization;

namespace Restion
{
    /// <summary>
    /// Represents the default implementation of <see cref="IRestionRequest"/>
    /// </summary>
    public class RestionClient : IRestionClient
    {
        #region Fields
        private string _baseUrlAddress;

        private CookieContainer _cookieContainer;

        private IDictionary<string, string> _defaultHeaders;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets the <see cref="ISerialiazer"/> implementation for this request
        /// </summary>
        public ISerialiazer Serializer { get; private set; }

        /// <summary>
        /// Gets the <see cref="IDeserialiazer"/> implementation for this request
        /// </summary>
        public IDeserialiazer Deserialiazer { get; private set; }

        /// <summary>
        /// Gets the <see cref="IRestionClientOptions"/> 
        /// </summary>
        public IRestionClientOptions RestionClientOptions { get; private set; }

        /// <summary>
        /// Gets the internal <see cref="HttpClient"/>
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        /// <summary>
        /// Gets the defaults headers that will be sent on every request
        /// </summary>
        public IDictionary<string, string> DefaultHeaders
        {
            get { return _defaultHeaders ?? (_defaultHeaders = new Dictionary<string, string>()); }
        }

        #endregion Public Properties

        #region Public Constructors
        /// <summary>
        /// Default constructor for RestionClient with JsonNet and inicialization of Dictionaries
        /// </summary>
        public RestionClient() : this(new JsonNetSerializer(), new JsonNetDeserializer())
        {

        }

        /// <summary>
        /// Default constructor for RestionClient
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="deSerialiazer"></param>
        public RestionClient(ISerialiazer serializer, IDeserialiazer deSerialiazer)
        {
            Serializer = serializer;
            Deserialiazer = deSerialiazer;
        }

        /// <summary>
        /// Default constructor for RestionClient with JsonNet and inicialization of Dictionaries
        /// </summary>
        public RestionClient(string baseUrl) : this(new JsonNetSerializer(), new JsonNetDeserializer())
        {
            _baseUrlAddress = baseUrl;

            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrlAddress)
            };
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Sets the <see cref="CookieContainer"/> 
        /// </summary>
        /// <param name="cookieContainer">The instance of CookieContainer</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        public IRestionClient SetCookieContainer(CookieContainer cookieContainer)
        {
            if (cookieContainer == null)
                throw new ArgumentNullException("cookieContainer");

            _cookieContainer = cookieContainer;

            HttpClient = new HttpClient(new HttpClientHandler { CookieContainer = _cookieContainer })
            {
                BaseAddress = new Uri(_baseUrlAddress)
            };

            return this;
        }

        /// <summary>
        /// Sets the base address 
        /// </summary>
        /// <param name="baseAddress">String with the base address</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        public IRestionClient SetBaseAddress(string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
                throw new ArgumentNullException("baseAddress");

            _baseUrlAddress = baseAddress;

            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrlAddress)
            };

            return this;
        }

        /// <summary>
        /// Sets the <see cref="ISerialiazer"/>
        /// </summary>
        /// <param name="serialiazer">Implementation of <see cref="ISerialiazer"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        public IRestionClient SetSerializer(ISerialiazer serialiazer)
        {
            Serializer = serialiazer;

            return this;
        }

        /// <summary>
        /// Sets the <see cref="IDeserialiazer"/>
        /// </summary>
        /// <param name="deSerialiazer">Implementation of <see cref="IDeserialiazer"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        public IRestionClient SetDeserializer(IDeserialiazer deSerialiazer)
        {
            Deserialiazer = deSerialiazer;

            return this;
        }

        /// <summary>
        /// Sets the <see cref="IRestionClientOptions"/>
        /// </summary>
        /// <param name="restionClientOptions">The restion client options</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        public IRestionClient SetRestionClientOptions(IRestionClientOptions restionClientOptions)
        {
            RestionClientOptions = restionClientOptions;

            return this;
        }

        /// <summary>
        /// Adds a default header that will be sent on every request
        /// </summary>
        /// <param name="headerKey"><see cref="string"/> with the header key</param>
        /// <param name="headerValue"><see cref="string"/> with the header value</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        public IRestionClient AddDefaultHeader(string headerKey, string headerValue)
        {
            _defaultHeaders.Add(headerKey, headerValue);

            return this;
        }

        /// <summary>
        /// Sets the Authorization header
        /// </summary>
        /// <param name="value">The value of the auth header e.g. a Access Token or a full scheme like : "Bearer :ACCESS_TOKEN" </param>
        /// <param name="type">The type of the auth header e.g. "Bearer" </param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        public IRestionClient SetAuthorizationHeader(string value, string type = null)
        {
           HttpClient.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(type) ? new AuthenticationHeaderValue(value) : new AuthenticationHeaderValue(type, value);

            return this;
        }

        /// <summary>
        /// Execute asynchronously a <see cref="IRestionRequest"/> 
        /// </summary>
        /// <typeparam name="TResponseContent">Content of the response</typeparam>
        /// <param name="restionRequest">The RestionRequest to be sent</param>
        /// <returns><see cref="IRestionResponse{TResponseContent}"/> of the request</returns>
        public async Task<IRestionResponse<TResponseContent>> ExecuteRequestAsync<TResponseContent>(IRestionRequest restionRequest) where TResponseContent : class
        {

            if (restionRequest == null)
                throw new ArgumentNullException("restionRequest");

            if (Serializer == null)
                throw new Exception("Serializer is not defined");

            if (Deserialiazer == null)
                throw new Exception("Deserializer is not defined");

            var restionResponse = new RestionResponse<TResponseContent>();

            try
            {
                if (RestionClientOptions != null && !string.IsNullOrWhiteSpace(RestionClientOptions.DateFormat))
                {
                    Serializer.DateFormat = RestionClientOptions.DateFormat;
                    Deserialiazer.DateFormat = RestionClientOptions.DateFormat;
                }

                //Pass the serializer to the request
                ((RestionRequest)restionRequest).Serialiazer = Serializer;

                restionRequest.BaseUrl = _baseUrlAddress;

                foreach (var defaultHeader in DefaultHeaders)
                {
                    restionRequest.AddHeader(defaultHeader.Key, defaultHeader.Value);
                }

                //Gets the HttpRequestMessage from RestionRequest
                var httpRequestMessage = await restionRequest.BuildHttpRequestMessageAsync();

                if(httpRequestMessage == null)
                    throw new Exception("An error occurred when buiding the HttpRequestMessage, please check if you are putting the content values in the right way");

                //Sends HttpRequestMessage
                restionResponse.HttpResponseMessage = await HttpClient.SendAsync(httpRequestMessage);

                //Mount the restion response
                var rawContent = await restionResponse.HttpResponseMessage.Content.ReadAsStringAsync();

                //Deserializes the content
                restionResponse.Content = await Deserialiazer.DeserializeAsync<TResponseContent>(rawContent);

                //Set restion client options
                if (RestionClientOptions != null && RestionClientOptions.AllowRawContent)
                {
                    restionResponse.RawContent = rawContent;
                }
            }
            catch (Exception ex)
            {
                restionResponse.Exception = ex;
            }
            finally
            {
                restionRequest.Dispose();
            }

            return restionResponse;
        }

        #endregion Public Methods
    }
}