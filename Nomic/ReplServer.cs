using System;
using System.Collections.Generic;
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
        }

        public ReplServer(IPEndPoint endpoint)
        {
            _listener = new TcpListener(endpoint);
        }

        private async Task AcceptLoop()
        {
            while (true)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
            }
        }

        private async Task ServiceLoop(Client client)
        {
            
        }

        public void Run()
        {
        }

        private TcpListener _listener;
        private List<Client> _clients;
    }
}
