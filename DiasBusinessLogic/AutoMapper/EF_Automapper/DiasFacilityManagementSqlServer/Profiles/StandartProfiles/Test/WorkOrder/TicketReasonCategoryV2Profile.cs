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
    public class TicketReasonCategoryV2Profile : Profile
    {
        public TicketReasonCategoryV2Profile() { }
        public TicketReasonCategoryV2Profile(Type dataContextType)
        {            
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.TicketReasonCategoryV2, Dto.TicketReasonCategoryV2Dto>();
                        
                        CreateMap<Dto.TicketReasonCategoryV2Dto, Model.TicketReasonCategoryV2>();

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
