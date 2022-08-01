using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IResolutionFormDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.ResolutionFormDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormDto>> InsertAsync(DevelopmentDto.ResolutionFormDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormDto>> UpdateAsync(DevelopmentDto.ResolutionFormDto updatedDto);
    }
}
