using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebApi.Data;
using Newtonsoft.Json;
using AutoMapper;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.Development.json", optional: true)
    .Build();

// Add services to the container.

builder.Services.AddDbContext<DataContext>(
    context => context.UseSqlite(configuration.GetConnectionString("Default"))
);

builder.Services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IRepository, Repository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddVersionedApiExplorer(options => 
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    })
    .AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1,0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        }
    );

var apiProviderDescription = builder.Services.BuildServiceProvider()
                                             .GetService<IApiVersionDescriptionProvider>();

builder.Services.AddSwaggerGen(options =>{
    
    foreach (var description in apiProviderDescription.ApiVersionDescriptions)
    {
        options.SwaggerDoc(
            description.GroupName,
            new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = "SmartSchool Api",
                Version = description.ApiVersion.ToString(),
                TermsOfService = new Uri("http://seustermosdeuso.com"),
                Description = "A descrição da WebApi do SmartSchool",
                License = new Microsoft.OpenApi.Models.OpenApiLicense{
                    Name = "SmartSchool License",
                    Url = new Uri("http://mit.com")
                },
                Contact = new Microsoft.OpenApi.Models.OpenApiContact{
                    Name = "Rafael Marra",
                    Email = "teste@gmail.com",
                    Url = new Uri("http://teste.com")
                }
            }
        );    
    }
    
    
    var xmlComentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlComentsFullPath = Path.Combine(AppContext.BaseDirectory,xmlComentsFile);

    options.IncludeXmlComments(xmlComentsFullPath);
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI(options => {
        foreach (var description in apiProviderDescription.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",description.GroupName.ToUpperInvariant());
        }
        options.RoutePrefix = "";
    });
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
