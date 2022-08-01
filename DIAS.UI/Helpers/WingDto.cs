using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIAS.UI.Helpers
{
    public class WingDto
    {
        public int Id { get; set; }
        public int LocationCodeId { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public FacilityDto LocationCode { get; set; }
        public BuildingDto Building { get; set; }
        public FloorDto Floor { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int AddedByUserId { get; set; }
    }
}
