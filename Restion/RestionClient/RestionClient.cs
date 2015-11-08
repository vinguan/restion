using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

        private readonly IDictionary<string, string> _defaultHeaders;

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
        /// Gets the <see cref="RestionClientOptions"/> 
        /// </summary>
        public RestionClientOptions RestionClientOptions { get; private set; }

        /// <summary>
        /// Gets the internal <see cref="HttpClient"/>
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        #endregion Public Properties

        #region Protected Properties
        #endregion Protected Properties

        #region Private Properties
        #endregion Private Properties

        #region Public Constructors
        /// <summary>
        /// /
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="deSerialiazer"></param>
        /// <param name="defaultHeaders"></param>
        public RestionClient(ISerialiazer serializer, IDeserialiazer deSerialiazer, IDictionary<string, string> defaultHeaders)
        {
            Serializer = serializer;
            Deserialiazer = deSerialiazer;
            _defaultHeaders = defaultHeaders;

            RestionClientOptions = new RestionClientOptions();
        }

        public RestionClient(ISerialiazer serializer, IDeserialiazer deSerialiazer) : this(serializer,deSerialiazer, new Dictionary<string, string>())
        {

        }

        public RestionClient() : this(new JsonNetSerializer(), new JsonNetDeserializer(), new Dictionary<string, string>())
        {

        }


        #endregion Public Constructors

        #region Private Constructors

        #endregion Private Constructors

        #region Private Methods
        #endregion Private Methods

        #region Protected Methods
        #endregion Protected Methods

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

            HttpClient = new HttpClient() { BaseAddress = new Uri(baseAddress) };

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
        public IRestionClient SetDeSerializer(IDeserialiazer deSerialiazer)
        {
            Deserialiazer = deSerialiazer;

            return this;
        }

        /// <summary>
        /// Adds a default heards that will be sent on every request
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
        /// Execute asynchronously a <see cref="IRestionRequest"/> 
        /// </summary>
        /// <typeparam name="TRestionRequest"><see cref="IRestionRequest"/> to be sent</typeparam>
        /// <typeparam name="TResponseContent">Content of the response</typeparam>
        /// <typeparam name="TRestionResponse"><see cref="IRestionResponse{TResponseContent}"/> of the request</typeparam>
        /// <param name="restionRequest">The RestionRequest to be sent</param>
        /// <returns><see cref="IRestionResponse{TResponseContent}"/> of the request</returns>
        public async Task<TRestionResponse> ExecuteRequestAsync<TRestionRequest, TResponseContent, TRestionResponse>(TRestionRequest restionRequest)
                                                                where TRestionRequest : IRestionRequest
                                                                where TResponseContent : class
                                                                where TRestionResponse : IRestionResponse<TResponseContent>, new()
        {

            if(restionRequest == null)
                throw new ArgumentNullException("restionRequest");

            if(Serializer == null)
                throw new Exception("Serializer is not defined");

            if (Deserialiazer == null)
                throw new Exception("DeSerializer is not defined");

            var restionResponse = new TRestionResponse();

            try
            {
                HttpResponseMessage httpReponseMessage;

                if (!string.IsNullOrWhiteSpace(RestionClientOptions.DateFormat))
                {
                    Serializer.DateFormat = RestionClientOptions.DateFormat;
                    Deserialiazer.DateFormat = RestionClientOptions.DateFormat;
                }

                restionRequest.Serialiazer = Serializer;

                restionRequest.BaseUrl = _baseUrlAddress;

                foreach (var defaultHeader in _defaultHeaders)
                {
                    restionRequest.AddHeader(defaultHeader.Key, defaultHeader.Value);
                }

                if (_cookieContainer != null)
                {
                    HttpClient = new HttpClient(new HttpClientHandler {CookieContainer = _cookieContainer})
                    {
                        BaseAddress = new Uri(_baseUrlAddress)
                    };

                    using (HttpClient)
                    {
                        var httpRequestMessage = await restionRequest.GetHttpRequestMessageAsync();

                        httpReponseMessage = await HttpClient.SendAsync(httpRequestMessage);
                    }
                }
                else
                {
                    using (HttpClient)
                    {
                        var httpRequestMessage = await restionRequest.GetHttpRequestMessageAsync();

                        httpReponseMessage = await HttpClient.SendAsync(httpRequestMessage);

                        restionResponse.HttpResponseMessage = httpReponseMessage;
                    }
                }

                var rawContent = await httpReponseMessage.Content.ReadAsStringAsync();

                restionResponse.Content = await Deserialiazer.DeserializeAsync<TResponseContent>(rawContent);

                //Set restion client options
                if (RestionClientOptions != null)
                {
                    if (RestionClientOptions.AllowRawContent)
                    {
                        restionResponse.RawContent = rawContent;
                    }
                }
            }
            catch (Exception ex)
            {
                restionResponse.Exception = ex;
            }

            return restionResponse;
        }

        #endregion Public Methods

        #region IDisposable
        #endregion IDisposable

        #region Destructor

        #endregion Destructor

    }
}