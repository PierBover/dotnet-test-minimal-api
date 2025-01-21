using DotnetTest.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => {
	options.UseNpgsql(builder.Configuration.GetConnectionString("AppDatabase"));
});

builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(options => {
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 15;
	options.Password.RequiredUniqueChars = 1;
});

builder.Services.ConfigureApplicationCookie(options => {
	options.Cookie.SameSite = SameSiteMode.Strict;
	options.Cookie.MaxAge = TimeSpan.FromDays(30);
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapOpenApi();
app.MapIdentityApi<IdentityUser>();

app.MapGet("/api/fruits", async (AppDbContext db) =>
	await db.Fruits.ToListAsync()
);

app.MapGet("/api/fruits/{id:long}", async (AppDbContext db, long id) =>
{
	var fruit = await db.Fruits.FindAsync(id);
	if (fruit is not null) return Results.Ok(fruit);
	return Results.NotFound();
})
.RequireAuthorization();

app.Run();
