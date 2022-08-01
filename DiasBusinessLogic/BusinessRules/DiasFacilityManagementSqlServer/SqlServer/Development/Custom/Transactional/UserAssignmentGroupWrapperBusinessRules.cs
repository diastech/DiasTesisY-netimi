using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using StandartDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasShared.Errors;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasBusinessLogic.Shared.Error;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Custom;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class UserAssignmentGroupWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.IUserAssignmentGroupWrapperBusinessRules, IBaseUserAssignmentGroupWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.IUserBusinessRules _userBusinessRules;

        private readonly DevelopmentUserInterface.IAssignmentGroupBusinessRules _assignmentGroupBusinessRules;

        private readonly DevelopmentUserInterface.IAssignmentGroupEmployeeBusinessRules _assignmentGroupEmployeeBusinessRules;


        private static AutoMapperProfileMapper<StandartDevelopmentTicketProfile.UserProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_User
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private static AutoMapperProfileMapper<StandartDevelopmentTicketProfile.AssignmentGroupProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_AssignmentGroup
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private static AutoMapperProfileMapper<StandartDevelopmentTicketProfile.AssignmentGroupEmployeeProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_AssignmentGroupEmployee
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));



        public UserAssignmentGroupWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IUserBusinessRules>(),
                                                                DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAssignmentGroupBusinessRules>(),
                                                                DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAssignmentGroupEmployeeBusinessRules>())
        {
        }
        private UserAssignmentGroupWrapperBusinessRules(DevelopmentUserInterface.IUserBusinessRules userBusinessRules,
                                                            DevelopmentUserInterface.IAssignmentGroupBusinessRules assignmentGroupBusinessRules,
                                                              DevelopmentUserInterface.IAssignmentGroupEmployeeBusinessRules assignmentGroupEmployeeBusinessRules)
        {
            _userBusinessRules = userBusinessRules;
            _assignmentGroupBusinessRules = assignmentGroupBusinessRules;
            _assignmentGroupEmployeeBusinessRules = assignmentGroupEmployeeBusinessRules;
        }

        public async Task<Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>> GetAllCombinedUserAndAssigmentGroups()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            Tuple<Error, IEnumerable<DevelopmentDto.UserDto>> resultGetUserList = await _userBusinessRules.GetUserListAsync();

                            try
                            {
                                if ((resultGetUserList.Item1.BusinessOperationSucceed == true) && (resultGetUserList.Item2 != null))
                                {
                                    Tuple<Error, IEnumerable<DevelopmentDto.AssignmentGroupDto>> resultGetAssignmentGroupList = await _assignmentGroupBusinessRules.GetAssignmentGroupListAsync();

                                    if ((resultGetAssignmentGroupList.Item1.BusinessOperationSucceed == true) && (resultGetAssignmentGroupList.Item2 != null))
                                    {
                                        Tuple<Error, IEnumerable<DevelopmentDto.AssignmentGroupEmployeeDto>> resultGetAssignmentGroupEmployeeList = await _assignmentGroupEmployeeBusinessRules.GetAllAssignmentGroupEmployeeAsync();

                                        if ((resultGetAssignmentGroupEmployeeList.Item1.BusinessOperationSucceed == true) && (resultGetAssignmentGroupEmployeeList.Item2 != null))
                                        {
                                            //Listeye önce grupları ekleyelim 
                                            List<CustomDto.CombinedUserAndAssignmentGroupDto> returnDtoList = new();

                                            foreach (DevelopmentDto.AssignmentGroupDto itemAssignmentGroup in resultGetAssignmentGroupList.Item2)
                                            {
                                                returnDtoList.Add(new()
                                                {
                                                    AssignmentGroupId = itemAssignmentGroup.Id,
                                                    Name = itemAssignmentGroup.GroupName,
                                                    UserId = null,
                                                    isUserOrGroup = false,
                                                    UserGroupName = null
                                                });
                                            }

                                            //Şimdi her bir kullanıcıya bakıp gruplarda ise kullanıcılar olarak ekleyelim
                                            //Eğer bir kullanıcı birden fazla gruba dahil ise yine aynı şekilde birden fazla eklenecektir
                                            foreach (DevelopmentDto.UserDto itemUser in resultGetUserList.Item2)
                                            {
                                                //Grup içinde ara
                                                List<DevelopmentDto.AssignmentGroupEmployeeDto> userGroups = resultGetAssignmentGroupEmployeeList.Item2.
                                                                                                                Where(p => p.EmployeeUserId == Convert.ToInt32(itemUser.Id)).ToList<DevelopmentDto.AssignmentGroupEmployeeDto>();
                                                //Kullanıcıları gruplara göre tek tek  listeye ekle
                                                if ((userGroups != null) && (userGroups.Count > 0))
                                                {
                                                    foreach (DevelopmentDto.AssignmentGroupEmployeeDto itemAssignmentGroupEmployee in userGroups)
                                                    {
                                                        returnDtoList.Add(new()
                                                        {
                                                            AssignmentGroupId = itemAssignmentGroupEmployee.AssignmentGroupId,
                                                            Name = itemUser.FirstName + ((itemUser.LastName != null) ? (" " + itemUser.LastName) : ""),
                                                            UserId = Convert.ToInt32(itemUser.Id),
                                                            isUserOrGroup = true,
                                                            UserGroupName = resultGetAssignmentGroupList.Item2.SingleOrDefault(p => p.Id == itemAssignmentGroupEmployee.AssignmentGroupId).GroupName
                                                        });
                                                    }
                                                }
                                                else
                                                {
                                                    //Listeye yalın kullanıcı ekle
                                                    returnDtoList.Add(new()
                                                    {
                                                        AssignmentGroupId = null,
                                                        Name = itemUser.FirstName + ((itemUser.LastName != null) ? (" " + itemUser.LastName) : ""),
                                                        UserId = Convert.ToInt32(itemUser.Id),
                                                        isUserOrGroup = true,
                                                        UserGroupName = null
                                                    });
                                                }
                                            }
                                            return new Tuple<Error, IEnumerable<CustomDto.CombinedUserAndAssignmentGroupDto>>(Errors.General.GetListSuccess("CombinedUserAndAssignmentGroupDto"), returnDtoList);
                                        }
                                        else
                                        {
                                            return new Tuple<Error, IEnumerable<CustomDto.CombinedUserAndAssignmentGroupDto>>(Errors.General.GeneralServerError(), null);
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                            break;
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CombinedUserAndAssignmentGroupDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }

            return new Tuple<Error, IEnumerable<CustomDto.CombinedUserAndAssignmentGroupDto>>(Errors.General.GeneralServerError(), null);
        }

    }
}
