using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    [NotMapped]
    public class KlantLogin
    {
		public long Id { get; set; }
		public long KlantId { get; set; }
		public Klant klant { get; set; }
		public string Naam { get; set; }
		public string Voornaam { get; set; }
		public string Gebruikersnaam { get; set; }
		public string Paswoord { get; set; }
		public byte Actief { get; set; }
		public byte Wijzigbaar { get; set; }


	}
}
