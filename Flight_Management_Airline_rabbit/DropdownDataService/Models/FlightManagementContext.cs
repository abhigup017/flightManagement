using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DropdownDataService.Models
{
    public partial class FlightManagementContext : DbContext
    {
        public FlightManagementContext()
        {
        }

        public FlightManagementContext(DbContextOptions<FlightManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airline> Airlines { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Bookingpassenger> Bookingpassengers { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Flightdaysschedule> Flightdaysschedules { get; set; }
        public virtual DbSet<Flightschedule> Flightschedules { get; set; }
        public virtual DbSet<Gendertype> Gendertypes { get; set; }
        public virtual DbSet<InstrumentType> InstrumentTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Mealplan> Mealplans { get; set; }
        public virtual DbSet<Roletype> Roletypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:flightmanagement.database.windows.net,1433;Initial Catalog=FlightManagement;Persist Security Info=False;User ID=dotnetappuser;Password=password@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Airline>(entity =>
            {
                entity.ToTable("AIRLINE");

                entity.Property(e => e.AirlineAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AirlineContact)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.AirlineDescription).HasMaxLength(500);

                entity.Property(e => e.AirlineLogo)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.AirlineName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.IsBlocked).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("BOOKING");

                entity.Property(e => e.BookedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerEmailId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.Pnrnumber)
                    .HasMaxLength(100)
                    .HasColumnName("PNRNumber");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TravelDate).HasColumnType("datetime");

                entity.HasOne(d => d.MealPlan)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.MealPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BOOKING__MealPla__73BA3083");
            });

            modelBuilder.Entity<Bookingpassenger>(entity =>
            {
                entity.HasKey(e => e.PassengerId)
                    .HasName("PK__BOOKINGP__88915FB05B0718F1");

                entity.ToTable("BOOKINGPASSENGERS");

                entity.Property(e => e.PassengerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SeatNo).HasMaxLength(20);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Bookingpassengers)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BOOKINGPA__Booki__74AE54BC");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("DISCOUNT");

                entity.Property(e => e.DiscountCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DiscountExpiryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Flightdaysschedule>(entity =>
            {
                entity.HasKey(e => e.FlightDayScheduleId)
                    .HasName("PK__FLIGHTDA__80AFD10CFD8D9E55");

                entity.ToTable("FLIGHTDAYSSCHEDULE");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.DestinationLocation)
                    .WithMany(p => p.FlightdaysscheduleDestinationLocations)
                    .HasForeignKey(d => d.DestinationLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FLIGHTDAY__Desti__75A278F5");

                entity.HasOne(d => d.SourceLocation)
                    .WithMany(p => p.FlightdaysscheduleSourceLocations)
                    .HasForeignKey(d => d.SourceLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FLIGHTDAY__Sourc__76969D2E");
            });

            modelBuilder.Entity<Flightschedule>(entity =>
            {
                entity.HasKey(e => e.FlightId)
                    .HasName("PK__FLIGHTSC__8A9E14EE2D1DB69F");

                entity.ToTable("FLIGHTSCHEDULE");

                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TicketCost).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.AirLine)
                    .WithMany(p => p.Flightschedules)
                    .HasForeignKey(d => d.AirLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FLIGHTSCH__AirLi__778AC167");

                entity.HasOne(d => d.FlightDaySchedule)
                    .WithMany(p => p.Flightschedules)
                    .HasForeignKey(d => d.FlightDayScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FLIGHTSCH__Fligh__787EE5A0");

                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.Flightschedules)
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FLIGHTSCH__Instr__797309D9");

                entity.HasOne(d => d.MealPlan)
                    .WithMany(p => p.Flightschedules)
                    .HasForeignKey(d => d.MealPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FLIGHTSCH__MealP__7A672E12");
            });

            modelBuilder.Entity<Gendertype>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                    .HasName("PK__GENDERTY__4E24E9F7BD83E7B9");

                entity.ToTable("GENDERTYPE");

                entity.Property(e => e.GenderValue)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<InstrumentType>(entity =>
            {
                entity.HasKey(e => e.InstrumentId)
                    .HasName("PK__Instrume__430A538608701CA5");

                entity.Property(e => e.InstrumentName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("LOCATIONS");

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Mealplan>(entity =>
            {
                entity.ToTable("MEALPLANS");

                entity.Property(e => e.MealPlanType)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Roletype>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__ROLETYPE__8AFACE1A35B6D3B8");

                entity.ToTable("ROLETYPE");

                entity.Property(e => e.RoleValue).HasMaxLength(20);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USERS__GenderId__7B5B524B");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USERS__RoleId__7C4F7684");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
