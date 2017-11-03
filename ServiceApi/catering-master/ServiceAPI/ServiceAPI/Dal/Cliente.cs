using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ServiceAPI.Dal
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nome {get;set;}
        public string Cognome {get;set;}
       
        
        public string Codice_Fiscale { get; set; }
        public string Citta { get; set; }
        public string Indirizzo { get; set; }
        public string Telefono { get; set; }
        public int N_Catering { get; set; }
        public float Spesa_Totale { get; set; }
    }
}
