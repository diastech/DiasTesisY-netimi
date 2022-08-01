using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DiasShared.Operations.JsonOperation.Resolvers
{
    public class NonVirtualResolver : DefaultContractResolver
    {
        //virtual olupta yine de serialize etmek istediğimiz propertyler
        //kullanım:
        //    NonVirtualResolver = new NonVirtualResolver(new []
        //{
        //    nameof(PC.Libs), PC entity, Libs property
        //    nameof(PC.Files) 
        //}),
        private readonly List<string> _namesOfVirtualPropsToKeep = new List<string>(new String[] { });

        public NonVirtualResolver() { }

        public NonVirtualResolver(IEnumerable<string> namesOfVirtualPropsToKeep)
        {
            this._namesOfVirtualPropsToKeep = namesOfVirtualPropsToKeep.Select(x => x.ToLower()).ToList();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);

            PropertyInfo propInfo = member as PropertyInfo;

            if (propInfo != null)
            {
                if ((propInfo.GetMethod.IsVirtual) && (!propInfo.GetMethod.IsFinal)
                    && (!_namesOfVirtualPropsToKeep.Contains(propInfo.Name.ToLower())))
                {
                    prop.ShouldSerialize = obj => false;
                }  
            }

            return prop;
        }
    }
}
