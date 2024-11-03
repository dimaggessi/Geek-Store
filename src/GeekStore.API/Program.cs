using GeekStore.API.Extensions;
using GeekStore.Application;
using GeekStore.Infrastructure;
using GeekStore.API.RequestPipeline;
using GeekStore.API.Middlewares;
using GeekStore.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGlobalErrorHandling();
builder.Services.AddIdentity();
builder.Services.AddCorsWithOptions();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");
app.UseMiddleware<CultureMiddleware>();
app.UseGlobalErrorHandling();
app.MapControllers();
app.MapGroup("api/Auth")
    .MapGeekStoreCustomIdentityApi<ApplicationUser>()
    .WithTags("Auth");
app.UseHttpsRedirection();
app.UseAuthorization();

await app.RunAsync();




