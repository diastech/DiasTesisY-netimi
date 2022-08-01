using DIAS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Domain.Models
{
    public class ReasonCategoryView
    {
        public int id { get; set; }
        public string reasonCategoryName { get; set; }
        public string reasonCategoryDescription { get; set; }
        public string reasonCategoryHierarchy { get; set; }
        public string reasonCategory { get; set; }
        public string reasonCategoryParentId { get; set; }
        public bool isDisabled { get; set; }
    }
}
