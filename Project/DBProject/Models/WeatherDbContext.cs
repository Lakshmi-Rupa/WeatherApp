using DBProject.Models.WeatherDetails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<LongWeatherForecastListItemModel>().HasNoKey();
            modelBuilder.Entity<LongWeatherForecastModel>().HasNoKey();
        }

        public virtual DbSet<Clouds> Clouds { get; set; }
        public virtual DbSet<Coord> Coord { get; set; }
        public virtual DbSet<Main> Main { get; set; }
        public virtual DbSet<Sys> Sys { get; set; }
        public virtual DbSet<Weather> Weather { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Wind> Wind { get; set; }
        public virtual DbSet<LongWeatherForecastListItemModel> LongWeatherForecastListItemModel { get; set; }
        public virtual DbSet<LongWeatherForecastModel> LongWeatherForecastModel { get; set; }
        public virtual DbSet<WeatherModel> WeatherModel { get; set; }
        
        public virtual void SetModified<T>(T entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
