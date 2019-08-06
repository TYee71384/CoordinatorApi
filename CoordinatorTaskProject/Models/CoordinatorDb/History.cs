using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Models.CoordinatorDb
{
    public class History
    {
        public int Id { get; set; }
        public string FileBy { get; set; }
        public DateTime FileTime { get; set; }

        public int TaskId { get; set; }
    }
}
