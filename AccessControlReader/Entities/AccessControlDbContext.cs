using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AccessControlReader.Entities
{
    internal class AccessControlDbContext : DbContext
    {
        readonly string ConnectionString;
        public DbSet<Card> CardItems { get; set; }
        public DbSet<Reader> ReadersItems { get; set; }
        public DbSet<Reading> ReadingsItems { get; set; }
        public DbSet<User> UsersItems { get; set; }

        public AccessControlDbContext(string ConnectionString) : base()
        {
            this.ConnectionString = ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(c =>
            {
                c.HasMany(cr => cr.Readings).
                    WithOne(ru => ru.Card).
                    IsRequired(false).
                    HasForeignKey(cr => cr.Card_ID);
            });

            modelBuilder.Entity<User>(u =>
            {
                u.HasMany(u => u.Readings).
                    WithOne(ru => ru.User).
                    IsRequired(false).
                    HasPrincipalKey(ru => ru.User_ID);

                u.HasMany(uc => uc.Cards).
                    WithOne(cu => cu.User).
                    IsRequired(false).
                    HasPrincipalKey(c => c.User_ID);
            });

            modelBuilder.Entity<Reader>(r =>
            {
                r.HasMany(rr => rr.Readings).
                    WithOne(rr => rr.Reader).
                    HasPrincipalKey(ru => ru.Reader_ID);
            });
        }
    }
}
