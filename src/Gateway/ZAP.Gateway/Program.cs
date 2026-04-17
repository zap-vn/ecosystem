var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", (HttpContext ctx) => 
{
    ctx.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.MapHealthChecks("/health");

app.UseRouting();
app.MapReverseProxy();

app.Run();


