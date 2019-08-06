using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Models.CoordinatorDb
{
    public class CoordinatorContext: DbContext
    {

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<History> History { get; set; }
        public CoordinatorContext(DbContextOptions<CoordinatorContext> opt): base(opt)
        {

        }
    }
}
