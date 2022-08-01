using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom
{
    public class CustomSubLocationRequestDto
    {
        public string HierarchyId { get; set; }

        public int? Level { get; set; }
    }
}
