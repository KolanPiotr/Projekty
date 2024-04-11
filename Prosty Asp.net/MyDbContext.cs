using Microsoft.EntityFrameworkCore;
using Lab02.Models;
namespace Lab02
{
    public class MyDbContext : DbContext {
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<EventType> eventTypes { get; set; } = null!;
        public DbSet<League> Leagues { get; set; } = null!;
        public DbSet<Match> Matchs { get; set; } = null!;
        public DbSet<MatchEvent> MatchEvents { get; set; } = null!;
        public DbSet<MatchPlayer> MatchPlayers { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
