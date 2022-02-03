using HttpServer;
using SUHttpServer.Responses;

var server = new MyHttpServer(routes => routes.MapGet("/", new TextResponse("Hello from the server!"))
.MapGet("/HTML", new HtmlResponse("<h1> HTML response <h1>"))
.MapGet("/Redirect", new RediretResponse("https://softuni.org")));
server.Start();

