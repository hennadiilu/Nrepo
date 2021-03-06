﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Testing.Common.FKAuditableEntities;

namespace Nrepo.DataAccess.EntityFramework.Testing.FKAuditableTesting
{
	public class EFContext : EFDbContext<Manager>
	{
		private static readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DB_Nrepo_UnitTesting;Integrated Security=True;MultipleActiveResultSets=True";

		static EFContext()
		{
			Database.SetInitializer<EFContext>(new DropCreateDatabaseAlways<EFContext>());
		}

		public EFContext()
			: base(connectionString)
		{
			Configuration.LazyLoadingEnabled = false;
			Configuration.ProxyCreationEnabled = false;
			Configuration.ValidateOnSaveEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Admin>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<Admin>().Property(e => e.CreatedOn).HasColumnType("datetime2");

			modelBuilder.Entity<Car>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<Car>().Property(e => e.CreatedOn).HasColumnType("datetime2");

			modelBuilder.Entity<Computer>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<Computer>().Property(e => e.CreatedOn).HasColumnType("datetime2");

			modelBuilder.Entity<Description>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<Description>().Property(e => e.CreatedOn).HasColumnType("datetime2");

			modelBuilder.Entity<Director>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<Director>().Property(e => e.CreatedOn).HasColumnType("datetime2");

			modelBuilder.Entity<DirectorTracker>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<DirectorTracker>().Property(e => e.CreatedOn).HasColumnType("datetime2");

			modelBuilder.Entity<Manager>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<Manager>().Property(e => e.CreatedOn).HasColumnType("datetime2");
			modelBuilder.Entity<Manager>().HasOptional(e => e.Car).WithMany(e => e.Managers).HasForeignKey(e => e.CarId);
			modelBuilder.Entity<Manager>().HasOptional(e => e.Description).WithMany(e => e.Managers).HasForeignKey(e => e.DescriptionId);
			modelBuilder.Entity<Manager>().HasMany(e => e.Things).WithRequired(e => e.Manager).WillCascadeOnDelete(false);

			modelBuilder.Entity<ManagerTracker>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<ManagerTracker>().Property(e => e.CreatedOn).HasColumnType("datetime2");

			modelBuilder.Entity<Office>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<Office>().Property(e => e.CreatedOn).HasColumnType("datetime2");

			modelBuilder.Entity<Project>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<Project>().Property(e => e.CreatedOn).HasColumnType("datetime2");
			modelBuilder.Entity<Project>().HasOptional(e => e.Manager).WithMany(e => e.Projects).HasForeignKey(e => e.ManagerId);

			modelBuilder.Entity<Thing>().Property(e => e.LastUpdateOn).HasColumnType("datetime2");
			modelBuilder.Entity<Thing>().Property(e => e.CreatedOn).HasColumnType("datetime2");
			modelBuilder.Entity<Thing>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
		}

		public static void ClearDatabase()
		{
			if (!Database.Exists(connectionString))
			{
				return;
			}

			var context = new EFContext();

			context.Things.RemoveRange(context.Things.Select(e => e));
			context.Descriptions.RemoveRange(context.Descriptions.Select(e => e));
			context.Computers.RemoveRange(context.Computers.Select(e => e));
			context.ManagerTrackers.RemoveRange(context.ManagerTrackers.Select(e => e));
			context.Managers.RemoveRange(context.Managers.Select(e => e));
			context.Offices.RemoveRange(context.Offices.Select(e => e));
			context.Cars.RemoveRange(context.Cars.Select(e => e));
			context.Projects.RemoveRange(context.Projects.Select(e => e));
			context.DirectorTrackers.RemoveRange(context.DirectorTrackers.Select(e => e));
			context.Directors.RemoveRange(context.Directors.Select(e => e));
			context.ManagerTrackers.RemoveRange(context.ManagerTrackers.Select(e => e));
			context.Admins.RemoveRange(context.Admins.Select(e => e));

			context.SaveChanges();
		}

		public static void DeleteDatabase()
		{
			if (!Database.Exists(connectionString))
			{
				return;
			}

			Database.Delete(connectionString);
		}

		public DbSet<Admin> Admins
		{
			get;
			set;
		}

		public DbSet<Car> Cars
		{
			get;
			set;
		}

		public DbSet<Computer> Computers
		{
			get;
			set;
		}

		public DbSet<Description> Descriptions
		{
			get;
			set;
		}

		public DbSet<Director> Directors
		{
			get;
			set;
		}

		public DbSet<DirectorTracker> DirectorTrackers
		{
			get;
			set;
		}

		public DbSet<Manager> Managers
		{
			get;
			set;
		}

		public DbSet<ManagerTracker> ManagerTrackers
		{
			get;
			set;
		}

		public DbSet<Office> Offices
		{
			get;
			set;
		}

		public DbSet<Project> Projects
		{
			get;
			set;
		}

		public DbSet<Thing> Things
		{
			get;
			set;
		}
	}
}
