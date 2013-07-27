using IronPython.Runtime;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Dynamic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nomic
{
    internal static class Program
    {
        private static void ServeLocalInteraction()
        {
            IronPythonReplLanguage lang = new IronPythonReplLanguage();
            Repl repl = new Repl(new LocalConsoleReplView(), lang);

            Task t = repl.Main();

            t.Wait();
        }

        private static void ServeRemoteInteractions()
        {

        }

        static void Main(string[] args)
        {
            Thread thr = new Thread(ServeLocalInteraction);

            thr.Start();

            thr.Join();
        }
    }
}
