using System.Data;
using LekarRez2.SlojPodataka;
using LekarRez2.SlojPodataka.ProcedureIPogledi;

namespace LekarRez2.PoslovnaLogika.Servisi
{
    public sealed class ServisCenovnik
    {
        private readonly ProcedureIPogledi _procedure;

        public ServisCenovnik()
        {
            var baza = new BazaKonekcija("Ambulanta");
            var db = new DbPomocnik(baza);
            _procedure = new ProcedureIPogledi(db);
        }

        public DataTable UcitajCenovnik()
        {
            return _procedure.UcitajCenovnik();
        }
    }
}
