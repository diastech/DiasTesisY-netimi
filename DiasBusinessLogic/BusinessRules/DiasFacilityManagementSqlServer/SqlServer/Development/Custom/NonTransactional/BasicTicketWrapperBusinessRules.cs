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
using CustomDevelopmentBasicTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Classes.Dto;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class BasicTicketWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.IBasicTicketWrapperBusinessRules, IBaseBasicTicketWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.IBasicTicketBusinessRules _basicTicketBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentBasicTicketProfile.CustomBasicTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public BasicTicketWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IBasicTicketBusinessRules>())
        {
        }
        private BasicTicketWrapperBusinessRules(DevelopmentUserInterface.IBasicTicketBusinessRules basicTicketBusinessRules)
        {
            _basicTicketBusinessRules = basicTicketBusinessRules;
        }

        public async Task<Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomBasicTicketDto>>>> GetAllBasicTicketsWrapperAsync(DevExpressRequest devExpressRequestObj)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.ConnectionTimeout(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, DevExpressLoadResultDto<List<DevelopmentDto.BasicTicketDto>>> resultGetTicketList = await _basicTicketBusinessRules.GetAllBasicTicketsAsync(devExpressRequestObj);                            

                            try
                            {
                                if ((resultGetTicketList.Item1.BusinessOperationSucceed == true) && (resultGetTicketList.Item2 != null))
                                {
                                    List<CustomDto.CustomBasicTicketDto> returnDtoList = new List<CustomDto.CustomBasicTicketDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentDto.BasicTicketDto>, List<CustomDto.CustomBasicTicketDto>>(resultGetTicketList.Item2.ResultDto);


                                    DevExpressLoadResultDto<IEnumerable<CustomDto.CustomBasicTicketDto>> returnDevExpressLoadResult =
                                                                                            new(returnDtoList, resultGetTicketList.Item2.LoadResultObj);


                                    return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomBasicTicketDto>>>(resultGetTicketList.Item1, returnDevExpressLoadResult);
                                }
                                else
                                {
                                    return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomBasicTicketDto>>>(resultGetTicketList.Item1, null);
                                    //return new Tuple<Error, IEnumerable<CustomDto.CustomBasicTicketDto>>(resultGetTicketList.Item1, null);
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

        public async Task<Tuple<Error, CustomDto.CustomBasicTicketDto>> GetBasicTicketWrapperByIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomDto.CustomBasicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, DevelopmentDto.BasicTicketDto> resultGetTicket = await _basicTicketBusinessRules.GetBasicTicketByIdAsync(Id);

                            try
                            {
                                if ((resultGetTicket.Item1.BusinessOperationSucceed == true) && (resultGetTicket.Item2 != null))
                                {
                                    CustomDto.CustomBasicTicketDto returnDto = new CustomDto.CustomBasicTicketDto();
                                    CustomDto.CustomBasicTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentDto.BasicTicketDto, CustomDto.CustomBasicTicketDto>(resultGetTicket.Item2);
                                    returnDto = convertedDto;

                                    return new Tuple<Error, CustomDto.CustomBasicTicketDto>(resultGetTicket.Item1, returnDto);
                                }
                                else
                                {
                                    return new Tuple<Error, CustomDto.CustomBasicTicketDto>(resultGetTicket.Item1, null);
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
