using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;


namespace ServiceAPI.Dal
{
    public class Utente
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Tipo { get; set; }
    }
}
