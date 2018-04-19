using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace odaeWeb.Models.DB
{
    public partial class Codificacion
    {
        public int CodificadorId { get; set; }
        public int FaseId { get; set; }
        public int MaterialId { get; set; }
        public int? NivelId { get; set; }
        public string NivelComentario { get; set; }
        public int? CursoId { get; set; }
        public int? EjeId { get; set; }
        public int? ObjetivoId { get; set; }
        public string ObjetivoComentario { get; set; }
        public int? HabilidadId { get; set; }
        public string HabilidadComentario { get; set; }
        public int? TipoTareaId { get; set; }
        public string TipoTareaComentario { get; set; }
        public bool? CorreccionProfesor { get; set; }
        public bool? ErrorEjecucion { get; set; }
        public bool? TrabajaDinero { get; set; }
        public bool? ErrorDiseno { get; set; }
        public string Observaciones { get; set; }
        public int Estado { get; set; }
        public int RowIndex { get; set; }

        public Codificador Codificador { get; set; }
        public Fase Fase { get; set; }
        public Habilidad Habilidad { get; set; }
        public Material Material { get; set; }
        public Nivel Nivel { get; set; }
        public Objetivo Objetivo { get; set; }
        public TipoTarea TipoTarea { get; set; }
    }
}
