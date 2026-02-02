using LekarRez2.Modeli;
using LekarRez2.SlojPodataka.Repozitorijumi;
using System.Collections.Generic;

namespace LekarRez2.PoslovnaLogika.Servisi
{
    public class ServisUsluga
    {
        private readonly UslugaRepozitorijum _repo;

        public ServisUsluga()
        {
            _repo = new UslugaRepozitorijum();
        }

        public List<Usluga> VratiSveUsluge()
        {
            return _repo.VratiSve();
        }
    }
}
