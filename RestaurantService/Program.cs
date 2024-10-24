using DataAccess;
using RestaurantService.Interfaces;
using RestaurantService.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure the database connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register the DataAccess service for Dapper usage
builder.Services.AddScoped<IDataAccess, DataAccess.DataAccess>(sp =>
    new DataAccess.DataAccess(connectionString));

// 3. Register the RestaurantService
builder.Services.AddScoped<IRestaurantService, RestaurantService.Services.RestaurantService>();

// 4. Add in-memory cache to support sessions
builder.Services.AddDistributedMemoryCache();  // <-- Added this line

// 5. Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 6. Add CORS policy to allow all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// 7. Add controllers (for API handling)
builder.Services.AddControllers();

// 8. Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 9. Build the app
var app = builder.Build();

// 10. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 11. Apply CORS policy
app.UseCors("AllowAllOrigins");

// 12. Apply session handling
app.UseSession();

// 13. Apply authentication and authorization (if required)
app.UseAuthentication();
app.UseAuthorization();

// 14. Map controller routes for your API
app.MapControllers();

// 15. Run the application
app.Run();
