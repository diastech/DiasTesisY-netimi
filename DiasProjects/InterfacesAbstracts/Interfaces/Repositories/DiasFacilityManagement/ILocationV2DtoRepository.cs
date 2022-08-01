using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ILocationV2DtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.LocationV2Dto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.LocationV2Dto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.LocationV2Dto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.LocationV2Dto>> InsertAsync(DevelopmentDto.LocationV2Dto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.LocationV2Dto>> UpdateAsync(DevelopmentDto.LocationV2Dto updatedDto);
        public Task<Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>> GetNodesTicketLocationByIdWithinLevelAsync(BusinessLogicRequest request);
    }
}
