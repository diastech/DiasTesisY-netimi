using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ILocationWrapperDtoRepository
    {
        #region Development
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetAllLocationsWrapperAsync();
        public Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationWrapperByIdAsync(string hierarchyId);
        public Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationWrapperByNfcCodeAsync(string nfcCode);
        #endregion

        #region Test
        public Task<Tuple<ErrorCodes, IEnumerable<CustomTestDto.CustomLocationDto>>> GetAllLocationsWrapperTestAsync();
        public Task<Tuple<ErrorCodes, CustomTestDto.CustomLocationDto>> GetLocationWrapperByIdTestAsync(string hierarchyId);
        #endregion

    }
}
