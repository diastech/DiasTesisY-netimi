using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class Shift : BaseEntity
    {
       public string shiftName { get; set; }
       public DateTime shiftStartTime { get; set; }
       public DateTime shiftEndTime { get; set; }

    }

}
