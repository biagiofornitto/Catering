using System;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceAPI.Dal
{
    public class Catering
    {
    public int Id { get; set; }
    public int Codice { get; set; }
    public DateTime Data { get; set; }
    public string Cliente { get; set; }
    public int Menu { get; set; }
    public int NumeroP { get; set; }
	public float CostoT { get; set; }

    }
    
}
