using System;
using System.Collections.Generic;

namespace Console_Testing.BLL.Sorting
{
    /// <inheritdoc />
    /// <summary>
    /// Support sort in-memory list with multiple sorting fields
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortingComparer<T> : IComparer<T>
    {
        private readonly string[] _sortFields;

        public SortingComparer(string sortCriteria)
        {
            sortCriteria = sortCriteria.Trim();
            _sortFields = sortCriteria.Split(',');
        }

        public int Compare(T x, T y)
        {
            if (x == null || y == null) return 0;
            var compareResult = 0;

            foreach (var sortField in _sortFields)
            {
                var compareData = GetCompareData(y, x, sortField);

                if (compareData.Previous is null || compareData.Next is null) return 0;
                compareResult = compareData.IsReverse
                    ? ((IComparable)compareData.Previous).CompareTo(compareData.Next)
                    : ((IComparable)compareData.Next).CompareTo(compareData.Previous);
                if (compareResult != 0)
                    break;
            }
            return compareResult;
        }

        private static object GetProperty(T enity, string propName)
        {
            return enity.GetType().GetProperty(propName)?.GetValue(enity);
        }

        private static dynamic GetCompareData(T y, T x, string sortField)
        {
            var sortData = sortField.Split(' ');
            var isDescending = sortData.Length > 1 && sortData[1].ToUpper().Equals("DESC");

            var firstProp = GetProperty(y, sortData[0]);
            var secondProp = GetProperty(x, sortData[0]);

            if (!(firstProp is IComparable && secondProp is IComparable))
                firstProp = secondProp = null;
			
            return new
            {
                Previous = firstProp,
                Next = secondProp,
                IsReverse = isDescending
            };
        }
    }
}