using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
{
    public class TicketWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketWrapperBusinessRules, IBaseTicketWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketBusinessRules _ticketBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        public TicketWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketBusinessRules>())
        {
        }
        private TicketWrapperBusinessRules(DevelopmentUserInterface.ITicketBusinessRules ticketBusinessRules)
        {
            _ticketBusinessRules = ticketBusinessRules;
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketsWrapperAsync(DevExpressRequest devExpressRequestObj)
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
                            Tuple<ErrorCodes, List<DevelopmentDto.TicketDto>> resultGetTicketList = await _ticketBusinessRules.GetAllTicketAsync(devExpressRequestObj);

                            try
                            {
                                if ((resultGetTicketList.Item1 == ErrorCodes.None) && (resultGetTicketList.Item2 != null))
                                {
                                    List<CustomDto.CustomTicketDto> returnDtoList = new List<CustomDto.CustomTicketDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                            Map<List<DevelopmentDto.TicketDto>, List<CustomDto.CustomTicketDto>>(resultGetTicketList.Item2);

                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomTicketDto>>(ErrorCodes.None, returnDtoList);
                                }
                                else
                                {
                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomTicketDto>>(resultGetTicketList.Item1, null);
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

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, CustomDto.CustomTicketDto>> GetTicketWrapperByTicketIdAsync(int Id)
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
                            Tuple<ErrorCodes, DevelopmentDto.TicketDto> resultGetTicket = await _ticketBusinessRules.GetTicketByIdAsync(Id);

                            try
                            {
                                if ((resultGetTicket.Item1 == ErrorCodes.None) && (resultGetTicket.Item2 != null))
                                {
                                    CustomDto.CustomTicketDto returnDto = new CustomDto.CustomTicketDto();
                                    CustomDto.CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentDto.TicketDto, CustomDto.CustomTicketDto>(resultGetTicket.Item2);
                                    returnDto = convertedDto;

                                    return new Tuple<ErrorCodes, CustomDto.CustomTicketDto>(ErrorCodes.None, returnDto);
                                }
                                else
                                {
                                    return new Tuple<ErrorCodes, CustomDto.CustomTicketDto>(resultGetTicket.Item1, null);
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

        public async Task<Tuple<ErrorCodes, IEnumerable<CustomTicketDto>>> GetAllTicketsWrapperByBasicTicketIdAsync(int Id)
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
                            Tuple<ErrorCodes, List<DevelopmentDto.TicketDto>> resultGetTicketList = await _ticketBusinessRules.GetAllTicketsByBasicTicketIdAsync(Id);

                            try
                            {
                                if ((resultGetTicketList.Item1 == ErrorCodes.None) && (resultGetTicketList.Item2 != null))
                                {
                                    List<CustomDto.CustomTicketDto> returnDtoList = new List<CustomDto.CustomTicketDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentDto.TicketDto>, List<CustomDto.CustomTicketDto>>(resultGetTicketList.Item2);

                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomTicketDto>>(ErrorCodes.None, returnDtoList);
                                }
                                else
                                {
                                    return new Tuple<ErrorCodes, IEnumerable<CustomDto.CustomTicketDto>>(resultGetTicketList.Item1, null);
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
