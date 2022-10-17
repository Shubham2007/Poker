using System;
using System.Collections.Generic;
using static System.Console;

namespace PokerGame.Helper
{
    static class ConsoleHelper
    {
        public static void PrintStringToConsole(string text, bool newLine = true)
        {
            if (string.IsNullOrWhiteSpace(text))
                WriteLine();

            if(newLine)
                WriteLine(text);

            else
                Write(text);
        }
             
        public static void PrintObjectToConsole<T>(T obj, bool newLine = true)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (typeof(T).GetMethod("ToString").DeclaringType != typeof(T))
                throw new InvalidOperationException("Object does not override ToString method");

            PrintStringToConsole(obj.ToString(), newLine);
        }      

        public static void PrintListToConsole<T>(IReadOnlyCollection<T> list, bool newLine = true)
        {
            if(list == null)
                throw new ArgumentNullException(nameof(list));

            if (typeof(T).GetMethod("ToString").DeclaringType != typeof(T))
                throw new InvalidOperationException("Object does not override ToString method");

            foreach(T item in list)
            {
                PrintObjectToConsole<T>(item, newLine);
            }
        }
    }
}
