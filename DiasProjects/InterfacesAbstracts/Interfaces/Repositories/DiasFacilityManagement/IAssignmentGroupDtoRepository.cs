using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IAssignmentGroupDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.AssignmentGroupDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupDto>> InsertAsync(DevelopmentDto.AssignmentGroupDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupDto>> UpdateAsync(DevelopmentDto.AssignmentGroupDto updatedDto);
    }
}
