using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArchitecture.Common
{
    class TcpCommunicator : INetworkCommunicator
    {
        public TcpClient Client { set; get; }

        public bool SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Message ReadMessage()
        {
            throw new NotImplementedException();
        }

        public bool Connect()
        {
            throw new NotImplementedException();
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
