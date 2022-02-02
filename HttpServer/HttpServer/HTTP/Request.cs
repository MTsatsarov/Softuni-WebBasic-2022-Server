using HttpServer.HTTP;

namespace SUHttpServer.HTTP
{
    public class Request
    {
        public Method Method { get; set; }

        public string Url { get; set; }

        public HeaderCollection Header { get; private set; }

        public string Body { get; set; }

        public static Request Parse(string request)
        {
            var lines = request.Split("\r\n");

            var firstLine = lines
                .First()
                .Split(" ");

            var url = firstLine[1];
            Method method = ParseMethod(firstLine[0]);

            HeaderCollection headers = ParseHeaders(lines.Skip(1));

            var bodyLines = lines.Skip(headers.Count + 2);

            string body = string.Join("\r\n", bodyLines);

            return new Request()
            {
                Method = method,
                Url = url,
                Body = body,
            };
        }

        private static HeaderCollection ParseHeaders(IEnumerable<string> lines)
        {
            var headers = new HeaderCollection();
            foreach (var line in lines)
            {
                if (line == String.Empty)
                {
                    break;
                }
                var parts = line.Split(":");

                if (parts.Length != 2)
                {
                    throw new InvalidOperationException("Request headers are not valid");
                }
                headers.Add(parts[0], parts[1].Trim());

            }
            return headers;
        }

        private static Method ParseMethod(string method)
        {
            try
            {
                return Enum.Parse<Method>(method);
            }
            catch (Exception)
            {

                throw new InvalidOperationException($"Method {method} is not supported");
            }
        }
    }
}
