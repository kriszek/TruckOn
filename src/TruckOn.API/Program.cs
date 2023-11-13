using TruckOn.API.Extensions;
using Mapster;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);
{
    IConfiguration conf = builder.Configuration;

    builder.Services.AddControllers()
                    .AddModules(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer()
                    .AddFluentValidationAutoValidation()
                    .AddSwaggerGen()
                    .AddMapster();
}

// Causes FluentValidation's messages to appear only in english
ValidatorOptions.Global.LanguageManager.Enabled = false;

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
