using HttpServer.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUHttpServer.HTTP
{
    public class Response
    {
        public StatusCode StatusCode{ get; init; }

        public HeaderCollection Headers { get; } = new HeaderCollection();

        public string Body { get; set; }


        public Response (StatusCode statusCode)
        {
            StatusCode = statusCode;
            Headers.Add(Header.Server, "SoftUni Server");
            Headers.Add(Header.Date, $"{DateTime.UtcNow:r}");
        }
    }
}
