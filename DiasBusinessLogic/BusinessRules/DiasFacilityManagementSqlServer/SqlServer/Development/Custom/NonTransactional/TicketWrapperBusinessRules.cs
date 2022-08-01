using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Classes.Dto;
using System.IdentityModel.Tokens.Jwt;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using static DiasShared.Enums.Standart.TicketEnums;
using static DiasShared.Enums.Standart.UserEnums;
using DiasShared.Services.Communication.BusinessLogicMessage;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class TicketWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketWrapperBusinessRules, IBaseTicketWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketBusinessRules _ticketBusinessRules;
        private readonly DevelopmentUserInterface.IAttachmentBusinessRules _attachmentBusinessRules;
        private readonly DevelopmentUserInterface.ITicketNoteBusinessRules _ticketNoteBusinessRules;
        private readonly DevelopmentUserInterface.IAssignmentGroupBusinessRules _assignmentGroupBusinessRules;
        private readonly IGenericStandartBusinessRules<CompanyRoleUserDto, CompanyRoleUserProfile> _genericStandartBusinessRules;
        private readonly IGenericStandartBusinessRules<AssignmentGroupDto, AssignmentGroupProfile> _genericStandartBusinessRulesAssignmentGroup;


        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public TicketWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketBusinessRules>(),
                                                    DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAttachmentBusinessRules>(),
                                                     DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketNoteBusinessRules>(),
                                                     DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAssignmentGroupBusinessRules>(),
                                                    DI_ServiceLocator.Current.Get<IGenericStandartBusinessRules<CompanyRoleUserDto, CompanyRoleUserProfile>>(),
                                                    DI_ServiceLocator.Current.Get<IGenericStandartBusinessRules<AssignmentGroupDto, AssignmentGroupProfile>>())
        {
        }
        private TicketWrapperBusinessRules(DevelopmentUserInterface.ITicketBusinessRules ticketBusinessRules,
                                            DevelopmentUserInterface.IAttachmentBusinessRules attachmentBusinessRules,
                                             DevelopmentUserInterface.ITicketNoteBusinessRules ticketNoteBusinessRules,
                                             DevelopmentUserInterface.IAssignmentGroupBusinessRules assignmentGroupBusinessRules,
                                             IGenericStandartBusinessRules<CompanyRoleUserDto, CompanyRoleUserProfile> genericStandartBusinessRules,
                                             IGenericStandartBusinessRules<AssignmentGroupDto, AssignmentGroupProfile> genericStandartBusinessRulesAssignmentGroup)
        {
            _ticketBusinessRules = ticketBusinessRules;
            _attachmentBusinessRules = attachmentBusinessRules;
            _ticketNoteBusinessRules = ticketNoteBusinessRules;
            _assignmentGroupBusinessRules = assignmentGroupBusinessRules;
            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesAssignmentGroup = genericStandartBusinessRulesAssignmentGroup;
        }

        public async Task<Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomTicketDto>>>> GetAllTicketsWrapperAsync(DevExpressRequest devExpressRequestObj, string token)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomTicketDto>>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            var handler = new JwtSecurityTokenHandler();
                            var jwtSecurityToken = handler.ReadJwtToken(token);
                            var claims = jwtSecurityToken.Payload.Claims;
                            int kullaniciId = 0;
                            foreach (var item in claims)
                            {
                                if (item.Type == "nameid")
                                {
                                    kullaniciId = Convert.ToInt32(item.Value);
                                }
                            }
                            Tuple<Error, IEnumerable<AssignmentGroupDto>> assignmentGroupEmployeeDto = await _assignmentGroupBusinessRules.GetManagedAssignmentGroupsByUserId(kullaniciId);
                            List<int> listAssignmentGroupInt = new();
                            List<int> listTicketStatus = new();
                            foreach (var item in assignmentGroupEmployeeDto.Item2)
                            {
                                listAssignmentGroupInt.Add(item.Id);
                            }
                            //Tuple<Error, DevExpressLoadResultDto<List<TicketDto>>> resultGetTicketList = await _ticketBusinessRules.GetAllTicketAsync(devExpressRequestObj, kullaniciId, listInt);
                            Tuple<Error, IEnumerable<CompanyRoleUserDto>> companyRoleUserList = await _genericStandartBusinessRules.GetAllV2();
                            var deneme = companyRoleUserList.Item2.Where(x => x.UserId == kullaniciId);
                            int count = 0;

                            //TODO: Enum olarak eklenecek(rol ve ticket status)
                            //TODO: Konfigürasyon dosyasında ayarlanacak
                            //TODO: if refactor

                            bool isAdmin = false;

                            foreach (var item in deneme)
                            {
                                if (count == 0)
                                {
                                    if (item.RoleId == (int)UserRolesTypes.FACILITYMANAGER || item.RoleId == (int)UserRolesTypes.TEAMMEMBER)
                                    {
                                        count++;
                                        listTicketStatus.Add(1);
                                        listTicketStatus.Add(2);
                                        listTicketStatus.Add(3);
                                        listTicketStatus.Add(4);
                                    }
                                    else if (item.RoleId == (int)UserRolesTypes.ADMINISTRATOR)//admin
                                    {
                                        //count++;
                                        //listTicketStatus.Add(1);
                                        //listTicketStatus.Add(2);
                                        //listTicketStatus.Add(3);
                                        //listTicketStatus.Add(4);
                                        //listTicketStatus.Add(5);
                                        //listTicketStatus.Add(6);
                                        //listTicketStatus.Add(7);
                                        //listTicketStatus.Add(8);
                                        //var getAllResult = await _genericStandartBusinessRulesAssignmentGroup.GetAllV2();
                                        //listAssignmentGroupInt = getAllResult.Item2.Select(p => p.Id).ToList();
                                        isAdmin = true;

                                    }
                                    else
                                    {
                                        count++;
                                        listTicketStatus.Add((int)TicketStatusEnum.NEW);
                                        listTicketStatus.Add((int)TicketStatusEnum.ASSIGNED);
                                        listTicketStatus.Add((int)TicketStatusEnum.WORKINGON);
                                        listTicketStatus.Add((int)TicketStatusEnum.SOLVED);
                                        listTicketStatus.Add((int)TicketStatusEnum.CLOSED);
                                        listTicketStatus.Add((int)TicketStatusEnum.SUSPENDED);
                                        listTicketStatus.Add((int)TicketStatusEnum.WAITING);
                                        listTicketStatus.Add((int)TicketStatusEnum.REJECTED);
                                    }

                                }

                            }

                            Tuple<Error, DevExpressLoadResultDto<List<TicketDto>>> resultGetTicketList = await _ticketBusinessRules.GetAllTicketAsync(devExpressRequestObj, kullaniciId, listAssignmentGroupInt, listTicketStatus, isAdmin);

                            try
                            {
                                if ((resultGetTicketList.Item1.BusinessOperationSucceed == true) && (resultGetTicketList.Item2 != null))
                                {
                                    List<CustomDto.CustomTicketDto> dtoList = new List<CustomDto.CustomTicketDto>();

                                    dtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                            Map<List<DevelopmentDto.TicketDto>, List<CustomDto.CustomTicketDto>>(resultGetTicketList.Item2.ResultDto);

                                    DevExpressLoadResultDto<IEnumerable<CustomDto.CustomTicketDto>> returnDevExpressLoadResult =
                                                                                            new(dtoList, resultGetTicketList.Item2.LoadResultObj);

                                    return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomTicketDto>>>(resultGetTicketList.Item1, returnDevExpressLoadResult);
                                }
                                else
                                {
                                    return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomTicketDto>>>(resultGetTicketList.Item1, null);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomTicketDto>>>(Errors.General.NotFoundDatabaseServer(), null);
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

        /// <summary>
        /// Birden çok mahal filtresi alabilir(kule olmak zorunda değil)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketWrapperWithLocationFilterNonWebAsync(BusinessLogicRequest request)
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
                            Tuple<Error, List<DevelopmentDto.TicketDto>> resultGetTicketList = await _ticketBusinessRules.GetAllTicketsWithLocationFilterNonWebAsync(request);

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

        /// <summary>
        /// Sadece tek bir mahal alır ve mahal kule olmak zorunda
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketWrapperWithTowerFilterNonWebAsync(BusinessLogicRequest request)
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
                            Tuple<Error, List<DevelopmentDto.TicketDto>> resultGetTicketList = await _ticketBusinessRules.GetAllTicketWrapperWithTowerFilterNonWebAsync(request);

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

        public async Task<Tuple<Error, CustomDto.CustomTicketDto>> GetTicketWrapperWithStatusByTicketIdAsync(int Id)
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
                            Tuple<Error, DevelopmentDto.TicketDto> resultGetTicket = await _ticketBusinessRules.GetTicketWithStatusByIdAsync(Id);

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
    }
}
