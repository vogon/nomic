using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    internal class ReplServer
    {
        private class Client
        {
            public TcpClient TcpClient { get; set; }
            public Repl Repl { get; set; }
            public Task ServiceTask { get; set; }
        }

        public ReplServer(IPEndPoint endpoint)
        {
            _listener = new TcpListener(endpoint);
        }

        private async Task AcceptLoop()
        {
            _listener.Start();

            while (true)
            {
                TcpClient tcpClient = await _listener.AcceptTcpClientAsync();

                // connection established; build a client object
                Client client = new Client { TcpClient = tcpClient };

                // set up a repl
                NetworkStream stream = tcpClient.GetStream();
                TextReader r = new StreamReader(stream);
                TextWriter w = new StreamWriter(stream);

                ConsoleReplView view = new ConsoleReplView(r, w);
                Repl repl = new Repl(view, new IronPythonReplLanguage());

                client.Repl = repl;

                // service the repl
                client.ServiceTask = Task.Run(() => ServiceLoop(client));
            }
        }

        private async Task ServiceLoop(Client client)
        {
            await client.Repl.Main();
        }

        public async Task Run()
        {
            await AcceptLoop();
        }

        private TcpListener _listener;
        private List<Client> _clients;
        private List<TcpClient> _newClients;
    }
}
