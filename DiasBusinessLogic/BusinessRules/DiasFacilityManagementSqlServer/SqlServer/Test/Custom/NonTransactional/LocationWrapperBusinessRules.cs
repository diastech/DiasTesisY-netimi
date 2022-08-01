using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
{
    public class LocationWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ILocationWrapperBusinessRules, IBaseLocationWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ILocationV2BusinessRules _locationBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentLocationProfile.CustomLocationProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        public LocationWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ILocationV2BusinessRules>())
        {
        }
        private LocationWrapperBusinessRules(DevelopmentUserInterface.ILocationV2BusinessRules locationBusinessRules)
        {
            _locationBusinessRules = locationBusinessRules;
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<CustomDto.CustomLocationDto>>> GetAllLocationsWrapperAsync()
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
                            Tuple<ErrorCodes, IEnumerable<CustomDto.CustomLocationDto>> resultGetLocationList = await _locationBusinessRules.GetAllLocationsAsync();
                            try
                            {
                                if ((resultGetLocationList.Item1 == ErrorCodes.None) && (resultGetLocationList.Item2 != null))
                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomLocationDto>>(ErrorCodes.None, resultGetLocationList.Item2);
                                else
                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomLocationDto>>(resultGetLocationList.Item1, null);
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

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, CustomDto.CustomLocationDto>> GetLocationWrapperByIdAsync(string hierarchyId)
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
                            Tuple<ErrorCodes, LocationV2Dto> resultGetLocation = await _locationBusinessRules.GetLocationByIdAsync(hierarchyId);

                            try
                            {
                                if ((resultGetLocation.Item1 == ErrorCodes.None) && (resultGetLocation.Item2 != null))
                                {
                                    CustomDto.CustomLocationDto returnDto = new CustomDto.CustomLocationDto();
                                    CustomDto.CustomLocationDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<LocationV2Dto, CustomDto.CustomLocationDto>(resultGetLocation.Item2);
                                    returnDto = convertedDto;

                                    return new Tuple<ErrorCodes, CustomDto.CustomLocationDto>(ErrorCodes.None, returnDto);
                                }
                                else
                                {
                                    return new Tuple<ErrorCodes, CustomDto.CustomLocationDto>(resultGetLocation.Item1, null);
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
