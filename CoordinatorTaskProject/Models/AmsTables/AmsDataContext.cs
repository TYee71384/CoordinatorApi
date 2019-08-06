using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Models.AmsTables
{
    public class AmsDataContext : DbContext
    {
        public AmsDataContext(DbContextOptions<AmsDataContext> options) : base(options)
        {
            
        }

        public DbSet<AmsTaskMain> AmsTaskMain { get; set; }
        public DbSet<AmsAccountMain> amsAccountMain { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<AmsTaskMain>(ent => {
                ent.HasKey(t => t.TaskID);
                ent.HasOne(x => x.AmsTaskText).WithOne(x => x.AmsTaskMain);
                ent.HasOne(x => x.AmsAccountMain).WithMany(x => x.AmsTaskMain).HasForeignKey(x => x.AccountID);
            });
            mb.Entity<AmsTaskText>(ent => { ent.HasKey(t => t.TaskID);
                ent.HasOne(x => x.AmsTaskMain).WithOne(x => x.AmsTaskText).HasForeignKey<AmsTaskMain>(x => x.TaskID);
            });

            mb.Entity<AmsAccountMain>(ent => { ent.HasKey(t => t.AccountID); });
            
            
        }
    }
}
