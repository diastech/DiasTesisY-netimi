using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketNotes : BaseEntity
    {
        public string notes { get; set; }
        public int notesAddedUser { get; set; }
        public DateTime addedTime { get; set; }
        public int ticketId { get; set; }
        public int? attachmentId { get; set; }
    }
}
