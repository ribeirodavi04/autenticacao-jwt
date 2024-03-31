using AutenticacaoJWT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Infra.Data.Context
{
    public class ContextApp : DbContext
    {
        private readonly IConfiguration _configuration;

        public ContextApp(DbContextOptions<ContextApp> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=127.0.0.1; Port=5432; Username=postgres; Password=admin; Database=AutenticacaoJWTDB;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users"); 
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired(); 
                entity.Property(e => e.Name).HasColumnName("Name"); 
                entity.Property(e => e.Email).HasColumnName("Email"); 
                entity.Property(e => e.Password).HasColumnName("Password"); 
            });
        }
    }
}
