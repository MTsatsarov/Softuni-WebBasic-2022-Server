using SUHttpServer.Common;

namespace HttpServer.HTTP
{
    public class Header
    {
        public Header(string _name, string _value)
        {
            Name = _name;
            Value = _value;
            Guard.AgainstNull(_name, nameof(_name));
            Guard.AgainstNull(_value, nameof(_value));
        }
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
