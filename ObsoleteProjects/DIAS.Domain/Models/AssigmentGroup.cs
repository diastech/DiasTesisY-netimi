using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class AssigmentGroup : BaseEntity
    {
        public string asgGroupName { get; set; }
        public int asgGroupManagerId { get; set; }
        public int reasonId { get; set; }
        public int reasonCatId { get; set; }
    }
}
