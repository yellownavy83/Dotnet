using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Socket
{
    class Program
    {
        // Client Main
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Client Socket Start!!!");

        //    string ip = "127.0.0.1";
        //    int port = 9090;
        //    Client client = new Client();
        //    Task.Run(() => client.Connect(ip, port));

        //    while (true) 
        //    {
        //        string input = Console.ReadLine();
        //        Console.WriteLine("User Input : " + input);
        //        client.SendFunc(input);
        //    }
        //}

        // Server Main
        static void Main(string[] args)
        {
            Console.WriteLine("Server Socket Start!!!");

            int port = 9090;
            Server server = new Server();
            Task.Run(() => server.Wait(port));

            while (true)
            {
                string input = Console.ReadLine();
                Console.WriteLine("User Input : " + input);
                server.SendFunc(input);
            }
        }
    }
}
