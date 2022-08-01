using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentUserProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DevelopmentModel = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Test.Standart
{
    public class UserBusinessRules : BusinessRuleAbstract, DevelopmentUserInterface.IUserBusinessRules, IBaseUserBusinessRules
    {
        //İş kuralına özel profil generic AutoMapper mapper nesnemiz, statikdir
        private static AutoMapperProfileMapper<DevelopmentUserProfile.UserProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

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
        public async Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.UserDto>>> GetUserListAsync()
        {
            //Contextin tipini öğrenip, iş kuralını uygulayalım
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
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
                                List<DevelopmentModel.User> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntityList = await Task.Run(() => 
                                    DiasFacilityManagementSqlServerContext.Users.AsQueryable().AsNoTracking<DevelopmentModel.User>().ToList<DevelopmentModel.User>());
                                }

                                //TODO: IsActive 'e göre tekrar düzenlenecek
                                //Entity deki status enum tanımlı değilse hata gönder 
                                //if (!(Enum.IsDefined(typeof(UserStatusTypes), item.StatusId)))
                                //{
                                //    return new  (ErrorCodes.UndefinedUserStatusTypesEnumValue, null);
                                //}

                                //TODO: Roller belirtilince tekrar yazılacak
                                //Entity deki role enum tanımlı değilse hata gönder 
                                //if (!(Enum.IsDefined(typeof(UserRolesTypes), item.UserTypeId)))
                                //{
                                //    return new (ErrorCodes.UndefinedUserRoleTypesEnumValue, null);
                                //}

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                    Map<List<DevelopmentModel.User>, List<DevelopmentDto.UserDto>>(sonucEntityList);

                                return new(ErrorCodes.None, returnDtoList);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception)
                            {
                                return new(ErrorCodes.UnknownErrorOnGettingEntityFromUserTable, null);
                            }
                        }

                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        //Örnek paremetreli Select standart iş kuralı(generic select by id kullanamadığımız durumlarda da olabilir)
        public async Task<Tuple<ErrorCodes, DevelopmentDto.UserDto>> Login(string email, string password)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
            }
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

                                    existentRecord = (User)await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.AsQueryable().Where(x => x.EmailAddress == email && x.IsActive == true
                                    && x.IsDeleted == false).AsNoTracking<DevelopmentModel.User>());

                                    selectedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.User, DevelopmentDto.UserDto>(existentRecord);

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                }

                                return new(ErrorCodes.None, selectedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (InvalidOperationException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                        }

                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        //Örnek standart  iş kuralı  bir property ile update olacaksa
        public async Task<Tuple<ErrorCodes, DevelopmentDto.UserDto>> MakeUserActiveOrPassiveByUserIdAsync(int userId, int userStatusId)
        {
            //Contextin tipini öğrenip, iş kuralını uygulayalım
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.UnknownError, null);
            }
            //Parametredeki status enum tanımlı değilse hata gönder 
            else if (!(Enum.IsDefined(typeof(UserStatusTypes), userStatusId)))
            {
                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.UnknownError, null);
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
                                DevelopmentModel.User existentRecord;

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Böyle bir kayıt var mı bakalım, yoksa exception fırlatır                               
                                    existentRecord = await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.AsQueryable().
                                                                Where(p => p.Id == userId).AsNoTracking<DevelopmentModel.User>().Single<DevelopmentModel.User>());

                                    existentRecord.IsActive = Convert.ToBoolean(userStatusId);

                                    DiasFacilityManagementSqlServerContext.Entry(existentRecord).Property(p => p.IsActive).IsModified = true;

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                }

                                updatedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.User, DevelopmentDto.UserDto>(existentRecord);

                                //başarılı
                                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.None, updatedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>
                                    (ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.UnknownError, null);
                            }
                            catch (InvalidOperationException)
                            {
                                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception)
                            {
                                return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.UnknownError, null);
                            }
                        }

                    default:
                        {
                            return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        //Örnek insert custom iş kuralı (generic insert kullanılamadığı durumlarda)
        public async Task<Tuple<ErrorCodes, DevelopmentDto.UserDto>> AddUserAsync(DevelopmentDto.UserDto userDto)
        {
            //Contextin tipini öğrenip, iş kuralını uygulayalım
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

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

                                DevelopmentModel.User convertedEntity =
                                    DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentDto.UserDto, DevelopmentModel.User>(userDto);


                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Mükerrer kayıt var mı bakalım                                
                                    IQueryable<DevelopmentModel.User> testDuplicateRecord = await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.AsQueryable()
                                            .Where(p => (p.EmailAddress == convertedEntity.EmailAddress)).AsNoTracking<DevelopmentModel.User>());

                                    //Varsa hata döndür
                                    if ((testDuplicateRecord != null) && (testDuplicateRecord.Count<DevelopmentModel.User>() > 0))
                                    {
                                        return new Tuple<ErrorCodes, DevelopmentDto.UserDto>
                                            (ErrorCodes.UnknownError, null);
                                    }


                                    await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.AddAsync(convertedEntity));
                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                }

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.User, DevelopmentDto.UserDto>(convertedEntity);

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
