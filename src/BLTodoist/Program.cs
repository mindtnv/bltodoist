using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables("APP_");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(b =>
{
    b.UsingRabbitMq((ctx, c) =>
    {
        var config = builder.Configuration.GetSection("RabbitMQ");
        c.Host(config["Host"], config["VHost"], x =>
        {
            x.Username(config["User"]);
            x.Password(config["Password"]);
        });

        c.ConfigureEndpoints(ctx);
    });
});
var app = builder.Build();
// app.Use(async (context, next) =>
// {
//     var stream = new MemoryStream();
//     await context.Request.Body.CopyToAsync(stream);
//     stream.Position = 0;
//     var sr = new StreamReader(stream);
//     Console.WriteLine(await sr.ReadToEndAsync());
//     await next();
// });
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();