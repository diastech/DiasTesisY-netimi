using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentAssignmentGroupEmployeeInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentAssignmentGroupEmployeeProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasShared.Errors;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class AssignmentGroupEmployeeBusinessRules : BusinessRuleAbstract, DevelopmentAssignmentGroupEmployeeInterface.IAssignmentGroupEmployeeBusinessRules, IBaseAssignmentGroupEmployeeBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentAssignmentGroupEmployeeProfile.AssignmentGroupEmployeeProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new (new DevelopmentAssignmentGroupEmployeeProfile.AssignmentGroupEmployeeProfile(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public AssignmentGroupEmployeeBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private AssignmentGroupEmployeeBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, IEnumerable<AssignmentGroupEmployeeDto>>> GetAllAssignmentGroupEmployeeAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                List<AssignmentGroupEmployeeDto> returnDtoList = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.AssignmentGroupEmployee> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntityList = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.AssignmentGroupEmployees.AsQueryable().Where(x => (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true).Include(x=>x.EmployeeUser)
                                    .AsNoTracking<DevelopmentModel.AssignmentGroupEmployee>().ToList<DevelopmentModel.AssignmentGroupEmployee>());
                                }
                                var convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentModel.AssignmentGroupEmployee>, List<AssignmentGroupEmployeeDto>>(sonucEntityList);
                                
                                return new(Errors.General.GetListSuccess("AssignmentGroupEmployee"), convertedDto);
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
                                return new(Errors.General.GridListError("AssignmentGroupEmployee"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
        public async Task<Tuple<Error, IEnumerable<AssignmentGroupEmployeeDto>>> GetById(int kullaniciId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                List<AssignmentGroupEmployeeDto> returnDtoList = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.AssignmentGroupEmployee> sonucEntity;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntity = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.AssignmentGroupEmployees.AsQueryable()
                                    .Where(x => (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true && x.EmployeeUserId == kullaniciId)
                                    .AsNoTracking<DevelopmentModel.AssignmentGroupEmployee>().ToList<DevelopmentModel.AssignmentGroupEmployee>());
                                }
                                var convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentModel.AssignmentGroupEmployee>,List<AssignmentGroupEmployeeDto>>(sonucEntity);

                                return new(Errors.General.SuccessGetById("AssignmentGroupEmployee"), convertedDto);
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
                                return new(Errors.General.ErrorGetById("AssignmentGroupEmployee"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, IEnumerable<AssignmentGroupEmployeeDto>>> GetUsersByGroupId(int groupId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                List<AssignmentGroupEmployeeDto> returnDtoList = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.AssignmentGroupEmployee> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntityList = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.AssignmentGroupEmployees.AsQueryable().Where(x => (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true && x.AssignmentGroupId == groupId)
                                    .AsNoTracking<DevelopmentModel.AssignmentGroupEmployee>().ToList<DevelopmentModel.AssignmentGroupEmployee>());
                                }
                                var convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentModel.AssignmentGroupEmployee>, List<AssignmentGroupEmployeeDto>>(sonucEntityList);

                                return new(Errors.General.GetListSuccess("AssignmentGroupEmployee"), convertedDto);
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
                                return new(Errors.General.GridListError("AssignmentGroupEmployee"), null);
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
