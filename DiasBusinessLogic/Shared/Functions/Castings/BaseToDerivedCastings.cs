using System;
using System.Reflection;
using BaseDiasFacManDev = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using BaseDiasFacManTest = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;


namespace DiasBusinessLogic.Shared.Functions.Castings
{
    /// <summary>
    /// contextde base entity i herhangi bir modele geçirmek(update) için kullanılır
    /// Bu extension metodla birlikte ürettiğimiz bir base entityi, bir veya toplu context model(ler)ine birer birer veya topluca geçirebiliriz
    /// </summary>
    public static class BaseToDerivedCastings
    {
        /// <summary>
        /// DiasFacilityManagementSqlServer development için
        /// </summary>
        /// <typeparam name="TDerived"></typeparam>
        /// <param name="baseInstance"></param>
        /// <param name="updateEntity"></param>
        /// <returns></returns>
        public static TDerived AsDiasFacManDevEntity<TDerived>(this BaseDiasFacManDev.DevelopmentBaseEntity baseInstance, TDerived updateEntity = null) where TDerived : BaseDiasFacManDev.DevelopmentBaseEntity, new()
        {
            Type baseType = typeof(BaseDiasFacManDev.DevelopmentBaseEntity);
            Type derivedType = typeof(TDerived);

            PropertyInfo[] properties = baseType.GetProperties();
            object instanceDerived = null;

            if (updateEntity == null)
            {
                instanceDerived = Activator.CreateInstance(derivedType);             
            }
            else
            {
                instanceDerived = (object)(updateEntity);
            }

            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    property.SetValue(instanceDerived, property.GetValue(baseInstance, null), null);
                }
            }

            return (TDerived)instanceDerived;
        }

        /// <summary>
        /// DiasFacilityManagementSqlServer test için
        /// </summary>
        /// <typeparam name="TDerived"></typeparam>
        /// <param name="baseInstance"></param>
        /// <param name="updateEntity"></param>
        /// <returns></returns>
        public static TDerived AsDiasFacManDevEntity<TDerived>(this BaseDiasFacManTest.DevelopmentBaseEntity baseInstance, TDerived updateEntity = null) where TDerived : BaseDiasFacManTest.DevelopmentBaseEntity, new()
        {
            Type baseType = typeof(BaseDiasFacManDev.DevelopmentBaseEntity);
            Type derivedType = typeof(TDerived);

            PropertyInfo[] properties = baseType.GetProperties();
            object instanceDerived = null;

            if (updateEntity == null)
            {
                instanceDerived = Activator.CreateInstance(derivedType);
            }
            else
            {
                instanceDerived = (object)(updateEntity);
            }

            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    property.SetValue(instanceDerived, property.GetValue(baseInstance, null), null);
                }
            }

            return (TDerived)instanceDerived;
        }


    }
}
