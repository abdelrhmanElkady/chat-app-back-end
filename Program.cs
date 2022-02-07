using autobus_complete.Hubs;

////for heroku
//using Microsoft.AspNetCore.HttpOverrides;


// for cors
var _loginOrigin = "_localorigin";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//// for heroku
//builder.WebHost.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));
//builder.Services.AddHttpsRedirection(options => { options.HttpsPort = 443; });

// Make sure the CORS middleware is ahead of SignalR.
builder.Services.AddCors(options =>
{
    options.AddPolicy(_loginOrigin, builder =>
    {
        builder.WithOrigins("https://abdelrhmanelkady.github.io/chat-app-front-end-");  
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowCredentials();
      
    });
});

//// for heroku
//builder.Services.Configure<ForwardedHeadersOptions>(options =>
// {
//     options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
//                                ForwardedHeaders.XForwardedProto;
//     options.KnownNetworks.Clear();
//     options.KnownProxies.Clear();
// });

//builder.Host.ConfigureWebHostDefaults(webBuilder => {
//    webBuilder.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));
//});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

////for heroku
//app.UseForwardedHeaders();

//if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DYNO")))
//{
//    Console.WriteLine("Use https redirection");
//    app.UseHttpsRedirection();
//}


app.UseCors(_loginOrigin);
app.UseAuthorization();

app.MapControllers();


app.MapHub<ChatHub>("/chatHub");
app.Run();
