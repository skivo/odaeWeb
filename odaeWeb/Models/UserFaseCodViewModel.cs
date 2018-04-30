using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb.Models
{
    public class UserFaseCodViewModel : BaseViewModel
    {
        public UserFaseCodViewModel (DB.UserFaseCodificador UserFaseCod)
        {
            UserId = UserFaseCod.UserId;
            CodificadorId = UserFaseCod.CodificadorId;
            FaseId = UserFaseCod.FaseId;
            Codificador = UserFaseCod.Codificador;
            Fase = UserFaseCod.Fase;
            User = UserFaseCod.User;
        }
            
        public string UserId { get; set; }
        public int CodificadorId { get; set; }
        public int FaseId { get; set; }

        public DB.Codificador Codificador { get; set; }
        public DB.Fase Fase { get; set; }
        public DB.User User { get; set; }

    }
}
