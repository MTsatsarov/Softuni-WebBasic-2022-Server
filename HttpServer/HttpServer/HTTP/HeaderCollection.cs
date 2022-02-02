using System.Collections;

namespace HttpServer.HTTP
{
    public class HeaderCollection : IEnumerable<Header>
    {
        private readonly Dictionary<string, Header> headers = new Dictionary<string, Header>();

        public int Count => headers.Count;

        public void Add(string name, string value)
        {
            headers.Add(name, new Header(name, value));
        }


        public IEnumerator<Header> GetEnumerator()
        {
            return this.headers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
