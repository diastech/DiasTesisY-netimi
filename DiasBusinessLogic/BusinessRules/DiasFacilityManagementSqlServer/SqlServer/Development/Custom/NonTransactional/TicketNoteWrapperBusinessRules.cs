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
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DiasBusinessLogic.Shared.Error;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class TicketNoteWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketNoteWrapperBusinessRules, IBaseTicketNoteWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketNoteBusinessRules _ticketNoteBusinessRules;

        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomTicketNoteProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public TicketNoteWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketNoteBusinessRules>())
        {
        }
        private TicketNoteWrapperBusinessRules(DevelopmentUserInterface.ITicketNoteBusinessRules ticketNoteBusinessRules)
        {
            _ticketNoteBusinessRules = ticketNoteBusinessRules;
        }

        public async Task<Tuple<Error, IEnumerable<CustomTicketNoteDto>>> GetTicketNotesByTicketId(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CustomTicketNoteDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, List<DevelopmentDto.TicketNoteDto>> resultGetTicketList = await _ticketNoteBusinessRules.GetNotesByTicketId(Id);

                            try
                            {
                                if ((resultGetTicketList.Item1.BusinessOperationSucceed == true) && (resultGetTicketList.Item2 != null))
                                {
                                    List<CustomDto.CustomTicketNoteDto> returnDtoList = new List<CustomDto.CustomTicketNoteDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                            Map<List<DevelopmentDto.TicketNoteDto>, List<CustomDto.CustomTicketNoteDto>>(resultGetTicketList.Item2);

                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketNoteDto>>(resultGetTicketList.Item1, returnDtoList);
                                }
                                else
                                {
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketNoteDto>>(resultGetTicketList.Item1, null);
                                }
                            }
                            catch (Exception e)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CustomTicketNoteDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
    }
}
