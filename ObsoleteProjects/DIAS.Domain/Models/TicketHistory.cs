using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketHistory : BaseEntity
    {
        public int ticketId { get; set; }
        public int ticketHistoryType { get; set; }
        public DateTime ticketHistoryInsertDate { get; set; }
        public int ticketHistoryInsertUserId { get; set; }
        public int? previousStatusId { get; set; }
        public int? nextStatusId { get; set; }
        public DateTime? activityStartTime { get; set; }
        public DateTime? activityEndTime { get; set; }
        public int? locationId { get; set; }
        public int? previousAssignmentUserId { get; set; }
        public int? nextAssignmentUserId { get; set; }
        public int? previousAssignmentGroupId { get; set;}
        public int? nextAssignmentGroupId { get; set; }
    }
}
