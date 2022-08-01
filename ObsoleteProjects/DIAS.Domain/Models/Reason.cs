using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class Reason : BaseEntity
    {
        public string reasonName { get; set; }
        public string reasonDescription { get; set; }
        public int responseTime { get; set; }
        public int resolutionTime { get; set; }
        public int reasonCategoryId { get; set; }
        public int reasonCode { get; set; }
    }

}
