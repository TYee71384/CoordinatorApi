using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Models.AmsTables
{
    public class AmsTaskText
    {
        
        public int TextID { get; set; }
        public string Description { get; set; }
        public int TaskID { get; set; }
      
        public virtual AmsTaskMain AmsTaskMain { get; set; }
    }
}
