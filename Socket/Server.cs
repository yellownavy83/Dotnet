using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket
{
    public class Server
    {
        StreamWriter writer = null;
        StreamReader reader = null;
        internal void Wait(int port)
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
                TcpListener tcpListener = new TcpListener(endPoint);
                tcpListener.Start();
                Console.WriteLine("Server Waiting...");

                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    Console.WriteLine("Connected : " + client.Client.RemoteEndPoint.ToString());

                    NetworkStream networkStream = client.GetStream();
                    writer = new StreamWriter(networkStream, Encoding.UTF8);
                    reader = new StreamReader(networkStream, Encoding.UTF8);

                    Task.Run(() => ReceiveFunc());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }

        private void ReceiveFunc()
        {
            try
            {
                while (true)
                {
                    if(reader != null)
                    {
                        string receivedMessage = reader.ReadLine();
                        Console.WriteLine("Receive : " + receivedMessage);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal void SendFunc(string input)
        {
            if (writer != null)
            {
                writer.WriteLine(input);
                writer.Flush();
            }
        }
    }
}
