using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentDto = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class LocationWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ILocationWrapperBusinessRules, IBaseLocationWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ILocationV2BusinessRules _locationBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentLocationProfile.CustomLocationProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public LocationWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ILocationV2BusinessRules>())
        {
        }
        private LocationWrapperBusinessRules(DevelopmentUserInterface.ILocationV2BusinessRules locationBusinessRules)
        {
            _locationBusinessRules = locationBusinessRules;
        }

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetAllLocationsWrapperAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>> resultGetLocationList = await _locationBusinessRules.GetAllLocationsAsync();
                            try
                            {
                                if ((resultGetLocationList.Item1.BusinessOperationSucceed == true) && (resultGetLocationList.Item2 != null))
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>(resultGetLocationList.Item1, resultGetLocationList.Item2);
                                else
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>(resultGetLocationList.Item1, null);
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }            
        }

        public async Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationWrapperByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {                
                return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, CustomLocationDto> resultGetLocation = await _locationBusinessRules.GetLocationByIdAsync(hierarchyId);

                            try
                            {
                                if ((resultGetLocation.Item1.BusinessOperationSucceed ==true) && (resultGetLocation.Item2 != null))
                                {
                                    //CustomDto.CustomLocationDto returnDto = new CustomDto.CustomLocationDto();
                                    //CustomDto.CustomLocationDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<LocationV2Dto, CustomDto.CustomLocationDto>(resultGetLocation.Item2);
                                    //returnDto = convertedDto;

                                    return new Tuple<Error, CustomDto.CustomLocationDto>(resultGetLocation.Item1, resultGetLocation.Item2);
                                }
                                else
                                {
                                    return new Tuple<Error, CustomDto.CustomLocationDto>(resultGetLocation.Item1, null);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationWrapperByNfcCodeAsync(string nfcCode)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, CustomLocationDto> resultGetLocation = await _locationBusinessRules.GetLocationByNfcCodeAsync(nfcCode);

                            try
                            {
                                if ((resultGetLocation.Item1.BusinessOperationSucceed == true) && (resultGetLocation.Item2 != null))
                                {
                                    return new Tuple<Error, CustomDto.CustomLocationDto>(resultGetLocation.Item1, resultGetLocation.Item2);
                                }
                                else
                                {
                                    return new Tuple<Error, CustomDto.CustomLocationDto>(resultGetLocation.Item1, null);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }


    }
}
