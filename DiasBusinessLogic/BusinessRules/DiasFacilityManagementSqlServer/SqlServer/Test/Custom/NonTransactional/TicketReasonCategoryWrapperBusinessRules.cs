using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasDataAccessLayer.Enums;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketReasonCategoryProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
{
    public class TicketReasonCategoryWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketReasonCategoryWrapperBusinessRules, IBaseTicketReasonCategoryWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketReasonCategoryV2BusinessRules _ticketReasonCategoryBusinessRules;
        private readonly DevelopmentUserInterface.ITicketReasonBusinessRules _ticketReasonBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentTicketReasonCategoryProfile.CustomTicketReasonCategoryProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

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

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>> GetAllTicketReasonCategoriesWrapperAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");
            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<ErrorCodes, List<DevelopmentDto.TicketReasonCategoryV2Dto>> resultGetTicketReasonCategoryList = await _ticketReasonCategoryBusinessRules.GetAllTicketReasonCategoriesAsync();
                            try
                            {
                                if ((resultGetTicketReasonCategoryList.Item1 == ErrorCodes.None) && (resultGetTicketReasonCategoryList.Item2 != null))
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
                                            value.CategoryName = reason.ReasonName;
                                            value.CategoryDescription = reason.ReasonDescription;
                                            value.HierarchyId = $"{item.HierarchyId}{reason.Id}/";
                                            value.IsReason = true;
                                            reasonList.Add(value);
                                        }
                                    }
                                    convertedDto.AddRange(reasonList);
                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(ErrorCodes.None, convertedDto);
                                }
                                else
                                {
                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategoryList.Item1, null);
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, CustomDto.CustomTicketReasonCategoryDto>> GetTicketReasonCategoryWrapperByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");
            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<ErrorCodes, DevelopmentDto.TicketReasonCategoryV2Dto> resultGetTicketReasonCategory = await _ticketReasonCategoryBusinessRules.GetTicketReasonCategoryByIdAsync(hierarchyId);
                            try
                            {
                                if ((resultGetTicketReasonCategory.Item1 == ErrorCodes.None) && (resultGetTicketReasonCategory.Item2 != null))
                                {
                                    CustomDto.CustomTicketReasonCategoryDto returnDto = new CustomDto.CustomTicketReasonCategoryDto();
                                    CustomDto.CustomTicketReasonCategoryDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentDto.TicketReasonCategoryV2Dto, CustomDto.CustomTicketReasonCategoryDto>(resultGetTicketReasonCategory.Item2);
                                    returnDto = convertedDto;

                                    return new Tuple<ErrorCodes, CustomDto.CustomTicketReasonCategoryDto>(ErrorCodes.None, returnDto);
                                }
                                else
                                {
                                    return new Tuple<ErrorCodes, CustomDto.CustomTicketReasonCategoryDto>(resultGetTicketReasonCategory.Item1, null);
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }
    }
}
