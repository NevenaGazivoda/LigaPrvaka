using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LigaPrvaka.Models
{
    public class Utakmica
    {
        public int PKUtakmicaID { get; set; }
        public int Domaci { get; set; }
        public int Gosti { get; set; }
        public int Br_golova_d { get; set; }
        public int Br_golova_g { get; set; }
    }
}