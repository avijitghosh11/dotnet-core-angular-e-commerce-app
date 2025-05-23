using Ekart.Api.Middleware;
using Ekart.Api.SignalR;
using Ekart.Core.Entites;
using Ekart.Core.Interfaces;
using Ekart.Infrastructure.Data;
using Ekart.Infrastructure.Repository;
using Ekart.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddCors();
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    string connStr = builder.Configuration.GetConnectionString("Redis");
    if (connStr == null) throw new Exception("Redis connection string not found.");
    var configOptions = ConfigurationOptions.Parse(connStr,true);
    return ConnectionMultiplexer.Connect(configOptions);
});
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<IResponseCacheService, ResponseCacheService>();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StoreContext>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddSignalR();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200", "https://localhost:4200"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSwaggerUI(opt=>opt.SwaggerEndpoint("/openapi/v1.json","Ekart.Api"));
app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();
app.MapHub<NotificationHub>("/hub/notifications");

app.MapFallbackToController("Index","Fallback");

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context, userManager);
}
catch(Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();
