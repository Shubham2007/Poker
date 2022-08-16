using System;
using System.Collections.Generic;

namespace PokerGame.Extensions
{
    /// <summary> Enum Extension Methods </summary>
    /// <typeparam name="T"> type of Enum </typeparam>
    public class Enum<T> where T : struct, IConvertible
    {
        //It's not a true extension method. It only works because Enum<> is a different type than System.Enum.
        public static T GetRandomValue(HashSet<T> exceptList)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("This method works for enums only");

            Array values = Enum.GetValues(typeof(T));

            if (values.Length <= exceptList.Count)
                throw new ArgumentException("except list must contain less element than original enum");

            Random random = new();
            T randomValue;
            do
            {
                randomValue = (T)values.GetValue(random.Next(values.Length));

            } while (exceptList.Contains(randomValue));           

            return randomValue;
        }
    }
}
