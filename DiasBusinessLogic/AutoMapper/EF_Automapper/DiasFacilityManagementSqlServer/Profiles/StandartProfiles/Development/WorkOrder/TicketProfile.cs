using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using ModelIdentity = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class TicketProfile : Profile
    {
        public TicketProfile() { }
        public TicketProfile(Type dataContextType)
        {            
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {

                        CreateMap<Model.Ticket, Dto.TicketDto>();                        

                        CreateMap<Dto.TicketReasonDto, Model.TicketReason>()
                            .ForMember(d => d.Tickets, f => f.Ignore())
                        .ForMember(d => d.AssignmentGroups, f => f.Ignore());

                        CreateMap<Model.TicketReason, Dto.TicketReasonDto>();

                        CreateMap<Dto.TicketStateDto, Model.TicketState>()
                            .ForMember(d => d.Tickets, f => f.Ignore());

                        CreateMap<Model.TicketState, Dto.TicketStateDto>();


                        CreateMap<Dto.TicketReasonCategoryV2Dto, Model.TicketReasonCategoryV2>()
                            .ForMember(d => d.TicketReasons, f => f.Ignore());

                        CreateMap<Model.TicketReasonCategoryV2, Dto.TicketReasonCategoryV2Dto>();


                        CreateMap<Model.TicketRelatedLocation, Dto.TicketRelatedLocationDto>()
                            .ForMember(d => d.Ticket, f => f.Ignore())
                            .ForMember(d => d.PeriodicTickets, f => f.Ignore());



                        CreateMap<Dto.TicketRelatedLocationDto, Model.TicketRelatedLocation>();


                        CreateMap<Dto.LocationV2Dto, Model.LocationV2>()
                            .ForMember(d => d.TicketRelatedLocations, f => f.Ignore());

                        CreateMap<Model.LocationV2, Dto.LocationV2Dto>();

                        CreateMap<Model.AssignmentGroup, Dto.AssignmentGroupDto>()
                            .ForMember(d => d.Tickets, f => f.Ignore())
                            .ForMember(d => d.TicketReason, f => f.Ignore());

                        CreateMap<Dto.AssignmentGroupDto, Model.AssignmentGroup>()
                        .ForMember(d => d.Tickets, f => f.Ignore())
                            .ForMember(d => d.TicketReason, f => f.Ignore());


                        CreateMap<ModelIdentity.User, Dto.UserDto>();

                        CreateMap<Dto.UserDto, ModelIdentity.User>();

                        CreateMap<Model.Attachment, Dto.AttachmentDto>()
                        .ForMember(d => d.Ticket, f => f.Ignore())
                        .ForMember(d => d.TicketNote, f => f.Ignore());


                        CreateMap<Dto.AttachmentDto, Model.Attachment>()
                            .ForMember(d => d.Ticket, f => f.Ignore())
                            .ForMember(d => d.TicketNote, f => f.Ignore());

                        CreateMap<Model.TicketPriority, Dto.TicketPriorityDto>();


                        CreateMap<Dto.TicketPriorityDto, Model.TicketPriority>();


                        CreateMap<Model.TicketNote, Dto.TicketNoteDto>();


                        CreateMap<Dto.TicketNoteDto, Model.TicketNote>()
                            .ForMember(d => d.Ticket, f => f.Ignore());

                        CreateMap<Model.TicketState, Dto.TicketStateDto>()
                            .ForMember(d => d.TicketStateTransitionDestinationTicketStates, f => f.Ignore());

                        CreateMap<Dto.TicketStateDto, Model.TicketState>()
                            .ForMember(d => d.TicketStateTransitionDestinationTicketStates, f => f.Ignore());

                        CreateMap<Model.TicketStateTransition, Dto.TicketStateTransitionDto>()
                            .ForMember(d=>d.SourceTicketState,f=>f.Ignore())
                        .ForMember(d => d.DestinationTicketState, f => f.Ignore());

                        CreateMap<Dto.TicketStateTransitionDto, Model.TicketStateTransition>()
                            .ForMember(d => d.SourceTicketState, f => f.Ignore())
                        .ForMember(d => d.DestinationTicketState, f => f.Ignore());



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
