using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class CustomTicketDto
    {
        public TicketView TicketViewObj { get; set; }
        public TicketNotes TicketNotesObj { get; set; }
    }
}
