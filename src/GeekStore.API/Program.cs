using GeekStore.API.Extensions;
using GeekStore.Application;
using GeekStore.Infrastructure;
using GeekStore.API.RequestPipeline;
using GeekStore.API.Middlewares;
using GeekStore.Domain.Entities;
using GeekStore.API.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGlobalErrorHandling();
builder.Services.AddCorsWithOptions();
builder.Services.AddControllers();
builder.Services.AddIdentity();
builder.Services.AddAuthentication();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAnyOrigin");
}
else
{
    app.UseCors("AllowSpecificOrigins");
}

app.UseMiddleware<CultureMiddleware>();
app.UseGlobalErrorHandling();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("api/Auth")
    .MapGeekStoreCustomIdentityApi<ApplicationUser>()
    .WithTags("Auth");
app.MapHub<NotificationHub>("/hub/Notifications");

await app.PopulateDatabase();
await app.RunAsync().ConfigureAwait(false);
