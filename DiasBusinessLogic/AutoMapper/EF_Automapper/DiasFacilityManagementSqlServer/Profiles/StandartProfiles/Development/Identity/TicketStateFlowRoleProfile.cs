using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class TicketStateFlowRoleProfile : Profile
    {
        public TicketStateFlowRoleProfile() { }

        public TicketStateFlowRoleProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.TicketStateFlowRole, Dto.TicketStateFlowRoleDto>();
                        CreateMap<Dto.TicketStateFlowRoleDto, Model.TicketStateFlowRole>();
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
