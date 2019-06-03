using Console_Testing.Model;
using System;
using System.Collections.Generic;

namespace Console_Testing.BLL.Sorting
{
    public class SortWithComparer
    {

        readonly List<Girl> _girls = new List<Girl>
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
            var sorter = new SortingComparer<Girl>("Id DESC,Age DESC");
            _girls.Sort(sorter);

            return _girls;
        }
    }
}