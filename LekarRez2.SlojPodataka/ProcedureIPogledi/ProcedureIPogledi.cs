using LekarRez2.Modeli;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LekarRez2.SlojPodataka.ProcedureIPogledi
{
    /// <summary>
    /// Klasa koja sadrži metode za pozivanje svih SQL procedura i pogleda u bazi "Ambulanta".
    /// </summary>
    public class ProcedureIPogledi
    {
        private readonly DbPomocnik _db;

        public ProcedureIPogledi(DbPomocnik db)
        {
            _db = db;
        }

        // ===========================================================
        // ==================   PROCEDURE: KORISNIK   =================
        // ===========================================================

        /// <summary>
        /// Poziva proceduru za registraciju novog korisnika.
        /// </summary>
        public bool RegistrujKorisnika(string ime, string prezime, string email, string lozinka, string uloga)
        {
            using (var konekcija = _db.Otvori())
            using (var kmd = new SqlCommand("RegistrujKorisnika", konekcija))
            {
                kmd.CommandType = CommandType.StoredProcedure;
                kmd.Parameters.AddWithValue("@Ime", ime);
                kmd.Parameters.AddWithValue("@Prezime", prezime);
                kmd.Parameters.AddWithValue("@Email", email);
                kmd.Parameters.AddWithValue("@Lozinka", lozinka);
                kmd.Parameters.AddWithValue("@Uloga", uloga);
                return kmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Poziva proceduru za prijavu korisnika (login).
        /// </summary>
        public DataTable LoginKorisnika(string ime, string prezime, string lozinka)
        {
            using (var konekcija = _db.Otvori())
            using (var adapter = new SqlDataAdapter("LoginKorisnika", konekcija))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@Ime", ime);
                adapter.SelectCommand.Parameters.AddWithValue("@Prezime", prezime);
                adapter.SelectCommand.Parameters.AddWithValue("@Lozinka", lozinka);

                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                return tabela;
            }
        }

        /// <summary>
        /// Poziva proceduru za ažuriranje korisnika.
        /// </summary>
        public bool UpdateKorisnika(int korisnikId, string ime, string prezime, string email, string lozinka, string uloga)
        {
            using (var konekcija = _db.Otvori())
            using (var kmd = new SqlCommand("UpdateKorisnika", konekcija))
            {
                kmd.CommandType = CommandType.StoredProcedure;
                kmd.Parameters.AddWithValue("@KorisnikID", korisnikId);
                kmd.Parameters.AddWithValue("@Ime", ime);
                kmd.Parameters.AddWithValue("@Prezime", prezime);
                kmd.Parameters.AddWithValue("@Email", email);
                kmd.Parameters.AddWithValue("@Lozinka", lozinka);
                kmd.Parameters.AddWithValue("@Uloga", uloga);
                return kmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Poziva proceduru za brisanje korisnika.
        /// </summary>
        public bool ObrisiKorisnika(int korisnikId)
        {
            using (var konekcija = _db.Otvori())
            using (var kmd = new SqlCommand("DeleteKorisnika", konekcija))
            {
                kmd.CommandType = CommandType.StoredProcedure;
                kmd.Parameters.AddWithValue("@KorisnikID", korisnikId);
                return kmd.ExecuteNonQuery() > 0;
            }
        }

        // ===========================================================
        // ==================   PROCEDURE: REZERVACIJA   =============
        // ===========================================================

        /// <summary>
        /// Dodaje novu rezervaciju pomoću procedure DodajRezervaciju.
        /// </summary>
        public bool DodajRezervaciju(int korisnikId, int doktorId, string pacijent, DateTime datum, TimeSpan vreme, string status, string usluga, decimal cena)
        {
            using (var konekcija = _db.Otvori())
            using (var kmd = new SqlCommand("DodajRezervaciju", konekcija))
            {
                kmd.CommandType = CommandType.StoredProcedure;
                kmd.Parameters.AddWithValue("@KorisnikID", korisnikId);
                kmd.Parameters.AddWithValue("@DoktorID", doktorId);
                kmd.Parameters.AddWithValue("@Pacijent", pacijent);
                kmd.Parameters.AddWithValue("@Datum", datum);
                kmd.Parameters.AddWithValue("@Vreme", vreme);
                kmd.Parameters.AddWithValue("@Status", status);
                kmd.Parameters.AddWithValue("@Usluga", usluga);
                kmd.Parameters.AddWithValue("@Cena", cena);
                return kmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Kraća verzija zakazivanja termina (koristi proceduru sp_Rezervacija_Zakazi).
        /// </summary>
        public bool ZakaziT(int korisnikId, string pacijent, DateTime datum, TimeSpan vreme,
                    string status, string usluga, int? doktorId, decimal? cena)
        {
            using (var konekcija = _db.Otvori())
            using (var cmd = new SqlCommand("sp_Rezervacija_Zakazi", konekcija))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@KorisnikID", korisnikId);
                cmd.Parameters.AddWithValue("@Pacijent", pacijent ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Datum", datum);
                cmd.Parameters.AddWithValue("@Vreme", vreme);
                cmd.Parameters.AddWithValue("@Status", status ?? "Zakazano");
                cmd.Parameters.AddWithValue("@Usluga", usluga ?? "Pregled");
                cmd.Parameters.AddWithValue("@DoktorID", (object)doktorId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cena", (object)cena ?? DBNull.Value);

                int redova = cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"[DEBUG SQL] sp_Rezervacija_Zakazi → {redova} redova");
                return redova > 0;
            }
        }
        public DataTable UcitajPregledZakazivanja()
        {
            using (var konekcija = _db.Otvori())
            using (var adapter = new SqlDataAdapter("SELECT * FROM dbo.View_StampaRezervacija", konekcija))
            {
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                return tabela;
            }
        }

        /// <summary>
        /// Učitava podatke iz pogleda View_StampaRezervacija.
        /// </summary>
        public DataTable UcitajStampaRezervacija()
        {
            using (var konekcija = _db.Otvori())
            using (var adapter = new SqlDataAdapter("SELECT * FROM dbo.View_StampaRezervacija", konekcija))
            {
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                return tabela;
            }
        }
        public bool Izmeni(Rezervacija r)
        {
            using (var conn = _db.Otvori())
            using (var cmd = new SqlCommand("sp_Rezervacija_Izmeni", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RezervacijaID", r.RezervacijaID);
                cmd.Parameters.AddWithValue("@Datum", r.Datum ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Vreme", r.Vreme ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Pacijent", r.Pacijent ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Usluga", r.Usluga ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", r.Status ?? (object)DBNull.Value);

                int redova = cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"[DEBUG SQL] sp_Rezervacija_Izmeni → {redova} redova");
                return redova > 0;
            }
        }

        public bool ObrisiRezervaciju(int id)
        {
            string sql = "DELETE FROM Rezervacija WHERE RezervacijaID = @ID";
            var parametri = new[] { new SqlParameter("@ID", id) };

            int redova = _db.IzvrsiNaredbu(sql, parametri);
            return redova > 0;
        }
        public DataTable UcitajCenovnik()
        {
            using (var citac = _db.IzvrsiCitanje("sp_Cenovnik_Pregled"))
            {
                DataTable tabela = new DataTable();
                tabela.Load(citac);
                return tabela;
            }
        }

    }
}
