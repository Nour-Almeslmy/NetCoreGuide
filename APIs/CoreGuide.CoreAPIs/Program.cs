using Serilog;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "Files"
});
#region Configure serilog
/*builder.Host.UseSerilog();

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", false, true)
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true, true)
.Build();

Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(configuration)
.Enrich.FromLogContext()
.CreateLogger();*/
#endregion


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
#region Custom middlewares
/*app.Use(async (ctx, next) =>
{
Console.WriteLine("Use 1 in");
Console.WriteLine(ctx.Request.Method);
await next();
Console.WriteLine("Use 1 out");
Console.WriteLine(ctx.Response.ToString());
});

app.Use(async (ctx, next) =>
{
Console.WriteLine("Use 2 in");
Console.WriteLine(ctx.Request.Method);
await next();
Console.WriteLine("Use 2 out");
Console.WriteLine(ctx.Response.ToString());
});

app.Map("/Return", app => app.Run(async (ctx) =>
{
Console.WriteLine("Run in");
await ctx.Response.WriteAsync("Short circiut");
}));*/ 
#endregion

app.MapControllerRoute("default", "api/{controller=Home}/{action=Index}/{id?}");

app.Use(async (ctx, next) =>
{
    Console.WriteLine("Use 2 in");
    Console.WriteLine(ctx.Request.Method);
    await next();
    Console.WriteLine("Use 2 out");
    Console.WriteLine(ctx.Response.ToString());
});

app.Run();

#region Generic host
/*
 var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => 
    {
        services.AddRazorPages();
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure((ctx, app) => 
        {
            if (ctx.HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", () => "Hello World!");
                endpoints.MapRazorPages();
            });
        });
    }); 

hostBuilder.Build().Run();
 */
#endregion
