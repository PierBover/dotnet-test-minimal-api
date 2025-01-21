using DotnetTest.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => {
	options.UseNpgsql(builder.Configuration.GetConnectionString("AppDatabase"));
});

builder.Services.AddOpenApi();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();
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
