using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class BasicTicketView
    {
       public int id { get; set; }
       public string description { get; set; }
       public string cellNumber { get; set; }
       public string insertUser { get; set; }
       public string insertUserId { get; set; }
       public string ticketAttachments { get; set; }
       public string basicTicketState { get; set; }
       public DateTime insertDate { get; set; }
    }
}
