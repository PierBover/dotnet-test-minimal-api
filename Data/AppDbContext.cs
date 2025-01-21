namespace DotnetTest.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DotnetTest.Models;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

	public DbSet<Fruit> Fruits { get; set; }
}