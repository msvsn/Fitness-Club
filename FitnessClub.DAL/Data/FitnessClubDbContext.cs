using FitnessClub.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.DAL.Data
{
    public class FitnessClubDbContext : IdentityDbContext<ApplicationUser>
    {
        public FitnessClubDbContext(DbContextOptions<FitnessClubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<ClassType> ClassTypes { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }
        public DbSet<ClassRegistration> ClassRegistrations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Models.FitnessClub> FitnessClubs { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<MembershipCard> MembershipCards { get; set; }
        public DbSet<Visit> Visits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування для Client
            modelBuilder.Entity<Client>()
                .HasMany(c => c.MembershipCards)
                .WithOne(m => m.Client)
                .HasForeignKey(m => m.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.ClassRegistrations)
                .WithOne(cr => cr.Client)
                .HasForeignKey(cr => cr.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Visits)
                .WithOne(v => v.Client)
                .HasForeignKey(v => v.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Зв'язок з ApplicationUser
            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Налаштування для Trainer
            modelBuilder.Entity<Trainer>()
                .HasMany(t => t.ClassSchedules)
                .WithOne(cs => cs.Trainer)
                .HasForeignKey(cs => cs.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Зв'язок з ApplicationUser
            modelBuilder.Entity<Trainer>()
                .HasOne(t => t.User)
                .WithOne(u => u.Trainer)
                .HasForeignKey<Trainer>(t => t.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Налаштування для FitnessClub
            modelBuilder.Entity<Models.FitnessClub>()
                .HasOne(f => f.Location)
                .WithMany(l => l.FitnessClubs)
                .HasForeignKey(f => f.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Models.FitnessClub>()
                .HasMany(f => f.ClassSchedules)
                .WithOne(cs => cs.FitnessClub)
                .HasForeignKey(cs => cs.FitnessClubId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Models.FitnessClub>()
                .HasMany(f => f.MembershipCards)
                .WithOne(m => m.HomeClub)
                .HasForeignKey(m => m.HomeClubId)
                .OnDelete(DeleteBehavior.Restrict);

            // Налаштування для ClassSchedule
            modelBuilder.Entity<ClassSchedule>()
                .HasOne(cs => cs.ClassType)
                .WithMany()
                .HasForeignKey(cs => cs.ClassTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Налаштування для ClassRegistration
            modelBuilder.Entity<ClassRegistration>()
                .HasOne(cr => cr.ClassSchedule)
                .WithMany()
                .HasForeignKey(cr => cr.ClassScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClassRegistration>()
                .HasOne(cr => cr.Visit)
                .WithOne(v => v.ClassRegistration)
                .HasForeignKey<Visit>(v => v.ClassRegistrationId)
                .OnDelete(DeleteBehavior.SetNull);

            // Налаштування для MembershipCard
            modelBuilder.Entity<MembershipCard>()
                .HasOne(m => m.MembershipType)
                .WithMany()
                .HasForeignKey(m => m.MembershipTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MembershipCard>()
                .HasMany(m => m.Visits)
                .WithOne(v => v.MembershipCard)
                .HasForeignKey(v => v.MembershipCardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Налаштування для Visit
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.FitnessClub)
                .WithMany(f => f.Visits)
                .HasForeignKey(v => v.FitnessClubId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 