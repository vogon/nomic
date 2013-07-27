using IronPython.Runtime;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Dynamic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    class Program
    {
        static void Main(string[] args)
        {
            IronPythonReplLanguage lang = new IronPythonReplLanguage();
            Repl repl = new Repl(new LocalConsoleReplView(), lang);

            Task t = repl.Main();

            t.Wait();
        }
    }
}
