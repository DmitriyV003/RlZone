using Microsoft.EntityFrameworkCore;
using rl.zone.Domain.Intermediary;
using rl.zone.Domain.Models;

namespace rl.zone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER4;Database=RlZoneApi;Trusted_Connection=True;");
        }

        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<CaseCategory> CaseCategories { get; set; }
        public virtual DbSet<CasePrice> CasePrices { get; set; }
        public virtual DbSet<CircleColor> CircleColors { get; set; }
        public virtual DbSet<CraftAttempt> CraftAttempts { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<ItemPrice> ItemPrices { get; set; }
        public virtual DbSet<ItemRunner> ItemRunners { get; set; }
        public virtual DbSet<ItemSale> ItemSales { get; set; }
        public virtual DbSet<LineColor> LineColors { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<OpenCase> OpenCases { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<RequestForOutput> RequestsForOutput { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserLink> UserLinks { get; set; }
        public virtual DbSet<CaseItem> CaseItems { get; set; }
        public virtual DbSet<CraftAttemptItem> CraftAttempItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many to Many // User has 3 Links 
            modelBuilder.Entity<UserLink>()
                .HasKey(c => new { c.UserId, c.LinkId });
            modelBuilder.Entity<UserLink>()
                .HasOne<User>(c => c.User)
                .WithMany(c => c.UserLinks)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<UserLink>()
                .HasOne<Link>(c => c.Link)
                .WithMany(c => c.UserLinks)
                .HasForeignKey(c => c.LinkId);
            
            // Many to Many // Case has many Items
            modelBuilder.Entity<CaseItem>()
                .HasKey(c => new { c.ItemId, c.CaseId });
            modelBuilder.Entity<CaseItem>()
                .HasOne<Item>(c => c.Item)
                .WithMany(c => c.CaseItems)
                .HasForeignKey(c => c.ItemId);
            modelBuilder.Entity<CaseItem>()
                .HasOne<Case>(c => c.Case)
                .WithMany(c => c.CaseItems)
                .HasForeignKey(c => c.CaseId);
            
            // Many to Many // Item has many CraftAttempts
            modelBuilder.Entity<CraftAttemptItem>()
                .HasKey(c => new { c.ItemId, c.CraftAttemptId });
            modelBuilder.Entity<CraftAttemptItem>()
                .HasOne<Item>(c => c.Item)
                .WithMany(c => c.CraftAttempItems)
                .HasForeignKey(c => c.ItemId);
            modelBuilder.Entity<CraftAttemptItem>()
                .HasOne<CraftAttempt>(c => c.CraftAttempt)
                .WithMany(c => c.CraftAttempItems)
                .HasForeignKey(c => c.CraftAttemptId);
            
            // One to Many // Link has one Platform
            modelBuilder.Entity<Link>()
                .HasOne<Platform>(c => c.Platform)
                .WithMany(c => c.Links)
                .HasForeignKey(c => c.PlatformId);
            
            // One to Many // ItemPrice has one Platform
            modelBuilder.Entity<ItemPrice>()
                .HasOne<Platform>(c => c.Platform)
                .WithMany(c => c.ItemPrices)
                .HasForeignKey(c => c.PlatformId);
            
            // One to Many // CasePrice has one Platform
            modelBuilder.Entity<CasePrice>()
                .HasOne<Platform>(c => c.Platform)
                .WithMany(c => c.CasePrices)
                .HasForeignKey(c => c.PlatformId);
            
            // One to Many // Item has 3 ItemPrice (For every platform)
            modelBuilder.Entity<ItemPrice>()
                .HasOne<Item>(c => c.Item)
                .WithMany(c => c.ItemPrices)
                .HasForeignKey(c => c.ItemId);
            
            // One to Many // Case has 3 CasePrice (For every platform)
            modelBuilder.Entity<CasePrice>()
                .HasOne<Case>(c => c.Case)
                .WithMany(c => c.CasePrices)
                .HasForeignKey(c => c.CaseId);
            
            // One to Many // Case has 1 CaseCategory
            modelBuilder.Entity<Case>()
                .HasOne<CaseCategory>(c => c.Category)
                .WithMany(c => c.Cases)
                .HasForeignKey(c => c.CategoryId);
            
            // One to Many // Item has 1 ItemCategory
            modelBuilder.Entity<Item>()
                .HasOne<ItemCategory>(c => c.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(c => c.CategoryId);
            
            // One to Many // OpenCase has one Item
            modelBuilder.Entity<OpenCase>()
                .HasOne<Item>(c => c.Item)
                .WithMany(c => c.OpenCases)
                .HasForeignKey(c => c.ItemId);
            
            // One to Many // OpenCase has one Case
            modelBuilder.Entity<OpenCase>()
                .HasOne<Case>(c => c.Case)
                .WithMany(c => c.OpenCases)
                .HasForeignKey(c => c.CaseId);
            
            // One to Many // User has many ItemSales
            modelBuilder.Entity<ItemSale>()
                .HasOne<User>(c => c.User)
                .WithMany(c => c.ItemSales)
                .HasForeignKey(c => c.UserId);
            
            // One to Many // Item has many ItemSales
            modelBuilder.Entity<ItemSale>()
                .HasOne<Item>(c => c.Item)
                .WithMany(c => c.ItemSales)
                .HasForeignKey(c => c.ItemId);
            
            // One to Many // User has many OpenCases
            modelBuilder.Entity<OpenCase>()
                .HasOne<User>(c => c.User)
                .WithMany(c => c.OpenCases)
                .HasForeignKey(c => c.UserId);
            
            // One to Many // User has many CraftAttempts
            modelBuilder.Entity<CraftAttempt>()
                .HasOne<User>(c => c.User)
                .WithMany(c => c.CraftAttempts)
                .HasForeignKey(c => c.UserId);
            
            // One to Many // User has many Transactions
            modelBuilder.Entity<Transaction>()
                .HasOne<User>(c => c.User)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.UserId);
        }
    }
}