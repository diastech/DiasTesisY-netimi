using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class TicketForm : BaseEntity
    {
        public int reasonId { get; set; }
        public int? reasonCategoryId { get; set; }
        public string formDescription { get; set; }
        public int ticketStateId { get; set; }
        public bool? mandatory { get; set; }
        public int? formQuestionType { get; set; }
    }
}
