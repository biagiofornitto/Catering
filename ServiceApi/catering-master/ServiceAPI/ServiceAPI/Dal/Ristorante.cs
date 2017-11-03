using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ServiceAPI.Dal
{
    public class Ristorante
    {
        public int Id { get; set; }
        public string Username { get; set; }
	/*FOREIGN KEY(username) references utenti(username), */
    
    public string Piva { get; set; }
    public string Indirizzo { get; set; }

       
    }
}
