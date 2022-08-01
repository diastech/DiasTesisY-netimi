using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIAS.UI.Helpers
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public int LocationCodeId { get; set; }
        public FacilityDto LocationCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int AddedByUserId { get; set; }
    }
}
