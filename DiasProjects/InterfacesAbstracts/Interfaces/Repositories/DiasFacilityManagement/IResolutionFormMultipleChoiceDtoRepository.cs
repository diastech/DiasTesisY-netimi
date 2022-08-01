using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IResolutionFormMultipleChoiceDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.ResolutionFormMultipleChoiceDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormMultipleChoiceDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormMultipleChoiceDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormMultipleChoiceDto>> InsertAsync(DevelopmentDto.ResolutionFormMultipleChoiceDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormMultipleChoiceDto>> UpdateAsync(DevelopmentDto.ResolutionFormMultipleChoiceDto updatedDto);
    }
}
