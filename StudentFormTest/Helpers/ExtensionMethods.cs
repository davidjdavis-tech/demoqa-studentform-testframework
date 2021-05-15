using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace NTT_Assessment.Helpers
{
    public static class ExtensionMethods
    {
        public static string TrimCommaSeparatedString(this string str)
        {
            var splitTr = str.Split(","); 
            return string.Join(",", splitTr.Select(s => s.Trim()));
        }

        public static ICollection<T> ReturnAndRemoveLastElement<T>(this ICollection<T> items, out T lastElement)
        {
            lastElement = items.Last();
            items.Remove(lastElement);

            return items;
        }

        public static ICollection<string> ToStringList(this Table table)
        {
            if (table.Header.Count != 1) Assert.Fail($"Expected a 1 column table. Table has {table.Header.Count} columns");
            return table.Rows.Select(row => row[0]).ToList();
        }
    }
}
