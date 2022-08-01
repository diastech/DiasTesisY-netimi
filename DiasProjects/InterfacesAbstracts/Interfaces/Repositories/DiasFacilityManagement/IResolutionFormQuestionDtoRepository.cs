using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IResolutionFormQuestionDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.ResolutionFormQuestionDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionDto>> InsertAsync(DevelopmentDto.ResolutionFormQuestionDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionDto>> UpdateAsync(DevelopmentDto.ResolutionFormQuestionDto updatedDto);
    }
}
