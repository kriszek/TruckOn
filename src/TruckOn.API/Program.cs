using TruckOn.API.Extensions;
using Mapster;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers()
                    .AddModules();
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
