using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketFormQView
    {
        public int id { get; set; }
        public int type { get; set; }
        public string question { get; set; }
        public int ticketFormId { get; set; }
        public bool? mandatory { get; set; }
        public string q1 { get; set; }
        public string q2 { get; set; }
        public string q3 { get; set; }
        public string q4 { get; set; }
        public string q5 { get; set; }
    }
}
