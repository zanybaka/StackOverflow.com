using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TCPServer server = new TCPServer();
            Console.WriteLine("Starting...");
            server.Start();
            Console.WriteLine("Done.");
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }
    }

    internal class TCPServer
    {
        private readonly TcpListener tcpListener;

        public TCPServer()
        {
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 1234);
        }

        public async Task Start()
        {
            tcpListener.Start();
            try
            {
                while (true)
                {
                    TcpClient     tcpClient     = await tcpListener.AcceptTcpClientAsync();
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[]        messageBuffer = new byte[tcpClient.ReceiveBufferSize];
                    int           bytesRead     = networkStream.Read(messageBuffer, 0, tcpClient.ReceiveBufferSize);
                    string        dataReceived  = Encoding.ASCII.GetString(messageBuffer, 0, bytesRead);
                    Console.WriteLine("Message from tcp client: " + dataReceived);
                }
            }
            finally
            {
                tcpListener.Stop();
            }
        }
    }
}
