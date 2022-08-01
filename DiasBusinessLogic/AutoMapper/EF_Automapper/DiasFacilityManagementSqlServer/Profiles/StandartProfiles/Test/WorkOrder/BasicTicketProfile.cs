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
    public class BasicTicketProfile : Profile
    {
        public BasicTicketProfile() { }
        public BasicTicketProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.BasicTicket, Dto.BasicTicketDto>()
                            .ForMember(x => x.BasicTicketState, y => y.MapFrom(src => src.StateOfBasicTicket));
                        
                        CreateMap<Dto.BasicTicketDto, Model.BasicTicket>()
                            .ForMember(x => x.StateOfBasicTicket, y => y.MapFrom(src => src.BasicTicketState));

                        CreateMap<Model.Attachment, Dto.AttachmentDto>()
                            .ForMember(x => x.BasicTicket, f => f.Ignore());

                        CreateMap<Dto.AttachmentDto, Model.Attachment>()
                            .ForMember(x => x.BasicTicket, f => f.Ignore());

                        CreateMap<Model.BasicTicketState, Dto.BasicTicketStateDto>();

                        CreateMap<Dto.BasicTicketDto, Model.BasicTicketState>();



                        CreateMap<Model.Ticket, Dto.TicketDto>()
                            .ForMember(x => x.Attachments, y => y.Ignore())
                            .ForMember(x => x.BasicTicket, y => y.Ignore());

                        CreateMap<Dto.TicketDto, Model.Ticket>();

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
