using Inlämningsuppgift_1.Repositories.Carts;
using Inlämningsuppgift_1.Repositories.Products;
using Inlämningsuppgift_1.Repositories.Users;
using Inlämningsuppgift_1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => {  endpoints.MapControllers(); });

app.Run();

