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

        public void StartReadWriteMessages()
        {
            try
            {
                while (true)
                {
                    if (Client == null)
                        throw new NullReferenceException();

                    var clientState = new ClientState(Client);

                    var result = Client.GetStream().BeginRead(clientState.RcvBuffer, 0, clientState.RcvBuffer.Length,
                        ReadCallback, clientState);

                    result.AsyncWaitHandle.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Connect()
        {
            throw new NotImplementedException();
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public TcpCommunicator(TcpClient client)
        {
            Client = client;
        }

        public void ReadCallback(IAsyncResult asyncResult)
        {
            var clientState = (ClientState) asyncResult.AsyncState;

            try
            {
                var msgSize = clientState.TcpClient.GetStream().EndRead(asyncResult);

                if (msgSize > 0)
                {
                    Console.WriteLine("Recieved message from cliet" + 
                                      Encoding.UTF8.GetString(clientState.RcvBuffer));

                    clientState.TcpClient.GetStream().BeginWrite(clientState.RcvBuffer,
                        0,clientState.RcvBuffer.Length,
                        WriteCallback,clientState);
                }
                else
                {
                    clientState.TcpClient.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void WriteCallback(IAsyncResult asyncResult)
        {
            var clientState = (ClientState) asyncResult.AsyncState;

            try
            {
                clientState.TcpClient.GetStream().EndWrite(asyncResult);

                Console.WriteLine("Send message to client" + 
                                  Encoding.UTF8.GetString(clientState.RcvBuffer));

                //clientState.TcpClient.GetStream().BeginRead(clientState.RcvBuffer,
                //    clientState.RcvBuffer.Length,
                //    0, ReadCallback, clientState);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
