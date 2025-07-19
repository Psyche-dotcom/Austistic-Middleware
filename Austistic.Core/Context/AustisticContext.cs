
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
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomMessages> RoomMessages { get; set; }
        public DbSet<ReadMassageCount> ReadMassageCount { get; set; }
        
        public AustisticContext(DbContextOptions options) : base(options) { }
     

    }
}
