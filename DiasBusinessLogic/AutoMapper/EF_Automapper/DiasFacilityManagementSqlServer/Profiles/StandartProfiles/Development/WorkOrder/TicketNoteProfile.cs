using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using ModelIdentity = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class TicketNoteProfile : Profile
    {
        public TicketNoteProfile() { }
        public TicketNoteProfile(Type dataContextType)
        {            
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.TicketNote, Dto.TicketNoteDto>();
                        
                        CreateMap<Dto.TicketNoteDto, Model.TicketNote>();

                        CreateMap<Model.Attachment, Dto.AttachmentDto>()
                            .ForMember(x=>x.TicketNote,d=>d.Ignore());

                        CreateMap<Dto.AttachmentDto, Model.Attachment>()
                            .ForMember(x => x.TicketNote, d => d.Ignore());

                        CreateMap<ModelIdentity.User, Dto.UserDto>();

                        CreateMap<Dto.UserDto, ModelIdentity.User>();


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
