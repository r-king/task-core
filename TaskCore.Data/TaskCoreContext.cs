using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskCore.Domain;

namespace TaskCore.Data
{
	public class TaskCoreContext : DbContext
	{
		public DbSet<Project> Projects { get; set; }
		public DbSet<ProjectTask> Tasks { get; set; }
		public DbSet<SubTask> SubTasks { get; set; }
		public DbSet<Attachment> Attachments { get; set; }
		public DbSet<Link> Links { get; set; }
		public DbSet<Comment> Comments { get; set; }

		public static readonly LoggerFactory MyLoggerFactory
	 = new LoggerFactory(new[] { new DebugLoggerProvider() });

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
				modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
				modelBuilder.Entity(entityType.Name).Ignore("IsDirty");
			}

			modelBuilder.Entity<ProjectTask>().Ignore(p => p.CompletedSubTasks);
			modelBuilder.Entity<Project>().Property(p => p.ProjectStatus).HasDefaultValue("O");
			modelBuilder.Entity<ProjectTask>().Property(p => p.TaskStatus).HasDefaultValue("O");			
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLoggerFactory(MyLoggerFactory);
			optionsBuilder.EnableSensitiveDataLogging();
			optionsBuilder.UseSqlServer(
			  "Server = DESKTOP-0CKIQJV\\SQLEXPRESS; Database = TaskCore2; Trusted_Connection = True;");			
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			foreach (var entry in ChangeTracker.Entries()
			 .Where(e => e.State == EntityState.Added ||
						 e.State == EntityState.Modified))
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property("Created").CurrentValue = DateTime.Now;
				}
				entry.Property("LastModified").CurrentValue = DateTime.Now;
			}
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries()
			 .Where(e => e.State == EntityState.Added ||
						 e.State == EntityState.Modified))
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property("Created").CurrentValue = DateTime.Now;
				}
				entry.Property("LastModified").CurrentValue = DateTime.Now;
			}
			return base.SaveChanges();
		}
	}
}