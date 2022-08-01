using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketState : BaseEntity
    {
        public string ticketStateDescription { get; set; }
    }
}
