using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LekarRez2.SlojPodataka.Repozitorijumi;

namespace LekarRez2.SlojServisa
{
    public class FilterServis
    {
        private readonly FilterRepozitorijum _repo;

        public FilterServis(FilterRepozitorijum repo)
        {
            _repo = repo;
        }

        public DataTable UcitajZaStampu()
        {
            return _repo.SveRezervacije();
        }

        public List<string> DohvatiSveUsluge()
        {
            var tabela = _repo.SveRezervacije();
            return tabela.AsEnumerable()
                         .Select(r => r["Usluga"].ToString())
                         .Where(x => !string.IsNullOrWhiteSpace(x))
                         .Distinct()
                         .OrderBy(x => x)
                         .ToList();
        }

        public List<string> DohvatiSveDoktore()
        {
            var tabela = _repo.SveRezervacije();
            return tabela.AsEnumerable()
                         .Select(r => r["Doktor"].ToString())
                         .Where(x => !string.IsNullOrWhiteSpace(x))
                         .Distinct()
                         .OrderBy(x => x)
                         .ToList();
        }

        public DataTable Filtriraj(string doktor, string usluga, DateTime? od, DateTime? doo, TimeSpan? vreme)
        {
            return _repo.FiltriraneRezervacije(doktor, usluga, od, doo, vreme);
        }
    }
}
