using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using programeren_3_eindwerk.classes;

namespace programeren_3_eindwerk.Deel2
{
    class ProvinciesContext : DbContext
    {
        public DbSet<Gemeente> Gemeenten { get; set; }
        public DbSet<Graaf> Grafen { get; set; }
        public DbSet<Knoop> Knopen { get; set; }
        public DbSet<Provincie> Provincies { get; set; }
        public DbSet<Punt> Punten { get; set; }
        public DbSet<Segment> Segmenten { get; set; }
        public DbSet<Straat> Straten { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-7B94T84\sqlexpress;Initial Catalog=Provincies;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provincie>().ToTable("Provincies");
            modelBuilder.Entity<Gemeente>().ToTable("Gemeenten");
            modelBuilder.Entity<Straat>().ToTable("Straten");
            modelBuilder.Entity<Graaf>().ToTable("Grafen");
            modelBuilder.Entity<Segment>().ToTable("Segmenten");
            modelBuilder.Entity<Knoop>().ToTable("Knopen");
            modelBuilder.Entity<Punt>().ToTable("Puntes");




            modelBuilder.Entity<Provincie>()
                .HasMany(pr => pr.Gemeenten)
                .WithOne();

            modelBuilder.Entity<Gemeente>()
                 .HasMany(ge => ge.Straten)
                .WithOne();

            modelBuilder.Entity<Straat>()
                 .HasOne(st => st.Graaf);

            modelBuilder.Entity<Graaf>()
                .HasMany(tr => tr.ListMap);

            modelBuilder.Entity<Segment>()
                .HasOne(se => se.Beginknoop);
            modelBuilder.Entity<Segment>()
                .HasOne(se => se.Eindknoop);
            modelBuilder.Entity<Segment>()
                .HasMany(se => se.Vertices)
                .WithOne();

            modelBuilder.Entity<Knoop>()
                .HasOne(kn => kn.Punt);




        }
    }
}
