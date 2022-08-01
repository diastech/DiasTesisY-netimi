using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface ILocationV2BusinessRules
    {
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetAllLocationsAsync();
        public Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationByIdAsync(string hierarchyId);
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetLastNodeTicketLocationByIdAsync(string hierarchyId);
        public Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationByNfcCodeAsync(string nfcCode);
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetNodesTicketLocationByIdWithinLevelAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetNodesTicketLocationByIdWithinLevelMobileAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomLocationDto>> InsertLocationV2WithinParentHierarchyId(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomLocationDto>> UpdateV2Async(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomLocationDto>> DeleteV2Async(BusinessLogicRequest request);

    }
}
