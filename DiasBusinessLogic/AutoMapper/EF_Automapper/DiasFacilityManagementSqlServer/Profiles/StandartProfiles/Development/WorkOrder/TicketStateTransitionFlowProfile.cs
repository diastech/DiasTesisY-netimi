using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using AutoMapper;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class TicketStateTransitionFlowProfile : Profile
    {
        public TicketStateTransitionFlowProfile() { }
        public TicketStateTransitionFlowProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
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
