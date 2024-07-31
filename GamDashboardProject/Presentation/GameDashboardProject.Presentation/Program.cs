using GameDashboardProject.Application;
using GameDashboardProject.Infrastructure;
using GameDashboardProject.Mapper;
using GameDashboardProject.Persistence;
using GameDashboardProject.Infrastructure.MongoServices;
using GameDashboardProject.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using YourNamespace.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddPersistence(configuration);
builder.Services.AddInfrastructureServices(configuration); 
builder.Services.AddApplication();
builder.Services.AddMapperServices();
builder.Services.AddSingleton<IMongoDbService, MongoDbService>();

builder.Services.AddScoped<MongoDbContext>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.MaxDepth = 1;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();

builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("AdminRole", policyBuilder => policyBuilder.RequireRole("Admin"));
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        builder =>
//        {
//            builder.WithOrigins("https://gameadminpanel.azurewebsites.net")
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//        });
//});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameDashboard API", Version = "v1", Description = "GameDashboard API swagger client." });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    c.MapType<ObjectId>(() => new OpenApiSchema { Type = "string", Format = "ObjectId" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var mongoDbContext = services.GetRequiredService<MongoDbContext>();
    await mongoDbContext.AddOrUpdateSeedData();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameDashboard API v1");
});

app.UseHttpsRedirection();
app.UseCors(builder=>builder.WithOrigins("https://gameadminpanel.azurewebsites.net").AllowAnyHeader().AllowAnyMethod());
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
