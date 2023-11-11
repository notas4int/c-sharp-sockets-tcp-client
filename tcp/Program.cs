using System;
using System.Data.SqlTypes;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tcp {
    internal class Program {
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static string url = "www.google.com";
        private static int port = 80;
        
        public static async Task Main(string[] args) {
            await socket.ConnectAsync(url, port);
            string message = $"GET / HTTP/1.1\r\nHOST: {url}\r\nConnection: close\r\n\r\n";
            var messageBytes = Encoding.UTF8.GetBytes(message);
            socket.Send(messageBytes);

            var responseBytes = new byte[512];
            var bytes = socket.Receive(responseBytes);
            string response = Encoding.UTF8.GetString(responseBytes, 0, bytes);
            Console.WriteLine(response);
        }
    }
}