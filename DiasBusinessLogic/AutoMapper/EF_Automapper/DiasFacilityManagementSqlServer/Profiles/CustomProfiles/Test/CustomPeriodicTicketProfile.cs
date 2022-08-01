using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test
{
    public class CustomPeriodicTicketProfile : Profile
    {
        public CustomPeriodicTicketProfile() { }
        public CustomPeriodicTicketProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.CustomPeriodicTicketDto, Dto.PeriodicTicketDto>();
                        CreateMap<Dto.PeriodicTicketDto, Model.CustomPeriodicTicketDto>();
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
