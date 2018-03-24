using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestClient
{
    internal class ClientState
    {
        // Object to contain client state, including the network stream
        // and the send/recv buffer\

        private readonly StringBuilder echoResponse;

        public ClientState(NetworkStream netStream, byte[] byteBuffer)
        {
            NetStream = netStream;
            ByteBuffer = byteBuffer;
            echoResponse = new StringBuilder();
        }

        public NetworkStream NetStream { get; }

        public byte[] ByteBuffer { set; get; }

        public string EchoResponse => echoResponse.ToString();

        public int TotalBytes { get; private set; }

        public void AppendResponse(string response)
        {
            echoResponse.Append(response);
        }

        public void AddToTotalBytes(int count)
        {
            TotalBytes += count;
        }
    }
    class Program
    {
        public static ManualResetEvent ReadDone = new ManualResetEvent(false);

        private static void Main(string[] args)
        {

            var server = "127.0.0.1"; // Server name or IP address

            // Use port argument if supplied, otherwise default to 7
            var servPort = 8800;

            var client = new TcpClient();

            client.Connect(server, servPort);

            var netStream = client.GetStream();

            while (true)
            {
                Console.WriteLine("Введите что-то");
                string userMsg = Console.ReadLine();

                if(userMsg == "stop") break;

                var clientState = new ClientState(netStream,
                    Encoding.UTF8.GetBytes(userMsg));
                // Send the encoded string to the server
                var result = netStream.BeginWrite(clientState.ByteBuffer, 0,
                    clientState.ByteBuffer.Length,
                    WriteCallback,
                    clientState);

                //doOtherStuff();

                result.AsyncWaitHandle.WaitOne(); // block until EndWrite is called

                string request = "";
                //while (netStream.DataAvailable)
                //{
                    byte[] bytes = new byte[32];
                    var readResult = netStream.Read(bytes,0,bytes.Length);
                    request += Encoding.UTF8.GetString(bytes);
                //}

                Console.WriteLine(request);
                // Receive the same string back from the server
                //result = netStream.BeginRead(clientState.ByteBuffer, clientState.TotalBytes,
                //    clientState.ByteBuffer.Length - clientState.TotalBytes,
                //    ReadCallback, clientState);

                //result.AsyncWaitHandle.WaitOne();
                //ReadDone.WaitOne(); // Block until ReadDone is manually set
            }

            netStream.Close(); // Close the stream
            client.Close(); // Close the socket
        }

        public static void doOtherStuff()
        {
            for (var x = 1; x <= 5; x++)
            {
                Console.WriteLine("Thread {0} ({1}) - doOtherStuff(): {2}...",
                    Thread.CurrentThread.GetHashCode(),
                    Thread.CurrentThread.ThreadState, x);
                Thread.Sleep(1000);
            }
        }

        public static void WriteCallback(IAsyncResult asyncResult)
        {
            var cs = (ClientState)asyncResult.AsyncState;

            cs.NetStream.EndWrite(asyncResult);
        }

        public static void ReadCallback(IAsyncResult asyncResult)
        {
            var cs = (ClientState)asyncResult.AsyncState;

            var bytesRcvd = cs.NetStream.EndRead(asyncResult);

            cs.AddToTotalBytes(bytesRcvd);
            cs.AppendResponse(Encoding.UTF8.GetString(cs.ByteBuffer, 0, bytesRcvd));

            if (cs.TotalBytes < cs.ByteBuffer.Length)
            {
                cs.NetStream.BeginRead(cs.ByteBuffer, cs.TotalBytes,
                    cs.ByteBuffer.Length - cs.TotalBytes,
                    ReadCallback, cs.NetStream);
            }
            else
            {
                Console.WriteLine(Encoding.UTF8.GetString(cs.ByteBuffer));
                ReadDone.Set(); // Signal read complete event
            }
        }
    }
}
