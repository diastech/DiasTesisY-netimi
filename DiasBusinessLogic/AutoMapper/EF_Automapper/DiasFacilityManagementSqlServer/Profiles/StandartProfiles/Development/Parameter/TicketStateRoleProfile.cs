using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class TicketStateRoleProfile : Profile
    {
        public TicketStateRoleProfile() { }

        public TicketStateRoleProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.TicketStateRole, Dto.TicketStateRoleDto>();
                        CreateMap<Dto.TicketStateRoleDto, Model.TicketStateRole>();
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
