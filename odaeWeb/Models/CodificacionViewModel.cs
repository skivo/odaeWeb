using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using odaeWeb.Models.DB;

namespace odaeWeb.Models
{
    public class CodificacionViewModel
    {

        public CodificacionViewModel()
        {
        }

        public CodificacionViewModel(Codificacion codificacion, int filter)
        {
            CodificadorId = codificacion.CodificadorId;
            FaseId = codificacion.FaseId;
            MaterialId = codificacion.MaterialId;
            FileName = codificacion.Material.FileName;
            TieneDuplicado = codificacion.Material.TieneDuplicado;
            NivelId = codificacion.NivelId;
            NivelComentario = codificacion.NivelComentario;
            CursoId = codificacion.CursoId;
            EjeId = codificacion.EjeId;
            ObjetivoId = codificacion.ObjetivoId;
            ObjetivoComentario = codificacion.ObjetivoComentario;
            HabilidadId = codificacion.HabilidadId;
            HabilidadComentario = codificacion.HabilidadComentario;
            TipoTareaId = codificacion.TipoTareaId;
            TipoTareaComentario = codificacion.TipoTareaComentario;
            CorreccionProfesor = codificacion.CorreccionProfesor;
            ErrorEjecucion = codificacion.ErrorEjecucion;
            TrabajaDinero = codificacion.TrabajaDinero;
            ErrorDiseno = codificacion.ErrorDiseno;
            Observaciones = codificacion.Observaciones;
            Estado = codificacion.Estado;
            Filtro = filter;
        }




        public void UpdateEstado()
        {
            int i = 0;

            if (NivelId != null && (NivelId > 0 || NivelComentario != null)) i++;
            if (ObjetivoId != null || (CursoId != null && EjeId != null && ObjetivoComentario != null)) i++;
            if (HabilidadId != null && (HabilidadId > 0 || HabilidadComentario != null)) i++;
            if (TipoTareaId != null && (TipoTareaId > 0 || TipoTareaComentario != null)) i++;
            if (CorreccionProfesor != null) i++;
            if (ErrorEjecucion != null) i++;
            if (TrabajaDinero != null) i++;
            if (ErrorDiseno != null) i++;
            if (Observaciones != null) i++;

            if (i < 2) Estado = i;
            else if (i < 4) Estado = 1;
            else if (i < 7) Estado = 2;
            else if (i < 9) Estado = 3;
            else Estado = 4;
        }

        public Codificacion getCodificacion()
        {
            return new Codificacion
            {
                CodificadorId = this.CodificadorId,
                FaseId = this.FaseId,
                MaterialId = this.MaterialId,
                NivelId = this.NivelId,
                NivelComentario = this.NivelComentario,
                CursoId = this.CursoId,
                EjeId = this.EjeId,
                ObjetivoId = this.ObjetivoId,
                ObjetivoComentario = this.ObjetivoComentario,
                HabilidadId = this.HabilidadId,
                HabilidadComentario = this.HabilidadComentario,
                TipoTareaId = this.TipoTareaId,
                TipoTareaComentario = this.TipoTareaComentario,
                CorreccionProfesor = this.CorreccionProfesor,
                ErrorEjecucion = this.ErrorEjecucion,
                TrabajaDinero = this.TrabajaDinero,
                ErrorDiseno = this.ErrorDiseno,
                Observaciones = this.Observaciones,
                Estado = this.Estado
            };
        }

        public int CodificadorId { get; set; }

        public int FaseId { get; set; }

        public int MaterialId { get; set; }

        public string FileName { get; set; }

        public bool? TieneDuplicado { get; set; }

        [Display(Name = "Nivel demanda cognitiva")]
        public int? NivelId { get; set; }

        [StringLength(255, ErrorMessage = "El comentario puede tener un máximo de {1} caracteres.")]
        public string NivelComentario { get; set; }

        [Display(Name = "Curso")]
        public int? CursoId { get; set; }

        [Display(Name = "Eje")]
        public int? EjeId { get; set; }

        [Display(Name = "Objetivo de aprendizaje")]
        public int? ObjetivoId { get; set; }

        [StringLength(255, ErrorMessage = "El comentario puede tener un máximo de {1} caracteres.")]
        public string ObjetivoComentario { get; set; }

        [Display(Name = "Habilidad")]
        public int? HabilidadId { get; set; }

        [StringLength(255, ErrorMessage = "El comentario puede tener un máximo de {1} caracteres.")]
        public string HabilidadComentario { get; set; }

        [Display(Name = "Tipo de tarea")]
        public int? TipoTareaId { get; set; }

        [StringLength(255, ErrorMessage = "El comentario puede tener un máximo de {1} caracteres.")]
        public string TipoTareaComentario { get; set; }

        [Display(Name = "Corrección")]
        public bool? CorreccionProfesor { get; set; }

        [Display(Name = "Error ejecución")]
        public bool? ErrorEjecucion { get; set; }

        [Display(Name = "Trabaja dinero")]
        public bool? TrabajaDinero { get; set; }

        [Display(Name = "Error diseño")]
        public bool? ErrorDiseno { get; set; }

        [StringLength(2000, ErrorMessage = "{0} puede tener un máximo de {1} caracteres.")]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        public int Estado { get; set; }

        public int? NextItem { get; set; }

        public int? PrevItem { get; set; }

        public int Pos { get; set; }

        public int Total { get; set; }

        public int Filtro { get; set; }
    }
}
