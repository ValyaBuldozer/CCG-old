using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetworkArchitecture.Server;

namespace TestServer
{
    class Program
    {
        private static TcpListener _tcpListener;
        private static int port;

        static void Main(string[] args)
        {
            port = 8800;

            try
            {
                TcpServer server = new TcpServer(port);

                server.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
