using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.HTTP;
using SUHttpServer.HTTP;

namespace SUHttpServer.Responses
{
    public class NotAuthorizedResponse: Response
    {
        public NotAuthorizedResponse() : base(StatusCode.Unauthorized)
        {
        }
    }
}
