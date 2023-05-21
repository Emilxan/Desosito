using Desosito.DAL.Repositories;
using Desosito.Domain.Entity;
using Desosito.Domain.Entity.UserAction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostComment> PostComment { get; set; }

        //User Action

        public DbSet<LikePost> LikePost { get; set; }
        public DbSet<RepostPost> RepostPost { get; set; }
    }
}
