using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketNotesView
    {
        public int id { get; set; }
        public string notes { get; set; }
        public string notesAddedUser { get; set; }
        public DateTime addedTime { get; set; }
        public int ticketId { get; set; }
        public string ticketNoteAttachments { get; set; }
    }
}
