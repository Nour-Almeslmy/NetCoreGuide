var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.MapControllers();

// Filters execution

app.Run();
