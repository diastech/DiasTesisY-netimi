using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test;
using DiasBusinessLogic.Shared.Configuration;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using Microsoft.EntityFrameworkCore.Storage;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
{
    public class PeriodicTicketWrapperTransactionalBusinessRules : BusinessRuleAbstract, TransactionalInterface.IPeriodicTicketWrapperTransactionalBusinessRules, IBasePeriodicTicketWrapperTransactionalBusinessRules
    {
        private readonly DevelopmentUserInterface.IPeriodicTicketBusinessRules _periodicTicketBusinessRules;
        private readonly IUnitOfWork_EF _unitOfWork_EF;
        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomPeriodicTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        public PeriodicTicketWrapperTransactionalBusinessRules() : this(
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IPeriodicTicketBusinessRules>(),
            DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {

        }

        public PeriodicTicketWrapperTransactionalBusinessRules(DevelopmentUserInterface.IPeriodicTicketBusinessRules periodicTicketBusinessRules,
            IUnitOfWork_EF unitOfWork_EF)
        {
            _periodicTicketBusinessRules = periodicTicketBusinessRules;
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, CustomPeriodicTicketDto>> AddPeriodicTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    if((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                          (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) || 
                                                (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomPeriodicTicketDto))))
                                    {
                                        return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.RequestNull("PeriodicTicketDto"), null);
                                    }

                                    CustomPeriodicTicketDto castedDto = JsonConvert.DeserializeObject<CustomPeriodicTicketDto>(request.RequestDtosJsons[0]);

                                    if(castedDto == null)
                                    {
                                        return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.MappingError("PeriodicTicketDto"), null);
                                    }

                                    castedDto.PeriodFrequency = "0 0/1 * 1/1 * ? *";

                                    PeriodicTicketDto periodicTicketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomPeriodicTicketDto, PeriodicTicketDto>(castedDto);

                                    Tuple<Error, PeriodicTicketDto> resultAddedPeriodicTicket = await _periodicTicketBusinessRules.AddAsync(periodicTicketDto);

                                    if ((resultAddedPeriodicTicket.Item1.BusinessOperationSucceed == true) && (resultAddedPeriodicTicket.Item2 != null))
                                    {
                                        transaction.Commit();

                                        CustomPeriodicTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<PeriodicTicketDto, CustomPeriodicTicketDto>(periodicTicketDto);

                                        return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.SuccessInsert("PeriodicTicketDto"), convertedDto);
                                    }
                                    else
                                    {
                                        return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.ErrorInsert("PeriodicTicketDto"), castedDto);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.ErrorInsert("PeriodicTicketDto"), null);
                                }
                            }
                        }

                    default:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomPeriodicTicketDto>> UpdatePeriodicTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                          (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomPeriodicTicketDto))))
                                    {
                                        return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.RequestNull("PeriodicTicketDto"), null);
                                    }
                                    CustomPeriodicTicketDto castedDto = JsonConvert.DeserializeObject<CustomPeriodicTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.MappingError("PeriodicTicketDto"), null);
                                    }


                                    PeriodicTicketDto periodicTicketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomPeriodicTicketDto, PeriodicTicketDto>(castedDto);


                                    Tuple<Error, PeriodicTicketDto> resultUpdatedPeriodicTicket = await _periodicTicketBusinessRules.UpdateAsync(periodicTicketDto);
                                    if ((resultUpdatedPeriodicTicket.Item1.BusinessOperationSucceed == true) && (resultUpdatedPeriodicTicket.Item2 != null))
                                    {
                                        return null;
                                    }

                                    return null;

                                }

                                catch (Exception)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.ErrorUpdate("PeriodicTicketDto"), null);
                                }
                            }
                           
                        }

                    default:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }

        }

        public async Task<Tuple<Error, CustomPeriodicTicketDto>> DeletePeriodicTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                          (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomPeriodicTicketDto))))
                                    {
                                        return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.RequestNull("PeriodicTicketDto"), null);
                                    }

                                    CustomPeriodicTicketDto castedDto = JsonConvert.DeserializeObject<CustomPeriodicTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.MappingError("PeriodicTicketDto"), null);
                                    }


                                    PeriodicTicketDto periodicTicketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomPeriodicTicketDto, PeriodicTicketDto>(castedDto);
                                    
                                    Tuple<Error, PeriodicTicketDto> resultDeletedPeriodicTicket = await _periodicTicketBusinessRules.DeleteAsync(periodicTicketDto);
                                    if ((resultDeletedPeriodicTicket.Item1.BusinessOperationSucceed == true) && (resultDeletedPeriodicTicket.Item2 != null))
                                    {

                                    }
                                    return null;

                                }

                                catch (Exception)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.ErrorDelete("PeriodicTicketDto"), null);
                                }
                            }                            
                        }

                    default:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
    }
}
