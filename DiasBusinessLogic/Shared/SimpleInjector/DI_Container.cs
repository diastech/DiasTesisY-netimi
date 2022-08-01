using SimpleInjector;
using System.Collections.Generic;
using System.Reflection;

namespace DiasBusinessLogic.Shared.SimpleInjector
{
    //Simple Injector konfigurasyonu
    public static class DI_Container
    {
        private static Container _container { get; set; }

        public static Container ContainerObj
        {
            get
            {
                if ((_container == null) || (_container.GetCurrentRegistrations() == null)
                    || (_container.GetCurrentRegistrations().Length < 1))
                {
                    PackageExtensions.RegisterPackages(_container, new List<Assembly>() { Assembly.GetExecutingAssembly() });

                    return _container;
                }
                else
                {
                    return _container;
                }
            }
        }
    }
}
