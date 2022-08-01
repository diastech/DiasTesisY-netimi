using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using ModelIdentity = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
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
                            .ForMember(x => x.BasicTicketState, y => y.MapFrom(src => src.StateOfBasicTicket))
                            .ForMember(x=>x.Tickets,y=>y.Ignore());
                        
                        CreateMap<Dto.BasicTicketDto, Model.BasicTicket>()
                            .ForMember(x => x.StateOfBasicTicket, y => y.MapFrom(src => src.BasicTicketState))
                            .ForMember(x => x.Tickets, y => y.Ignore());

                        CreateMap<ModelIdentity.User, Dto.UserDto>();

                        CreateMap<Dto.UserDto, ModelIdentity.User>();

                        CreateMap<Model.Attachment, Dto.AttachmentDto>()
                            .ForMember(x => x.BasicTicket, f => f.Ignore());

                        CreateMap<Dto.AttachmentDto, Model.Attachment>()
                            .ForMember(x => x.BasicTicket, f => f.Ignore());

                        CreateMap<Model.BasicTicketState, Dto.BasicTicketStateDto>()
                            .ForMember(x => x.BasicTicketStateFK, y => y.Ignore());

                        CreateMap<Dto.BasicTicketDto, Model.BasicTicketState>()
                            .ForMember(x => x.BasicTicketStateFK, y => y.Ignore());


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
