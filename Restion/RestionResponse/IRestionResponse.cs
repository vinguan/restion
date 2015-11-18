using System;
using System.Net;
using System.Net.Http;

namespace Restion
{
    /// <summary>
    /// Represents the contracts for a response of a <see cref="IRestionRequest"/>
    /// </summary>
    /// <typeparam name="TContent">Type of the content of the response</typeparam>
    public interface IRestionResponse<TContent>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="TContent"/> instance of the response
        /// </summary>
        TContent Content { get; set; }

        /// <summary>
        /// Get the raw content of the response if allowed through the <see cref="RestionClientOptions"/>
        /// </summary>
        string RawContent { get; set; }

        /// <summary>
        /// Gets the HttpStatusCode of the response
        /// </summary>
        HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Gets or sets the HttpResponseMessage of the response
        /// </summary>
        HttpResponseMessage HttpResponseMessage { get; set; }

        /// <summary>
        /// Gets or sets the exception of the response
        /// </summary>
        Exception Exception { get; set; }

        #endregion Properties
    }
}