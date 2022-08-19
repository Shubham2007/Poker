using System;
using System.Reflection;

namespace PokerGame.Core.Cloners
{
    class ReflectionCloning
    {
        /// <summary>
        /// Return the deep clone of the given type
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="originalObject"></param>
        /// <returns></returns>
        public static TObject Clone<TObject>(TObject originalObject)
        {
            //step : 1 Get the type of source object and create a new instance of that type
            Type typeSource = originalObject.GetType();
            object objTarget = Activator.CreateInstance(typeSource);

            //Step2 : Get all the properties of source object type
            PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //Step : 3 Assign all source property to taget object 's properties
            foreach (PropertyInfo property in propertyInfo)
            {
                //Check whether property can be written to
                if (property.CanWrite)
                {
                    //Step : 4 check whether property type is value type, enum or string type
                    if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))
                    {
                        property.SetValue(objTarget, property.GetValue(originalObject, null), null);
                    }
                    //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                    else
                    {
                        object objPropertyValue = property.GetValue(originalObject, null);

                        // If reference type is null then simply assign null to cloned onject
                        if (objPropertyValue == null)
                        {
                            property.SetValue(objTarget, null, null);
                        }
                        else
                        {
                            property.SetValue(objTarget, Clone(objPropertyValue), null);
                        }
                    }
                }
            }
            return (TObject)objTarget;
        }
    }
}
