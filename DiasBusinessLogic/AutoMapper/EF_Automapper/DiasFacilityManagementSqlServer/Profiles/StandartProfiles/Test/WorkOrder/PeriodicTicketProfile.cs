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
    public class PeriodicTicketProfile : Profile
    {
        public PeriodicTicketProfile() { }
        public PeriodicTicketProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.PeriodicTicket, Dto.PeriodicTicketDto>();
                        
                        CreateMap<Dto.PeriodicTicketDto, Model.PeriodicTicket>();

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
