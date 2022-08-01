using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentTicketReasonCategoryV2Interface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentTicketReasonCategoryV2Profile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Test.Standart
{
    public class TicketReasonCategoryV2BusinessRules : BusinessRuleAbstract, DevelopmentTicketReasonCategoryV2Interface.ITicketReasonCategoryV2BusinessRules, IBaseTicketReasonCategoryV2BusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentTicketReasonCategoryV2Profile.TicketReasonCategoryV2Profile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new  (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public TicketReasonCategoryV2BusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private TicketReasonCategoryV2BusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<ErrorCodes, List<TicketReasonCategoryV2Dto>>> GetAllTicketReasonCategoriesAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(ErrorCodes.UnknownError, null);
            else
            {
                List<DevelopmentDto.TicketReasonCategoryV2Dto> returnDtoList = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.TicketReasonCategoryV2> sonucEntityList;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntityList = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.TicketReasonCategoryV2s.AsQueryable().Where(x => !x.IsDeleted && x.IsActive == true)
                                    .AsNoTracking<DevelopmentModel.TicketReasonCategoryV2>().ToList<DevelopmentModel.TicketReasonCategoryV2>());
                                }
                                var convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentModel.TicketReasonCategoryV2>, List<DevelopmentDto.TicketReasonCategoryV2Dto>>(sonucEntityList);
                                return new(ErrorCodes.None, convertedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception e)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                        }
                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        public async Task<Tuple<ErrorCodes, TicketReasonCategoryV2Dto>> GetTicketReasonCategoryByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(ErrorCodes.UnknownError, null);
            else
            {
                DevelopmentDto.TicketReasonCategoryV2Dto returnDto = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.TicketReasonCategoryV2 sonucEntity;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntity = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.TicketReasonCategoryV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == hierarchyId && !x.IsDeleted && x.IsActive == true)
                                    .FirstOrDefault<DevelopmentModel.TicketReasonCategoryV2>());
                                }
                                DevelopmentDto.TicketReasonCategoryV2Dto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.TicketReasonCategoryV2, DevelopmentDto.TicketReasonCategoryV2Dto>(sonucEntity);
                                returnDto = convertedDto;
                                return new(ErrorCodes.None, returnDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception)
                            {
                                return new(ErrorCodes.UnknownError, null);
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
