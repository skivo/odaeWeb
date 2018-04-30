using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb.Models
{
    public class ListaImagenesViewModel : BaseViewModel
    {
        public ListaImagenesViewModel(BaseViewModel baseVM, int filter, PaginatedList<odaeWeb.Models.DB.Codificacion> lista)
        {
            UserId = baseVM.UserId;
            Perfil = baseVM.Perfil;
            FaseActual = baseVM.FaseActual;
            FaseSel = baseVM.FaseSel;
            Fases = baseVM.Fases;
            Filter = filter;
            Lista = lista;
        }

        public PaginatedList<odaeWeb.Models.DB.Codificacion> Lista { get; set; }
        public int Filter { get; set; }
    }

}
