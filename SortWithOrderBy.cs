using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Console_Testing.Model;

namespace Console_Testing.BLL.Sorting
{
    public class SortWithOrderBy
    {
        List<Girl> _girls = new List<Girl>
        {
            new Girl{ Id = 1, Age = 24, Name = "Kiều Diễm", IsPretty = true },
            new Girl{ Id = 2, Age = 25, Name = "Diễm Xưa", IsPretty = false },
            new Girl{ Id = 3, Age = 19, Name = "Hồng Đào", IsPretty = true},
            new Girl{ Id = 4, Age = 22, Name = "Như Mộng", IsPretty = true },
            new Girl{ Id = 5, Age = 15, Name = "Hồng Tơ", IsPretty = true },
            new Girl{ Id = 5, Age = 16, Name = "Anh Đào", IsPretty = true }
        };

        public List<Girl> Sort()
        {
            _girls = _girls.Sort("Id,Age DESC").ToList();

            return _girls;

        }
    }


    public static class SortExtension
    {
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> inputList, string sortData)
        {
            if (inputList == null) return null;

            var sortItems = sortData.Trim().Split(' ');
            var sortFields = sortItems[0].Split(',');
            var isDescending = sortItems[1].ToUpper().Equals("DESC");

            IOrderedQueryable<T> orderedList = null;
            PropertyInfo sortProp = null;
            var objectValue = new Func<T, object>(obj => sortProp.GetValue(obj, null));
            for (var index = 0; index < sortFields.Length; index++)
            {
                var field = sortFields[index];
                sortProp = typeof(T).GetProperty(field);
                orderedList = (IOrderedQueryable<T>)(isDescending
                    ? index == 0 ? inputList.AsQueryable().OrderByDescending(p => objectValue(p)) : orderedList.ThenByDescending(p => objectValue(p))
                    : index == 0 ? inputList.AsQueryable(). OrderBy(p => objectValue(p)).AsQueryable() : orderedList.ThenBy(p => objectValue(p)));
            }
            return orderedList ?? inputList;
        }
    }
}