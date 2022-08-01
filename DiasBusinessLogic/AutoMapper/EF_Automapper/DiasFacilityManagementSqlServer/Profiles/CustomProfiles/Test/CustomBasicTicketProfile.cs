using AutoMapper;
using System;
using Model = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test
{
    public class CustomBasicTicketProfile : Profile
    {
        public CustomBasicTicketProfile() { }
        public CustomBasicTicketProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.CustomBasicTicketDto, Dto.BasicTicketDto>();
                        CreateMap<Dto.BasicTicketDto, Model.CustomBasicTicketDto>();
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
