using System.Net.Http;

namespace Restion
{
    /// <summary>
    /// Represents a set of custom Http Methods that System.Net.Http.HttpMethod
    /// </summary>
    public static class CustomHttpMethods
    {
        /// <summary>
        /// Gets the PATCH http method
        /// </summary>
        public static readonly HttpMethod Patch = new HttpMethod("PATCH");
    }
}
