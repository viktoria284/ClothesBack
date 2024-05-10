using ClothesBack;
using ClothesBack.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=ClothesDB;Username=postgres;Password=vika1234"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Products.AddRange(
        //new Product(3, "Balance V3 Seamless Crop Top", "Tops", "Re-designed with enhanced softness for a luxurious handfeel and new improved stretch for better comfort, it ensures optimal performance. ", 3000),
        //new Product(4, "Balance V3 Seamless Leggings", "Leggings", "With an increased fabric weight, these leggings ensure a squat-proof finish, empowering you during your workouts. The refined waistband depth provides a true mid-high waist fit, offering essential support.", 4800),
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

app.UseAuthorization();

app.MapControllers();

app.Run();
