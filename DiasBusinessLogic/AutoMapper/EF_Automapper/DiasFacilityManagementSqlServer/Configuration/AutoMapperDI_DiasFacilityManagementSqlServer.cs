using AutoMapper;
using DiasBusinessLogic.AutoMapper.Configuration;
using SimpleInjector;
using SimpleInjector.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Configuration
{
    public class AutoMapperDI_DiasFacilityManagementSqlServer : IPackage
    {
        /// <summary>
        /// AutoMapper' ı proje boyunca Dependency Injection yaptığımız sınıf
        /// Generic IMapper nesnelerini kabul edebilir hale getirildi 
        /// </summary>       
        public void RegisterServices(Container container)
        {
            //TODO : Environmente göre register yapılacak
            //Tüm AutoMapper Profil sınıf tiplerini al
            IEnumerable<Type> profileRegistrations =
                from type in Assembly.GetExecutingAssembly().GetExportedTypes()
                where type.Namespace == "DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development"//AutoMapper Profil sınıflarının namespace i
                where type.BaseType.Equals(typeof(Profile))
                select type;

            //DI registerı yaptığımız yer
            foreach (Type profileType in profileRegistrations)
            {
                container.RegisterSingleton(profileType, profileType);//Tek bir IMapper instance ı olacak
                Type mapperClosedType = typeof(AutoMapperProfileMapper<>).MakeGenericType(profileType);//IMapper ı AutoMapperProfileMapper a çevirdiğimiz yer
                container.RegisterSingleton(typeof(AutoMapperProfileMapper<>), mapperClosedType);//Tek olan IMapper instance'ından tek bir AutoMapperProfileMapper instance'ı olacak
            }
        }       
    }
}
