using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguage { get; set; }
        public DbSet<ProgrammingTechnology> ProgrammingTechnologies { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.ProgrammingTechnologies);
            });

            modelBuilder.Entity<ProgrammingTechnology>(a =>
            {
                a.ToTable("ProgrammingTechnologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasOne(p => p.ProgrammingLanguage);
            });



            ProgrammingLanguage[] programmingLanguageSeeds = { new(1, "C#"), new(2, "JAVA") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeeds);

            ProgrammingTechnology[] programmingTechnologiesSeeds = { new(1, 1, "WPF"), new(2, 1, "Asp.Net"), new(3, 2, "Sprint"), new(4, 2, "JSP") };
            modelBuilder.Entity<ProgrammingTechnology>().HasData(programmingTechnologiesSeeds);
        }
    }
}
