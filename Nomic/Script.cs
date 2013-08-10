using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    internal class Script
    {
        public Script(string code)
        {
            Code = code;
        }

        public string Code { get; private set; }
    }
}
