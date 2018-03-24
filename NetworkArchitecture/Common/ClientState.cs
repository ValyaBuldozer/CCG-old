using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArchitecture
{
    class ClientState
    {
        // Object to contain client state, including the client socket
        // and the receive buffer

        private const int BUFSIZE = 32; // Size of receive buffer

        public ClientState(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
            RcvBuffer = new byte[BUFSIZE]; // Receive buffer
        }

        public byte[] RcvBuffer { get; }

        public TcpClient TcpClient { get; }
    }
}
