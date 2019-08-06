using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Models.AmsTables
{
    public class AmsAccountMain
    {
        public int AccountID { get; set; }
        public string BillingCodeID { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<AmsTaskMain> AmsTaskMain { get; set; }
    }
}
