using AutoMapper;
using System;
using Model = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test
{
    public class CustomTicketProfile : Profile
    {
        public CustomTicketProfile() { }

        public CustomTicketProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.CustomTicketDto, Dto.TicketDto>();                        
                        CreateMap<Dto.TicketDto, Model.CustomTicketDto>();

                        CreateMap<Model.CustomMobileTicketDto, Dto.TicketDto>();
                        CreateMap<Dto.TicketDto, Model.CustomMobileTicketDto>();
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
