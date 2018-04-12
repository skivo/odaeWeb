using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb.Models
{
    public class Navigation
    {

        public int? Previous { get; set; }
        public int? Next { get; set; }
        public int Pos { get; set; }
        public int Total { get; set; }

    }
}
