using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using System;
using System.Collections.Generic;
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
using CustomDevelopmentBasicTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
{
    public class BasicTicketWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.IBasicTicketWrapperBusinessRules, IBaseBasicTicketWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.IBasicTicketBusinessRules _basicTicketBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentBasicTicketProfile.CustomBasicTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        public BasicTicketWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IBasicTicketBusinessRules>())
        {
        }
        private BasicTicketWrapperBusinessRules(DevelopmentUserInterface.IBasicTicketBusinessRules basicTicketBusinessRules)
        {
            _basicTicketBusinessRules = basicTicketBusinessRules;
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<CustomDto.CustomBasicTicketDto>>> GetAllBasicTicketsWrapperAsync(DevExpressRequest devExpressRequestObj)
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
                            Tuple<ErrorCodes, List<DevelopmentDto.BasicTicketDto>> resultGetTicketList = await _basicTicketBusinessRules.GetAllBasicTicketsAsync(devExpressRequestObj);

                            try
                            {
                                if ((resultGetTicketList.Item1 == ErrorCodes.None) && (resultGetTicketList.Item2 != null))
                                {
                                    List<CustomDto.CustomBasicTicketDto> returnDtoList = new List<CustomDto.CustomBasicTicketDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentDto.BasicTicketDto>, List<CustomDto.CustomBasicTicketDto>>(resultGetTicketList.Item2);

                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomBasicTicketDto>>(ErrorCodes.None, returnDtoList);
                                }
                                else
                                {
                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomBasicTicketDto>>(resultGetTicketList.Item1, null);
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

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, CustomDto.CustomBasicTicketDto>> GetBasicTicketWrapperByIdAsync(int Id)
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
                            Tuple<ErrorCodes, DevelopmentDto.BasicTicketDto> resultGetTicket = await _basicTicketBusinessRules.GetBasicTicketByIdAsync(Id);

                            try
                            {
                                if ((resultGetTicket.Item1 == ErrorCodes.None) && (resultGetTicket.Item2 != null))
                                {
                                    CustomDto.CustomBasicTicketDto returnDto = new CustomDto.CustomBasicTicketDto();
                                    CustomDto.CustomBasicTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentDto.BasicTicketDto, CustomDto.CustomBasicTicketDto>(resultGetTicket.Item2);
                                    returnDto = convertedDto;

                                    return new Tuple<ErrorCodes, CustomDto.CustomBasicTicketDto>(ErrorCodes.None, returnDto);
                                }
                                else
                                {
                                    return new Tuple<ErrorCodes, CustomDto.CustomBasicTicketDto>(resultGetTicket.Item1, null);
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
