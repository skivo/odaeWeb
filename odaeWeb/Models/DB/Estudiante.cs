using System;
using System.Collections.Generic;

namespace odaeWeb.Models.DB
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            Material = new HashSet<Material>();
        }

        public int EstudianteId { get; set; }
        public short EscuelaId { get; set; }
        public byte? Curso { get; set; }
        public string NombreEstudiante { get; set; }

        public Escuela Escuela { get; set; }
        public ICollection<Material> Material { get; set; }
    }
}
