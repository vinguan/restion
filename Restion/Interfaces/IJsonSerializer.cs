using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restion.Interfaces
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T obj);

        Task<string> SerializeAsync<T>(T obj);

        T Deserialize<T>(string json);

        /// <summary>
        /// Deserializes type async based on the given Json
        /// </summary>
        /// <typeparam name="T">Type to be deserialized</typeparam>
        /// <param name="json">Json string</param>
        /// <returns></returns>
        Task<T> DeserializeAsync<T>(string json);
    }
}
