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
            Repl repl = new Repl(new ConsoleReplView(), lang);

            Task t = repl.Main();

            t.Wait();
        }

        private static async void ServeRemoteInteractions()
        {
            ReplServer sv = new ReplServer(new System.Net.IPEndPoint(0, 3569));

            await sv.Run();
        }

        static void Main(string[] args)
        {
            Thread thr = new Thread(ServeLocalInteraction);
            Thread thr2 = new Thread(ServeRemoteInteractions);

            thr.Start();
            thr2.Start();

            thr.Join();
            thr2.Join();
        }
    }
}
