using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIAS.UI.Helpers
{
    public class RoleHelperDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Reasons { get; set; }
        public List<int> Locations { get; set; }
        public bool ActiveStatus { get; set; }

    }
}
