using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketHistoryView
    {
        public int id { get; set; }
        public int ticketId { get; set; }
        public int ticketHistoryType { get; set; }
        public DateTime ticketHistoryInsertDate { get; set; }
        public int ticketHistoryInsertUserId { get; set; }
        public string historyDescription { get; set; }

    }
}
