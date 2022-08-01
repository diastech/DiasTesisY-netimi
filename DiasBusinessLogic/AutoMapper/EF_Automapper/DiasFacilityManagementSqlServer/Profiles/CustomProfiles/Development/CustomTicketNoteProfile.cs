using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development
{
    public class CustomTicketNoteProfile : Profile
    {
        public CustomTicketNoteProfile() { }

        public CustomTicketNoteProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.CustomTicketNoteDto, Dto.TicketNoteDto>();
                        CreateMap<Dto.TicketNoteDto, Model.CustomTicketNoteDto>();                        
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
