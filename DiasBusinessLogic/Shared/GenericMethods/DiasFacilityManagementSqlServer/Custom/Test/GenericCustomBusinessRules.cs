using AutoMapper;
using DiasBusinessLogic.AutoMapper.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasBusinessLogic.Shared.Configuration;
using DiasDataAccessLayer.Shared.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using DiasShared.Operations.EnumOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using static DiasBusinessLogic.Shared.Enums.EntityDtoMapping;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;


namespace DiasBusinessLogic.Shared.GenericMethods.DiasFacilityManagementSqlServer.Custom.Test
{
    public class GenericCustomBusinessRules<TDto, TAutomapperProfile> : IGenericCustomBusinessRules<TDto, TAutomapperProfile> where TDto : IBaseDevelopmentCustomDto,  new()
                                                                                                                                     where TAutomapperProfile : Profile, new()     
    {
        //Tam Generic AutoMapper mapper nesnemiz, statikdir 
        private static AutoMapperProfileMapper<TAutomapperProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
                                => new ((TAutomapperProfile)Activator.CreateInstance(typeof(TAutomapperProfile),
                                         new object[] { DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer") }));

        //İş kuralı için UoW injected interface imiz
        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public async Task<Tuple<ErrorCodes, IEnumerable<TDto>>> GetAll()
        {
            //Dtodan maplemeye bakarak Entity sınıfını bul
            //Olası duplicate isimli sınıflara karşılık assembly ve namespaceli isim alalım
            string dtoClassFullName = typeof(TDto).FullName;
            EntityDtoMap entityDtoMapEnum = dtoClassFullName.GetValueFromDescription<EntityDtoMap>();
            string workingEntityFullName = entityDtoMapEnum.GetDisplayOrValueFromEnum<EntityDtoMap>().ToString();

            //Contextin tipini öğrenip, iş kuralını uygulayalım
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new (ErrorCodes.UnknownError, null);
            }
            else
            {
                List<TDto> returnDtoList = new ();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new ();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                Type genericListEntityType = typeof(List<>).MakeGenericType(tEntity);
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = null;

                                //Contexte böyle bir tablo(entity) var mı?
                                foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                {
                                    if (pi.PropertyType == genericListEntityDbSetType)
                                    {
                                        //Tüm tabloyu çek
                                        existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));
                                    }
                                }

                                if (existentRecordList != null)
                                {
                                    //Entity listesini Dto listesine çevirelim
                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<TDto>>(existentRecordList, null);

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                    return new (ErrorCodes.None, returnDtoList);
                                }
                                else
                                {
                                    return new (ErrorCodes.UnknownError, null);
                                }
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new (ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new (ErrorCodes.UnknownError, null);
                            }
                            catch (Exception)
                            {
                                return new (ErrorCodes.UnknownError, null);
                            }
                        }

                    default:
                        {
                            return new (ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        public async Task<Tuple<ErrorCodes, TDto>> GetByIdFromInt(int id)
        {
            ////Dtodan maplemeye bakarak Entity sınıfını bul
            ////Olası duplicate isimli sınıflara karşılık assembly ve namespaceli isim alalım
            //string dtoClassFullName = typeof(TDto).FullName;
            //EntityDtoMap entityDtoMapEnum = dtoClassFullName.GetValueFromDescription<EntityDtoMap>();
            //string workingEntityFullName = entityDtoMapEnum.GetDisplayOrValueFromEnum<EntityDtoMap>().ToString();

            ////Contextin tipini öğrenip, iş kuralını uygulayalım
            //Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            //if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            //{
            //    return new (ErrorCodes.UnknownError, null);
            //}
            //else
            //{
            //    TDto returnDto = new();

            //    switch (dataContextType.Name)
            //    {
            //        case "DiasFacilityManagementSqlServer":
            //            {
            //                try
            //                {
            //                    DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new ();

            //                    //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
            //                    Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
            //                    //Dto ya karşılık gelen entity'in tipini al
            //                    Type tEntity = dalAssembly.GetType(workingEntityFullName);

            //                    Type genericListEntityType = typeof(List<>).MakeGenericType(tEntity);
            //                    Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

            //                    dynamic existentRecordList = null;

            //                    //Contexte böyle bir tablo(entity) var mı?
            //                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
            //                    {
            //                        if (pi.PropertyType == genericListEntityDbSetType)
            //                        {
            //                            //Tüm tabloyu çek
            //                            existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));
            //                        }
            //                    }

            //                    if (existentRecordList != null)
            //                    {
            //                        //Entity listesini Dto listesine çevirelim
            //                        foreach (var item in existentRecordList)
            //                        {
            //                            TDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(item, null);
            //                            returnDtoList.Add(convertedDto);
            //                        }

            //                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

            //                        return new (ErrorCodes.None, returnDtoList);
            //                    }
            //                    else
            //                    {
            //                        return new (ErrorCodes.UnknownError, null);
            //                    }
            //                }
            //                catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
            //                {
            //                    return new (ErrorCodes.UnknownError, null);
            //                }
            //                catch (ArgumentNullException)
            //                {
            //                    return new (ErrorCodes.UnknownError, null);
            //                }
            //                catch (Exception)
            //                {
            //                    return new (ErrorCodes.UnknownError, null);
            //                }
            //            }

            //        default:
            //            {
            //                return new (ErrorCodes.UnknownError, null);
            //            }
            //    }
            //}

            return null;
        }

        public async Task<Tuple<ErrorCodes, IEnumerable<TDto>>> DeleteFromInt(int id)
        {
            ////Contextin tipini öğrenip, iş kuralını uygulayalım
            //Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            //if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            //{
            //    return new (ErrorCodes.UnknownError, null);
            //}
            //else
            //{
            //    List<TDto> returnDtoList = new ();

            //    switch (dataContextType.Name)
            //    {
            //        case "DiasFacilityManagementSqlServer":
            //            {
            //                try
            //                {
            //                    DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new ();

            //                    TDto updatedDto;
            //                    TEntity existentRecord;

            //                    using (DiasFacilityManagementSqlServerContext)
            //                    {
            //                        //Böyle bir kayıt var mı bakalım                                
            //                        existentRecord = await Task.Run(() => DiasFacilityManagementSqlServerContext.Users.
            //                                                    Where(p => p.Id == userId).AsNoTracking().Single<DevelopmentModel.User>());

            //                        existentRecord.IsActive = Convert.ToBoolean(userStatusId);

            //                        DiasFacilityManagementSqlServerContext.Entry(existentRecord).Property(p => p.IsActive).IsModified = true;

            //                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
            //                    }

            //                    updatedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.User, DevelopmentDto.UserDto>(existentRecord);

            //                    //başarılı
            //                    return new (ErrorCodes.None, updatedDto);
            //                }
            //                catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
            //                {
            //                    return new (ErrorCodes.UnknownError, null);
            //                }
            //                catch (ArgumentNullException)
            //                {
            //                    return new (ErrorCodes.UnknownError, null);
            //                }
            //                catch (InvalidOperationException)
            //                {
            //                    return new (ErrorCodes.UnknownError, null);
            //                }
            //                catch (Exception)
            //                {
            //                    return new (ErrorCodes.UnknownError, null);
            //                }
            //            }

            //        default:
            //            {
            //                return new (ErrorCodes.UnknownError, null);
            //            }
            //    }
            //}




            //var entity = await GetByIdFromInt(id);

            return null;
        }

        public async Task<Tuple<ErrorCodes, TDto>> DeleteFromLong(long id)
        {
            throw new NotImplementedException();
        }

       

        public async Task<Tuple<ErrorCodes, TDto>> GetByIdFromLong(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<ErrorCodes, TDto>> Insert(TDto insertedDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<ErrorCodes, TDto>> Update(TDto updatedDto)
        {
            throw new NotImplementedException();
        }
    }
}
