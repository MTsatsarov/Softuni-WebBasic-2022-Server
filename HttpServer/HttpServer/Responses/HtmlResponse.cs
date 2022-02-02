using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUHttpServer.HTTP;

namespace SUHttpServer.Responses
{
    public class HtmlResponse:ContentResponse
    {
        public HtmlResponse(string text) : base(text, ContentType.Html)
        {
        }
    }
}
