
using Austistic.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlpaStock.Core.Context
{
    public class AustisticContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ConfirmEmailToken> ConfirmEmailTokens { get; set; }
        public DbSet<ForgetPasswordToken> ForgetPasswordTokens { get; set; }
        public DbSet<SymbolImage> SymbolImage { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<CategorySymbol> CategorySymbol { get; set; }
        public DbSet<SupportTicket> SupportTicket { get; set; }
        public DbSet<SupportMessage> SupportMessage { get; set; }
        
        public AustisticContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Friend>()
                .HasKey(f => new { f.UserId, f.FriendId });

            builder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friend>()
                .HasOne(f => f.FriendUser)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friend>()
                .Property(f => f.Status)
                .HasDefaultValue(FriendStatus.Pending);
        }

    }
}
