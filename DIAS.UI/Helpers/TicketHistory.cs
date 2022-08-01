using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIAS.UI.Helpers
{
    public class TicketHistory
    {
        public int Id { get; set; }
        public DateTime DateWithTime { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string SurnaName { get; set; }
        public string UpdatedArea { get; set; }
        public string oldValue { get; set; }
        public  string newValue { get; set; }
    }
}
