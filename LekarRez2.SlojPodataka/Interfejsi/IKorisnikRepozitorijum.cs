using LekarRez2.Modeli;
using System.Collections.Generic;

namespace LekarRez2.SlojPodataka.Interfejsi
{
    /// <summary>
    /// Interfejs definiše koje metode mora imati svaki repozitorijum za rad sa korisnicima.apstrakcija
    /// </summary>
    public interface IKorisnikRepozitorijum
    {
        Korisnik Prijavi(string ime, string prezime, string lozinka);   // prijava korisnika
        bool Registruj(Korisnik korisnik);                             // registracija
        IEnumerable<Korisnik> SviDoktori();                            // lista doktora za dropdown
    }
}
