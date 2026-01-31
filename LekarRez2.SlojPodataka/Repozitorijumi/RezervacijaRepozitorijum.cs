using LekarRez2.Modeli;
using LekarRez2.SlojPodataka.Interfejsi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LekarRez2.SlojPodataka.Repozitorijumi
{
    public sealed class RezervacijaRepozitorijum : IRezervacijaRepozitorijum
    {
        private readonly DbPomocnik _db;

        public RezervacijaRepozitorijum(DbPomocnik db)
        {
            _db = db;
        }

        public int BrojZaDan(DateTime datum)
        {
            var o = _db.IzvrsiSkalar("sp_Rezervacija_BrojZaDan", new SqlParameter("@Datum", datum));
            return Convert.ToInt32(o ?? 0);
        }

        public int BrojZauzetih(int doktorId, DateTime datum, TimeSpan vreme)
        {
            var o = _db.IzvrsiSkalar("sp_Rezervacija_BrojZauzetih",
                new SqlParameter("@DoktorID", doktorId),
                new SqlParameter("@Datum", datum),
                new SqlParameter("@Vreme", vreme));
            return Convert.ToInt32(o ?? 0);
        }

        public bool Zakazi(Rezervacija r)
        {
            int n = _db.IzvrsiNeupit("sp_Rezervacija_Zakazi",
                new SqlParameter("@KorisnikID", (object)r.KorisnikID ?? DBNull.Value),
                new SqlParameter("@Pacijent", (object)r.Pacijent ?? DBNull.Value),
                new SqlParameter("@Datum", (object)r.Datum ?? DBNull.Value),
                new SqlParameter("@Vreme", (object)r.Vreme ?? DBNull.Value),
                new SqlParameter("@Status", (object)r.Status ?? "rezervisano"),
                new SqlParameter("@Usluga", (object)r.Usluga ?? DBNull.Value),
                new SqlParameter("@DoktorID", (object)r.DoktorID ?? DBNull.Value),
                new SqlParameter("@Cena", (object)r.Cena ?? DBNull.Value));
            return n > 0;
        }

        public bool Izmeni(Rezervacija r)
        {
            // priprema parametara (DBNull za prazna polja)
            var parametri = new[]
            {
        new SqlParameter("@RezervacijaID", r.RezervacijaID),
        new SqlParameter("@Datum", r.Datum ?? (object)DBNull.Value),
        new SqlParameter("@Vreme", r.Vreme ?? (object)DBNull.Value),
        new SqlParameter("@Pacijent", string.IsNullOrWhiteSpace(r.Pacijent) ? (object)DBNull.Value : r.Pacijent),
        new SqlParameter("@Usluga", string.IsNullOrWhiteSpace(r.Usluga) ? (object)DBNull.Value : r.Usluga),
        new SqlParameter("@Status", string.IsNullOrWhiteSpace(r.Status) ? (object)DBNull.Value : r.Status)
    };

            // poziv stored procedure
            int redova = _db.IzvrsiNeupit("sp_Rezervacija_Izmeni", parametri);

            // vraća true ako je bar jedan red izmenjen
            return redova > 0;
        }


        public bool Obrisi(int id)
        {
            int n = _db.IzvrsiNeupit("sp_Rezervacija_Obrisi", new SqlParameter("@RezervacijaID", id));
            return n > 0;
        }

        public IEnumerable<Rezervacija> Sve()
        {
            var lista = new List<Rezervacija>();
            using (var citac = _db.IzvrsiCitanje("sp_Rezervacija_ListaSve"))
            {
                while (citac.Read())
                {
                    lista.Add(new Rezervacija
                    {
                        RezervacijaID = (int)citac["RezervacijaID"],
                        KorisnikID = citac["KorisnikID"] as int?,
                        DoktorID = citac["DoktorID"] as int?,
                        Pacijent = citac["Pacijent"].ToString(),
                        Datum = citac["Datum"] as DateTime?,
                        Vreme = citac["Vreme"] as TimeSpan?,
                        Status = citac["Status"].ToString(),
                        Usluga = citac["Usluga"].ToString(),
                        Cena = citac["Cena"] as decimal?,
                        Doktor = citac["Doktor"] != DBNull.Value ? citac["Doktor"].ToString() : "Nema dodeljenog doktora"
                    });
                }
            }
            return lista;
        }

        public IEnumerable<string> SveUsluge()
        {
            var lista = new List<string>();
            using (var citac = _db.IzvrsiCitanje("sp_Usluge_Distinct"))
            {
                while (citac.Read())
                {
                    lista.Add(citac[0].ToString());
                }
            }
            return lista;
        }
        public IEnumerable<Korisnik> SviDoktori()
        {
            var lista = new List<Korisnik>();

            using (var citac = _db.IzvrsiCitanje("sp_Doktori_Lista"))
            {
                while (citac.Read())
                {
                    lista.Add(new Korisnik
                    {
                        KorisnikID = (int)citac["KorisnikID"],
                        Ime = citac["Ime"].ToString(),
                        Prezime = citac["Prezime"].ToString(),
                        Uloga = "Doktor"
                    });
                }
            }

            return lista;
        }
        public IEnumerable<Korisnik> DohvatiDoktore()
        {
            var baza = new BazaKonekcija("Ambulanta");
            var db = new DbPomocnik(baza);
            var repo = new RezervacijaRepozitorijum(db);
            return repo.SviDoktori();
        }

        public IEnumerable<string> DohvatiUsluge()
        {
            var baza = new BazaKonekcija("Ambulanta");
            var db = new DbPomocnik(baza);
            var repo = new RezervacijaRepozitorijum(db);
            return repo.SveUsluge();
        }



    }
}
