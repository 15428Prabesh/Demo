using Microsoft.EntityFrameworkCore;
using SMS.Core.Entities;

namespace SMS.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<User> user { get; set; }
		public DbSet<UserActivityLog> UserActivityLogs { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id)
					  .ValueGeneratedOnAdd();
				//entity.Property(e => e.UserName).IsRequired();
				entity.Property(e => e.FirstName).IsRequired();
				entity.Property(e => e.LastName).IsRequired();
				entity.Property(e => e.Email).IsRequired();
			});
			modelBuilder.Entity<UserActivityLog>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id)
					  .ValueGeneratedOnAdd();
				entity.Property(e => e.Action).IsRequired();
				entity.Property(e => e.Timestamp).IsRequired();
				entity.Property(e => e.Details).IsRequired();
				entity.Property(e => e.UserId).IsRequired();

				entity.HasOne<User>() 
					 .WithMany() 
					 .HasForeignKey("UserId") 
					 .OnDelete(DeleteBehavior.Cascade); 
			});
		}
	}
}
