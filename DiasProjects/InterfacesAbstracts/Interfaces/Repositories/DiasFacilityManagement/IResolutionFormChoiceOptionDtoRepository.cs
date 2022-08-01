using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IResolutionFormChoiceOptionDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.ResolutionFormChoiceOptionDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormChoiceOptionDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormChoiceOptionDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormChoiceOptionDto>> InsertAsync(DevelopmentDto.ResolutionFormChoiceOptionDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.ResolutionFormChoiceOptionDto>> UpdateAsync(DevelopmentDto.ResolutionFormChoiceOptionDto updatedDto);
    }
}
