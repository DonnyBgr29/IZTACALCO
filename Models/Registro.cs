using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iztacalco.Models
{

    public class Registro
    {
        public Guid Id {get; set;}

    	public string Sector {get; set;}
        
        public string JSector {get; set;}

        public string TAtendidos {get; set;}

        public string TNAtendidos {get; set;}

        public string Calles {get; set;}

        public string Imagen {get; set;}

        public string Observaciones {get; set;}
    }







}