using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public interface ILocationWrapperBusinessRules
    {
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetAllLocationsWrapperAsync();
        public Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationWrapperByIdAsync(string hierarchyId);
        public Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationWrapperByNfcCodeAsync(string nfcCode);
    }
}
