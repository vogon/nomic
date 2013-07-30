using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    public class ServerProxy
    {
        public ServerProxy() { }

        public void Shutdown()
        {
            Program.Shutdown();

            // HACK: doesn't work in non-ipy languages
            throw new IronPython.Runtime.Exceptions.SystemExitException();
        }
    }
}
