using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DiasBusinessLogic.AutoMapper.Configuration
{
    public class AutoMapperProfileMapper<TProfile> : IMapper where TProfile : Profile
    {
        //private çünkü map işlemlerini public TDestination Map<TDestination>(object source) ile yapacağız
        private static IMapper mapper;

        private static Profile profile;

        public AutoMapperProfileMapper(TProfile profileP)
        {
            //singleton property benzeri
            if ((profile == null) || (mapper == null))
            {
                profile = profileP;

                mapper = new MapperConfiguration(cfg => cfg.AddProfile(profileP))
                       .CreateMapper();
            }
        }

        //iş kurallarında kullanılacak metod, IMapper dan implement ediyoruz
        public TDestination Map<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            return mapper.Map<TSource, TDestination>(source, opts);
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return mapper.Map(source, sourceType, destinationType);
        }

        //Aşağıdaki metodları şimdilik implement etmeye gerek yok
        #region ihtiyaç olmayan metodlar

        public IConfigurationProvider ConfigurationProvider => throw new NotImplementedException();

        public Func<Type, object> ServiceCtor => throw new NotImplementedException();



        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
        {
            if (opts == null)
            {
                return mapper.Map<TDestination>(source);
            }
            else
            {
                return mapper.Map<TDestination>(source, opts);
            }
        }



        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            throw new NotImplementedException();
        }


        //TODO: new method to analyse
        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions<object, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        //TODO: new method to analyse
        public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            throw new NotImplementedException();
        }

        //TODO: new method to analyse
        public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            throw new NotImplementedException();
        }

        //TODO: new method to analyse
        public IQueryable ProjectTo(IQueryable source, Type destinationType, IDictionary<string, object> parameters = null, params string[] membersToExpand)
        {
            throw new NotImplementedException();
        }
        #endregion ihtiyaç olmayan metodlar
    }

}
