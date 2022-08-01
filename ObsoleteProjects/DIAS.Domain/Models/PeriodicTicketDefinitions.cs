using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class PeriodicTicketDefinitions : BaseEntity
    {
       public string ticketPeriodName { get; set; }
       public DateTime startDateTime { get; set; }
       public DateTime endDateTime { get; set; }
       public int priority { get; set; }
       public int locationId { get; set; }
       public string frequency { get; set; }
       public int reasonId { get; set; }


    }
}
