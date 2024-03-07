using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Plathub.Models;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Plathub.Data {
	public class ApplicationDbContext : IdentityDbContext {
		public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base( options ) {}
		public DbSet<UserGame> UserGames {  get; set; }
		public DbSet<Friendship> Friendships {  get; set; }
		public DbSet<Profile> Profiles {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserGame>()
                .HasKey(x => new { x.UserId, x.GameId });

            modelBuilder.Entity<Friendship>().HasKey(x => new { x.UserId1, x.UserId2 });
        }
    }
}
