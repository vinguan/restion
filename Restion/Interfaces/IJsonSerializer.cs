using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restion.Interfaces
{
    public interface IJsonSerializer<T>
    {
        void Serialize(T obj);
        T Deserialize();
    }
}
