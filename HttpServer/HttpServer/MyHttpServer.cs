using SUHttpServer.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class MyHttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListner;

        public MyHttpServer(string _ipAddress, int _port)
        {
            ipAddress = IPAddress.Parse(_ipAddress);
            port = _port;

            serverListner = new TcpListener(ipAddress, port);
        }

        public void Start()
        {
            serverListner.Start();

            Console.WriteLine($"Server is listening on port {port}");
            Console.WriteLine("Listening for requtests");
            while (true)
            {

                var connection = serverListner.AcceptTcpClient();
                var networkStream = connection.GetStream();
              var strRequest =   ReadRequest(networkStream);
                Request request = Request.Parse(strRequest);
                Console.WriteLine(strRequest);

                WriteResponse(networkStream, "Hello world");

                connection.Close();

            }
        }

        private static void WriteResponse(NetworkStream networkStream, string content)
        {
            string response = $@"HTTP/1.1 200 OK
Content-Type: text/plain; charset=UTF-8
Content-Length: {content.Length}

{content}";
            var responseBytes = Encoding.UTF8.GetBytes(response);
            networkStream.Write(responseBytes, 0, responseBytes.Length);
        }

        private string ReadRequest (NetworkStream networkStream)
        {
            byte[] buffer = new byte[1024];
            StringBuilder request = new StringBuilder();
            int totalBytes = 0;

            do
            {
              int  bytesRead = networkStream.Read(buffer, totalBytes, buffer.Length);

                totalBytes += bytesRead;
                if (totalBytes> 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large");
                }
                request.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);


            return request.ToString();
        }
    }
}
