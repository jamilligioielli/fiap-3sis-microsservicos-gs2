using System.Net;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddScoped<IVersionamentoPromptsService, VersionamentoPromptsService>()
    .AddScoped<IPromptRepository, PromptRepository>()
    .AddScoped<IVersaoRepository, VersaoRepository>()
    .AddDbContext<DatabaseContext>(opt => opt.UseInMemoryDatabase("gs2"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (exception != null)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception.Error, "Erro não tratado capturado pelo middleware global");

            var errorResponse = new
            {
                message = "Erro interno do servidor",
                timestamp = DateTime.UtcNow,
                requestId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
        }
    });
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();