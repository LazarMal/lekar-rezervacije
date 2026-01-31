using LekarRez2.Modeli;
using System;
using System.Collections.Generic;

namespace LekarRez2.SlojPodataka.Interfejsi
{
    /// <summary>
    /// Interfejs za sve operacije nad tabelom Rezervacija.
    /// </summary>
    public interface IRezervacijaRepozitorijum
    {
        int BrojZaDan(DateTime datum);                                   // koliko rezervacija ima tog dana
        int BrojZauzetih(int doktorId, DateTime datum, TimeSpan vreme);  // proveri da li je termin zauzet
        bool Zakazi(Rezervacija rezervacija);                            // dodaj novu rezervaciju
        bool Izmeni(Rezervacija rezervacija);                            // izmeni postojeću
        bool Obrisi(int rezervacijaId);                                  // obriši rezervaciju
        IEnumerable<Rezervacija> Sve();                                  // lista svih rezervacija
        IEnumerable<string> SveUsluge();                                 // različite usluge iz baze
        IEnumerable<Korisnik> SviDoktori();
        
    }
}
