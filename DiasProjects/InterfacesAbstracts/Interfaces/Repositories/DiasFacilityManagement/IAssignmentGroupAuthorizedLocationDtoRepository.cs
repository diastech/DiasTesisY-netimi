using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IAssignmentGroupAuthorizedLocationDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.AssignmentGroupAuthorizedLocationDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupAuthorizedLocationDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupAuthorizedLocationDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupAuthorizedLocationDto>> InsertAsync(DevelopmentDto.AssignmentGroupAuthorizedLocationDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupAuthorizedLocationDto>> UpdateAsync(DevelopmentDto.AssignmentGroupAuthorizedLocationDto updatedDto);
    }
}
