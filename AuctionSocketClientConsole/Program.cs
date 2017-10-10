using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8300;
            var host = "10.80.65.44";
            var pre = "10.80.65.44:8300:";
            var ip = IPAddress.Parse(host);
            var ipe = new IPEndPoint(ip, port);

            var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(ipe);

            //receive message
            new Task(()=> 
            {
                while(true)
                {
                    string recStr = "";
                    byte[] recBytes = new byte[4096];
                    int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                    recStr += Encoding.UTF8.GetString(recBytes, 0, bytes);
                    Console.WriteLine(ipe + " " + recStr);
                    Thread.Sleep(100);
                }
            }).Start();

            Console.Read();
            clientSocket.Close();
        }
    }
}
