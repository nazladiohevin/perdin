using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<BusinessTripRequest> BusinessTripRequests { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =============================================
            // USERS
            // =============================================
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(30)")
                    .IsRequired();

                entity.HasIndex(e => e.Username).IsUnique();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(30)")
                    .IsRequired();

                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            // =============================================
            // COUNTRIES
            // =============================================
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                entity.Property(e => e.IsForeign)
                    .HasColumnName("is_foreign")
                    .HasColumnType("bit")
                    .IsRequired()
                    .HasDefaultValue(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            // =============================================
            // PROVINCES
            // =============================================
            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("provinces");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(e => e.Country)
                    .WithMany(c => c.Provinces)
                    .HasForeignKey(e => e.CountryId)
                    .HasConstraintName("FK_Provinces_Countries")
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // =============================================
            // CITIES
            // =============================================
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("cities");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProvinceId)
                    .HasColumnName("province_id")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(9,6)")
                    .IsRequired();

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(9,6)")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(e => e.Province)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(e => e.ProvinceId)
                    .HasConstraintName("FK_Cities_Provinces")
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // =============================================
            // BUSINESS TRIP REQUESTS
            // =============================================
            modelBuilder.Entity<BusinessTripRequest>(entity =>
            {
                entity.ToTable("business_trip_requests", t =>
                {
                    t.HasCheckConstraint("CK_BusinessTrip_Status",
                        "status IN ('reviewed','rejected','approved')");
                });

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RequestNumber)
                    .HasColumnName("request_number")
                    .HasColumnType("varchar(30)")
                    .IsRequired();

                entity.HasIndex(e => e.RequestNumber).IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.Property(e => e.DepartureDate)
                    .HasColumnName("departure_date")
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.ReturnDate)
                    .HasColumnName("return_date")
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.OriginCityId)
                    .HasColumnName("origin_city_id")
                    .IsRequired();

                entity.Property(e => e.DestinationCityId)
                    .HasColumnName("destination_city_id");

                entity.Property(e => e.DestinationCountryId)
                    .HasColumnName("destination_country_id")
                    .IsRequired();

                entity.Property(e => e.DurationInDays)
                    .HasColumnName("duration_in_days")
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(20)")
                    .IsRequired()
                    .HasDefaultValue("reviewed");

                entity.Property(e => e.ApproverId)
                    .HasColumnName("approver_id")
                    .IsRequired();

                entity.Property(e => e.Purpose)
                    .HasColumnName("purpose")
                    .HasColumnType("text")
                    .IsRequired();

                entity.Property(e => e.PocketMoney)
                    .HasColumnName("pocket_money")
                    .IsRequired();

                entity.Property(e => e.ApprovedAt)
                    .HasColumnName("approved_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                // Foreign keys
                entity.HasOne(e => e.User)
                    .WithMany(u => u.BusinessTripRequestsAsUser)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_BusinessTrip_User")
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Approver)
                    .WithMany(u => u.BusinessTripRequestsAsApprover)
                    .HasForeignKey(e => e.ApproverId)
                    .HasConstraintName("FK_BusinessTrip_Approver")
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.OriginCity)
                    .WithMany(c => c.BusinessTripRequestsAsOrigin)
                    .HasForeignKey(e => e.OriginCityId)
                    .HasConstraintName("FK_BusinessTrip_OriginCity")
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.DestinationCity)
                    .WithMany(c => c.BusinessTripRequestsAsDestination)
                    .HasForeignKey(e => e.DestinationCityId)
                    .HasConstraintName("FK_BusinessTrip_DestinationCity")
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.DestinationCountry)
                    .WithMany(c => c.BusinessTripRequests)
                    .HasForeignKey(e => e.DestinationCountryId)
                    .HasConstraintName("FK_BusinessTrip_DestinationCountry")
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .IsRequired();
            });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("user_roles"));


            // =============================================
            // SEEDING
            // =============================================
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "ADMIN" },
                new Role { Id = 2, Name = "PEGAWAI" },
                new Role { Id = 3, Name = "SDM" }
            );

            // pw: "Password123!"
            var hashedPassword = "$2a$11$xTpqaQGkrP1q6EwUAVyAceZFAFmfs8Bz4jiDYaFqHG9s1dwvyeZIC";

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Rudi Hartono", Username = "rudi", Email = "rudi@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 2, Name = "Siti Nurhaliza", Username = "siti", Email = "siti@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 3, Name = "Budi Santoso", Username = "budi", Email = "budi@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 4, Name = "Andi Prasetyo", Username = "andi", Email = "andi@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 5, Name = "Dewi Lestari", Username = "dewi", Email = "dewi@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 6, Name = "Eko Wibowo", Username = "eko", Email = "eko@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 7, Name = "Fitri Handayani", Username = "fitri", Email = "fitri@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 8, Name = "Gunawan Putra", Username = "gunawan", Email = "gunawan@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 9, Name = "Hana Permata", Username = "hana", Email = "hana@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 10, Name = "Irfan Maulana", Username = "irfan", Email = "irfan@gmail.com", Password = hashedPassword, CreatedAt = new DateTime(2026, 1, 1) }
            );


            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j =>
                {
                    j.ToTable("user_roles");
                    j.HasData(
                        new { UsersId = 1, RolesId = 1 },
                        new { UsersId = 2, RolesId = 2 },
                        new { UsersId = 3, RolesId = 3 },
                        new { UsersId = 4, RolesId = 2 },
                        new { UsersId = 4, RolesId = 1 },
                        new { UsersId = 5, RolesId = 2 },
                        new { UsersId = 5, RolesId = 3 },
                        new { UsersId = 6, RolesId = 2 },
                        new { UsersId = 7, RolesId = 1 },
                        new { UsersId = 8, RolesId = 2 },
                        new { UsersId = 8, RolesId = 3 },
                        new { UsersId = 9, RolesId = 3 },
                        new { UsersId = 10, RolesId = 2 },
                        new { UsersId = 10, RolesId = 1 }
                    );
                });
        }
    }
}