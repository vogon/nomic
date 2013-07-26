using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    internal interface IReplView
    {
        Task<string> Read();
        Task Print(dynamic result);
    }
}
