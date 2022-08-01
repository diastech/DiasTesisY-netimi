using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace DiasWebApi.Shared.Filters.Action
{
    /// <summary>
    ///Model(Dto) geçersiz olduğunda 400 hatası üreten varsayılan ApiController davranışını bastırır.
    ///Sadece actionlarda kullanılmalıdır
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SuppressModelStateInvalidFilterAttribute : Attribute, IActionModelConvention
    {
        private static readonly Type ModelStateInvalidFilterFactory = 
            typeof(ModelStateInvalidFilter).Assembly.GetType("Microsoft.AspNetCore.Mvc.Infrastructure.ModelStateInvalidFilterFactory");

        public void Apply(ActionModel action)
        {
            for (var i = 0; i < action.Filters.Count; i++)
            {
                if (action.Filters[i] is ModelStateInvalidFilter || action.Filters[i].GetType() == ModelStateInvalidFilterFactory)
                {
                    action.Filters.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
