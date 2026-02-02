using System;

namespace LekarRez2.PoslovnaLogika.PravilaSistema
{
    /// <summary>
    /// Centralna statička klasa za poslovna pravila sistema.
    /// Koristi se u svim servisima i slojevima logike.
    /// </summary>
    public static class PravilaLogike
    {
        // ==========================================================
        // ✅ PROVERE VEZANE ZA REZERVACIJE / TERMIN
        // ==========================================================

        /// <summary>
        /// Proverava da li je uneti datum danas ili u budućnosti.
        /// </summary>
        public static bool ProveriDatumUBuducnosti(DateTime datum)
        {
            return datum.Date >= DateTime.Today;
        }

        /// <summary>
        /// Proverava da li je vreme u okviru radnog vremena (08:00–16:00).
        /// </summary>
        public static bool ProveriRadnoVreme(string vreme)
        {
            if (!TimeSpan.TryParse(vreme, out var termin))
                return false;

            var pocetak = new TimeSpan(8, 0, 0);
            var kraj = new TimeSpan(16, 0, 0);
            return termin >= pocetak && termin <= kraj;
        }

        /// <summary>
        /// Proverava da li broj rezervacija za dan ne prelazi maksimum.
        /// </summary>
        public static bool ProveriBrojRezervacija(int broj, int maxDozvoljeno)
        {
            return broj < maxDozvoljeno;
        }

        // ==========================================================
        // ✅ PROVERE ZA LOGIN / REGISTRACIJU
        // ==========================================================

        /// <summary>
        /// Proverava da li su uneta sva tri obavezna polja za prijavu.
        /// </summary>
        public static bool ProveriPoljaLogin(string ime, string prezime, string lozinka)
        {
            return !string.IsNullOrWhiteSpace(ime)
                && !string.IsNullOrWhiteSpace(prezime)
                && !string.IsNullOrWhiteSpace(lozinka);
        }

        /// <summary>
        /// Proverava da li lozinka ima najmanje 6 karaktera.
        /// </summary>
        public static bool ProveriJacinuLozinke(string lozinka)
        {
            return !string.IsNullOrWhiteSpace(lozinka) && lozinka.Length >= 6;
        }

        /// <summary>
        /// Proverava da li su popunjena sva obavezna polja prilikom registracije.
        /// </summary>
        public static bool ProveriPoljaRegistracija(string ime, string prezime, string email, string lozinka, string uloga)
        {
            return !string.IsNullOrWhiteSpace(ime)
                && !string.IsNullOrWhiteSpace(prezime)
                && !string.IsNullOrWhiteSpace(email)
                && !string.IsNullOrWhiteSpace(lozinka)
                && !string.IsNullOrWhiteSpace(uloga);
        }



        /// <summary>
        /// Proverava da li je email adresa u validnom formatu.
        /// </summary>
        public static bool ProveriEmailFormat(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
        }

        /// <summary>
        /// Proverava da li je tekst (npr. ime, prezime) duži od 2 karaktera.
        /// </summary>
        public static bool ProveriMinimalnuDuzinu(string tekst, int min = 2)
        {
            return !string.IsNullOrWhiteSpace(tekst) && tekst.Trim().Length >= min;
        }
    }
}
