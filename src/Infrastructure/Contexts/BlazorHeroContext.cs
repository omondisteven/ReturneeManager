using ReturneeManager.Application.Interfaces.Services;
using ReturneeManager.Application.Models.Chat;
using ReturneeManager.Infrastructure.Models.Identity;
using ReturneeManager.Domain.Contracts;
using ReturneeManager.Domain.Entities.Catalog;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReturneeManager.Domain.Entities.ExtendedAttributes;
using ReturneeManager.Domain.Entities.Misc;

namespace ReturneeManager.Infrastructure.Contexts
{
    public class BlazorHeroContext : AuditableContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public BlazorHeroContext(DbContextOptions<BlazorHeroContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public DbSet<ChatHistory<BlazorHeroUser>> ChatHistories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<IdType> IdTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Upazila> Upazilas { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<FromCountry> FromCountries { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentExtendedAttribute> DocumentExtendedAttributes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }
            }
            if (_currentUserService.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);

            builder.Entity<IdType>().HasData(
                new IdType { Id = 4, Name = "Passport", Description = "Passport" },
                new IdType { Id = 5, Name = "Birth Certificate", Description = "Birth Certificate" },
                new IdType { Id = 6, Name = "National ID", Description = "National ID" }
            );

            builder.Entity<Gender>().HasData(
                new Gender { Id = 4, Name = "Male", Description = "Male gender" },
                new Gender { Id = 5, Name = "Female", Description = "Female gender" },
                new Gender { Id = 6, Name = "Unknown", Description = "Unknown gender" }
            );
            builder.Entity<District>().HasData(
                new District { Id = 4, Name = "BAGERHAT", Description = "BAGERHAT" },
                new District { Id = 5, Name = "BANDARBAN", Description = "BANDARBAN" },
                new District { Id = 6, Name = "BARGUNA", Description = "BARGUNA" }
            );
            builder.Entity<Division>().HasData(
                new Division { Id = 4, Name = "BARISAL", Description = "BARISAL" },
                new Division { Id = 5, Name = "CHITAGONG", Description = "CHITAGONG" },
                new Division { Id = 6, Name = "DHAKA", Description = "DHAKA" }
            );
            builder.Entity<Upazila>().HasData(
                new Upazila { Id = 4, Name = "ABHAYNAGAR", Description = "ABHAYNAGAR " },
                new Upazila { Id = 5, Name = "ADAMDIGHI", Description = "ADAMDIGHI" },
                new Upazila { Id = 6, Name = "ADARSHA SADAR", Description = "ADARSHA SADAR" }
            );
            builder.Entity<FromCountry>().HasData(
                new FromCountry { Id = 4, Name = "Afghanistan", Description = "Afghanistan 1" },
                new FromCountry { Id = 5, Name = "Pakistan", Description = "Pakistan" },
                new FromCountry { Id = 6, Name = "Iran", Description = "Iran" }
            );
            builder.Entity<Ward>().HasData(
                new Ward { Id = 4, Name = "BAGHUTIA", Description = "BAGHUTIA" },
                new Ward { Id = 5, Name = "CHALISHA", Description = "CHALISHA" },
                new Ward { Id = 6, Name = "PAYRA", Description = "PAYRA" }
            );

            builder.Entity<ChatHistory<BlazorHeroUser>>(entity =>
            {
                entity.ToTable("ChatHistory");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.ChatHistoryFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatHistoryToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<BlazorHeroUser>(entity =>
            {
                entity.ToTable(name: "Users", "Identity");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<BlazorHeroRole>(entity =>
            {
                entity.ToTable(name: "Roles", "Identity");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");
            });

            builder.Entity<BlazorHeroRoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims", "Identity");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Identity");
            });
        }
    }
}
