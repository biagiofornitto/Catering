using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceAPI.Dal
{
   public class Invitato
    {
        
        public int Id { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public int IdCat { get; set; }
    }
}
