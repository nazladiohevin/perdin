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

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.HasQueryFilter(e => e.DeletedAt == null);
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

            var now = new DateTime(2026, 1, 1);

            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Indonesia", IsForeign = false, CreatedAt = now },
                new Country { Id = 2, Name = "Malaysia", IsForeign = true, CreatedAt = now },
                new Country { Id = 3, Name = "Singapura", IsForeign = true, CreatedAt = now },
                new Country { Id = 4, Name = "Thailand", IsForeign = true, CreatedAt = now },
                new Country { Id = 5, Name = "Vietnam", IsForeign = true, CreatedAt = now },
                new Country { Id = 6, Name = "Filipina", IsForeign = true, CreatedAt = now },
                new Country { Id = 7, Name = "Brunei", IsForeign = true, CreatedAt = now },
                new Country { Id = 8, Name = "Jepang", IsForeign = true, CreatedAt = now },
                new Country { Id = 9, Name = "Korea Selatan", IsForeign = true, CreatedAt = now },
                new Country { Id = 10, Name = "China", IsForeign = true, CreatedAt = now },
                new Country { Id = 11, Name = "Amerika Serikat", IsForeign = true, CreatedAt = now },
                new Country { Id = 12, Name = "Inggris", IsForeign = true, CreatedAt = now },
                new Country { Id = 13, Name = "Jerman", IsForeign = true, CreatedAt = now },
                new Country { Id = 14, Name = "Perancis", IsForeign = true, CreatedAt = now },
                new Country { Id = 15, Name = "Belanda", IsForeign = true, CreatedAt = now },
                new Country { Id = 16, Name = "Australia", IsForeign = true, CreatedAt = now },
                new Country { Id = 17, Name = "Selandia Baru", IsForeign = true, CreatedAt = now },
                new Country { Id = 18, Name = "Arab Saudi", IsForeign = true, CreatedAt = now },
                new Country { Id = 19, Name = "Uni Emirat Arab", IsForeign = true, CreatedAt = now },
                new Country { Id = 20, Name = "India", IsForeign = true, CreatedAt = now }
            );

            modelBuilder.Entity<Province>().HasData(
                new Province { Id = 1, CountryId = 1, Name = "DKI Jakarta", CreatedAt = now },
                new Province { Id = 2, CountryId = 1, Name = "Jawa Barat", CreatedAt = now },
                new Province { Id = 3, CountryId = 1, Name = "Jawa Tengah", CreatedAt = now },
                new Province { Id = 4, CountryId = 1, Name = "DI Yogyakarta", CreatedAt = now },
                new Province { Id = 5, CountryId = 1, Name = "Jawa Timur", CreatedAt = now },
                new Province { Id = 6, CountryId = 1, Name = "Banten", CreatedAt = now },
                new Province { Id = 7, CountryId = 1, Name = "Bali", CreatedAt = now },
                new Province { Id = 8, CountryId = 1, Name = "Sumatera Utara", CreatedAt = now },
                new Province { Id = 9, CountryId = 2, Name = "Kuala Lumpur", CreatedAt = now },
                new Province { Id = 10, CountryId = 2, Name = "Selangor", CreatedAt = now },
                new Province { Id = 11, CountryId = 3, Name = "Central Singapore", CreatedAt = now },
                new Province { Id = 12, CountryId = 4, Name = "Bangkok", CreatedAt = now },
                new Province { Id = 13, CountryId = 5, Name = "Hanoi", CreatedAt = now },
                new Province { Id = 14, CountryId = 6, Name = "Metro Manila", CreatedAt = now },
                new Province { Id = 15, CountryId = 8, Name = "Tokyo", CreatedAt = now },
                new Province { Id = 16, CountryId = 9, Name = "Seoul", CreatedAt = now },
                new Province { Id = 17, CountryId = 10, Name = "Beijing", CreatedAt = now },
                new Province { Id = 18, CountryId = 11, Name = "California", CreatedAt = now },
                new Province { Id = 19, CountryId = 11, Name = "New York", CreatedAt = now },
                new Province { Id = 20, CountryId = 12, Name = "Greater London", CreatedAt = now }
            );

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, ProvinceId = 1, Name = "Jakarta Pusat", Latitude = -6.1805m, Longitude = 106.8284m, CreatedAt = now },
                new City { Id = 2, ProvinceId = 1, Name = "Jakarta Selatan", Latitude = -6.2615m, Longitude = 106.8106m, CreatedAt = now },
                new City { Id = 3, ProvinceId = 2, Name = "Bandung", Latitude = -6.9175m, Longitude = 107.6191m, CreatedAt = now },
                new City { Id = 4, ProvinceId = 2, Name = "Bogor", Latitude = -6.5971m, Longitude = 106.7932m, CreatedAt = now },
                new City { Id = 5, ProvinceId = 3, Name = "Semarang", Latitude = -6.9667m, Longitude = 110.4167m, CreatedAt = now },
                new City { Id = 6, ProvinceId = 3, Name = "Surakarta", Latitude = -7.5667m, Longitude = 110.8167m, CreatedAt = now },
                new City { Id = 7, ProvinceId = 4, Name = "Yogyakarta", Latitude = -7.7956m, Longitude = 110.3695m, CreatedAt = now },
                new City { Id = 8, ProvinceId = 5, Name = "Surabaya", Latitude = -7.2504m, Longitude = 112.7688m, CreatedAt = now },
                new City { Id = 9, ProvinceId = 5, Name = "Malang", Latitude = -7.9839m, Longitude = 112.6214m, CreatedAt = now },
                new City { Id = 10, ProvinceId = 6, Name = "Tangerang", Latitude = -6.1702m, Longitude = 106.6403m, CreatedAt = now },
                new City { Id = 11, ProvinceId = 7, Name = "Denpasar", Latitude = -8.6705m, Longitude = 115.2126m, CreatedAt = now },
                new City { Id = 12, ProvinceId = 8, Name = "Medan", Latitude = 3.5952m, Longitude = 98.6722m, CreatedAt = now },
                new City { Id = 13, ProvinceId = 9, Name = "Kuala Lumpur City", Latitude = 3.1390m, Longitude = 101.6869m, CreatedAt = now },
                new City { Id = 14, ProvinceId = 10, Name = "Petaling Jaya", Latitude = 3.1073m, Longitude = 101.6067m, CreatedAt = now },
                new City { Id = 15, ProvinceId = 11, Name = "Singapore City", Latitude = 1.3521m, Longitude = 103.8198m, CreatedAt = now },
                new City { Id = 16, ProvinceId = 12, Name = "Bangkok City", Latitude = 13.7563m, Longitude = 100.5018m, CreatedAt = now },
                new City { Id = 17, ProvinceId = 15, Name = "Shinjuku", Latitude = 35.6938m, Longitude = 139.7034m, CreatedAt = now },
                new City { Id = 18, ProvinceId = 16, Name = "Gangnam", Latitude = 37.4979m, Longitude = 127.0276m, CreatedAt = now },
                new City { Id = 19, ProvinceId = 18, Name = "Los Angeles", Latitude = 34.0522m, Longitude = -118.2437m, CreatedAt = now },
                new City { Id = 20, ProvinceId = 20, Name = "London City", Latitude = 51.5074m, Longitude = -0.1278m, CreatedAt = now }
            );
        }
    }
}