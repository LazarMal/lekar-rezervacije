using LekarRez2.Modeli;
using LekarRez2.SlojPodataka.Interfejsi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LekarRez2.SlojPodataka.Repozitorijumi
{
    public sealed class KorisnikRepozitorijum : IKorisnikRepozitorijum
    {
        private readonly DbPomocnik _db;

        public KorisnikRepozitorijum(DbPomocnik db)
        {
            _db = db;
        }

        public Korisnik Prijavi(string ime, string prezime, string lozinka)
        {
            using (var citac = _db.IzvrsiCitanje("LoginKorisnika",
                new SqlParameter("@Ime", ime),
                new SqlParameter("@Prezime", prezime),
                new SqlParameter("@Lozinka", lozinka)))
            {
                if (citac.Read())
                {
                    return new Korisnik
                    {
                        KorisnikID = citac["KorisnikID"] != DBNull.Value ? Convert.ToInt32(citac["KorisnikID"]) : 0,
                        Ime = citac["Ime"].ToString(),
                        Prezime = citac["Prezime"].ToString(),
                        Uloga = citac["Uloga"].ToString()
                    };
                }
            }
            return null;
        }


        public bool Registruj(Korisnik korisnik)
        {
            int n = _db.IzvrsiNeupit("RegistrujKorisnika",
                new SqlParameter("@Ime", korisnik.Ime),
                new SqlParameter("@Prezime", korisnik.Prezime),
                new SqlParameter("@Email", korisnik.Email),
                new SqlParameter("@Lozinka", korisnik.Lozinka),
                new SqlParameter("@Uloga", korisnik.Uloga));
            return n > 0;
        }

        public IEnumerable<Korisnik> SviDoktori()
        {
            var lista = new List<Korisnik>();
            using (var citac = _db.IzvrsiCitanje("sp_Doktori_Svi"))
            {
                while (citac.Read())
                {
                    lista.Add(new Korisnik
                    {
                        KorisnikID = (int)citac["KorisnikID"],
                        Ime = citac["Ime"].ToString(),
                        Prezime = citac["Prezime"].ToString(),
                        Uloga = citac["Uloga"].ToString()
                    });
                }
            }
            return lista;
        }
    }
}
