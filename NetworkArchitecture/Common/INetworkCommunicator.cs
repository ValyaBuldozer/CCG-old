using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArchitecture.Common
{
    public interface INetworkCommunicator
    {
        bool SendMessage(Message message);
        Message ReadMessage();
        bool Connect();
        bool Disconnect();
    }
}
