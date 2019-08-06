using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Models.CoordinatorDb
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Comment { get; set; }
        public int UpdateId { get; set; }
        public virtual Update Update { get; set; }
    }
}
