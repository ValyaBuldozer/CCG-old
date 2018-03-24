using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkArchitecture.Server
{
    public class TcpServer : IServer
    {
        private const string LOCALHOST_IP = "127.0.0.1";
        private readonly int PORT;

        private readonly TcpListener _tcpListener;

        public ICollection<IClient> Clients { set; get; }

        public ManualResetEvent _tcpClientConnected =
            new ManualResetEvent(false);

        public void Start()
        {
            _tcpClientConnected.Reset();
            _tcpListener.Start();
            while (true)
            {
                var result = 
                    _tcpListener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), _tcpListener);
                result.AsyncWaitHandle.WaitOne();
            }
        }

        public void Stop()
        {

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

            TcpClient tcpClient = listener.EndAcceptTcpClient(ar);

            Client client = new Client(tcpClient);
            Clients.Add(client);
            client.Communicator.StartReadWriteMessages();
            _tcpClientConnected.Set();



        }
    }
}
