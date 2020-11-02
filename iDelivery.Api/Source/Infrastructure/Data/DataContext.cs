using Microsoft.EntityFrameworkCore;

namespace iDelivery.Api.Entities
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AllowedLocation> AllowedLocations { get; set; }
        public virtual DbSet<AppRating> AppRatings { get; set; }
        public virtual DbSet<BlockedCustomer> BlockedCustomers { get; set; }
        public virtual DbSet<BlockedRider> BlockedRiders { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingHistory> BookingHistories { get; set; }
        public virtual DbSet<BookingStatus> BookingStatus { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerBookingHistory> CustomerBookingHistories { get; set; }
        public virtual DbSet<CustomerFare> CustomerFares { get; set; }
        public virtual DbSet<CustomerRating> CustomerRatings { get; set; }
        public virtual DbSet<CustomerStatus> CustomerStatus { get; set; }
        public virtual DbSet<EstimatedWeight> EstimatedWeights { get; set; }
        public virtual DbSet<ExternalAccount> ExternalAccounts { get; set; }
        public virtual DbSet<Faq> Faqs { get; set; }
        public virtual DbSet<Fare> Fares { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<MenuAccess> MenuAccesses { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Otpregistration> Otpregistrations { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<ReportCustomer> ReportCustomers { get; set; }
        public virtual DbSet<ReportRider> ReportRiders { get; set; }
        public virtual DbSet<Rider> Riders { get; set; }
        public virtual DbSet<RiderBookingHistory> RiderBookingHistories { get; set; }
        public virtual DbSet<RiderRating> RiderRatings { get; set; }
        public virtual DbSet<RiderStatus> RiderStatus { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SurchargeSchedule> SurchargeSchedules { get; set; }
        public virtual DbSet<TermsAndCondition> TermsAndConditions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserInRole> UserInRoles { get; set; }
        public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<WalletLog> WalletLogs { get; set; }
        public virtual DbSet<WalletStatus> WalletStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=iDelivery;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllowedLocation>(entity =>
            {
                entity.ToTable("AllowedLocation");

                entity.Property(e => e.Location).IsRequired();
            });

            modelBuilder.Entity<AppRating>(entity =>
            {
                entity.ToTable("AppRating");

                entity.Property(e => e.Feedback).HasColumnType("ntext");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.AppRatings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppRating_Customer");
            });

            modelBuilder.Entity<BlockedCustomer>(entity =>
            {
                entity.ToTable("BlockedCustomer");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BlockedCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockedCustomer_Customer");
            });

            modelBuilder.Entity<BlockedRider>(entity =>
            {
                entity.ToTable("BlockedRider");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.Rider)
                    .WithMany(p => p.BlockedRiders)
                    .HasForeignKey(d => d.RiderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockedRider_Rider");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DropOffLocation).IsRequired();

                entity.Property(e => e.DropOffTime).HasColumnType("datetime");

                entity.Property(e => e.EstimatedTime).HasMaxLength(10);

                entity.Property(e => e.Items)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Notes).HasColumnType("ntext");

                entity.Property(e => e.PhotoUrl).HasMaxLength(256);

                entity.Property(e => e.PickupLocation).IsRequired();

                entity.Property(e => e.PickupTime).HasColumnType("datetime");

                entity.Property(e => e.ReferenceNumber).IsRequired();

                entity.Property(e => e.TotalFare).HasColumnType("money");

                entity.Property(e => e.TotalKilometers).HasMaxLength(10);

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.BookingStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_BookingStatus");

                entity.HasOne(d => d.Fare)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.FareId)
                    .HasConstraintName("FK_Booking_Fare");
            });

            modelBuilder.Entity<BookingHistory>(entity =>
            {
                entity.ToTable("BookingHistory");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.BookingHistories)
                    .HasForeignKey(d => d.BookingStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingHistory_BookingStatus");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BookingHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingHistory_Customer");
            });

            modelBuilder.Entity<BookingStatus>(entity =>
            {
                entity.HasKey(e => e.BookingStatusId);

                entity.Property(e => e.BookingStatusName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StatusColor).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhotoUrl)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.CustomerStatus)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustomerStatusId)
                    .HasConstraintName("FK_Customer_CustomerStatus");

                entity.HasOne(d => d.Fare)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.FareId)
                    .HasConstraintName("FK_Customer_Fare");

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.RatingId)
                    .HasConstraintName("FK_Customer_CustomerRating");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Customer_User");
            });

            modelBuilder.Entity<CustomerBookingHistory>(entity =>
            {
                entity.ToTable("CustomerBookingHistory");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.EstimatedTime)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ItemDetails)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Receipt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReceiverCompleteAddress).IsRequired();

                entity.Property(e => e.ReceiverCompleteName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalFare).HasColumnType("money");

                entity.Property(e => e.TotalKilometers)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.CustomerBookingHistories)
                    .HasForeignKey(d => d.BookingStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerBookingHistory_BookingStatus");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerBookingHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CustomerBookingHistory_Customer");
            });

            modelBuilder.Entity<CustomerFare>(entity =>
            {
                entity.ToTable("CustomerFare");
            });

            modelBuilder.Entity<CustomerRating>(entity =>
            {
                entity.ToTable("CustomerRating");
            });

            modelBuilder.Entity<CustomerStatus>(entity =>
            {
                entity.HasKey(e => e.CustomerStatusId);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<EstimatedWeight>(entity =>
            {
                entity.ToTable("EstimatedWeight");

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<ExternalAccount>(entity =>
            {
                entity.ToTable("ExternalAccount");

                entity.Property(e => e.AccountId).IsRequired();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("FAQ");

                entity.Property(e => e.Faqid).HasColumnName("FAQId");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Faqcontent)
                    .IsRequired()
                    .HasColumnName("FAQContent")
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Fare>(entity =>
            {
                entity.ToTable("Fare");

                entity.Property(e => e.AllowedBalance).HasColumnType("money");

                entity.Property(e => e.BaseFare).HasColumnType("money");

                entity.Property(e => e.CompanyPercentage)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PricePerKilometer).HasColumnType("money");

                entity.Property(e => e.RidersPercentage)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Surcharge).HasColumnType("money");

                entity.Property(e => e.TotalBaseKilometers)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Feedback_User");
            });

            modelBuilder.Entity<MenuAccess>(entity =>
            {
                entity.ToTable("MenuAccess");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.MenuAccesses)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuAccess_Role");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("MenuItem");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Otpregistration>(entity =>
            {
                entity.ToTable("OTPRegistration");

                entity.Property(e => e.OtpregistrationId).HasColumnName("OTPRegistrationId");

                entity.Property(e => e.DateRegistered).HasColumnType("datetime");

                entity.Property(e => e.Otpcode)
                    .IsRequired()
                    .HasColumnName("OTPCode")
                    .HasMaxLength(10);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate");

                entity.Property(e => e.Fare).HasColumnType("money");

                entity.Property(e => e.Kilometer)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");
            });

            modelBuilder.Entity<ReportCustomer>(entity =>
            {
                entity.ToTable("ReportCustomer");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ReportCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCustomer_Customer");

                entity.HasOne(d => d.Rider)
                    .WithMany(p => p.ReportCustomers)
                    .HasForeignKey(d => d.RiderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCustomer_Rider");
            });

            modelBuilder.Entity<ReportRider>(entity =>
            {
                entity.ToTable("ReportRider");

                entity.Property(e => e.ReportRiderId).ValueGeneratedNever();

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Rider>(entity =>
            {
                entity.ToTable("Rider");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhotoUrl)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.Riders)
                    .HasForeignKey(d => d.RatingId)
                    .HasConstraintName("FK_Rider_RiderRating");

                entity.HasOne(d => d.RiderStatus)
                    .WithMany(p => p.Riders)
                    .HasForeignKey(d => d.RiderStatusId)
                    .HasConstraintName("FK_Rider_RiderStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Riders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Rider_User");
            });

            modelBuilder.Entity<RiderBookingHistory>(entity =>
            {
                entity.ToTable("RiderBookingHistory");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DropOffLocation).IsRequired();

                entity.Property(e => e.ItemDetails)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.PickupLocation).IsRequired();

                entity.Property(e => e.ReceiverName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReceiverNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RiderDeduction).HasColumnType("money");

                entity.Property(e => e.RiderFare).HasColumnType("money");

                entity.Property(e => e.RiderShares)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.TotalFare).HasColumnType("money");

                entity.Property(e => e.TotalKilometers)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.RiderBookingHistories)
                    .HasForeignKey(d => d.BookingStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RiderBookingHistory_BookingStatus");
            });

            modelBuilder.Entity<RiderRating>(entity =>
            {
                entity.ToTable("RiderRating");
            });

            modelBuilder.Entity<RiderStatus>(entity =>
            {
                entity.HasKey(e => e.RiderStatusId);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName).IsRequired();
            });

            modelBuilder.Entity<SurchargeSchedule>(entity =>
            {
                entity.ToTable("SurchargeSchedule");
            });

            modelBuilder.Entity<TermsAndCondition>(entity =>
            {
                entity.HasKey(e => e.TermsAndConditionsId);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });

            modelBuilder.Entity<UserInRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("UserInRole");

                entity.HasIndex(e => new { e.RoleId, e.UserId })
                    .HasName("AK_UserInRole_RoleId_UserId")
                    .IsUnique();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserInRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<VehicleDetail>(entity =>
            {
                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Orcr)
                    .IsRequired()
                    .HasColumnName("ORCR")
                    .HasMaxLength(50);

                entity.Property(e => e.PlateNumber)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Rider)
                    .WithMany(p => p.VehicleDetails)
                    .HasForeignKey(d => d.RiderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleDetails_User");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.Property(e => e.CurrentPoints).HasColumnType("money");

                entity.Property(e => e.NegativeBalance).HasColumnType("money");

                entity.Property(e => e.PointsLoaded).HasColumnType("money");

                entity.HasOne(d => d.Rider)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.RiderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wallet_Rider");

                entity.HasOne(d => d.WalletStatus)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.WalletStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wallet_WalletStatus");
            });

            modelBuilder.Entity<WalletLog>(entity =>
            {
                entity.ToTable("WalletLog");

                entity.Property(e => e.CurrentPoints).HasColumnType("money");

                entity.Property(e => e.CurrentStatus)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.Points).HasColumnType("money");
            });

            modelBuilder.Entity<WalletStatus>(entity =>
            {
                entity.HasKey(e => e.WalletStatusId);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
