using System;
using System.Collections;
using System.Collections.Generic;

namespace WellStralerWebshop.Models.Domain
{
	public class Klant 
    {
		public long Id { get; set; }
		public short? Taal { get; set; }
		public string Firma { get; set; }
		public string Titel { get; set; }
		public string Naam { get; set; }
		public string Voornaam { get; set; }
		public string Btwnummer { get; set; }
		public string Straat { get; set; }
		public string Land { get; set; }

		public string Postcode { get; set; }
		public string FacturatieAdres { get; set; }
		public string FacturatiePostcode { get; set; }
		public string Telefoon { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
		public string Website { get; set; }
		public string Mobile { get; set; }
		public short? Leverdag { get; set; }
		public DateTime? LaatsteLevering { get; set; }
		public short? Korting1 { get; set; }
		public short? Korting2 { get; set; }
		public DateTime DatumInbreng { get; set; }
		public short? UserIdInbreng { get; set; }
		public DateTime DatumWijziging { get; set; }
		public short? UserIdWijziging { get; set; }
		public byte? BtwPlichtig { get; set; }
		public decimal? OpenstaandeSaldo { get; set; }
		public decimal? Omzet { get; set; }
		public decimal? OmzetVJ { get; set; }

		public ICollection<KlantKoppeling> KlantKoppelingen { get; set; }
	}
}
