using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb.Models
{
    public class AdminIndexViewModel : BaseViewModel
    {
        public IEnumerable<odaeWeb.Models.DB.UserFaseCodificador> UserFaseCod { get; set; }
    }
}
