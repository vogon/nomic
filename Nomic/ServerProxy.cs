using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    public class ServerProxy
    {
        internal ServerProxy(Repl repl)
        {
            this._repl = repl;
        }

        public void Shutdown()
        {
            Program.Shutdown();

            this._repl.RequestExit();
        }

        private Repl _repl;
    }
}
