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
    public class CustomTicketReasonCategoryProfile : Profile
    {
        public CustomTicketReasonCategoryProfile() { }
        public CustomTicketReasonCategoryProfile(Type dataContextType) 
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.CustomTicketReasonCategoryDto, Dto.TicketReasonCategoryV2Dto>();
                        CreateMap<Dto.TicketReasonCategoryV2Dto, Model.CustomTicketReasonCategoryDto>();
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
