using System;
using System.Reflection;

namespace DiasShared.Operations.ReflectionOperations
{
    /// <summary>
    /// A static class for reflection type functions
    /// </summary>
    public static class ReflectionOperations
    {
        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void CopyProperties(this object source, object destination, bool addVirtualProperties = false)
        {
            // If any this null throw an exception
            if ((source == null) || (destination == null))
            {
                throw new ArgumentException("Source or/and Destination Objects are null");
            }

            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            PropertyInfo[] srcProps = typeSrc.GetProperties();

            foreach (PropertyInfo srcProp in srcProps)
            {
                //Is source property readable?
                if (!srcProp.CanRead)
                {
                    continue;
                }

                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);              

                //Is target property writable?
                if (!targetProperty.CanWrite)
                {
                    continue;
                }

                //properties' get set methods private?
                if ((targetProperty.GetSetMethod(true) != null) && (targetProperty.GetSetMethod(true).IsPrivate))
                {
                    continue;
                }

                //property static?
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }

                //is target property  assignable from source property?
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }

                //add virtual properties?
                if ((!addVirtualProperties) && (srcProp.GetMethod.IsVirtual) && (!srcProp.GetMethod.IsFinal))
                {
                    continue;
                }

                //property is indexed? If indexed iterate
                if(targetProperty.GetIndexParameters().Length != 0)
                {
                    continue;
                }

                // Passed all tests, lets set the value
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }

        }
    }
}
