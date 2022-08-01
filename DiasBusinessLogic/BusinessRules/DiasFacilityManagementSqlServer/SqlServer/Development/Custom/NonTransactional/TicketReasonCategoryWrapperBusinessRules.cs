using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasDataAccessLayer.Enums;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketReasonCategoryProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class TicketReasonCategoryWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketReasonCategoryWrapperBusinessRules, IBaseTicketReasonCategoryWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketReasonCategoryV2BusinessRules _ticketReasonCategoryBusinessRules;
        private readonly DevelopmentUserInterface.ITicketReasonBusinessRules _ticketReasonBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentTicketReasonCategoryProfile.CustomTicketReasonCategoryProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public TicketReasonCategoryWrapperBusinessRules() : this(
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketReasonCategoryV2BusinessRules>(), 
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketReasonBusinessRules>())
        {
        }
        private TicketReasonCategoryWrapperBusinessRules(
            DevelopmentUserInterface.ITicketReasonCategoryV2BusinessRules ticketReasonCategoryBusinessRules, 
            DevelopmentUserInterface.ITicketReasonBusinessRules ticketReasonBusinessRules
            )
        {
            _ticketReasonCategoryBusinessRules = ticketReasonCategoryBusinessRules;
            _ticketReasonBusinessRules = ticketReasonBusinessRules;
        }

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>> GetAllTicketReasonCategoriesWrapperAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, List<DevelopmentDto.TicketReasonCategoryV2Dto>> resultGetTicketReasonCategoryList = await _ticketReasonCategoryBusinessRules.GetAllTicketReasonCategoriesAsync();
                            try
                            {
                                if ((resultGetTicketReasonCategoryList.Item1.BusinessOperationSucceed == true) && (resultGetTicketReasonCategoryList.Item2 != null))
                                {
                                    List<CustomDto.CustomTicketReasonCategoryDto> reasonList = new List<CustomDto.CustomTicketReasonCategoryDto>();
                                    var convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentDto.TicketReasonCategoryV2Dto>, List<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategoryList.Item2);
                                    var lastNodes = convertedDto.Where(x => convertedDto.All(y => y.ParentHierarchy != x.HierarchyId));
                                    foreach (var item in lastNodes)
                                    {
                                        var reasonDtoList = await _ticketReasonBusinessRules.GetTicketReasonsByCategoryIdAsync(item.Id);
                                        foreach (var reason in reasonDtoList.Item2)
                                        {                                            
                                            
                                            CustomTicketReasonCategoryDto value = new();
                                            value.Id = reason.Id;
                                            value.CategoryName = reason.ReasonName + "(ÇS:" + (reason.ResolutionTime).ToString() +"dk"+ ")" + "(MS:" + (reason.ResponseTime).ToString() +"dk"+")";
                                            value.CategoryDescription = reason.ReasonDescription;
                                            value.HierarchyId = $"{item.HierarchyId}{reason.Id}/";
                                            value.IsReason = true;
                                            value.ResolutionTime = reason.ResolutionTime;
                                            value.ResponseTime = reason.ResponseTime;
                                            value.ResolutionTimeText = (reason.ResolutionTime).ToString();
                                            value.ResponseTimeText = (reason.ResponseTime).ToString();
                                            reasonList.Add(value);
                                        }
                                    }
                                    convertedDto.AddRange(reasonList);
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategoryList.Item1, convertedDto);
                                }
                                else
                                {
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategoryList.Item1, null);
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>> GetTicketReasonCategoryWrapperByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, DevelopmentDto.TicketReasonCategoryV2Dto> resultGetTicketReasonCategory = await _ticketReasonCategoryBusinessRules.GetTicketReasonCategoryByIdAsync(hierarchyId);
                            try
                            {
                                if ((resultGetTicketReasonCategory.Item1.BusinessOperationSucceed == true) && (resultGetTicketReasonCategory.Item2 != null))
                                {
                                    CustomDto.CustomTicketReasonCategoryDto returnDto = new CustomDto.CustomTicketReasonCategoryDto();
                                    CustomDto.CustomTicketReasonCategoryDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentDto.TicketReasonCategoryV2Dto, CustomDto.CustomTicketReasonCategoryDto>(resultGetTicketReasonCategory.Item2);
                                    returnDto = convertedDto;

                                    return new Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>(resultGetTicketReasonCategory.Item1, returnDto);
                                }
                                else
                                {
                                    return new Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>(resultGetTicketReasonCategory.Item1, null);
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
    }
}
