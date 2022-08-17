using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.UnitTests.Extensions
{
    static class AssertExtensions
    {
        public static void AllItemsAreDifferent<T>(this Assert assert, IEqualityComparer<T> comparer, params T[] items) where T: class
        {
            if (items == null || !items.Any())
                throw new AssertFailedException("List cannot be empty");

            bool allDifferent = items.Distinct(comparer).Count() == items.Length;

            if (allDifferent)
                return;

            throw new AssertFailedException("All items are not different");
        }

        public static void AllItemsAreDifferent<T>(this Assert assert, params T[] items) where T : class
        {
            if (items == null || !items.Any())
                throw new AssertFailedException("List cannot be empty");

            bool allDifferent = items.Distinct().Count() == items.Length;

            if (allDifferent)
                return;

            throw new AssertFailedException("All items are not different");
        }

        public static void DoesNotThrow(this Assert assert, Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch
            {
                throw new AssertFailedException("Method throws an error");
            }
        }
    }
}
