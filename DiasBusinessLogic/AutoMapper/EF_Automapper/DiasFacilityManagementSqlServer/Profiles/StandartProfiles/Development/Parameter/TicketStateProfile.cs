using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class TicketStateProfile : Profile
    {
        public TicketStateProfile() { }

        public TicketStateProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.TicketState, Dto.TicketStateDto>();
                        
                        CreateMap<Dto.TicketStateDto, Model.TicketState>();

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
