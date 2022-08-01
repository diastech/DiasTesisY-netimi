using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class ReasonCategory : BaseEntity
    {
        public string reasonCategoryName { get; set; }
        public string reasonCategoryDescription { get; set; }
        public string reasonCategoryHierarchy { get; set; }
        public string reasonCategoryParentId { get; set; }
    }
}
