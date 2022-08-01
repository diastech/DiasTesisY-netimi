using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentAssignmentGroupInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentAssignmentGroupProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;


namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class AssignmentGroupBusinessRules : BusinessRuleAbstract, DevelopmentAssignmentGroupInterface.IAssignmentGroupBusinessRules, IBaseAssignmentGroupBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentAssignmentGroupProfile.AssignmentGroupProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new  (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public AssignmentGroupBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private AssignmentGroupBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }


        //Örnek paremetresiz Select all standart iş kuralı(generic select all kullanamadığımız durumlarda)
        public async Task<Tuple<Error, IEnumerable<DevelopmentDto.AssignmentGroupDto>>> GetAssignmentGroupListAsync()
        {
            //Contextin tipini öğrenip, iş kuralını uygulayalım
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.AssignmentGroupDto> returnDtoList = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.AssignmentGroup> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntityList = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.AssignmentGroups.AsQueryable().AsNoTracking<DevelopmentModel.AssignmentGroup>().ToList<DevelopmentModel.AssignmentGroup>());
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                    Map<List<DevelopmentModel.AssignmentGroup>, List<DevelopmentDto.AssignmentGroupDto>>(sonucEntityList);

                                return new(Errors.General.GetListSuccess("AssignmentGroup"), returnDtoList);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.GeneralServerError(), null);
                        }
                }
            }
        }

    }
}
