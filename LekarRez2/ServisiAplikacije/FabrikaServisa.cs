using LekarRez2.SlojPodataka;
using LekarRez2.SlojPodataka.Repozitorijumi;
using LekarRez2.PoslovnaLogika.Servisi;

namespace LekarRez2.ServisiAplikacije
{
    /// <summary>
    /// Centralna tačka koja kreira i povezuje servise sa repozitorijumima i bazom.
    /// </summary>
    public static class FabrikaServisa
    {
        public static ServisKorisnik NapraviKorisnik()
        {
            var db = new DbPomocnik(new BazaKonekcija());
            var repo = new KorisnikRepozitorijum(db);
            return new ServisKorisnik(repo);
        }

        public static ServisRezervacija NapraviRezervacija()
        {
            return new ServisRezervacija();
        }
    }
}
