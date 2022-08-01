using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IAssignmentGroupEmployeeDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.AssignmentGroupEmployeeDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupEmployeeDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupEmployeeDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupEmployeeDto>> InsertAsync(DevelopmentDto.AssignmentGroupEmployeeDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AssignmentGroupEmployeeDto>> UpdateAsync(DevelopmentDto.AssignmentGroupEmployeeDto updatedDto);
    }
}
