using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.Domain.Entities.Activities;
using bookingSystemZBC.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace bookingSystemZBC.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<ActivitySession> ActivitySessions => Set<ActivitySession>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<YogaActivity>();
        modelBuilder.Entity<SwimmingActivity>();
        modelBuilder.Entity<ClimbingActivity>();

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Price).HasColumnType("decimal(10,2)");
            entity.HasKey(x => x.Id);
            entity.HasDiscriminator<string>("ActivityType")
            .HasValue<YogaActivity>("Yoga")
            .HasValue<SwimmingActivity>("Swimming")
            .HasValue<ClimbingActivity>("Climbing");

        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(200).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.UserName).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Password).HasMaxLength(500).IsRequired();
            entity.Property(x => x.FirstName).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Surname).HasMaxLength(120).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();

            entity.HasOne(x => x.Member)
                .WithOne()
                .HasForeignKey<User>(x => x.MemberId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<ActivitySession>(entity =>
        {
            entity.HasOne(x => x.Activity)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Location)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Bookings)
                .WithOne(x => x.ActivitySession)
                .HasForeignKey(x => x.ActivitySessionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasOne(x => x.Member)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(x => new { x.MemberId, x.ActivitySessionId }).IsUnique();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasMany(x => x.Users)
                  .WithMany(x => x.Roles)
                  .UsingEntity<Dictionary<string, object>>(
                        "UserRoles",
                        j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasPrincipalKey(u => u.Id)
                        .OnDelete(DeleteBehavior.Cascade),
                        j => j
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasPrincipalKey(r => r.RoleId)
                        .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");
                            j.HasData(
                                new { UserId = 1, RoleId = 2 },
                                new { UserId = 2, RoleId = 1 },
                                new { UserId = 3, RoleId = 1 },
                                new { UserId = 4, RoleId = 1 }
                            );
                        });

        });

    

        modelBuilder.Entity<Location>().HasData(
            new Location { Id = 1, Name = "Main Hall", Description = "Large multi-purpose hall", IsAvailable = true, Capacity = 60 },
            new Location { Id = 2, Name = "Pool Area", Description = "Indoor swimming pool", IsAvailable = true, Capacity = 30 },
            new Location { Id = 3, Name = "Climbing Wall", Description = "Indoor climbing wall with instructor area", IsAvailable = true, Capacity = 24 }
        );

        modelBuilder.Entity<Member>().HasData(
            new Member { Id = 1, Name = "Alice Jensen", Email = "alice@example.com", Age = 26 },
            new Member { Id = 2, Name = "Bob Nielsen", Email = "bob@example.com", Age = 31 },
            new Member { Id = 3, Name = "Clara Madsen", Email = "clara@example.com", Age = 22 }
        );

        modelBuilder.Entity<Role>().HasData(
          new Role { RoleId = 1, RoleName = "User" },
          new Role { RoleId = 2, RoleName = "Admin" }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@example.com",
                Password = "Admin123!",
                FirstName = "System",
                Surname = "Administrator"
            },
            new User
            {
                Id = 2,
                UserName = "alice",
                Email = "alice@example.com",
                Password = "Password123!",
                FirstName = "Alice",
                Surname = "Jensen",
                MemberId = 1
            },
            new User
            {
                Id = 3,
                UserName = "bob",
                Email = "bob@example.com",
                Password = "Password123!",
                FirstName = "Bob",
                Surname = "Nielsen",
                MemberId = 2
            },
            new User
            {
                Id = 4,
                UserName = "clara",
                Email = "clara@example.com",
                Password = "Password123!",
                FirstName = "Clara",
                Surname = "Madsen",
                MemberId = 3
            }
        );

        modelBuilder.Entity<YogaActivity>().HasData(
            new
            {
                Id = 1,
                Name = "Morning Yoga",
                Type = ActivityType.Yoga,
                MaxParticipants = 20,
                Price = 75.00,
                AdditionalInfoField1 = "Hatha flow",
                AddtionalInfoField1 = "Hatha flow",
                Level = "Beginner"
            }
        );

        modelBuilder.Entity<SwimmingActivity>().HasData(
            new
            {
                Id = 2,
                Name = "Evening Swim",
                Type = ActivityType.Swimming,
                MaxParticipants = 16,
                Price = 95.00,
                AdditionalInfoField1 = "Lane training",
                Level = "Intermediate"
            }
        );

        modelBuilder.Entity<ClimbingActivity>().HasData(
            new
            {
                Id = 3,
                Name = "Intro Climbing",
                Type = ActivityType.Climbing,
                MaxParticipants = 12,
                Price = 125.00,
                AdditionalInfoField1 = "Instructor included",
                Level = "Beginner"
            }
        );

        modelBuilder.Entity<ActivitySession>().HasData(
            new ActivitySession
            {
                Id = 1,
                ActivityId = 1,
                LocationId = 1,
                StartTimeUtc = new DateTime(2026, 5, 4, 16, 0, 0, DateTimeKind.Utc),
                EndTimeUtc = new DateTime(2026, 5, 4, 17, 0, 0, DateTimeKind.Utc),
                Notes = "Bring your own mat if you prefer."
            },
            new ActivitySession
            {
                Id = 2,
                ActivityId = 2,
                LocationId = 2,
                StartTimeUtc = new DateTime(2026, 5, 5, 17, 30, 0, DateTimeKind.Utc),
                EndTimeUtc = new DateTime(2026, 5, 5, 18, 30, 0, DateTimeKind.Utc),
                Notes = "Meet at the pool entrance 10 minutes before start."
            },
            new ActivitySession
            {
                Id = 3,
                ActivityId = 3,
                LocationId = 3,
                StartTimeUtc = new DateTime(2026, 5, 6, 15, 0, 0, DateTimeKind.Utc),
                EndTimeUtc = new DateTime(2026, 5, 6, 17, 0, 0, DateTimeKind.Utc),
                Notes = "Harnesses are provided."
            },
            new ActivitySession
            {
                Id = 4,
                ActivityId = 1,
                LocationId = 1,
                StartTimeUtc = new DateTime(2026, 5, 7, 7, 30, 0, DateTimeKind.Utc),
                EndTimeUtc = new DateTime(2026, 5, 7, 8, 30, 0, DateTimeKind.Utc),
                Notes = "Quiet morning session."
            }
        );

        modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                Id = 1,
                MemberId = 1,
                ActivitySessionId = 1,
                CreatedAtUtc = new DateTime(2026, 4, 28, 9, 0, 0, DateTimeKind.Utc)
            },
            new Booking
            {
                Id = 2,
                MemberId = 2,
                ActivitySessionId = 2,
                CreatedAtUtc = new DateTime(2026, 4, 28, 9, 15, 0, DateTimeKind.Utc)
            },
            new Booking
            {
                Id = 3,
                MemberId = 3,
                ActivitySessionId = 3,
                CreatedAtUtc = new DateTime(2026, 4, 28, 9, 30, 0, DateTimeKind.Utc)
            },
            new Booking
            {
                Id = 4,
                MemberId = 1,
                ActivitySessionId = 4,
                CreatedAtUtc = new DateTime(2026, 4, 28, 9, 45, 0, DateTimeKind.Utc)
            }
        );

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);
    }    
}
