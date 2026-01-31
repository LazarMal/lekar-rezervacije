using System.Configuration;
using System.Data.SqlClient;

namespace LekarRez2.SlojPodataka
{
    /// <summary>
    /// Centralna klasa za konekciju ka bazi Ambulanta.
    /// Čita connection string (po imenu "Ambulanta") iz Web.config koji treba biti kreiran u projektu za web aplikaciju.
    /// </summary>
    public sealed class BazaKonekcija
    {
        private readonly string _cs; // čuva tekst konekcije

        // Konstruktor – učitava connection string iz Web.config-a
        public BazaKonekcija(string naziv = "Ambulanta")
        {
            _cs = ConfigurationManager.ConnectionStrings[naziv].ConnectionString;
        }

        /// <summary>
        /// Otvara SQL konekciju i vraća je.
        /// </summary>
        public SqlConnection Otvori()
        {
            var konekcija = new SqlConnection(_cs);
            konekcija.Open();
            return konekcija;
        }

        /// <summary>
        /// Vraća connection string (ako je potrebno negde proslediti kao tekst).
        /// </summary>
        public string GetConnectionString()
        {
            return _cs;
        }
    }
}
