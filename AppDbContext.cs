using Bogus;
using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Organizers;
using Sukalibur.Graph.Trips;
using Sukalibur.Graph.Users;
using System.Reflection.Emit;

namespace Sukalibur
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            #region Seed initial data
            int accountId = 11;
            var accountData = new Faker<User>()
                .UseSeed(42)
                .RuleFor(m => m.Id, f => accountId++)
                .RuleFor(m => m.Username, f => f.Person.UserName)
                .RuleFor(m => m.Email, f => f.Person.Email)
                .RuleFor(m => m.Password, f => f.Internet.Password())
                .RuleFor(m => m.Dob, f => null)
                .RuleFor(m => m.Phone, f => f.Person.Phone)
                .RuleFor(m => m.FullName, f => f.Person.FullName)
                .RuleFor(m => m.Role, f => f.PickRandom<UserRole>())
                .RuleFor(m => m.Gender, f => f.PickRandom<UserGender>())
                .RuleFor(m => m.CreatedAt, f => f.Date.Past())
                .RuleFor(m => m.UpdatedAt, f => f.Date.Past())
                .Generate(100);
            accountData.InsertRange(0, [
                new User { Id = 1, Username = "root", Email = "root@sukalibur.com", FullName = "Root", Password = "", Role = UserRole.Super, Gender = UserGender.Other }
             ]);
            builder.Entity<User>()
                .HasData(accountData);
            builder.Entity<TripCategory>()
                .HasData([
                    new TripCategory { Id = 1, Name = "Open Trip" },
                    new TripCategory { Id = 2, Name = "Private Trip" },
                    new TripCategory { Id = 4, Name = "Group Trip" },
                ]);
            builder.Entity<Organizer>()
                .HasData([
                    new Organizer { Id = 1, Username = "root", Name = "Root", Email="root@sukalibur.com", Phone = "14025", Status = OrganizerStatus.Active },
                ]);
            builder.Entity<Trip>()
                .HasData([
                    new Trip { Id = 1, CategoryId = 1, OrganizerId = 1, Title = "Main Trip", Description = "Main Trip Description" },    
                ]);
            #endregion

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripCategory> TripCategories { get; set; }
    }
}
