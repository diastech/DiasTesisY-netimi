using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class BasicTicket : BaseEntity
    {
       public string description { get; set; }
       public string folder { get; set; }
       public string cellNumber { get; set; }
       public int insertUser { get; set; }
       public DateTime insertDate { get; set; }

    }
}
