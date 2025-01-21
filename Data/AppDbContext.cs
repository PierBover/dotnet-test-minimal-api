namespace DotnetTest.Data;

using Microsoft.EntityFrameworkCore;
using DotnetTest.Models;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Fruit> Fruits { get; set; }
}