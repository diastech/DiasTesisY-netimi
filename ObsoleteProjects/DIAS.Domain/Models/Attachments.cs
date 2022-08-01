using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class Attachments : BaseEntity
    {
        public int? ticketId { get; set; }
        public string description { get; set; }
        public string folder { get; set; }
        public int? ticketNotesId { get; set; }
        public int? basicTicketId { get; set; }
    }
}
