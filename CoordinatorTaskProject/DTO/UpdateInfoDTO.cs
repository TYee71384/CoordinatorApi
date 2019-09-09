using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.DTO
{
    public class UpdateInfoDTO
    {
        public int Id { get; set; }
        public string CurrentRelease { get; set; }
        public string NextUpdate { get; set; }
        public int UpdateNumber { get; set; }
        public string BillingCodeID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public IEnumerable<TaskData> Tasks { get; set; }
    }
}
