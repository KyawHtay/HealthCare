using HealthCare.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<ABCContext>(option =>
    option.UseSqlServer(sqlConnectionString, sqlOptions =>
    {
        sqlOptions.CommandTimeout(120); // Set the timeout to 120 seconds
    }));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ABCContext>();

    // Apply any pending migrations
    context.Database.Migrate();

    // Seed the data using the separate class
    DataSeeder.Seed(context);
}

app.Run();
