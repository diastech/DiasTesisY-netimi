using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;


namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IUserDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.UserDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.UserDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.UserDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.UserDto>> InsertAsync(DevelopmentDto.UserDto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.UserDto>> UpdateAsync(DevelopmentDto.UserDto updatedDto);
        public Task<Tuple<Error, DevelopmentDto.UserDto>> Login(string email, string password);
        public Task<Tuple<Error, DevelopmentDto.UserDto>> LoginV2(BusinessLogicRequest request);
        public Task<Tuple<Error, DevelopmentDto.UserDto>> MakeUserActiveOrPassiveByUserIdAsync(int userId, int userStatusId);
        public Task<Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>> GetAllCombinedUserAndAssigmentGroups();


    }
}
