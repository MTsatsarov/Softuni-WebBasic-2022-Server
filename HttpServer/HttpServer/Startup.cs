using HttpServer;
using SUHttpServer.HTTP;
using SUHttpServer.Responses;


public class Startup
{
    private const string HtmlForm = @"<form action '/HTML method = 'POST'>
Name: <input type ='text name = 'name/>
Age: <input type = 'number'name = 'Age'/>
<input type='submit' value = 'Save'/>
</form>";
    public static void Main()
    {
        var server = new MyHttpServer(routes => routes
        .MapGet("/", new TextResponse("Hello from the server!"))
        .MapGet("/HTML", new HtmlResponse(Startup.HtmlForm))
        .MapGet("/Redirect", new RediretResponse("https://softuni.org"))
        .MapPost("/HTML", new TextResponse("",Startup.AddFormDataAction)));

        server.Start();
    }
    private static void AddFormDataAction (Request request, Response response)
    {
        response.Body = "";

        foreach (var kvp in request.Form)
        {
            response.Body += $"{kvp.Key} - {kvp.Value}";
            response.Body += Environment.NewLine;
        }
    }

}


