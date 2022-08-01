using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test
{
    public class TicketReasonCategoryProfile : Profile
    {
        public TicketReasonCategoryProfile() { }
        public TicketReasonCategoryProfile(Type dataContextType)
        {            
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.TicketReasonCategory, Dto.TicketReasonCategoryDto>();
                        
                        CreateMap<Dto.TicketReasonCategoryDto, Model.TicketReasonCategory>();

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
