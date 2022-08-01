using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketView
    {
        public int id { get; set; }
        public int reasonId { get; set; }
        public string reasonName { get; set; }
        public string reasonCategory { get; set; }
        public string ticketLocations { get; set; }
        public string locationHierarchy { get; set; }
        public string locationName { get; set; }
        public int ticketStatus { get; set; }
        public string ticketStateDescription { get; set; }
        public int priority { get; set; }
        public string ticketDescription { get; set; }
        public DateTime ticketOpenedTime { get; set; }
        public int responseTime { get; set; }
        public int resolutionTime { get; set; }
        public DateTime expectedResponseTime { get; set; }
        public DateTime expectedResolutionTime { get; set; }
        public string ticketOwner { get; set; }
        public string ticketOwnerUser { get; set; }
        public string responsibleUserId { get; set; }
        public string responsibleUser { get; set; }
        public int? responsibleAssignmentGroupId { get; set; }
        public string asgGroupName { get; set; }
        public int? basicTicketsId { get; set; }
        public int? periodicTicketId { get; set; }
    }
}
