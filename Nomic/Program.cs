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
            ScriptEngine e = Python.CreateEngine();

            Repl repl = new Repl(new LocalConsoleReplView(), e);

            Task t = repl.Main();

            t.Wait();
        }
    }
}
