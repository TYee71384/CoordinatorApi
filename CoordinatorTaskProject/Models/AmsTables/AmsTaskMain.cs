using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Models.AmsTables
{
    public class AmsTaskMain
    {
        public int TaskID { get; set; }
        public string StatusID { get; set; }
        public string ModuleID { get; set; }
        public string AssignedTo { get; set; }
        public string PriorityID { get; set; }
        public int AccountID { get; set; }
        public virtual AmsAccountMain AmsAccountMain { get; set; }
        public virtual AmsTaskText AmsTaskText { get; set; }

    }
}
