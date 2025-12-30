using Scalar.AspNetCore;
using TechnicalAssestmentMSA.Application.Clientes.Commands;
using TechnicalAssestmentMSA.Application.Clientes.Queries;
using TechnicalAssestmentMSA.Infrastructure.Persistence;
using TechnicalAssestmentMSA.API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using TechnicalAssestmentMSA.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteRequestValidator>();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CriaClienteCommand>();
    cfg.RegisterServicesFromAssemblyContaining<ObtemClientePorIdQuery>();
});

// Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    // Habilita os summaries presentes nos controllers, via arquivo XML.
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        o.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Technical Assestment MSA API")
               .WithTheme(ScalarTheme.Moon)
               .WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json")
               .AddDocument("v1");
    });

    app.MapGet("/", context =>
    {
        context.Response.Redirect("/scalar");
        return Task.CompletedTask;
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
