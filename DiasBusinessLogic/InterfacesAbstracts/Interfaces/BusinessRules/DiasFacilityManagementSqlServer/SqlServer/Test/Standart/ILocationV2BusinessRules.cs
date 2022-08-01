using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart
{
    public interface ILocationV2BusinessRules
    {
        public Task<Tuple<ErrorCodes, IEnumerable<CustomDto.CustomLocationDto>>> GetAllLocationsAsync();
        public Task<Tuple<ErrorCodes, StandartDto.LocationV2Dto>> GetLocationByIdAsync(string hierarchyId);
    }
}
