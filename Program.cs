using System.Dynamic;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHttpsRedirection();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Endpoint
app.MapPost("/api/v1/endpoint", () => {

    dynamic obj = new ExpandoObject();
    dynamic response = new ExpandoObject();

    response.endpoint = "https://iroha.proyectograndorder.es/game";
    response.version = 110;
    response.max_threads = 20;

    obj.status = 200;
    obj.response = response;
    obj.message = "ok";

    return obj;
});

app.MapGet("/api/v1/endpoint", () => {

    dynamic obj = new ExpandoObject();
    dynamic response = new ExpandoObject();

    response.endpoint = "https://iroha.proyectograndorder.es/game";
    response.version = 110;
    response.max_threads = 20;

    obj.status = 200;
    obj.response = response;
    obj.message = "ok";

    return obj;
});


FileServerOptions fileServerOptions = new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "game")),
    RequestPath = "/game",
    EnableDirectoryBrowsing = app.Environment.IsDevelopment()
};

app.UseStaticFiles();
app.UseFileServer(fileServerOptions);
app.UseRouting();
app.UseAuthorization();
app.MapReverseProxy();
app.MapRazorPages();
app.Run();
