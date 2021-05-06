using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public virtual DbSet<BranchOffice> BranchOffice { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<WeatherCondition> WeatherCondition { get; set; }
        public virtual DbSet<WeatherType> WeatherType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchOffice>(entity =>
            {
                entity.ToTable("Branch_office");

                entity.HasIndex(e => e.CityId)
                    .HasName("IX_Branch_office_City_ID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CITY_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

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

                entity.HasIndex(e => e.Id)
                    .HasName("CITY_PK")
                    .IsClustered();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryId).HasColumnName("COUNTRY_ID");

                entity.Property(e => e.Latitude).HasColumnName("LATITUDE");

                entity.Property(e => e.Longitude).HasColumnName("LONGITUDE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("STATE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Country1");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("CODE")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WeatherCondition>(entity =>
            {
                entity.ToTable("Weather_condition");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Base)
                    .IsRequired()
                    .HasColumnName("BASE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

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
                    .HasColumnName("REG_DATE")
                    .HasColumnType("datetime");

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
                    .WithMany(p => p.WeatherCondition)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Weather_condition_City");
            });

            modelBuilder.Entity<WeatherType>(entity =>
            {
                entity.ToTable("Weather_type");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_Weather_type");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConditionId).HasColumnName("CONDITION_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasColumnName("ICON")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Main)
                    .IsRequired()
                    .HasColumnName("MAIN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TYPE_ID");

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.WeatherType)
                    .HasForeignKey(d => d.ConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Weather_type_Weather_condition");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
