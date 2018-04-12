using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class TipoMaterial
    {
        public TipoMaterial()
        {
            Material = new HashSet<Material>();
        }

        public string TipoMaterialId { get; set; }
        public string TipoMaterial1 { get; set; }

        public ICollection<Material> Material { get; set; }
    }
}
