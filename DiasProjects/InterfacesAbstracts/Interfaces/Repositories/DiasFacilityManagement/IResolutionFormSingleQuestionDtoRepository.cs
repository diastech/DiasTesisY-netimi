using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IResolutionFormSingleQuestionDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.ResolutionFormSingleQuestionDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormSingleQuestionDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormSingleQuestionDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormSingleQuestionDto>> InsertAsync(DevelopmentDto.ResolutionFormSingleQuestionDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormSingleQuestionDto>> UpdateAsync(DevelopmentDto.ResolutionFormSingleQuestionDto updatedDto);
    }
}
