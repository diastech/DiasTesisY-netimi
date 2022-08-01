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
using Microsoft.AspNetCore.Identity;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using DiasShared.Errors;
using System.Collections.Generic;
using DiasBusinessLogic.Shared.Error;
using DiasBusinessLogic.Shared.Classes.Identity.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.CustomIdentity.Development;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static DiasShared.Enums.Standart.UserEnums;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class CompanyRoleClaimBusinessRules : BusinessRuleAbstract, DevelopmentLocationInterface.ICompanyRoleClaimBusinessRules, IBaseCompanyRoleClaimBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentLocationProfile.CompanyRoleClaimProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
                            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public CompanyRoleClaimBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {

        }

        private CompanyRoleClaimBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>>> TestAddCompanyRoleClaimAsync()
        {
            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

            IUserStore<User> userStore = new CustomUserStore();
            UserManager<User> userManager = new UserManager<User>(userStore, null, new PasswordHasher<User>(), null, null, null, null, null, null);


            ICustomRoleStore<CompanyRole> companyRoleStore = new CustomRoleStore();
            CustomRoleManager<CompanyRole> roleManager = new CustomRoleManager<CompanyRole>(companyRoleStore, null, null, null, null);


            CompanyRole administratorRole = await roleManager.FindByNameAsync("ADMINISTRATOR");
            CompanyRole facilityManagerRole = await roleManager.FindByNameAsync("FACILITYMANAGER");
            CompanyRole teamMemberRole = await roleManager.FindByNameAsync("TEAMMEMBER");

            List<DevelopmentDto.CompanyRoleClaimDto> insertedDtos = new List<DevelopmentDto.CompanyRoleClaimDto>();

            if (administratorRole != null)
            {
                DevelopmentDto.CompanyRoleClaimDto insertedDto = 
                    await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "7:0000000000000000000000000000000000000000000000000000000000000001"), 7);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "21:0000000000000000000000000000000000000000000000000000000010011011"), 21);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "17:0000000000000000000000000000000000000000000000000000000000000001"), 17);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "27:0000000000000000000000000000000000000000000000000000000000000001"), 27);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "41:0000000000000000000000000000000000000000000000000000000000000001"), 41);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "15:0000000000000000000000000000000000000000000000000000000000011101"), 15);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "38:0000000000000000000000000000000000000000000000000000000000000011"), 38);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "32:0000000000000000000000000000000000000000000000000000000000000011"), 32);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "4:0000000000000000000000000000000000000000000000000000000000011101"), 4);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(administratorRole, new Claim("CompanyRoleAuthorizationCode", "20:0000000000000000000000000000000000000000000000000000000000011101"), 20);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }


            }

            if (facilityManagerRole != null)
            {
                DevelopmentDto.CompanyRoleClaimDto insertedDto =
                    await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "7:0000000000000000000000000000000000000000000000000000000000000001"), 7);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "21:0000000000000000000000000000000000000000000000000000000010011011"), 21);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "17:0000000000000000000000000000000000000000000000000000000000000001"), 17);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "27:0000000000000000000000000000000000000000000000000000000000000001"), 27);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "41:0000000000000000000000000000000000000000000000000000000000000001"), 41);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "15:0000000000000000000000000000000000000000000000000000000000000001"), 15);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "38:0000000000000000000000000000000000000000000000000000000000000001"), 38);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "32:0000000000000000000000000000000000000000000000000000000000000011"), 32);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "4:0000000000000000000000000000000000000000000000000000000000000001"), 4);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(facilityManagerRole, new Claim("CompanyRoleAuthorizationCode", "20:0000000000000000000000000000000000000000000000000000000000000001"), 20);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }


            }

            if (teamMemberRole != null)
            {
                DevelopmentDto.CompanyRoleClaimDto insertedDto =
                    await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "7:0000000000000000000000000000000000000000000000000000000000000001"), 7);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "21:0000000000000000000000000000000000000000000000000000000000000001"), 21);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "17:0000000000000000000000000000000000000000000000000000000000000001"), 17);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "27:0000000000000000000000000000000000000000000000000000000000000001"), 27);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "41:0000000000000000000000000000000000000000000000000000000000000001"), 41);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "15:0000000000000000000000000000000000000000000000000000000000000001"), 15);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "38:0000000000000000000000000000000000000000000000000000000000000011"), 38);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "32:0000000000000000000000000000000000000000000000000000000000000001"), 32);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "4:0000000000000000000000000000000000000000000000000000000000000001"), 4);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }

                insertedDto = await roleManager.AddClaimV2Async(teamMemberRole, new Claim("CompanyRoleAuthorizationCode", "20:0000000000000000000000000000000000000000000000000000000000000001"), 20);

                if (insertedDto != null)
                {
                    insertedDtos.Add(insertedDto);
                }
            }


            if (insertedDtos.Count < 1)
            {
                return new(Errors.General.GeneralServerError(), null);
            }
            else
            {
                return new(Errors.General.Success("CompanyRoleClaimDto"), insertedDtos);
            }
        }


        public async Task<Tuple<Error, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>>> GetCompanyRoleClaimsByCompanyRoleIdAsync(int companyRoleId)
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

                                IEnumerable<DevelopmentDto.CompanyRoleClaimDto> selectedDtos;
                                IEnumerable<DevelopmentModel.CompanyRoleClaim> existentRecords;

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    existentRecords = await Task.Run(() => DiasFacilityManagementSqlServerContext.CompanyRoleClaims.AsQueryable().Where(p => p.RoleId == companyRoleId).
                                        AsNoTracking<DevelopmentModel.CompanyRoleClaim>());

                                    selectedDtos = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<IEnumerable<DevelopmentModel.CompanyRoleClaim>, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>>(existentRecords);

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                }

                                return new(Errors.General.Success("CompanyRoleClaimDto"), selectedDtos);
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

        public async Task<Tuple<Error, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>>> 
            GetCompanyRoleClaimsByCompanyRoleIdAndRemoteDomainIdAsync(int companyRoleId, int remoteDomainId)
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

                                IEnumerable<DevelopmentDto.CompanyRoleClaimDto> selectedDtos;
                                IEnumerable<DevelopmentDto.CompanyRoleClaimDto> selectedDtosWithTicketRole;
                                IEnumerable<DevelopmentModel.CompanyRoleClaim> existentRecords;
                                IEnumerable<DevelopmentModel.CompanyRoleClaim> existentRecordsWithTicketRole;
                                List<DevelopmentDto.CompanyRoleClaimDto> fullListselectedDtos;

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    existentRecords = await Task.Run(() => DiasFacilityManagementSqlServerContext.CompanyRoleClaims.AsQueryable().
                                        Where(p => (p.RoleId == companyRoleId) && (p.RestClientTypeId == remoteDomainId)).
                                            AsNoTracking<DevelopmentModel.CompanyRoleClaim>());

                                    existentRecordsWithTicketRole = await Task.Run(() => DiasFacilityManagementSqlServerContext.CompanyRoleClaims.AsQueryable().
                                        Where(p => (p.RoleId == (int)(UserRolesTypes.DEFAULT)) && (p.TicketStateRoleId.HasValue) && (p.RestClientTypeId == remoteDomainId)).
                                            AsNoTracking<DevelopmentModel.CompanyRoleClaim>());

                                    selectedDtos = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<IEnumerable<DevelopmentModel.CompanyRoleClaim>, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>>(existentRecords);

                                    selectedDtosWithTicketRole = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<IEnumerable<DevelopmentModel.CompanyRoleClaim>, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>>(existentRecordsWithTicketRole);

                                    fullListselectedDtos = selectedDtos.ToList();
                                    fullListselectedDtos.AddRange(selectedDtosWithTicketRole);

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                }

                                return new(Errors.General.Success("CompanyRoleClaimDto"), fullListselectedDtos);
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
