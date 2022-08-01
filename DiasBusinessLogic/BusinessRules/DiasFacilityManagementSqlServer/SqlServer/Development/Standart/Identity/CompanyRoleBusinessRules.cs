using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentLocationInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using System.Threading.Tasks;
using DiasShared.Errors;
using System;
using DiasBusinessLogic.Shared.Error;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class CompanyRoleBusinessRules : BusinessRuleAbstract, DevelopmentLocationInterface.ICompanyRoleBusinessRules, IBaseCompanyRoleBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentLocationProfile.CompanyRoleProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
                    => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public CompanyRoleBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private CompanyRoleBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        //TODO : Errorlar güncellenecek
        public async Task<Tuple<Error, DevelopmentDto.CompanyRoleDto>> GetCompanyRoleByNormalizedNameAsync(string normalizedRole)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                DevelopmentDto.CompanyRoleDto selectedDto;
                                DevelopmentModel.CompanyRole existentRecord;
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    existentRecord = (DevelopmentModel.CompanyRole)await Task.Run(() => DiasFacilityManagementSqlServerContext.CompanyRoles.AsQueryable().Where(x => x.NormalizedName == normalizedRole)
                                        .AsNoTracking<DevelopmentModel.CompanyRole>().Single<DevelopmentModel.CompanyRole>());

                                    selectedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.CompanyRole, DevelopmentDto.CompanyRoleDto>(existentRecord);

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                }

                                return new(Errors.General.Success("CompanyRole"), selectedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (ArgumentNullException)//kayıt yok
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (InvalidOperationException)//birden çok kayıt
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


        //TODO : Errorlar güncellenecek
        public async Task<Tuple<Error, IEnumerable<DevelopmentDto.CompanyRoleDto>>> GetCompanyRolesByIdsAsync(List<int> roleIds)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                IEnumerable<DevelopmentDto.CompanyRoleDto> selectedDtos;
                                IEnumerable<DevelopmentModel.CompanyRole> existentRecords;

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    existentRecords = await Task.Run(() => DiasFacilityManagementSqlServerContext.CompanyRoles.AsQueryable().Where(item => roleIds.Contains(item.Id)).
                                        AsNoTracking<DevelopmentModel.CompanyRole>());

                                    selectedDtos = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<IEnumerable<DevelopmentModel.CompanyRole>, IEnumerable<DevelopmentDto.CompanyRoleDto>>(existentRecords);

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                }

                                return new(Errors.General.Success("CompanyRole"), selectedDtos);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (ArgumentNullException)//kayıt yok
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (InvalidOperationException)//birden çok kayıt
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
