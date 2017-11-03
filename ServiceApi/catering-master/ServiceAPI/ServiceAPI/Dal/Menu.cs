using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceAPI.Dal
{
    public class Menu
    {
    
    public int Id { get; set; }
    public float Costo_Totale { get; set; }
    /*public int Ristorante { get; set; }*/
        public string IdRistorante { get; set; }
        
    }
}
