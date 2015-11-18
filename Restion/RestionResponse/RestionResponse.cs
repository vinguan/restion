using System;
using System.Net;
using System.Net.Http;

namespace Restion
{
    /// <summary>
    /// Default implementation of <see cref="IRestionResponse{TContent}"/>
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class RestionResponse<TContent> : IRestionResponse<TContent> where TContent : class 
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="TContent"/> instance of the response
        /// </summary>
        public TContent Content { get; set; }

        /// <summary>
        /// Get the raw content of the response if allowed through the <see cref="RestionClientOptions"/>
        /// </summary>
        public string RawContent { get; set; }

        /// <summary>
        /// Gets the HttpStatusCode of the response
        /// </summary>
        public HttpStatusCode HttpStatusCode {
            get
            {
                return HttpResponseMessage != null ? HttpResponseMessage.StatusCode : default(HttpStatusCode);
            }

        }

        /// <summary>
        /// Gets or sets the HttpResponseMessage of the response
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; set; }

        /// <summary>
        /// Gets or sets the exception of the response
        /// </summary>
        public Exception Exception { get; set; }

        #endregion Public Properties
    }
}