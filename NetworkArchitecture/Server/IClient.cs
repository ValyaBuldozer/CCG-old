﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkArchitecture.Common;

namespace NetworkArchitecture.Server
{
    public interface IClient
    {
        //INetworkCommunicator Communicator { set; get; }
        string IdentificatorTocken { set; get; }
        bool IsInSystem { set; get; }
    }
}
