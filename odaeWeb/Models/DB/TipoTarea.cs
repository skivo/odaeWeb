using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class TipoTarea
    {
        public TipoTarea()
        {
            Codificacion = new HashSet<Codificacion>();
        }

        public int TipoTareaId { get; set; }
        public string NombreTipoTarea { get; set; }

        public ICollection<Codificacion> Codificacion { get; set; }
    }
}
