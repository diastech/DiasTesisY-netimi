using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentLocationInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
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
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DevelopmentCustomLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Test.Standart
{
    public class LocationV2BusinessRules : BusinessRuleAbstract, DevelopmentLocationInterface.ILocationV2BusinessRules, IBaseLocationV2BusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentLocationProfile.LocationV2Profile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new  (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        private static AutoMapperProfileMapper<DevelopmentCustomLocationProfile.CustomLocationProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_Custom
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public LocationV2BusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private LocationV2BusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<ErrorCodes, IEnumerable<CustomDto.CustomLocationDto>>> GetAllLocationsAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(ErrorCodes.UnknownError, null);
            else
            {
                List<CustomDto.CustomLocationDto> returnDtoList = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.LocationV2> sonucEntityList;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntityList = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => !x.IsDeleted && x.IsActive == true)
                                    .AsNoTracking<DevelopmentModel.LocationV2>().ToList<DevelopmentModel.LocationV2>());
                                }
                                var convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.Map<List<DevelopmentModel.LocationV2>, List<CustomDto.CustomLocationDto>>(sonucEntityList);
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

        public async Task<Tuple<ErrorCodes, LocationV2Dto>> GetLocationByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(ErrorCodes.UnknownError, null);
            else
            {
                DevelopmentDto.LocationV2Dto returnDto = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.LocationV2 sonucEntity;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntity = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == hierarchyId && !x.IsDeleted && x.IsActive == true)
                                    .FirstOrDefault<DevelopmentModel.LocationV2>());
                                }
                                DevelopmentDto.LocationV2Dto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.LocationV2, DevelopmentDto.LocationV2Dto>(sonucEntity);
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
