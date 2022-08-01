using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IResolutionFormQuestionTypeDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.ResolutionFormQuestionTypeDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionTypeDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionTypeDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionTypeDto>> InsertAsync(DevelopmentDto.ResolutionFormQuestionTypeDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormQuestionTypeDto>> UpdateAsync(DevelopmentDto.ResolutionFormQuestionTypeDto updatedDto);
    }
}
