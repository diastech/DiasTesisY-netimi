using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface IUserBusinessRules
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.UserDto>>> GetUserListAsync();
        public Task<Tuple<Error, DevelopmentDto.UserDto>> MakeUserActiveOrPassiveByUserIdAsync(int userId, int userStatusId);
        public Task<Tuple<Error, DevelopmentDto.UserDto>> Login(string email,string password);
        public Task<Tuple<Error, DevelopmentDto.UserDto>> LoginV2(DevelopmentCustomDto.UserCredentialsDto request);
    }
}
