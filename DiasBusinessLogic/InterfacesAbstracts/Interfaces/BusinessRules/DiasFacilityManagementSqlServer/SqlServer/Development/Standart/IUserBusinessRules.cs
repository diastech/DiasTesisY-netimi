using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface IUserBusinessRules
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.UserDto>>> GetUserListAsync();
        public Task<Tuple<Error, DevelopmentDto.UserDto>> MakeUserActiveOrPassiveByUserIdAsync(int userId, int userStatusId);
        public Task<Tuple<Error, DevelopmentDto.UserDto>> Login(string email,string password);
    }
}
