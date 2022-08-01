using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentTicketReasonCategoryV2Interface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentTicketReasonCategoryV2Profile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using Microsoft.EntityFrameworkCore;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class TicketReasonCategoryV2BusinessRules : BusinessRuleAbstract, DevelopmentTicketReasonCategoryV2Interface.ITicketReasonCategoryV2BusinessRules, IBaseTicketReasonCategoryV2BusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentTicketReasonCategoryV2Profile.TicketReasonCategoryV2Profile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new  (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public TicketReasonCategoryV2BusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private TicketReasonCategoryV2BusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, List<TicketReasonCategoryV2Dto>>> GetAllTicketReasonCategoriesAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
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
                                    DiasFacilityManagementSqlServerContext.TicketReasonCategoryV2s.AsQueryable().Where(x => (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                    .AsNoTracking<DevelopmentModel.TicketReasonCategoryV2>().ToList<DevelopmentModel.TicketReasonCategoryV2>());
                                }
                                var convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentModel.TicketReasonCategoryV2>, List<DevelopmentDto.TicketReasonCategoryV2Dto>>(sonucEntityList);
                                return new(Errors.General.GetListSuccess("TicketReasonCategoryV2"), convertedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception e)
                            {
                                return new(Errors.General.GridListError("TicketReasonCategoryV2"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, TicketReasonCategoryV2Dto>> GetTicketReasonCategoryByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.ConnectionTimeout(), null);
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
                                    DiasFacilityManagementSqlServerContext.TicketReasonCategoryV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == hierarchyId && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                    .First<DevelopmentModel.TicketReasonCategoryV2>());
                                }
                                DevelopmentDto.TicketReasonCategoryV2Dto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.TicketReasonCategoryV2, DevelopmentDto.TicketReasonCategoryV2Dto>(sonucEntity);
                                returnDto = convertedDto;                                
                                return new(Errors.General.SuccessGetById("TicketReasonCategoryV2"), returnDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.ErrorGetById("TicketReasonCategoryV2"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, IEnumerable<TicketReasonCategoryV2Dto>>> GetLastNodeTicketReasonCategoryByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.ConnectionTimeout(), null);
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.TicketReasonCategoryV2 parentEntity;
                                List<DevelopmentModel.TicketReasonCategoryV2> descendantEntityList = new();
                                List<DevelopmentModel.TicketReasonCategoryV2> lastModeDescendantEntityList = new();

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    parentEntity = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.TicketReasonCategoryV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == hierarchyId && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                    .First<DevelopmentModel.TicketReasonCategoryV2>());


                                    //şimdi bunun varsa altındaki nodelara bakarak en alt node'u bulmaya çalışalım                               
                                    descendantEntityList = await Task.Run(() =>
                                                                DiasFacilityManagementSqlServerContext.TicketReasonCategoryV2s.AsQueryable()
                                                                .Where(subNode => (subNode.HierarchyId.IsDescendantOf(parentEntity.HierarchyId))
                                                                    && (subNode != parentEntity) && (subNode.IsActive.HasValue && subNode.IsDeleted.HasValue)
                                                                        && (!subNode.IsDeleted.Value) && subNode.IsActive == true)
                                                                            .ToList<DevelopmentModel.TicketReasonCategoryV2>());

                                    if (descendantEntityList.Count == 0)
                                    {
                                        lastModeDescendantEntityList.Add(parentEntity);

                                    }
                                    else
                                    {
                                        foreach (DevelopmentModel.TicketReasonCategoryV2 item in descendantEntityList)
                                        {
                                            parentEntity = item;
                                            List<DevelopmentModel.TicketReasonCategoryV2> subDescendantEntityList = new();

                                            subDescendantEntityList = await Task.Run(() =>
                                                               DiasFacilityManagementSqlServerContext.TicketReasonCategoryV2s.AsQueryable()
                                                               .Where(subNode => (subNode.HierarchyId.IsDescendantOf(parentEntity.HierarchyId))
                                                                   && (subNode != parentEntity) && (subNode.IsActive.HasValue && subNode.IsDeleted.HasValue)
                                                                       && (!subNode.IsDeleted.Value) && subNode.IsActive == true)
                                                                        .ToList<DevelopmentModel.TicketReasonCategoryV2>());


                                            if (subDescendantEntityList.Count == 0)
                                            {
                                                lastModeDescendantEntityList.Add(parentEntity);
                                            }
                                        }
                                    }
                                }


                                List<DevelopmentDto.TicketReasonCategoryV2Dto> convertedDtoList = 
                                    DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentModel.TicketReasonCategoryV2>, List<DevelopmentDto.TicketReasonCategoryV2Dto>>(lastModeDescendantEntityList);

                                return new(Errors.General.GetListSuccess("TicketReasonCategoryV2"), convertedDtoList);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.ErrorGetList("TicketReasonCategoryV2"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

    }
}
