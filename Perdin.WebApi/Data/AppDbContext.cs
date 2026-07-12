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

                entity.Property(e => e.Island)
                    .HasColumnName("island")
                    .HasColumnType("varchar(50)")
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
                    .HasColumnName("approver_id");

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
                new Province { Id = 1, CountryId = 1, Name = "DKI Jakarta", Island = "Jawa", CreatedAt = now },
                new Province { Id = 2, CountryId = 1, Name = "Jawa Barat", Island = "Jawa", CreatedAt = now },
                new Province { Id = 3, CountryId = 1, Name = "Jawa Tengah", Island = "Jawa", CreatedAt = now },
                new Province { Id = 4, CountryId = 1, Name = "DI Yogyakarta", Island = "Jawa", CreatedAt = now },
                new Province { Id = 5, CountryId = 1, Name = "Jawa Timur", Island = "Jawa", CreatedAt = now },
                new Province { Id = 6, CountryId = 1, Name = "Banten", Island = "Jawa", CreatedAt = now },
                new Province { Id = 7, CountryId = 1, Name = "Bali", Island = "Bali", CreatedAt = now },
                new Province { Id = 8, CountryId = 1, Name = "Sumatera Utara", Island = "Sumatera", CreatedAt = now },
                new Province { Id = 9, CountryId = 1, Name = "Riau", Island = "Sumatera", CreatedAt = now },
                new Province { Id = 10, CountryId = 1, Name = "Kepulauan Riau", Island = "Sumatera", CreatedAt = now },
                new Province { Id = 11, CountryId = 1, Name = "Sumatera Barat", Island = "Sumatera", CreatedAt = now },
                new Province { Id = 12, CountryId = 1, Name = "Sumatera Selatan", Island = "Sumatera", CreatedAt = now },
                new Province { Id = 13, CountryId = 1, Name = "Lampung", Island = "Sumatera", CreatedAt = now },
                new Province { Id = 14, CountryId = 1, Name = "Kalimantan Barat", Island = "Kalimantan", CreatedAt = now },
                new Province { Id = 15, CountryId = 1, Name = "Kalimantan Timur", Island = "Kalimantan", CreatedAt = now },
                new Province { Id = 16, CountryId = 1, Name = "Sulawesi Selatan", Island = "Sulawesi", CreatedAt = now },
                new Province { Id = 17, CountryId = 1, Name = "Sulawesi Utara", Island = "Sulawesi", CreatedAt = now },
                new Province { Id = 18, CountryId = 1, Name = "Nusa Tenggara Barat", Island = "Lombok", CreatedAt = now },
                new Province { Id = 19, CountryId = 1, Name = "Nusa Tenggara Timur", Island = "Timor", CreatedAt = now },
                new Province { Id = 20, CountryId = 1, Name = "Papua", Island = "Papua", CreatedAt = now }
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
                new City { Id = 13, ProvinceId = 9, Name = "Pekanbaru", Latitude = 0.5074m, Longitude = 101.4478m, CreatedAt = now },
                new City { Id = 14, ProvinceId = 10, Name = "Batam", Latitude = 1.1301m, Longitude = 104.0531m, CreatedAt = now },
                new City { Id = 15, ProvinceId = 11, Name = "Padang", Latitude = -0.9471m, Longitude = 100.4172m, CreatedAt = now },
                new City { Id = 16, ProvinceId = 12, Name = "Palembang", Latitude = -2.9909m, Longitude = 104.7567m, CreatedAt = now },
                new City { Id = 17, ProvinceId = 13, Name = "Bandar Lampung", Latitude = -5.3971m, Longitude = 105.2668m, CreatedAt = now },
                new City { Id = 18, ProvinceId = 14, Name = "Pontianak", Latitude = -0.0263m, Longitude = 109.3425m, CreatedAt = now },
                new City { Id = 19, ProvinceId = 15, Name = "Samarinda", Latitude = -0.5022m, Longitude = 117.1536m, CreatedAt = now },
                new City { Id = 20, ProvinceId = 16, Name = "Makassar", Latitude = -5.1476m, Longitude = 119.4327m, CreatedAt = now }
            );

            modelBuilder.Entity<BusinessTripRequest>().HasData(
                new BusinessTripRequest
                {
                    Id = 1,
                    RequestNumber = "PERDIN/20260701/0001",
                    UserId = 2,
                    DepartureDate = new DateOnly(2026, 7, 1),
                    ReturnDate = new DateOnly(2026, 7, 4),
                    OriginCityId = 1,
                    DestinationCityId = 2,
                    DestinationCountryId = 1,
                    DurationInDays = 3,
                    Status = "reviewed",
                    Purpose = "Meeting Internal",
                    PocketMoney = 0,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 2,
                    RequestNumber = "PERDIN/20260701/0002",
                    UserId = 4,
                    DepartureDate = new DateOnly(2026, 7, 1),
                    ReturnDate = new DateOnly(2026, 7, 3),
                    OriginCityId = 1,
                    DestinationCityId = 3,
                    DestinationCountryId = 1,
                    DurationInDays = 2,
                    Status = "approved",
                    Purpose = "Kunjungan Cabang Bandung",
                    PocketMoney = 500000,
                    ApproverId = 1,
                    ApprovedAt = now,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 3,
                    RequestNumber = "PERDIN/20260702/0001",
                    UserId = 5,
                    DepartureDate = new DateOnly(2026, 7, 5),
                    ReturnDate = new DateOnly(2026, 7, 7),
                    OriginCityId = 3,
                    DestinationCityId = 4,
                    DestinationCountryId = 1,
                    DurationInDays = 2,
                    Status = "reviewed",
                    Purpose = "Audit Kebun Bogor",
                    PocketMoney = 400000,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 4,
                    RequestNumber = "PERDIN/20260702/0002",
                    UserId = 6,
                    DepartureDate = new DateOnly(2026, 7, 6),
                    ReturnDate = new DateOnly(2026, 7, 10),
                    OriginCityId = 1,
                    DestinationCityId = 11,
                    DestinationCountryId = 1,
                    DurationInDays = 4,
                    Status = "reviewed",
                    Purpose = "Rapat Koordinasi Nasional Bali",
                    PocketMoney = 1200000,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 5,
                    RequestNumber = "PERDIN/20260703/0001",
                    UserId = 8,
                    DepartureDate = new DateOnly(2026, 7, 10),
                    ReturnDate = new DateOnly(2026, 7, 15),
                    OriginCityId = 1,
                    DestinationCityId = null,
                    DestinationCountryId = 3,
                    DurationInDays = 5,
                    Status = "approved",
                    Purpose = "IT Conference Singapore",
                    PocketMoney = 4000000,
                    ApproverId = 3,
                    ApprovedAt = now,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 6,
                    RequestNumber = "PERDIN/20260703/0002",
                    UserId = 10,
                    DepartureDate = new DateOnly(2026, 7, 12),
                    ReturnDate = new DateOnly(2026, 7, 18),
                    OriginCityId = 1,
                    DestinationCityId = null,
                    DestinationCountryId = 8,
                    DurationInDays = 6,
                    Status = "rejected",
                    Purpose = "Studi Banding Sistem Kereta",
                    PocketMoney = 4800000,
                    ApproverId = 1,
                    ApprovedAt = now,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 7,
                    RequestNumber = "PERDIN/20260704/0001",
                    UserId = 2,
                    DepartureDate = new DateOnly(2026, 7, 15),
                    ReturnDate = new DateOnly(2026, 7, 17),
                    OriginCityId = 8,
                    DestinationCityId = 9,
                    DestinationCountryId = 1,
                    DurationInDays = 2,
                    Status = "reviewed",
                    Purpose = "Sosialisasi SOP Baru",
                    PocketMoney = 400000,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 8,
                    RequestNumber = "PERDIN/20260704/0002",
                    UserId = 4,
                    DepartureDate = new DateOnly(2026, 7, 20),
                    ReturnDate = new DateOnly(2026, 7, 25),
                    OriginCityId = 12,
                    DestinationCityId = 1,
                    DestinationCountryId = 1,
                    DurationInDays = 5,
                    Status = "reviewed",
                    Purpose = "General Meeting Direksi",
                    PocketMoney = 1500000,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 9,
                    RequestNumber = "PERDIN/20260705/0001",
                    UserId = 5,
                    DepartureDate = new DateOnly(2026, 7, 22),
                    ReturnDate = new DateOnly(2026, 7, 26),
                    OriginCityId = 1,
                    DestinationCityId = null,
                    DestinationCountryId = 2,
                    DurationInDays = 4,
                    Status = "approved",
                    Purpose = "Negosiasi Kontrak Vendor Malaysia",
                    PocketMoney = 3200000,
                    ApproverId = 3,
                    ApprovedAt = now,
                    CreatedAt = now
                },
                new BusinessTripRequest
                {
                    Id = 10,
                    RequestNumber = "PERDIN/20260705/0002",
                    UserId = 6,
                    DepartureDate = new DateOnly(2026, 7, 28),
                    ReturnDate = new DateOnly(2026, 8, 4),
                    OriginCityId = 1,
                    DestinationCityId = null,
                    DestinationCountryId = 12,
                    DurationInDays = 7,
                    Status = "reviewed",
                    Purpose = "Pelatihan Manajemen Risiko London",
                    PocketMoney = 5600000,
                    CreatedAt = now
                }
            );
        }
    }
}