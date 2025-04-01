using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RoadAccidentsAPI;
using System.Text.Json.Serialization;
using Testcontainers.PostgreSql;

var postgresSqlContainer = new PostgreSqlBuilder().Build();
await postgresSqlContainer.StartAsync();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "RoadAccidentsAPI.xml"));
});
builder.Services.AddProblemDetails();
builder.Services.AddAutoMapper(typeof(AccidentsMappingProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});
builder.Services.AddDbContext<AppDbContext>(options=>
{
    var conn = postgresSqlContainer.GetConnectionString();
    options.UseNpgsql(conn);
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

//kill container on shutdown
app.Lifetime.ApplicationStopping.Register(() => postgresSqlContainer.DisposeAsync());

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Starting the app.");

app.Run();

public partial class Program {};
