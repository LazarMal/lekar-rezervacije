using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LekarRez2.Modeli
{
    /// <summary>
    /// Domenski model korisnika (pacijent/doktor/admin).
    /// Mapira se na tabelu dbo.Korisnik.
    /// </summary>
    public class Korisnik
    {
        public int KorisnikID { get; set; }   // PK
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Uloga { get; set; }     // "Korisnik", "Doktor", "Admin"
    }
}
