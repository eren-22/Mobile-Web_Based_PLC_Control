using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PLC_Control.Entitity;

namespace PLC_Control.Context
{
	public class APIDbContext : DbContext
	{
		public APIDbContext(DbContextOptions<APIDbContext> options)
			: base(options)
		{

		}
		public DbSet<Request> Requests { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseSqlServer(
					"db_connection-string",
					options => options.EnableRetryOnFailure());
		}
	}
}
