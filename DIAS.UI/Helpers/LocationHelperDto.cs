using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIAS.UI.Helpers
{
    public class LocationHelperDto
    {
        public int Id { get; set; }
        public int LocationCodeId { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public int WingId { get; set; }
        public FacilityDto LocationCodeDto { get; set; }
        public BuildingDto Building { get; set; }
        public FloorDto Floor { get; set; }
        public WingDto Wing { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int AddedByUserId { get; set; }
    }
}
