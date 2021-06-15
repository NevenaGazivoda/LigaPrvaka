using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LigaPrvaka.Models
{
    public class Igrac
    {
        public int PKIgracID { get; set; }
        public string Ime { get; set; }
        public int FKTimID { get; set; } 
    }
}