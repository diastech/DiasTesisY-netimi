using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class TicketWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketWrapperBusinessRules, IBaseTicketWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketBusinessRules _ticketBusinessRules;
        private readonly DevelopmentUserInterface.IAttachmentBusinessRules _attachmentBusinessRules;
        private readonly DevelopmentUserInterface.ITicketNoteBusinessRules _ticketNoteBusinessRules;


        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public TicketWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketBusinessRules>(),
                                                    DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAttachmentBusinessRules>(),
                                                     DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketNoteBusinessRules>()
            )
        {
        }
        private TicketWrapperBusinessRules(DevelopmentUserInterface.ITicketBusinessRules ticketBusinessRules,
                                            DevelopmentUserInterface.IAttachmentBusinessRules attachmentBusinessRules,
                                             DevelopmentUserInterface.ITicketNoteBusinessRules ticketNoteBusinessRules)
        {
            _ticketBusinessRules = ticketBusinessRules;
            _attachmentBusinessRules = attachmentBusinessRules;
            _ticketNoteBusinessRules = ticketNoteBusinessRules;
        }

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketsWrapperAsync(DevExpressRequest devExpressRequestObj)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, List<DevelopmentDto.TicketDto>> resultGetTicketList = await _ticketBusinessRules.GetAllTicketAsync(devExpressRequestObj);

                            try
                            {
                                if ((resultGetTicketList.Item1.BusinessOperationSucceed == true) && (resultGetTicketList.Item2 != null))
                                {
                                    List<CustomDto.CustomTicketDto> returnDtoList = new List<CustomDto.CustomTicketDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                            Map<List<DevelopmentDto.TicketDto>, List<CustomDto.CustomTicketDto>>(resultGetTicketList.Item2);


                                    foreach (var item in returnDtoList)
                                    {
                                        var sonuc = "";
                                        for (var i = 0; i < (10 - item.Id.ToString().Length); i++)
                                        {
                                            sonuc += "0";
                                        }
                                        var ticketCode = item.LocationNameGetByCodeId + sonuc + item.Id.ToString();
                                        item.TicketCode = ticketCode;
                                    }

                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>(resultGetTicketList.Item1, returnDtoList);
                                }
                                else
                                {                                    
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>(resultGetTicketList.Item1, null);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }            
        }

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketsWrapperMobileAsync(DevExpressRequest devExpressRequestObj)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, List<DevelopmentDto.TicketDto>> resultGetTicketList = await _ticketBusinessRules.GetAllTicketsMobileAsync(devExpressRequestObj);

                            try
                            {
                                if ((resultGetTicketList.Item1.BusinessOperationSucceed == true) && (resultGetTicketList.Item2 != null))
                                {
                                    List<int> attachmentCountList = new();
                                    List<int> ticketNoteCountList = new();
                                    
                                    foreach (TicketDto item in resultGetTicketList.Item2)
                                    {
                                        Tuple<Error, int> resultGetTicketAttachmentCount = await _attachmentBusinessRules.GetAttachmentsCountByTicketId(item.Id);

                                        Tuple<Error, int> resultGetTicketNoteCount = await _ticketNoteBusinessRules.GetNotesCountByTicketId(item.Id);

                                        //sayı -1 dönüyorsa hata vardır
                                        attachmentCountList.Add(resultGetTicketAttachmentCount.Item2);
                                        ticketNoteCountList.Add(resultGetTicketNoteCount.Item2);
                                    }                            
                           
                                    List<CustomDto.CustomTicketDto> returnDtoList = new List<CustomDto.CustomTicketDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                            Map<List<DevelopmentDto.TicketDto>, List<CustomDto.CustomTicketDto>>(resultGetTicketList.Item2);


                                    if ((returnDtoList.Count == attachmentCountList.Count) && (returnDtoList.Count == ticketNoteCountList.Count))
                                        {
                                        for (int i = 0; i < returnDtoList.Count; i++)
                                        {
                                            returnDtoList[i].AttachemntsCount = attachmentCountList[i];
                                            returnDtoList[i].NotesCount = ticketNoteCountList[i];
                                        }
                                        foreach (var item in returnDtoList)
                                        {
                                            var sonuc = "";
                                            for (var i = 0; i < (10 - item.Id.ToString().Length); i++)
                                            {
                                                sonuc += "0";
                                            }
                                            var ticketCode = item.LocationNameGetByCodeId + sonuc + item.Id.ToString();
                                            item.TicketCode = ticketCode;
                                        }
                                    }
                                    

                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>(resultGetTicketList.Item1, returnDtoList);
                                }
                                else
                                {
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>(resultGetTicketList.Item1, null);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }


        public async Task<Tuple<Error, CustomDto.CustomTicketDto>> GetTicketWrapperByTicketIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, DevelopmentDto.TicketDto> resultGetTicket = await _ticketBusinessRules.GetTicketByIdAsync(Id);

                            try
                            {
                                if ((resultGetTicket.Item1.BusinessOperationSucceed == true) && (resultGetTicket.Item2 != null))
                                {
                                    CustomDto.CustomTicketDto returnDto = new CustomDto.CustomTicketDto();
                                    CustomDto.CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentDto.TicketDto, CustomDto.CustomTicketDto>(resultGetTicket.Item2);
                                    returnDto = convertedDto;

                                    
                                        var sonuc = "";
                                        for (var i = 0; i < (10 - returnDto.Id.ToString().Length); i++)
                                        {
                                            sonuc += "0";
                                        }
                                        var ticketCode = returnDto.LocationNameGetByCodeId + sonuc + returnDto.Id.ToString();
                                        returnDto.TicketCode = ticketCode;
                                    
                                    return new Tuple<Error, CustomDto.CustomTicketDto>(Errors.General.SuccessGetById("Ticket"), returnDto);
                                }
                                else
                                {
                                    return new Tuple<Error, CustomDto.CustomTicketDto>(resultGetTicket.Item1, null);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, IEnumerable<CustomTicketDto>>> GetAllTicketsWrapperByBasicTicketIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, List<DevelopmentDto.TicketDto>> resultGetTicketList = await _ticketBusinessRules.GetAllTicketsByBasicTicketIdAsync(Id);

                            try
                            {
                                if ((resultGetTicketList.Item1.BusinessOperationSucceed == true) && (resultGetTicketList.Item2 != null))
                                {
                                    List<CustomDto.CustomTicketDto> returnDtoList = new List<CustomDto.CustomTicketDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentDto.TicketDto>, List<CustomDto.CustomTicketDto>>(resultGetTicketList.Item2);

                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>(Errors.General.SuccessGetById("Ticket"), returnDtoList);
                                }
                                else
                                {
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>(resultGetTicketList.Item1, null);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
    }
}
