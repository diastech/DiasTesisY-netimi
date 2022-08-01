using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketRelatedLocationView
    {
        public int id { get; set; }
        public int ticketId { get; set; }
        public string locationName { get; set; }
        public string locationNumber { get; set; }
        public string locationDescription { get; set; }
        public string locationLangLong { get; set; }
        public string locationHierarchy { get; set; }
        public string locationParentId { get; set; }
    }
}
