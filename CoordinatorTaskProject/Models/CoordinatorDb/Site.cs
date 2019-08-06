using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Models.CoordinatorDb
{
    public class Site
    {
        public int Id { get; set; }
        public string Mnemonic { get; set; }
        public IEnumerable<Update> Updates { get; set; }
    }
}
