using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart
{
    public interface IUserBusinessRules
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.UserDto>>> GetUserListAsync();

        public Task<Tuple<ErrorCodes, DevelopmentDto.UserDto>> MakeUserActiveOrPassiveByUserIdAsync(int userId, int userStatusId);
        public Task<Tuple<ErrorCodes, DevelopmentDto.UserDto>> Login(string email,string password);
    }
}
