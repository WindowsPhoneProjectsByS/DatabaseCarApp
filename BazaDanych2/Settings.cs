using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaDanych2
{
    class Settings
    {
        public enum Operation { Add, Edit };

        public static Operation operation = Operation.Add;
    }
}
