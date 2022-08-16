using System;
using System.Reflection;

namespace PokerGame.UnitTests.Extensions
{
    static class CommonExtensions
    {
        public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val)
        {
            Type type = obj.GetType();
            FieldInfo field = type.GetField(propName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
                throw new ArgumentOutOfRangeException(nameof(propName), string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));

            field.SetValue(obj, val);
            //type.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val });
        }
    }
}
