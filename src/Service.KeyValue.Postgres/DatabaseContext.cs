using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;
using MyJetWallet.Sdk.Service;
using Service.KeyValue.Postgres.Models;

namespace Service.KeyValue.Postgres
{
	public class DatabaseContext : MyDbContext
	{
		public const string Schema = "education";
		private const string KeyValueTableName = "keyvalue";

		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<KeyValueEntity> KeyValues { get; set; }

		public static DatabaseContext Create(DbContextOptionsBuilder<DatabaseContext> options)
		{
			MyTelemetry.StartActivity($"Database context {Schema}")?.AddTag("db-schema", Schema);

			return new DatabaseContext(options.Options);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema);

			SetUserInfoEntityEntry(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}

		private static void SetUserInfoEntityEntry(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<KeyValueEntity>().ToTable(KeyValueTableName);
			modelBuilder.Entity<KeyValueEntity>().HasKey(e => e.Id);
			modelBuilder.Entity<KeyValueEntity>().Property(e => e.Key).IsRequired();
			modelBuilder.Entity<KeyValueEntity>().Property(e => e.UserId).IsRequired();
			modelBuilder.Entity<KeyValueEntity>().Property(e => e.Value).IsRequired();
			modelBuilder.Entity<KeyValueEntity>().HasIndex(e => new {e.UserId, e.Key}).IsUnique();
		}
	}
}