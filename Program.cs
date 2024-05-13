using ClothesBack;
using ClothesBack.Interfaces;
using ClothesBack.Models;
using ClothesBack.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=forVebDB;Username=postgres;Password=vika1234"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<AppDbContext>();

// Настройка аутентификации
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Products.AddRange(
        new Product(3, "Balance V3 Seamless Crop Top", "Tops", "Re-designed with enhanced softness for a luxurious handfeel and new improved stretch for better comfort, it ensures optimal performance. ", 3000),
        new Product(4, "Balance V3 Seamless Leggings", "Leggings", "With an increased fabric weight, these leggings ensure a squat-proof finish, empowering you during your workouts. The refined waistband depth provides a true mid-high waist fit, offering essential support.", 4800),
        new Product(5, "Phys Ed Graphic T-Shirt", "T-Shirts", "Premium heavyweight fabric for comfort that hits different. Physical Education graphic to chest", 3000),
        new Product(6, "Phys Ed Hoodie", "Hoodies", "From rest day relaxing to brunch with the girls, elevate your off-duty vibe in the Phys Ed collection.", 8200)
        );
    context.SaveChanges();
}*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();