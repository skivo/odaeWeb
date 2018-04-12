using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Material
    {
        public Material()
        {
            Codificacion = new HashSet<Codificacion>();
            InverseOriginalNavigation = new HashSet<Material>();
        }

        public int MaterialId { get; set; }
        public string TipoMaterialId { get; set; }
        public int EstudianteId { get; set; }
        public string FileName { get; set; }
        public bool? TieneDuplicado { get; set; }
        public bool? EsDuplicado { get; set; }
        public int? Original { get; set; }

        public Estudiante Estudiante { get; set; }
        public Material OriginalNavigation { get; set; }
        public TipoMaterial TipoMaterial { get; set; }
        public ICollection<Codificacion> Codificacion { get; set; }
        public ICollection<Material> InverseOriginalNavigation { get; set; }
    }
}
