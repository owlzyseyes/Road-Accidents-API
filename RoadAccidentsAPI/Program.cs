using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RoadAccidentsAPI;
using System.Text.Json.Serialization;

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
    options.UseNpgsql(builder.Configuration.GetConnectionString("TestConnection"));
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddHttpContextAccessor();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    
    // This ensures database is created with latest schema
    await context.Database.EnsureCreatedAsync();
    
    // Seed initial data
    await SeedData.Seed(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Starting the app.");

app.Run();

public partial class Program {};
