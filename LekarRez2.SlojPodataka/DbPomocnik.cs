using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LekarRez2.SlojPodataka
{
   
        /// <summary>
        /// Klasa za pomoćne metode pri radu sa bazom (SP i SQL komande).
        /// </summary>
        public sealed class DbPomocnik
        {
            private readonly BazaKonekcija _baza;

        // Konstruktor – dobija instancu BazaKonekcija
        public DbPomocnik(BazaKonekcija baza)
        {
            _baza = baza;
        }

        /// <summary>
        /// Otvara novu SQL konekciju i vraća je (koristi se u drugim klasama koje same rade upite).
        /// </summary>
        public SqlConnection Otvori()
        {
            var konekcija = _baza.Otvori();
            return konekcija;
        }

        /// <summary>
        /// Izvršava stored proceduru ili SQL komandu bez vraćanja rezultata.
        /// (INSERT, UPDATE, DELETE)
        /// </summary>
        public int IzvrsiNeupit(string nazivSp, params SqlParameter[] parametri)
            {
                using (var konekcija = _baza.Otvori())
                using (var cmd = new SqlCommand(nazivSp, konekcija))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametri?.Length > 0)
                        cmd.Parameters.AddRange(parametri);
                    return cmd.ExecuteNonQuery();
                }
            }

            /// <summary>
            /// Izvršava stored proceduru i vraća jednu vrednost (SELECT COUNT, MAX...).
            /// </summary>
            public object IzvrsiSkalar(string nazivSp, params SqlParameter[] parametri)
            {
                using (var konekcija = _baza.Otvori())
                using (var cmd = new SqlCommand(nazivSp, konekcija))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametri?.Length > 0)
                        cmd.Parameters.AddRange(parametri);
                    return cmd.ExecuteScalar();
                }
            }

            /// <summary>
            /// Izvršava stored proceduru koja vraća više redova (SELECT ...).
            /// </summary>
            public IDataReader IzvrsiCitanje(string nazivSp, params SqlParameter[] parametri)
            {
                var konekcija = _baza.Otvori();
                var cmd = new SqlCommand(nazivSp, konekcija)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (parametri?.Length > 0)
                    cmd.Parameters.AddRange(parametri);

                // CommandBehavior.CloseConnection → zatvori konekciju automatski kad reader.Dispose()
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

        /// <summary>
        /// Izvršava običan SQL tekst (UPDATE, INSERT, DELETE) – za komande koje nisu stored procedure.
        /// </summary>
        public int IzvrsiNaredbu(string sqlTekst, params SqlParameter[] parametri)
        {
            using (var konekcija = _baza.Otvori())
            using (var cmd = new SqlCommand(sqlTekst, konekcija))
            {
                cmd.CommandType = CommandType.Text; // ključno!
                if (parametri?.Length > 0)
                    cmd.Parameters.AddRange(parametri);
                return cmd.ExecuteNonQuery();
            }
        }

    }
}
