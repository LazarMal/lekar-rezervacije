using LekarRez2.Modeli;
using LekarRez2.SlojPodataka;
using LekarRez2.SlojPodataka.Interfejsi;
using LekarRez2.SlojPodataka.Repozitorijumi;

namespace LekarRez2.PoslovnaLogika.Servisi
{
    /// <summary>
    /// Poslovni servis za korisnike (prijava, registracija, dohvat doktora).
    /// Povezuje sloj podataka (repozitorijum) sa slojem prezentacije.
    /// </summary>
    public sealed class ServisKorisnik
    {
        private readonly IKorisnikRepozitorijum _repozitorijum;

        // 🔹 Konstruktor sa repozitorijumom (koristi se ako se ručno prosleđuje iz fabrike)
        public ServisKorisnik(IKorisnikRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum;
        }

        // 🔹 Konstruktor bez parametara — sam kreira repozitorijum i konekciju
        public ServisKorisnik()
        {
            var baza = new BazaKonekcija("Ambulanta");  // čita connection string iz Web.config
            var db = new DbPomocnik(baza);              // pomoćna klasa za izvršavanje upita
            _repozitorijum = new KorisnikRepozitorijum(db); // konkretna implementacija interfejsa
        }

        /// <summary>
        /// Prijavljuje korisnika (login) – vraća objekat tipa Korisnik ako su podaci ispravni.
        /// </summary>
        public Korisnik Prijavi(string ime, string prezime, string lozinka)
        {
            return _repozitorijum.Prijavi(ime, prezime, lozinka);
        }

        /// <summary>
        /// Registruje novog korisnika – vraća true ako je registracija uspešna.
        /// </summary>
        public bool Registruj(Korisnik korisnik)
        {
            return _repozitorijum.Registruj(korisnik);
        }
    }
}
