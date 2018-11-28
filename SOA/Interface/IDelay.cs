using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA.Interface
{
    public interface IDelay<T>
    { 
        T Delay(int ms);
    }
}
