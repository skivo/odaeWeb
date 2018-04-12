using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Region
    {
        public Region()
        {
            Escuela = new HashSet<Escuela>();
        }

        public byte RegionId { get; set; }
        public string NombreRegion { get; set; }

        public ICollection<Escuela> Escuela { get; set; }
    }
}
