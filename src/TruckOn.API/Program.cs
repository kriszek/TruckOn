using TruckOn.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers()
                    .AddModules();
    builder.Services.AddEndpointsApiExplorer()
                    .AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
