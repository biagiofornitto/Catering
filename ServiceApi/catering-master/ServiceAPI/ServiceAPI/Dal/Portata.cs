using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ServiceAPI.Dal
{
    public class Portata
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Costo { get; set; }

        public int IdMenu { get; set; }
    }
}
