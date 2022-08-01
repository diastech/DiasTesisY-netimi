using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasShared.Errors;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasBusinessLogic.Shared.Error;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class TicketStateFlowWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketStateFlowWrapperBusinessRules, IBaseTicketStateFlowWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketStateFlowBusinessRules _ticketStateFlowBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomTicketStateFlowProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public TicketStateFlowWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketStateFlowBusinessRules>())
        {
        }
        private TicketStateFlowWrapperBusinessRules(DevelopmentUserInterface.ITicketStateFlowBusinessRules ticketStateFlowBusinessRules)
        {
            _ticketStateFlowBusinessRules = ticketStateFlowBusinessRules;
        }

        public async Task<Tuple<Error, IEnumerable<CustomTicketStateFlowDto>>> GetAllTicketStateFlowWrapperAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CustomTicketStateFlowDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, List<DevelopmentDto.TicketStateFlowDto>> resultGetTicketList = await _ticketStateFlowBusinessRules.GetAllTicketAsync();

                            try
                            {
                                if ((resultGetTicketList.Item1.BusinessOperationSucceed == true) && (resultGetTicketList.Item2 != null))
                                {
                                    List<CustomDto.CustomTicketStateFlowDto> returnDtoList = new List<CustomDto.CustomTicketStateFlowDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                            Map<List<DevelopmentDto.TicketStateFlowDto>, List<CustomDto.CustomTicketStateFlowDto>>(resultGetTicketList.Item2);

                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketStateFlowDto>>(resultGetTicketList.Item1, returnDtoList);
                                }
                                else
                                {
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketStateFlowDto>>(resultGetTicketList.Item1, null);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CustomTicketStateFlowDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
    }
}
