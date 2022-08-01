using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketStateLevel : BaseEntity
    {
       public int ticketStateSource { get; set; }
       public int ticketStateDestination { get; set; }
    }
}
