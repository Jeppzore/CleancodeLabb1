using Inlämningsuppgift_1.Repositories;
using Inlämningsuppgift_1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => {  endpoints.MapControllers(); });

app.Run();

