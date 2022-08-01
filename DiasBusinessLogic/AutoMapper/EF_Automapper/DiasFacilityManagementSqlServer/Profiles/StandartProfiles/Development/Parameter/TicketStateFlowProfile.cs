using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class TicketStateFlowProfile : Profile
    {
        public TicketStateFlowProfile() { }

        public TicketStateFlowProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.TicketStateFlow, Dto.TicketStateFlowDto>();
                        CreateMap<Dto.TicketStateFlowDto, Model.TicketStateFlow>();
                        CreateMap<Model.TicketStateTransitionFlow, Dto.TicketStateTransitionFlowDto>();
                        CreateMap<Dto.TicketStateTransitionFlowDto, Model.TicketStateTransitionFlow>();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

    }
}
