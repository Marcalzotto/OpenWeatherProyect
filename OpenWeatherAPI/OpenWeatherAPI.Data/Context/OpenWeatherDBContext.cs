using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OpenWeatherAPI.Data.DataEntities
{
    public partial class OpenWeatherDBContext : DbContext
    {
        public OpenWeatherDBContext()
        {
        }

        public OpenWeatherDBContext(DbContextOptions<OpenWeatherDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BranchOffice> BranchOffices { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WeatherCondition> WeatherConditions { get; set; }
        public virtual DbSet<WeatherType> WeatherTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<BranchOffice>(entity =>
            {
                entity.ToTable("Branch_office");

                entity.HasIndex(e => e.CityId, "IX_Branch_office_City_ID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CITY_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.HasOne(d => d.City)
                    .WithOne(p => p.BranchOffice)
                    .HasForeignKey<BranchOffice>(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branch_office_City1");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("City");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("COUNTRY_ID");

                entity.Property(e => e.Latitude).HasColumnName("LATITUDE");

                entity.Property(e => e.Longitude).HasColumnName("LONGITUDE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Country1");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("datetime")
                    .HasColumnName("BIRTHDATE");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("SALT");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SURNAME");
            });

            modelBuilder.Entity<WeatherCondition>(entity =>
            {
                entity.ToTable("Weather_condition");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Base)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BASE");

                entity.Property(e => e.CityId).HasColumnName("CITY_ID");

                entity.Property(e => e.Clouds).HasColumnName("CLOUDS");

                entity.Property(e => e.Dt).HasColumnName("DT");

                entity.Property(e => e.FeelsLike).HasColumnName("FEELS_LIKE");

                entity.Property(e => e.GroundLevel).HasColumnName("GROUND_LEVEL");

                entity.Property(e => e.Humidity).HasColumnName("HUMIDITY");

                entity.Property(e => e.Pressure).HasColumnName("PRESSURE");

                entity.Property(e => e.RainVolume1h).HasColumnName("RAIN_VOLUME_1H");

                entity.Property(e => e.RainVolume3h).HasColumnName("RAIN_VOLUME_3H");

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasColumnName("REG_DATE");

                entity.Property(e => e.SeaLevel).HasColumnName("SEA_LEVEL");

                entity.Property(e => e.SnowVolume1h).HasColumnName("SNOW_VOLUME_1H");

                entity.Property(e => e.SnowVolume3h).HasColumnName("SNOW_VOLUME_3H");

                entity.Property(e => e.StatusCode).HasColumnName("STATUS_CODE");

                entity.Property(e => e.Sunrise).HasColumnName("SUNRISE");

                entity.Property(e => e.Sunset).HasColumnName("SUNSET");

                entity.Property(e => e.TempMax).HasColumnName("TEMP_MAX");

                entity.Property(e => e.TempMin).HasColumnName("TEMP_MIN");

                entity.Property(e => e.Temperature).HasColumnName("TEMPERATURE");

                entity.Property(e => e.Timezone).HasColumnName("TIMEZONE");

                entity.Property(e => e.WindDegrees).HasColumnName("WIND_DEGREES");

                entity.Property(e => e.WindGust).HasColumnName("WIND_GUST");

                entity.Property(e => e.WindSpeed).HasColumnName("WIND_SPEED");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.WeatherConditions)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Weather_condition_City");
            });

            modelBuilder.Entity<WeatherType>(entity =>
            {
                entity.ToTable("Weather_type");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConditionId).HasColumnName("CONDITION_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ICON");

                entity.Property(e => e.Main)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MAIN");

                entity.Property(e => e.TypeId).HasColumnName("TYPE_ID");

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.WeatherTypes)
                    .HasForeignKey(d => d.ConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Weather_type_Weather_condition");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
