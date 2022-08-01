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
    public class CompanyRoleUserBusinessRules : BusinessRuleAbstract, DevelopmentLocationInterface.ICompanyRoleUserBusinessRules, IBaseCompanyRoleUserBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentLocationProfile.CompanyRoleUserProfile> DtoMapper_IdentityManagementSqlServer_Development
            => new  (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public CompanyRoleUserBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private CompanyRoleUserBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        //TODO : Errorlar güncellenecek
        public async Task<Tuple<Error, IEnumerable<DevelopmentDto.CompanyRoleUserDto>>> GetCompanyRoleUsersByUserIdAsync(int userId)
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
                                IEnumerable<DevelopmentDto.CompanyRoleUserDto> selectedDtos;
                                IEnumerable<DevelopmentModel.CompanyRoleUser> existentRecords;

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    existentRecords = await Task.Run(() => DiasFacilityManagementSqlServerContext.CompanyRoleUsers.AsQueryable().Where(x => x.UserId == userId)
                                        .AsNoTracking<DevelopmentModel.CompanyRoleUser>());

                                    selectedDtos = DtoMapper_IdentityManagementSqlServer_Development.Map<IEnumerable<DevelopmentModel.CompanyRoleUser>, IEnumerable<DevelopmentDto.CompanyRoleUserDto>>(existentRecords);

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                }

                                return new(Errors.General.Success("CompanyRoleUser"), selectedDtos);
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
