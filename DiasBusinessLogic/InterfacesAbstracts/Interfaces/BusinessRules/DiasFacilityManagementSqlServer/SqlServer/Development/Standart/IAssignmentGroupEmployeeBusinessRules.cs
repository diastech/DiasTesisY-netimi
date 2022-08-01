using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface IAssignmentGroupEmployeeBusinessRules
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.AssignmentGroupEmployeeDto>>> GetAllAssignmentGroupEmployeeAsync();
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.AssignmentGroupEmployeeDto>>> GetById(int kullaniciId);
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.AssignmentGroupEmployeeDto>>> GetUsersByGroupId(int groupId);
    }
}
