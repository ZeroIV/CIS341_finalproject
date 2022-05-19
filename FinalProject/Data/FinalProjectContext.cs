using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Models
{
    public class FinalProjectContext : IdentityDbContext<UserAccount, ApplicationRole, int>
    {
        public FinalProjectContext (DbContextOptions<FinalProjectContext> options)
            : base(options)
        {
        }

        //public DbSet<ApplicationRole> UserRoles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageComment> ImageComments { get; set; }
        public DbSet<ImageVote> ImageVotes { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserVote> UserVotes { get; set; }
        public DbSet<Vote> Votes { get; set; }
        //public DbSet<VoteType> VoteTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAccount>(b =>
            {
                b.ToTable("UserAccounts");
                b.Property(u => u.UserName).HasMaxLength(128);
                b.Property(u => u.NormalizedUserName).HasMaxLength(128);
                b.Property(u => u.Email).HasMaxLength(128);
                b.Property(u => u.NormalizedEmail).HasMaxLength(128);


            });

        }
    }
}
