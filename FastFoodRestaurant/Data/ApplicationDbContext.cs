using FastFoodRestaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodRestaurant.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Bill> Bills { get; set; }  
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<ComboDetail> ComboDetails { get; set; }
        public DbSet<ComboInformation> ComboInformations { get; set; }
        public DbSet<VoucherDetail> VoucherDetails { get; set; }

        public DbSet<CartItem> CartItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillDetail>()
                .HasKey(sc => new { sc.BillId, sc.MenuId }); 

            modelBuilder.Entity<BillDetail>()
                .HasOne(sc => sc.Bill) 
                .WithMany(s => s.BillDetails) 
                .HasForeignKey(sc => sc.BillId);

            modelBuilder.Entity<BillDetail>()
                .HasOne(sc => sc.Menu)
                .WithMany(c => c.BillDetails)
                .HasForeignKey(sc => sc.MenuId);

            modelBuilder.Entity<ComboDetail>()
                .HasKey(sc => new { sc.ComboId, sc.MenuId });

            modelBuilder.Entity<ComboDetail>()
                .HasOne(sc => sc.Combo)
                .WithMany(q => q.ComboDetails)
                .HasForeignKey(sc => sc.ComboId);

            modelBuilder.Entity<ComboDetail>()
                .HasOne(sc => sc.Menu)
                .WithMany(c => c.ComboDetails)
                .HasForeignKey(sc => sc.MenuId);

            modelBuilder.Entity<ComboInformation>()
                .HasKey(sc => new { sc.BillId, sc.MenuId, sc.ComboId });

            modelBuilder.Entity<ComboInformation>()
                .HasOne(sc => sc.Bill)
                .WithMany(s => s.ComboInformations)
                .HasForeignKey(sc => sc.BillId);

            modelBuilder.Entity<ComboInformation>()
                .HasOne(sc => sc.Menu)
                .WithMany(c => c.ComboInformations)
                .HasForeignKey(sc => sc.MenuId);

            modelBuilder.Entity<ComboInformation>()
                .HasOne(sc => sc.Combo)
                .WithMany(q => q.ComboInformations)
                .HasForeignKey(sc => sc.ComboId);

            modelBuilder.Entity<VoucherDetail>()
                .HasKey(sc => new { sc.VoucherId, sc.ClientId });

            modelBuilder.Entity<VoucherDetail>()
                .HasOne(sc => sc.Voucher)
                .WithMany(q => q.VoucherDetails)
                .HasForeignKey(sc => sc.VoucherId);

            modelBuilder.Entity<VoucherDetail>()
                .HasOne(sc => sc.Client)
                .WithMany(q => q.VoucherDetails)
                .HasForeignKey(sc => sc.ClientId);
        }
    }
}
