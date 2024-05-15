using System.Text.Json;
using manage_columns.src.clients;
using manage_columns.src.dataservice;
using manage_columns.src.repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);
var connectionString = builder.Configuration.GetConnectionString("ProjectBLocalConnection");


// Add services to the container.
builder.Services.AddControllers();;
builder.Services.AddSingleton<IRequestValidator, RequestValidator>();
builder.Services.AddSingleton<IColumnRepository, ColumnRepository>();
builder.Services.AddSingleton<IColumnDataservice, ColumnDataservice>();
builder.Services.AddSingleton<ITasksClient, TasksClient>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "manage-columns", 
            Version = "v1", 
            Description = "An ASP.NET Core Web API for managing Columns",
            Contact = new OpenApiContact
                    {
                        Name = "Tyler Simeone",
                        Url = new Uri("https://github.com/tyler-simeone")
                    },
        });
    });

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "manage-columns v1");
    });
    // app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();