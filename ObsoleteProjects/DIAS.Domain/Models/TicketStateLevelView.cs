using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketStateLevelView
    {
       public int id { get; set; }
       public int ticketStateSourceId { get; set; }
       public string ticketStateSource { get; set; }
       public int ticketStateDestinationId { get; set; }
       public string ticketStateDestination { get; set; }
       public string ticketStateDestinationDisplayName { get; set; }
    }
}
