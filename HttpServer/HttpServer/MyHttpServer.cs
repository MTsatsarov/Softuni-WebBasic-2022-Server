using SUHttpServer.HTTP;
using SUHttpServer.Routing;
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
        private readonly RoutingTable routingTable;
        public MyHttpServer(string _ipAddress, int _port, Action<IRoutingTable> routingTableConfiguration)
        {
            ipAddress = IPAddress.Parse(_ipAddress);
            port = _port;
            routingTableConfiguration(this.routingTable = new RoutingTable());
            serverListner = new TcpListener(ipAddress, port);
        }

        public MyHttpServer(int port, Action<IRoutingTable> routingTable) 
            : this("127.0.0.1", port, routingTable)
        {

        }

        public MyHttpServer(Action<IRoutingTable> routingTable) 
            : this(8080, routingTable)
        {

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
                var strRequest = ReadRequest(networkStream);
                Console.WriteLine(strRequest);
                Request request = Request.Parse(strRequest);

                var response = this.routingTable.MatchRequest(request);


                WriteResponse(networkStream, response);

                connection.Close();

            }
        }

        private static void WriteResponse(NetworkStream networkStream, Response response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());
            networkStream.Write(responseBytes, 0, responseBytes.Length);
        }

        private string ReadRequest(NetworkStream networkStream)
        {
            byte[] buffer = new byte[1024];
            StringBuilder request = new StringBuilder();
            int totalBytes = 0;

            do
            {
                int bytesRead = networkStream.Read(buffer, totalBytes, buffer.Length);

                totalBytes += bytesRead;
                if (totalBytes > 10 * 1024)
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
