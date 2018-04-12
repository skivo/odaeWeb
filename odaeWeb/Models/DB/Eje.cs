using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Eje
    {
        public Eje()
        {
            Objetivo = new HashSet<Objetivo>();
        }

        public int EjeId { get; set; }
        public string DescripcionEje { get; set; }

        public ICollection<Objetivo> Objetivo { get; set; }
    }
}
