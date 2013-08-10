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
using System.IO;
using System.Diagnostics;

namespace Nomic
{
    internal class Program
    {
        private const string STATIC_PATH = "../../../Nomic.StaticScripts";

        private void ServeLocalInteraction()
        {
            while (!_shutdown)
            {
                IronPythonEvaluationContext lang = new IronPythonEvaluationContext();
                Repl repl = new Repl(new ConsoleReplView(), lang);

                // HACK: load static scripts so I have something to test
                repl.LoadScripts(_staticScripts);

                Task t = repl.Main();

                t.Wait();

                if (!_shutdown)
                {
                    Console.Out.WriteLine("!!! local interaction detached; restarting (server.Shutdown() to shut down server)...");
                }
            }
        }

        private async void ServeRemoteInteractions()
        {
            ReplServer sv = new ReplServer(new System.Net.IPEndPoint(0, 3569));

            await sv.Run();
        }

        internal void Shutdown()
        {
            _shutdown = true;
        }

        private bool _shutdown = false;

        private List<Script> _staticScripts = new List<Script>();

        private void LoadAllStaticScripts()
        {
            IEnumerable<string> scriptPaths = Directory.EnumerateFiles(STATIC_PATH, "*.py", SearchOption.AllDirectories);
            
            foreach (string path in scriptPaths)
            {
                using (FileStream script = File.Open(path, FileMode.Open))
                {
                    StreamReader r = new StreamReader(script);

                    Script s = new Script(r.ReadToEnd());
                    _staticScripts.Add(s);

                    Debug.WriteLine("... loaded script {0}", path);
                }
            }
        }

        private void Run(string[] args)
        {
            LoadAllStaticScripts();

            Thread thr = new Thread(ServeLocalInteraction);
            Thread thr2 = new Thread(ServeRemoteInteractions);

            thr.Start();
            thr2.Start();

            thr.Join();
            thr2.Join();
        }

        public static Program Instance { get; private set; }

        public static void Main(string[] args)
        {
            Instance = new Program();
            Instance.Run(args);
        }
    }
}
