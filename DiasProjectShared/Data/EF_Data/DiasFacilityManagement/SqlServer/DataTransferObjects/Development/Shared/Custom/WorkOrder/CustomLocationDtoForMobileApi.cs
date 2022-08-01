using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom.WorkOrder
{
    public class CustomLocationDtoForMobileApi:IBaseDevelopmentCustomDto
    {
        public int Id { get; set; }
        public string HierarchyId { get; set; }
        public string ParentHierarchy { get; set; }
        public string LocationNumber { get; set; }
        public string LocationName { get; set; }
        public string LocationWithName { get; set; }
        public string LocationOriginalName { get; set; }
        public string LocationDescription { get; set; }
    }
}
