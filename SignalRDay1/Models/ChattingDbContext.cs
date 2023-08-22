using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SignalRDay1.Models {
    public class ChattingDbContext:IdentityDbContext {
        public ChattingDbContext(DbContextOptions<ChattingDbContext> options):base(options){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserConnectionId> userConnectionIds { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup>  UserGroups { get; set; }
        public DbSet<Message>  Messages { get; set; }
    }
}
