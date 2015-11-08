using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Restion.Deserialization;
using Restion.Serialization;

namespace Restion
{
    /// <summary>
    /// Represents the contracts of a client to send <see cref="IRestionRequest"/>
    /// </summary>
    public interface IRestionClient
    {
        #region Properties
        /// <summary>
        /// Gets the <see cref="ISerialiazer"/> implementation for this request
        /// </summary>
        ISerialiazer Serializer { get; }

        /// <summary>
        /// Gets the <see cref="IDeserialiazer"/> implementation for this request
        /// </summary>
        IDeserialiazer Deserialiazer { get;  }

        /// <summary>
        /// Gets the <see cref="RestionClientOptions"/> 
        /// </summary>
        RestionClientOptions RestionClientOptions { get;  }

        /// <summary>
        /// Gets the internal <see cref="HttpClient"/>
        /// </summary>
        HttpClient HttpClient { get; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Sets the <see cref="CookieContainer"/> 
        /// </summary>
        /// <param name="cookieContainer">The instance of CookieContainer</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        IRestionClient SetCookieContainer(CookieContainer cookieContainer);

        /// <summary>
        /// Sets the base address 
        /// </summary>
        /// <param name="baseAddress">String with the base address</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        IRestionClient SetBaseAddress(string baseAddress);

        /// <summary>
        /// Sets the <see cref="ISerialiazer"/>
        /// </summary>
        /// <param name="serialiazer">Implementation of <see cref="ISerialiazer"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        IRestionClient SetSerializer(ISerialiazer serialiazer);

        /// <summary>
        /// Sets the <see cref="IDeserialiazer"/>
        /// </summary>
        /// <param name="deSerialiazer">Implementation of <see cref="IDeserialiazer"/></param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        IRestionClient SetDeSerializer(IDeserialiazer deSerialiazer);

        /// <summary>
        /// Adds a default heards that will be sent on every request
        /// </summary>
        /// <param name="headerKey"><see cref="string"/> with the header key</param>
        /// <param name="headerValue"><see cref="string"/> with the header value</param>
        /// <returns>An instance of a concrete implementation of <see cref="IRestionClient"/></returns>
        IRestionClient AddDefaultHeader(string headerKey, string headerValue);

        /// <summary>
        /// Execute asynchronously a <see cref="IRestionRequest"/> 
        /// </summary>
        /// <typeparam name="TRestionRequest"><see cref="IRestionRequest"/> to be sent</typeparam>
        /// <typeparam name="TResponseContent">Content of the response</typeparam>
        /// <typeparam name="TRestionResponse"><see cref="IRestionResponse{TResponseContent}"/> of the request</typeparam>
        /// <param name="restionRequest">The RestionRequest to be sent</param>
        /// <returns><see cref="IRestionResponse{TResponseContent}"/> of the request</returns>
        Task<TRestionResponse> ExecuteRequestAsync<TRestionRequest, TResponseContent, TRestionResponse>(
            TRestionRequest restionRequest)
            where TRestionRequest : IRestionRequest
            where TResponseContent : class
            where TRestionResponse : IRestionResponse<TResponseContent>, new();

        #endregion Methods
    }
}