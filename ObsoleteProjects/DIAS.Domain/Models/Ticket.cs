using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class Ticket : BaseEntity
    {
        public int ticketInsertUser { get; set; }
        public string ticketOwner { get; set; }
        public string ticketDescription { get; set; }
        public DateTime ticketOpenedTime { get; set; }
        public int ticketStatus { get; set; }
        public int? periodicTicketId { get; set; }
        public int? responsibleUserId { get; set; }
        public int? responsibleAssignmentGroupId { get; set; }
        public int reasonId { get; set; }
        public int priority { get; set; }
        public int? locationId { get; set; }
        public int? basicTicketsId { get; set; }
        public string lastModifiedBy { get; set; }
        public DateTime? lastModified { get; set; }
    }
}
