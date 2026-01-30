using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LekarRez2.Modeli
{
    /// <summary>
    /// Domenski model termina/rezervacije.
    /// Mapira se na tabelu dbo.Rezervacija.
    /// </summary>
    public class Rezervacija
    {
        public int RezervacijaID { get; set; }  // PK

        // Veze:
        public int? KorisnikID { get; set; }    // Pacijent (FK -> Korisnik), može biti null
        public int? DoktorID { get; set; }      // Doktor (FK -> Korisnik), može biti null

        // Podaci o terminu:
        public string Pacijent { get; set; }    // Ime i prezime (tekstualno)
        public DateTime? Datum { get; set; }    // DATE
        public TimeSpan? Vreme { get; set; }    // TIME(7)
        public string Status { get; set; }      // npr. "rezervisano", "otkazano"
        public string Usluga { get; set; }      // naziv usluge (ako nema posebne tabele)
        public decimal? Cena { get; set; }      // DECIMAL(10,2), može biti null
        public string Doktor { get; set; }
    }
}
