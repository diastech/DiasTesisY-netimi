using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IResolutionFormQuestionAnswerDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.ResolutionFormQuestionAnswerDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionAnswerDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionAnswerDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionAnswerDto>> InsertAsync(DevelopmentDto.ResolutionFormQuestionAnswerDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionAnswerDto>> UpdateAsync(DevelopmentDto.ResolutionFormQuestionAnswerDto updatedDto);
    }
}
