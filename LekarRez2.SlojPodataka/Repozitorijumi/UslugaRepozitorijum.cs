using LekarRez2.Modeli;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LekarRez2.SlojPodataka.Repozitorijumi
{
    public class UslugaRepozitorijum
    {
        private readonly BazaKonekcija _baza;

        public UslugaRepozitorijum()
        {
            _baza = new BazaKonekcija();
        }

        public List<Usluga> VratiSve()
        {
            var lista = new List<Usluga>();
            using (var konekcija = _baza.Otvori())
            {
                var cmd = new SqlCommand("SELECT * FROM Usluga", konekcija);
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        lista.Add(new Usluga
                        {
                            UslugaID = (int)r["UslugaID"],
                            Naziv = r["Naziv"].ToString(),
                            Opis = r["Opis"].ToString(),
                            Cena = (decimal)r["Cena"]
                        });
                    }
                }
            }
            return lista;
        }
    }
}
