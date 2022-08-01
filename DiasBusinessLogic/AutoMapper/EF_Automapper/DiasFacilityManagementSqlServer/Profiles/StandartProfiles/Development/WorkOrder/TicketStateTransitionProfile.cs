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
    public class TicketStateTransitionProfile : Profile
    {
        public TicketStateTransitionProfile() { }
        public TicketStateTransitionProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.TicketStateTransition, Dto.TicketStateTransitionDto>();

                        CreateMap<Dto.TicketStateTransitionDto, Model.TicketStateTransition>();

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
