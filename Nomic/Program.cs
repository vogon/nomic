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
            while (!_shutdown)
            {
                IronPythonReplLanguage lang = new IronPythonReplLanguage();
                Repl repl = new Repl(new ConsoleReplView(), lang);

                Task t = repl.Main();

                t.Wait();

                if (!_shutdown)
                {
                    Console.Out.WriteLine("!!! local interaction detached; restarting (server.Shutdown() to shut down server)...");
                }
            }
        }

        private static async void ServeRemoteInteractions()
        {
            ReplServer sv = new ReplServer(new System.Net.IPEndPoint(0, 3569));

            await sv.Run();
        }

        internal static void Shutdown()
        {
            _shutdown = true;
        }

        private static bool _shutdown = false;

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
