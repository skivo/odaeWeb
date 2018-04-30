using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb.Models
{
    public class BaseViewModel
    {
        public string UserId { get; set; }
        public int Perfil { get; set; }
        public int FaseActual { get; set; }
        public int FaseSel { get; set; }
        public List<Models.DB.Fase> Fases { get; set; }
    }
}
