using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentUserProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DevelopmentModel = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using ModelIdentityDev = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Operations.JwtOperation;
using DiasShared.Operations.SecurityOperations;
using Microsoft.AspNetCore.Identity;
using DiasBusinessLogic.Shared.Classes.Identity.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.CustomIdentity.Development;
using System.Security.Claims;
using DiasShared.Services.Communication.BusinessLogicMessage;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class UserBusinessRules : BusinessRuleAbstract, DevelopmentUserInterface.IUserBusinessRules, IBaseUserBusinessRules
    {
        //İş kuralına özel profil generic AutoMapper mapper nesnemiz, statikdir
        private static AutoMapperProfileMapper<DevelopmentUserProfile.UserProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        //İş kuralı için UoW injected interface imiz
        private readonly IUnitOfWork_EF _unitOfWork_EF;

        //Simple Injector Service Factory vasıtasıyla iş kuralında kullanacağımız UoW nesnemizi inject edelim
        public UserBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        //Sadece boş kurucu metod tarafından çağrılır
        private UserBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        //Örnek paremetresiz Select all standart iş kuralı(generic select all kullanamadığımız durumlarda)
        public async Task<Tuple<Error, IEnumerable<DevelopmentDto.UserDto>>> GetUserListAsync()
        {
            //Contextin tipini öğrenip, iş kuralını uygulayalım
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.UserDto> returnDtoList = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<ModelIdentityDev.User> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntityList = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.Users.AsQueryable().AsNoTracking<ModelIdentityDev.User>().ToList<ModelIdentityDev.User>());
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                    Map<List<ModelIdentityDev.User>, List<DevelopmentDto.UserDto>>(sonucEntityList);

                                return new(Errors.General.GetListSuccess("User"), returnDtoList);
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

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, DevelopmentDto.UserDto>> Login(string email, string password)
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
                                DevelopmentDto.UserDto selectedDto;
                                DevelopmentModel.User existentRecord;
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    existentRecord = (DevelopmentModel.User)await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.AsQueryable().Where(x => x.Email == email)
                                        .AsNoTracking<DevelopmentModel.User>().Single<DevelopmentModel.User>());                                    

                                    //Hash kontrolü
                                    SaltOperations saltOperationsObj = new SaltOperations();

                                    PasswordVerificationResult hashVerificationObj =
                                        saltOperationsObj.VerifyHashedPassword(existentRecord.PasswordHash, password);

                                    switch (hashVerificationObj)
                                    {
                                        case PasswordVerificationResult.Failed:
                                            {
                                                //MoreOrLessThanOneUserRecordByLoginInformationOnUserTable
                                                return new(Errors.General.GeneralServerError(), null);
                                            }

                                        case PasswordVerificationResult.SuccessRehashNeeded:
                                            {
                                                //DeprecatedHashMechanismUsedOnUserPassword
                                                return new(Errors.General.GeneralServerError(), null);
                                            }

                                        case PasswordVerificationResult.Success:
                                            {
                                                break;
                                            }

                                        default:
                                            {
                                                //UnknownHashMechanismUsedOnUserPassword
                                                return new(Errors.General.GeneralServerError(), null);
                                            }
                                    }

                                    //Rolünü al
                                    ICustomUserStore<DevelopmentModel.User> userStore = new CustomUserStore();
                                    CustomUserManager<DevelopmentModel.User> userManager = new CustomUserManager<DevelopmentModel.User>(userStore, null, new PasswordHasher<DevelopmentModel.User>(), null, null, null, null, null, null);

                                    ICustomRoleStore<DevelopmentModel.CompanyRole> companyRoleStore = new CustomRoleStore();
                                    CustomRoleManager<DevelopmentModel.CompanyRole> roleManager = new CustomRoleManager<DevelopmentModel.CompanyRole>(companyRoleStore, null, null, null, null);

                                    //rol listesi döner
                                    IList<DevelopmentModel.CompanyRole> userRoles = await userManager.GetRolesV2Async(existentRecord);

                                    IList<Claim> roleClaims = new List<Claim>();

                                    //Rollere ait authorization claimleri getir
                                    foreach (DevelopmentModel.CompanyRole item in userRoles)
                                    {
                                       roleClaims =  await roleManager.GetClaimsAsync(item);
                                    }

                                    //create a identity and add claims to the user which we want to log in
                                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(roleClaims);                                    

                                    selectedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.User, DevelopmentDto.UserDto>(existentRecord);

                                    //jwt token'ı üret, Dto'ya ata
                                    selectedDto.JwtToken =  JwtOperations.ProduceNewJwtToken(email, existentRecord.Id, roleClaims);
                                   

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                }

                                return new(Errors.General.Success("User"), selectedDto);
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
                            catch (Exception ex)
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

        public async Task<Tuple<Error, DevelopmentDto.UserDto>> LoginV2(DevelopmentCustomDto.UserCredentialsDto request)
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
                                DevelopmentDto.UserDto selectedDto;
                                DevelopmentModel.User existentRecord;

                                if (!(String.IsNullOrEmpty(request.EmailAddress)))
                                {
                                    using (DiasFacilityManagementSqlServerContext)
                                    {
                                        existentRecord = (DevelopmentModel.User)await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.AsQueryable().Where(x => x.Email == request.EmailAddress)
                                            .AsNoTracking<DevelopmentModel.User>().Single<DevelopmentModel.User>());

                                        //Hash kontrolü
                                        SaltOperations saltOperationsObj = new SaltOperations();

                                        if (!(String.IsNullOrEmpty(request.Password)))
                                        {
                                            PasswordVerificationResult hashVerificationObj =
                                            saltOperationsObj.VerifyHashedPassword(existentRecord.PasswordHash, request.Password);

                                            switch (hashVerificationObj)
                                            {
                                                case PasswordVerificationResult.Failed:
                                                    {
                                                        //MoreOrLessThanOneUserRecordByLoginInformationOnUserTable
                                                        return new(Errors.General.GeneralServerError(), null);
                                                    }

                                                case PasswordVerificationResult.SuccessRehashNeeded:
                                                    {
                                                        //DeprecatedHashMechanismUsedOnUserPassword
                                                        return new(Errors.General.GeneralServerError(), null);
                                                    }

                                                case PasswordVerificationResult.Success:
                                                    {
                                                        break;
                                                    }

                                                default:
                                                    {
                                                        //UnknownHashMechanismUsedOnUserPassword
                                                        return new(Errors.General.GeneralServerError(), null);
                                                    }
                                            }
                                        }

                                        //Rolünü al
                                        ICustomUserStore<DevelopmentModel.User> userStore = new CustomUserStore();
                                        CustomUserManager<DevelopmentModel.User> userManager = new CustomUserManager<DevelopmentModel.User>(userStore, null, new PasswordHasher<DevelopmentModel.User>(), null, null, null, null, null, null);

                                        ICustomRoleStore<DevelopmentModel.CompanyRole> companyRoleStore = new CustomRoleStore();
                                        CustomRoleManager<DevelopmentModel.CompanyRole> roleManager = new CustomRoleManager<DevelopmentModel.CompanyRole>(companyRoleStore, null, null, null, null);

                                        //rol listesi döner
                                        IList<DevelopmentModel.CompanyRole> userRoles = await userManager.GetRolesV2Async(existentRecord);

                                        IList<Claim> roleClaims = new List<Claim>();

                                        //Rollere ait authorization claimleri getir
                                        foreach (DevelopmentModel.CompanyRole item in userRoles)
                                        {
                                            roleClaims = await roleManager.GetClaimsV2Async(item, request.ClientId);
                                        }

                                        //create a identity and add claims to the user which we want to log in
                                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(roleClaims);

                                        selectedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.User, DevelopmentDto.UserDto>(existentRecord);

                                        //jwt token'ı üret, Dto'ya ata
                                        selectedDto.JwtToken = JwtOperations.ProduceNewJwtToken(request.EmailAddress, existentRecord.Id, roleClaims);


                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                    }

                                    return new(Errors.General.Success("User"), selectedDto);
                                }
                                else
                                {
                                    return new(Errors.General.GeneralServerError(), null);
                                }
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
                            catch (Exception ex)
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


        //Örnek standart  iş kuralı  bir property ile update olacaksa
        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, DevelopmentDto.UserDto>> MakeUserActiveOrPassiveByUserIdAsync(int userId, int userStatusId)
            {
                //Contextin tipini öğrenip, iş kuralını uygulayalım
                Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

                if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
                {
                    return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.NotFoundDatabaseServer(), null);
                }
                //Parametredeki status enum tanımlı değilse hata gönder 
                else if (!(Enum.IsDefined(typeof(UserStatusTypes), userStatusId)))
                {
                    return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                }
                else
                {
                    switch (dataContextType.Name)
                    {
                        case "DiasFacilityManagementSqlServer":
                            {
                                try
                                {
                                    DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new DevelopmentContext.DiasFacilityManagementSqlServer();

                                    DevelopmentDto.UserDto updatedDto;
                                    ModelIdentityDev.User existentRecord;

                                    using (DiasFacilityManagementSqlServerContext)
                                    {
                                        //Böyle bir kayıt var mı bakalım, yoksa exception fırlatır                               
                                        existentRecord = await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.AsQueryable().
                                                                    Where(p => p.Id == userId).AsNoTracking<ModelIdentityDev.User>().Single<ModelIdentityDev.User>());

                                        existentRecord.IsActive = Convert.ToBoolean(userStatusId);

                                        DiasFacilityManagementSqlServerContext.Entry(existentRecord).Property(p => p.IsActive).IsModified = true;

                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                    }

                                    updatedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<ModelIdentityDev.User, DevelopmentDto.UserDto>(existentRecord);

                                    //başarılı
                                    return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.Success("User"), updatedDto);
                                }
                                catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                                {
                                    return new Tuple<Error, DevelopmentDto.UserDto>
                                        (Errors.General.GeneralServerError(), null);
                                }
                                catch (ArgumentNullException)
                                {
                                    return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                                }
                                catch (InvalidOperationException)
                                {
                                    return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                                }
                                catch (Exception)
                                {
                                    return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                                }
                            }

                        default:
                            {
                                return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                            }
                    }
                }
            }

        //Örnek insert custom iş kuralı (generic insert kullanılamadığı durumlarda)
        public async Task<Tuple<ErrorCodes, DevelopmentDto.UserDto>> AddUserAsync(DevelopmentDto.UserDto userDto)
        {
            //Contextin tipini öğrenip, iş kuralını uygulayalım
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>
                    (ErrorCodes.UnknownError, null);
            }            
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new DevelopmentContext.DiasFacilityManagementSqlServer();

                                DevelopmentDto.UserDto addedDto;

                                ModelIdentityDev.User convertedEntity =
                                    DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentDto.UserDto, ModelIdentityDev.User>(userDto);


                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Mükerrer kayıt var mı bakalım                                
                                    IQueryable<ModelIdentityDev.User> testDuplicateRecord = await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.AsQueryable()
                                            .Where(p => (p.Email == convertedEntity.Email)).AsNoTracking<ModelIdentityDev.User>());

                                    //Varsa hata döndür
                                    if ((testDuplicateRecord != null) && (testDuplicateRecord.Count<ModelIdentityDev.User>() > 0))
                                    {
                                        return new Tuple<ErrorCodes, DevelopmentDto.UserDto>
                                            (ErrorCodes.UnknownError, null);
                                    }


                                    await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.AddAsync(convertedEntity));
                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                }

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<ModelIdentityDev.User, DevelopmentDto.UserDto>(convertedEntity);

                                //başarılı
                                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.None, addedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>
                                    (ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>
                                                (ErrorCodes.UnknownError, null);
                            }
                            catch (Exception)
                            {
                                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>
                                    (ErrorCodes.UnknownError, null);
                            }
                        }

                    default:
                        {
                            return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        //Örnek delete standart iş kuralı aslında IsDeleted'in update(true) olmuş halidir(gerçek silme hiç bir zaman olmayacak)
     }
}
