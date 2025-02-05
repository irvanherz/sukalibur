using Bogus;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Sukalibur.Graph.Auth;
using Sukalibur.Graph.Carts;
using Sukalibur.Graph.Invoices;
using Sukalibur.Graph.Medias;
using Sukalibur.Graph.Notifications;
using Sukalibur.Graph.Orders;
using Sukalibur.Graph.Organizers;
using Sukalibur.Graph.Payments;
using Sukalibur.Graph.Trips;
using Sukalibur.Graph.Users;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;

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
            builder.Entity<User>()
                .Property(u => u.FullName)
                .HasDefaultValue("");
            builder.Entity<User>()
                .Property(u => u.Phone)
                .HasDefaultValue("");
            builder.Entity<User>()
                .Property(u => u.Password)
                .HasDefaultValue("");
            builder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue(UserRole.User);
            builder.Entity<User>()
                .Property(u => u.Gender)
                .HasDefaultValue(UserGender.Unspecified);
            builder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Organizer>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Organizer>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<OrganizerMember>()
                .Property(om => om.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<OrganizerMember>()
                .Property(om => om.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Trip>()
                .HasGeneratedTsVectorColumn(
                    p => p.SearchVector,
                    "indonesian",  // Text search config
                    p => new { p.Name, p.Description })  // Included properties
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");
            builder.Entity<Trip>()
                .Property(t => t.Description)
                .HasDefaultValue("");
            builder.Entity<Trip>()
                .Property(t => t.Includes)
                .HasDefaultValue(new List<string>());
            builder.Entity<Trip>()
                .Property(t => t.Excludes)
                .HasDefaultValue(new List<string>());
            builder.Entity<Trip>()
                .Property(t => t.Status)
                .HasDefaultValue(TripStatus.Inactive);
            builder.Entity<Trip>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Trip>()
                .Property(t => t.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripPackage>()
                .Property(tp => tp.Description)
                .HasDefaultValue("");
            builder.Entity<TripPackage>()
                .Property(tp => tp.Status)
                .HasDefaultValue(TripPackageStatus.Inactive);
            builder.Entity<TripPackage>()
                .Property(tp => tp.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripPackage>()
                .Property(tp => tp.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripAddon>()
                .Property(ta => ta.Description)
                .HasDefaultValue("");
            builder.Entity<TripAddon>()
                .Property(ta => ta.Status)
                .HasDefaultValue(TripAddonStatus.Inactive);
            builder.Entity<TripAddon>()
                .Property(ta => ta.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripAddon>()
                .Property(ta => ta.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripItinerary>()
                .Property(ti => ti.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripItinerary>()
                .Property(ti => ti.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripSchedule>()
                .Property(ts => ts.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripSchedule>()
                .Property(ts => ts.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripReservation>()
                .Property(tr => tr.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripReservation>()
                .Property(tr => tr.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripCategory>()
                .Property(tc => tc.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripCategory>()
                .Property(tc => tc.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<TripFeature>()
               .HasIndex(tf => tf.TripId)
               .IsUnique();
            builder.Entity<Cart>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Cart>()
                .Property(c => c.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<CartItem>()
                .Property(ci => ci.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<CartItem>()
                .Property(ci => ci.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Order>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Order>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<OrderItem>()
                .Property(oi => oi.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<OrderItem>()
                .Property(oi => oi.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Payment>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Payment>()
                .Property(p => p.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<Media>()
               .Property(m => m.CreatedAt)
               .HasDefaultValueSql("NOW()");
            builder.Entity<Media>()
                .Property(m => m.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<FcmToken>()
                .HasIndex(ft => new { ft.Token })
                .IsUnique();
            builder.Entity<FcmToken>()
                .Property(ft => ft.CreatedAt)
                .HasDefaultValueSql("NOW()");
            builder.Entity<FcmToken>()
                .Property(ft => ft.UpdatedAt)
                .HasDefaultValueSql("NOW()");
            #region Seed initial data
            builder.Entity<User>()
                .HasData([
                    new User { Id = 1, Username = "root", Email = "root@sukalibur.com", FullName = "Root", Password = "$2y$08$418wcq/JSnXBzU3yN/Xpje7tnqxEP8uGx7R9r3YMzLc1AF71a.Tj6", Role = UserRole.Super, Gender = UserGender.Other, CreatedAt = Instant.FromUnixTimeTicks(17366053651766652L), UpdatedAt = Instant.FromUnixTimeTicks(17366053651766652L) }
                ]);
            builder.Entity<TripCategory>()
                .HasData([
                    new TripCategory { Id = 1, Name = "Open Trip", CreatedAt = Instant.FromUnixTimeTicks(17366053651766652L), UpdatedAt = Instant.FromUnixTimeTicks(17366053651766652L) },
                ]);
            builder.Entity<Organizer>()
                .HasData([
                    new Organizer { Id = 1, Username = "root", Name = "Root", Email="root@sukalibur.com", Phone = "14025", Status = OrganizerStatus.Active, CreatedAt = Instant.FromUnixTimeTicks(17366053651766652L), UpdatedAt = Instant.FromUnixTimeTicks(17366053651766652L) },
                ]);
            builder.Entity<OrganizerMember>()
                .HasData([
                    new OrganizerMember { Id = 1, OrganizerId = 1, UserId = 1, Role = OrganizerMemberRole.Super, Status = OrganizerMemberStatus.Approved, CreatedAt = Instant.FromUnixTimeTicks(17366053651766652L), UpdatedAt = Instant.FromUnixTimeTicks(17366053651766652L) },
                ]);
            builder.Entity<Trip>()
                .HasData([
                    new Trip { Id = 1, CategoryId = 1, OrganizerId = 1, Name = "Main Trip", Description = "Main Trip Description", CreatedAt = Instant.FromUnixTimeTicks(17366053651766652L), UpdatedAt = Instant.FromUnixTimeTicks(17366053651766652L) },
                ]);
            builder.Entity<TripSchedule>()
                .HasData([
                    new TripSchedule { Id = 1, TripId = 1, ScheduledAt =  Instant.FromUnixTimeTicks(17366053651766652L), Status = TripScheduleStatus.Finished, CreatedAt = Instant.FromUnixTimeTicks(17366053651766652L), UpdatedAt = Instant.FromUnixTimeTicks(17366053651766652L) },
                ]);
            builder.Entity<TripPackage>()
               .HasData([
                   new TripPackage { Id = 1, TripId = 1, Name = "Root", Description = "Root", Status = TripPackageStatus.Active, Price = 100000, CreatedAt = Instant.FromUnixTimeTicks(17366053651766652L), UpdatedAt = Instant.FromUnixTimeTicks(17366053651766652L) },
               ]);
            builder.Entity<Cart>()
                .HasData([
                    new Cart { Id = 1, UserId = 1, CreatedAt = Instant.FromUnixTimeTicks(17366053651766652L), UpdatedAt = Instant.FromUnixTimeTicks(17366053651766652L) },
                ]);
            #endregion
            builder.HasPostgresExtension("vector");
            base.OnModelCreating(builder);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<OrganizerMember> OrganizerMembers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripCategory> TripCategories { get; set; }
        public DbSet<TripItinerary> TripItineraries { get; set; }
        public DbSet<TripSchedule> TripSchedules { get; set; }
        public DbSet<TripPackage> TripPackages { get; set; }
        public DbSet<TripReservation> TripReservations { get; set; }
        public DbSet<TripFeature> TripFeatures { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderDetails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentChannel> PaymentChannels { get; set; }
        public DbSet<TripAddon> TripAddons { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<FcmToken> FcmTokens { get; set; }
    }
}
