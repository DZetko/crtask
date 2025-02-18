using Alphacloud.MessagePack.AspNetCore.Formatters;
using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Repository;
using Coderama.DocumentManager.Persistence;
using Coderama.DocumentManager.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

builder.Services
    .AddControllers()
    // Add more formatters below to support more response formats
    .AddXmlDataContractSerializerFormatters()
    // Map controllers defined outside of application
    .AddApplicationPart(typeof(Coderama.DocumentManager.Presentation.Controller.DocumentController).Assembly);
builder.Services.AddMessagePack();

// MediatR is used to handle synchronous commands and queries in the system
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Coderama.DocumentManager.Application.Command.UpdateCommand.UpdateDocumentCommand).Assembly));
var connectionString = builder.Configuration.GetConnectionString("DocumentManagerStore");
builder.Services.AddDbContext<DocumentManagerDbContext>(options => options.UseSqlServer(connectionString, dbContextBuilder => dbContextBuilder.MigrationsAssembly("Coderama.DocumentManager.Persistence")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();