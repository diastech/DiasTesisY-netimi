using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
{
    public interface ILocationWrapperBusinessRules
    {
        public Task<Tuple<ErrorCodes, IEnumerable<CustomDto.CustomLocationDto>>> GetAllLocationsWrapperAsync();
        public Task<Tuple<ErrorCodes, CustomDto.CustomLocationDto>> GetLocationWrapperByIdAsync(string hierarchyId);
    }
}
