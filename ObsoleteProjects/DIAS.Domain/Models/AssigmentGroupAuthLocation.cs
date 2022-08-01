using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class AssigmentGroupAuthLocation : BaseEntity
    {
        public int asgId { get; set; }
        public int locationId { get; set; }
    }
}
