using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket
{
    public class Client
    {
        TcpClient client = null;
        StreamWriter writer = null;
        StreamReader reader = null;

        internal void Connect(string ip, int port)
        {
            try
            {
                while (true)
                {
                    IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse(ip), port);
                    client = new TcpClient();
                    client.Connect(remotePoint);

                    if (client.Connected)
                    {
                        NetworkStream networkStream = client.GetStream();
                        writer = new StreamWriter(networkStream, Encoding.UTF8);
                        reader = new StreamReader(networkStream, Encoding.UTF8);
                        Task.Run(() => ReceiveFunc());
                        break;
                    }
                }
                
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendFunc(string message)
        {
            try
            {
                if(client != null)
                {
                    writer.Write(message);
                    writer.Flush();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void ReceiveFunc()
        {
            try
            {
                while (client != null)
                {
                    string receivedMessage = reader.ReadLine();
                    Console.WriteLine(receivedMessage);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
    }
}
