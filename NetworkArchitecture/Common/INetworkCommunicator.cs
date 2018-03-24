using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArchitecture.Common
{
    interface INetworkCommunicator
    {
        bool SendMessage(Message message);
        Message ReadMessage();
    }
}
