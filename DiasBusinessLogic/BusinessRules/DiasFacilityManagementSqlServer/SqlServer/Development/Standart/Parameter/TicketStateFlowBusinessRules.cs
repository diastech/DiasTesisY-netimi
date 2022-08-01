using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentLocationInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using System.Threading.Tasks;
using System;
using DiasShared.Errors;
using System.Collections.Generic;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasBusinessLogic.Shared.Error;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class TicketStateFlowBusinessRules : BusinessRuleAbstract, DevelopmentLocationInterface.ITicketStateFlowBusinessRules, IBaseTicketStateFlowBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentLocationProfile.TicketStateFlowProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public TicketStateFlowBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private TicketStateFlowBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, List<TicketStateFlowDto>>> GetAllTicketAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.TicketStateFlowDto> returnDtoList = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.TicketStateFlow> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntityList = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.TicketStateFlows.AsQueryable().Where(x => x.IsDeleted == false && x.IsActive == true).Include(x=>x.TicketStateTransitionFlowByTicketStateFlows)
                                        .AsNoTracking<DevelopmentModel.TicketStateFlow>().ToList<DevelopmentModel.TicketStateFlow>());
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentModel.TicketStateFlow>, List<DevelopmentDto.TicketStateFlowDto>>(sonucEntityList);

                                return new(Errors.General.GetListSuccess("TicketStateFlow"), returnDtoList);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception e)
                            {
                                return new(Errors.General.GridListError("TicketStateFlow"), null);
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
