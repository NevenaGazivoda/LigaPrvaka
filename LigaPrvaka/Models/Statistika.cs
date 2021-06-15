using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LigaPrvaka.Models
{
    public class Statistika
    {
        public int FKIgracID { get; set; }
        public int FKUtakmicaID { get; set; }
        public int Br_golova { get; set; }
    }
}