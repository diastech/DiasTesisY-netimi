using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketRelatedLocation : BaseEntity
    {
        public int ticketId { get; set; }
        public int ticketLocationId { get; set; }
        [NotMapped]
        public virtual List<string> ticketRelatedLocations { get; set; }
    }
}
