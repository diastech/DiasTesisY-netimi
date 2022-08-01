using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class BasicTicketStateProfile : Profile
    {
        public BasicTicketStateProfile() { }

        public BasicTicketStateProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        this.CreateMap<Model.BasicTicketState, Dto.BasicTicketStateDto>();
                        this.CreateMap<Dto.BasicTicketStateDto, Model.BasicTicketState>();

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
