using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.DTO
{
    public class TaskData
    {
        public int TaskID { get; set; }
        public string StatusID { get; set; }
        public string ModuleID { get; set; }
        public string AssignedTo { get; set; }
        public string PriorityID { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string SiteMnemonic { get; set; }
        public string SiteName { get; set; }

    }
}
