using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkArchitecture.Server
{
    class TcpServer : IServer
    {
        private const string LOCALHOST_IP = "127.0.0.1";
        private readonly int PORT;

        private readonly TcpListener _tcpListener;

        public ICollection<IClient> Clients { set; get; }

        public static ManualResetEvent _tcpClientConnected =
            new ManualResetEvent(false);

        public void Start()
        {
            _tcpClientConnected.Reset();
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), _tcpListener);
            _tcpClientConnected.WaitOne();
        }

        public void Stop()
        {
            foreach (var iClient in Clients)
            {
                iClient.Communicator.Disconnect();
            }
            //_tcpListener.EndAcceptSocket()
        }

        public TcpServer(int port)
        {
            PORT = port;
            _tcpListener = new TcpListener(IPAddress.Parse(LOCALHOST_IP),PORT);
            Clients = new List<IClient>();
        }

        public void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            TcpListener listener = (TcpListener)ar.AsyncState;

            TcpClient client = listener.EndAcceptTcpClient(ar);
            _tcpClientConnected.Set();

            Clients.Add(new Client(client));
        }
    }
}
