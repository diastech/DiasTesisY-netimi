using AutoMapper;
using DiasBusinessLogic.AutoMapper.Configuration;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.Shared.Functions.Ef_Function;
using DiasDataAccessLayer.Shared.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using DiasShared.Operations.EnumOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using static DiasBusinessLogic.Shared.Enums.EntityDtoMapping;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using BaseDevelopment = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using Newtonsoft.Json;
using DiasBusinessLogic.Shared.Functions.Castings;
using System.Dynamic;
using System.Linq;
using DiasShared.Operations.JsonOperation.Resolvers;
using DiasShared.Operations.ReflectionOperations;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.Shared.GenericMethods.DiasFacilityManagementSqlServer.Standart.Development
{
    public class GenericStandartBusinessRules<TDto, TAutomapperProfile> : IGenericStandartBusinessRules<TDto, TAutomapperProfile> where TDto : BaseDevelopmentStandartDto,  new()
                                                                                                                                     where TAutomapperProfile : Profile, new()     
    { 
        //Tam Generic AutoMapper mapper nesnemiz, statikdir 
        private static AutoMapperProfileMapper<TAutomapperProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
                                => new ((TAutomapperProfile)Activator.CreateInstance(typeof(TAutomapperProfile),
                                         new object[] { DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer") }));

        //İş kuralı için UoW injected interface imiz
        private readonly IUnitOfWork_EF _unitOfWork_EF;       

        public GenericStandartBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private GenericStandartBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        //TODO : obsolete, tüm ErroCodes lar temizlenince silinecektir
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
                List<TDto> returnDtoList = new();

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

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = null;

                                using (DiasFacilityManagementSqlServerContext)
                                {
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

                                        return new(ErrorCodes.None, returnDtoList);
                                    }
                                    else
                                    {
                                        //tablo yok
                                        return new(ErrorCodes.UnknownError, null);
                                    }
                                }
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new (ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException ex)
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

        //TODO : obsolete, tüm ErroCodes lar temizlenince silinecektir
        public async Task<Tuple<ErrorCodes, TDto>> GetByIdFromInt(int id)
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
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecord = new ExpandoObject();
                                
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            dynamic existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));
                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi https://stackoverflow.com/questions/2630370/c-sharp-dynamic-cannot-access-properties-from-anonymous-types-declared-in-anot de belirtilmiş
                                            // EFCore dan gelen bir tipe atanmış dinamik obje üzerinde farklı assemblylerde çalışamıyoruz(dynamic in atandığı tip EF Core a göre internal olduğu için)
                                            //Üstad Jon Skeet'in önerdiği ne tek başına ExpandoObject ne de InternalsVisibleTo işe yaramadı
                                            //Bu durumda tek çare olarak dynamic tipini ayrı bir memory bloğuna sahip IQueryable objesine taşımaktan başka çare kalmadı
                                            //.Net'in design flawlarından biri de bu, dinamik anonymous bir objeyi internal'den kurtulabilmemiz gerekirdi
                                            //Bu noktada entity tipini async generic hale getirebilmek (IAsyncQueryable<T>) için
                                            //CallAsDiasFacManDevEntityFunctionWithDynamic<T> benzer şekilde  CallToQueryableWithDynamic i çağırıyoruz
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            //Filtrele
                                            //Elimizde IQueryable<T> olduğundan generic şekilde gönderebiliriz
                                            existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput("id", id.ToString(), tEntity, queryableParam);
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        //Entity i Dto ya çevirelim
                                        returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(existentRecord, null);


                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                        return new(ErrorCodes.None, returnDto);
                                    }
                                    else
                                    {
                                        //kayıt yok veya tablo yok
                                        return new(ErrorCodes.UnknownError, null);
                                    }
                                }
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

        //TODO : obsolete, tüm ErroCodes lar temizlenince silinecektir
        public async Task<Tuple<ErrorCodes, TDto>> GetByIdFromLong(long id)
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
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecord = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            dynamic existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));
                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            //Filtrele
                                            //Elimizde IQueryable<T> olduğundan generic şekilde gönderebiliriz
                                            existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput("id", id.ToString(), tEntity, queryableParam);
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        //Entity i Dto ya çevirelim
                                        returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(existentRecord, null);


                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                        return new(ErrorCodes.None, returnDto);
                                    }
                                    else
                                    {
                                        //kayıt yok veya tablo yok
                                        return new(ErrorCodes.UnknownError, null);
                                    }
                                }
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

        //TODO : obsolete, tüm ErroCodes lar temizlenince silinecektir
        //TODO : uniqueColumns entity propertysi değil, dto propertysi olmalıdır
        public async Task<Tuple<ErrorCodes, TDto>> Insert(TDto insertedDto, List<string> uniqueColumns = null, List<object> uniqueValues = null)
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
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = new ExpandoObject();
                                dynamic existentRecord = new ExpandoObject();

                                dynamic convertedEntity = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));
                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            if ((uniqueColumns != null) && (uniqueValues != null))
                                            {
                                                if ((uniqueColumns.Count == 1) && (uniqueColumns[0] != null) &&
                                                    (uniqueValues.Count == 1) && (uniqueValues[0] != null))
                                                {
                                                    //Filtrele
                                                    existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput(uniqueColumns[0], uniqueValues[0].ToString(), tEntity, queryableParam, false);
                                                }
                                                else 
                                                {
                                                    //Filtrele(by unique parameters)
                                                    existentRecord = await EF_DynamicExpressions.FilterPredicateWithMultiParameterSingleOutput(uniqueColumns, uniqueValues, true, tEntity, queryableParam, false);
                                                }
                                            }
                                        }
                                    }

                                    if (existentRecordList != null)
                                    {
                                        if ((uniqueColumns != null) && (uniqueValues != null))
                                        {
                                            //Aynı kayıt veritabanında var mı?
                                            if (existentRecord == null)
                                            {
                                                convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map(insertedDto, typeof(TDto), tEntity);
                                            }
                                            else
                                            {
                                                //Aynı kayıt varsa hata gönder
                                                return new(ErrorCodes.UnknownError, null);
                                            }
                                        }
                                        else
                                        {
                                            convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map(insertedDto, typeof(TDto), tEntity);
                                        }

                                        await Task.Run(() => existentRecordList.AddAsync(convertedEntity));
                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                        returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(convertedEntity, null);
                                        return new(ErrorCodes.None, returnDto);
                                    }
                                    else
                                    {
                                        //Tablo bulunamadı
                                        return new(ErrorCodes.UnknownError, null);
                                    }
                                }
                            }
                            //veritabanından gelen
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            //dynamic expressiondan gelen
                            catch (ArgumentException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            //dynamic expressiondan gelen
                            catch (InvalidOperationException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception ex)
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

        //TODO : obsolete, tüm ErroCodes lar temizlenince silinecektir
        //TODO : uniqueColumns entity propertysi değil, dto propertysi olmalıdır
        public async Task<Tuple<ErrorCodes, TDto>> Update(TDto updatedDto, List<string> uniqueColumns = null, List<object> uniqueValues = null, bool isBaseDtoUpdated = true )
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
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = new ExpandoObject();

                                //Kaydın update edilmeden önceki veritabanındaki hali
                                dynamic existentRecord = new ExpandoObject();

                                //Kaydın update edildikten sonra veritabanındaki hali
                                dynamic convertedEntity = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));

                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);


                                            if ((uniqueColumns != null) && (uniqueValues != null))
                                            {
                                                if ((uniqueColumns.Count == 1) && (uniqueColumns[0] != null) &&
                                                    (uniqueValues.Count == 1) && (uniqueValues[0] != null))
                                                {
                                                    //Filtrele(by id)
                                                    existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput(uniqueColumns[0], uniqueValues[0].ToString(), tEntity, queryableParam);
                                                }
                                                else
                                                {
                                                    //Filtrele(by unique parameters)
                                                    existentRecord = await EF_DynamicExpressions.FilterPredicateWithMultiParameterSingleOutput(uniqueColumns, uniqueValues, true, tEntity, queryableParam);
                                                }
                                            }
                                            else
                                            {
                                                //update edeceğimiz kayıdı bilmeliyiz
                                                return new(ErrorCodes.UnknownError, null);
                                            }
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map(updatedDto, typeof(TDto), tEntity);
                                        //TODO: Dtoların id property isimlerini dinamik almamız gerek
                                        convertedEntity.Id = existentRecord.Id;

                                        //TODO: encapsulate etmeden CopyProperties i çalıştırabilir miyiz?
                                        ((object)(convertedEntity)).CopyProperties(((object)(existentRecord)));

                                        BaseDevelopment.DevelopmentBaseEntity tempBaseEntity;

                                        if (!isBaseDtoUpdated)
                                        {
                                            //Base entity güncelleme yapılmayacak, veritabandaki orjinal base entity koruyalım
                                            //Türetilenden base e atama en performanslı olmasa da en kolay yol deep copy yapılması

                                            // Şimdi burada hem infinite reference loopu engellemek
                                            //hem de entitydeki virtual navigation propertylerin serialize ve deserialize edilmesini
                                            //engellemek için newtonsoft u özelleştiriyoruz
                                            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                                            {
                                                ContractResolver = new NonVirtualResolver(),                                               
                                                PreserveReferencesHandling = PreserveReferencesHandling.None,
                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                Formatting = Formatting.Indented
                                            };

                                            tempBaseEntity = JsonConvert.DeserializeObject<BaseDevelopment.DevelopmentBaseEntity>(JsonConvert.SerializeObject(existentRecord, jsonSettings));

                                            //şimdi converted entity e bu base entity i geçirelim
                                            //Generic metoda direk dynamic i geçiremeyeceğimiz için ara bir geçiş çağrısı yapalım
                                            convertedEntity = CallAsDiasFacManDevEntityFunctionWithDynamic(convertedEntity, tempBaseEntity);
                                        }

                                        await Task.Run(() => existentRecordList.Update(existentRecord));
                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                        returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(convertedEntity, null);
                                        return new(ErrorCodes.None, returnDto);
                                    }
                                    else
                                    {
                                        //update edilecek kayıt veritabanında yok veya tablo yok
                                        return new(ErrorCodes.UnknownError, null);
                                    }
                                }
                            }
                            //veritabanından gelen
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            //dynamic expressiondan gelen
                            catch (ArgumentException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            //dynamic expressiondan gelen
                            catch (InvalidOperationException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception ex)
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

        //TODO : obsolete, tüm ErroCodes lar temizlenince silinecektir
        public async Task<Tuple<ErrorCodes, TDto>> DeleteFromInt(int id)
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
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = new ExpandoObject();
                                dynamic existentRecord = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));

                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            //Filtrele
                                            existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput("id", id.ToString(), tEntity, queryableParam);
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        existentRecord.IsDeleted = true;
                                    }
                                    else
                                    {
                                        //kayıt yok veya tablo yok
                                        return new(ErrorCodes.UnknownError, null);
                                    }

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                    returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(existentRecord, null);
                                    return new(ErrorCodes.None, returnDto);
                                }
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

        //TODO : obsolete, tüm ErroCodes lar temizlenince silinecektir
        public async Task<Tuple<ErrorCodes, TDto>> DeleteFromLong(long id)
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
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                //Sonarqube ün duplicate fonksiyonun uyarısından kaçmak için 
                                int a = 1;
                                a++;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = new ExpandoObject();
                                dynamic existentRecord = new ExpandoObject(); 

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));

                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            //Filtrele
                                            existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput("id", id.ToString(), tEntity, queryableParam);
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        existentRecord.IsDeleted = true;
                                    }
                                    else
                                    {
                                        //kayıt yok veya tablo yok
                                        return new(ErrorCodes.UnknownError, null);
                                    }

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                    returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(existentRecord, null);
                                    return new(ErrorCodes.None, returnDto);
                                }
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


        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<DiasShared.Errors.Error, IEnumerable<TDto>>> GetAllV2()
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
                return new(Errors.General.GeneralServerError(), null);
            }
            else
            {
                List<TDto> returnDtoList = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = null;

                                using (DiasFacilityManagementSqlServerContext)
                                {
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

                                        return new(Errors.General.Success("generic"), returnDtoList);
                                    }
                                    else
                                    {
                                        //tablo yok
                                        return new(Errors.General.GeneralServerError(), null);
                                    }
                                }
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
        public async Task<Tuple<DiasShared.Errors.Error, TDto>> GetByIdFromIntV2(int id)
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
                return new(Errors.General.GeneralServerError(), null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecord = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            dynamic existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));
                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi https://stackoverflow.com/questions/2630370/c-sharp-dynamic-cannot-access-properties-from-anonymous-types-declared-in-anot de belirtilmiş
                                            // EFCore dan gelen bir tipe atanmış dinamik obje üzerinde farklı assemblylerde çalışamıyoruz(dynamic in atandığı tip EF Core a göre internal olduğu için)
                                            //Üstad Jon Skeet'in önerdiği ne tek başına ExpandoObject ne de InternalsVisibleTo işe yaramadı
                                            //Bu durumda tek çare olarak dynamic tipini ayrı bir memory bloğuna sahip IQueryable objesine taşımaktan başka çare kalmadı
                                            //.Net'in design flawlarından biri de bu, dinamik anonymous bir objeyi internal'den kurtulabilmemiz gerekirdi
                                            //Bu noktada entity tipini async generic hale getirebilmek (IAsyncQueryable<T>) için
                                            //CallAsDiasFacManDevEntityFunctionWithDynamic<T> benzer şekilde  CallToQueryableWithDynamic i çağırıyoruz
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            //Filtrele
                                            //Elimizde IQueryable<T> olduğundan generic şekilde gönderebiliriz
                                            existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput("id", id.ToString(), tEntity, queryableParam);
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        //Entity i Dto ya çevirelim
                                        returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(existentRecord, null);


                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                        return new(Errors.General.Success("generic"), returnDto);
                                    }
                                    else
                                    {
                                        //kayıt yok veya tablo yok
                                        return new(Errors.General.GeneralServerError(), null);
                                    }
                                }
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
        public async Task<Tuple<DiasShared.Errors.Error, TDto>> GetByIdFromLongV2(long id)
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
                return new(Errors.General.GeneralServerError(), null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecord = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            dynamic existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));
                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            //Filtrele
                                            //Elimizde IQueryable<T> olduğundan generic şekilde gönderebiliriz
                                            existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput("id", id.ToString(), tEntity, queryableParam);
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        //Entity i Dto ya çevirelim
                                        returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(existentRecord, null);


                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                        return new(Errors.General.Success("generic"), returnDto);
                                    }
                                    else
                                    {
                                        //kayıt yok veya tablo yok
                                        return new(Errors.General.GeneralServerError(), null);
                                    }
                                }
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
        //TODO : uniqueColumns entity propertysi değil, dto propertysi olmalıdır
        public async Task<Tuple<DiasShared.Errors.Error, TDto>> InsertV2(TDto insertedDto, List<string> uniqueColumns = null, List<object> uniqueValues = null)
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
                return new(Errors.General.GeneralServerError(), null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = new ExpandoObject();
                                dynamic existentRecord = new ExpandoObject();

                                dynamic convertedEntity = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));
                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            if ((uniqueColumns != null) && (uniqueValues != null))
                                            {
                                                if ((uniqueColumns.Count == 1) && (uniqueColumns[0] != null) &&
                                                    (uniqueValues.Count == 1) && (uniqueValues[0] != null))
                                                {
                                                    //Filtrele
                                                    existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput(uniqueColumns[0], uniqueValues[0].ToString(), tEntity, queryableParam, false);
                                                }
                                                else
                                                {
                                                    //Filtrele(by unique parameters)
                                                    existentRecord = await EF_DynamicExpressions.FilterPredicateWithMultiParameterSingleOutput(uniqueColumns, uniqueValues, true, tEntity, queryableParam, false);
                                                }
                                            }
                                        }
                                    }

                                    if (existentRecordList != null)
                                    {
                                        if ((uniqueColumns != null) && (uniqueValues != null))
                                        {
                                            //Aynı kayıt veritabanında var mı?
                                            if (existentRecord == null)
                                            {
                                                convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map(insertedDto, typeof(TDto), tEntity);
                                            }
                                            else
                                            {
                                                //Aynı kayıt varsa hata gönder
                                                return new(Errors.General.GeneralServerError(), null);
                                            }
                                        }
                                        else
                                        {
                                            convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map(insertedDto, typeof(TDto), tEntity);
                                        }

                                        await Task.Run(() => existentRecordList.AddAsync(convertedEntity));
                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                        returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(convertedEntity, null);
                                        return new(Errors.General.Success("generic"), returnDto);
                                    }
                                    else
                                    {
                                        //Tablo bulunamadı
                                        return new(Errors.General.GeneralServerError(), null);
                                    }
                                }
                            }
                            //veritabanından gelen
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            //dynamic expressiondan gelen
                            catch (ArgumentException)
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            //dynamic expressiondan gelen
                            catch (InvalidOperationException)
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (Exception  ex)
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
        //TODO : uniqueColumns entity propertysi değil, dto propertysi olmalıdır
        public async Task<Tuple<DiasShared.Errors.Error, TDto>> UpdateV2(TDto updatedDto, List<string> uniqueColumns = null, List<object> uniqueValues = null, bool isBaseDtoUpdated = true)
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
                return new(Errors.General.GeneralServerError(), null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = new ExpandoObject();

                                //Kaydın update edilmeden önceki veritabanındaki hali
                                dynamic existentRecord = new ExpandoObject();

                                //Kaydın update edildikten sonra veritabanındaki hali
                                dynamic convertedEntity = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));

                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);


                                            if ((uniqueColumns != null) && (uniqueValues != null))
                                            {
                                                if ((uniqueColumns.Count == 1) && (uniqueColumns[0] != null) &&
                                                    (uniqueValues.Count == 1) && (uniqueValues[0] != null))
                                                {
                                                    //Filtrele(by id)
                                                    existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput(uniqueColumns[0], uniqueValues[0].ToString(), tEntity, queryableParam);
                                                }
                                                else
                                                {
                                                    //Filtrele(by unique parameters)
                                                    existentRecord = await EF_DynamicExpressions.FilterPredicateWithMultiParameterSingleOutput(uniqueColumns, uniqueValues, true, tEntity, queryableParam);
                                                }
                                            }
                                            else
                                            {
                                                //update edeceğimiz kayıdı bilmeliyiz
                                                return new(Errors.General.GeneralServerError(), null);
                                            }
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map(updatedDto, typeof(TDto), tEntity);
                                        //TODO: Dtoların id property isimlerini dinamik almamız gerek
                                        convertedEntity.Id = existentRecord.Id;

                                        //TODO: encapsulate etmeden CopyProperties i çalıştırabilir miyiz?
                                        ((object)(convertedEntity)).CopyProperties(((object)(existentRecord)));

                                        BaseDevelopment.DevelopmentBaseEntity tempBaseEntity;

                                        if (!isBaseDtoUpdated)
                                        {
                                            //Base entity güncelleme yapılmayacak, veritabandaki orjinal base entity koruyalım
                                            //Türetilenden base e atama en performanslı olmasa da en kolay yol deep copy yapılması

                                            // Şimdi burada hem infinite reference loopu engellemek
                                            //hem de entitydeki virtual navigation propertylerin serialize ve deserialize edilmesini
                                            //engellemek için newtonsoft u özelleştiriyoruz
                                            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                                            {
                                                ContractResolver = new NonVirtualResolver(),
                                                PreserveReferencesHandling = PreserveReferencesHandling.None,
                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                Formatting = Formatting.Indented
                                            };

                                            tempBaseEntity = JsonConvert.DeserializeObject<BaseDevelopment.DevelopmentBaseEntity>(JsonConvert.SerializeObject(existentRecord, jsonSettings));

                                            //şimdi converted entity e bu base entity i geçirelim
                                            //Generic metoda direk dynamic i geçiremeyeceğimiz için ara bir geçiş çağrısı yapalım
                                            convertedEntity = CallAsDiasFacManDevEntityFunctionWithDynamic(convertedEntity, tempBaseEntity);
                                        }

                                        await Task.Run(() => existentRecordList.Update(existentRecord));
                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                        returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(convertedEntity, null);
                                        return new(Errors.General.Success("generic"), returnDto);
                                    }
                                    else
                                    {
                                        //update edilecek kayıt veritabanında yok veya tablo yok
                                        return new(Errors.General.GeneralServerError(), null);
                                    }
                                }
                            }
                            //veritabanından gelen
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            //dynamic expressiondan gelen
                            catch (ArgumentException)
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            //dynamic expressiondan gelen
                            catch (InvalidOperationException)
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
        public async Task<Tuple<DiasShared.Errors.Error, TDto>> DeleteFromIntV2(int id)
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
                return new(Errors.General.GeneralServerError(), null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = new ExpandoObject();
                                dynamic existentRecord = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));

                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            //Filtrele
                                            existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput("id", id.ToString(), tEntity, queryableParam);
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        existentRecord.IsDeleted = true;
                                    }
                                    else
                                    {
                                        //kayıt yok veya tablo yok
                                        return new(Errors.General.GeneralServerError(), null);
                                    }

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                    returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(existentRecord, null);
                                    return new(Errors.General.Success("generic"), returnDto);
                                }
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (InvalidOperationException)
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
        public async Task<Tuple<DiasShared.Errors.Error, TDto>> DeleteFromLongV2(long id)
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
                return new(Errors.General.GeneralServerError(), null);
            }
            else
            {
                TDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                //Sonarqube ün duplicate fonksiyonun uyarısından kaçmak için 
                                int a = 1;
                                a++;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                //Burada muhakkak DiasDataAccessLayer de hep olacak bir sınıf yazmalıyız
                                Assembly dalAssembly = typeof(DataAccessLayerEnums).Assembly;
                                //Dto ya karşılık gelen entity'in tipini al
                                Type tEntity = dalAssembly.GetType(workingEntityFullName);

                                //DbSet<Entity> karşılık gelen tipi al
                                Type genericListEntityDbSetType = typeof(DbSet<>).MakeGenericType(tEntity);

                                dynamic existentRecordList = new ExpandoObject();
                                dynamic existentRecord = new ExpandoObject();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Contexte böyle bir tablo(entity) var mı?
                                    foreach (PropertyInfo pi in typeof(DevelopmentContext.DiasFacilityManagementSqlServer).GetProperties())
                                    {
                                        if (pi.PropertyType == genericListEntityDbSetType)
                                        {
                                            //Tüm tabloyu çek
                                            existentRecordList = await Task.Run(() => pi.GetValue(DiasFacilityManagementSqlServerContext));

                                            dynamic instanceDerived = Activator.CreateInstance(tEntity);

                                            //Burada InnerDBSet'i IQueryable hale dönüştürüyoruz
                                            //Sebebi ve yapılan işlem GetByIdFromInt de açıklandı
                                            var queryableParam = CallToQueryableWithDynamic(instanceDerived, existentRecordList, tEntity);

                                            //Filtrele
                                            existentRecord = await EF_DynamicExpressions.FilterPredicateWithOneParameterSingleOutput("id", id.ToString(), tEntity, queryableParam);
                                        }
                                    }

                                    if (existentRecord != null)
                                    {
                                        existentRecord.IsDeleted = true;
                                    }
                                    else
                                    {
                                        //kayıt yok veya tablo yok
                                        return new(Errors.General.GeneralServerError(), null);
                                    }

                                    await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                    returnDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TDto>(existentRecord, null);
                                    return new(Errors.General.Success("generic"), returnDto);
                                }
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.GeneralServerError(), null);
                            }
                            catch (InvalidOperationException)
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


        /// <summary>
        /// AsDiasFacManDevEntity generic metoduna dynamic geçirmek için ara geçiş metodu
        /// </summary>
        private T CallAsDiasFacManDevEntityFunctionWithDynamic<T>(T ignored, BaseDevelopment.DevelopmentBaseEntity param)
                                                                                where T : BaseDevelopment.DevelopmentBaseEntity, new()
        {
            return param.AsDiasFacManDevEntity<T>(ignored);
        }

        // <summary>
        /// InternalDbSet'i IAsyncQueryable<T> yapmak için ara geçiş metodu
        /// </summary>
        private IAsyncQueryable<T> CallToQueryableWithDynamic<T>(T ignored, dynamic instance, Type tEntity) where T : class
        {
            //InternalDbSet<T> i önce IAsyncEnumerable<T> e sonra IAsyncQueryable<T> e çevireceğiz
            //IAsyncEnumerable<T> değişkenine yeni bir memory alanı assign etmek zorundayız yoksa yine InternalDbSet<T> döner
            IAsyncEnumerable<T> tempFirst = AsyncEnumerable.Empty<T>();
            IAsyncQueryable<T> tempSecond;
            tempFirst = instance.AsAsyncEnumerable();
            tempSecond = tempFirst.AsAsyncQueryable();
            return tempSecond;
        }
    }
}
