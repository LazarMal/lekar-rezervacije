using System;
using System.Data;
using LekarRez2.Modeli;
using LekarRez2.SlojPodataka;
using LekarRez2.SlojPodataka.ProcedureIPogledi;
using LekarRez2.PoslovnaLogika.PravilaSistema;
using LekarRez2.PoslovnaLogika.Izuzeci;
using LekarRez2.SlojPodataka.Repozitorijumi;
using System.Collections.Generic;
using System.Linq;

namespace LekarRez2.PoslovnaLogika.Servisi
{
    /// <summary>
    /// Servis koji upravlja zakazivanjima u sistemu (CRUD za rezervacije).
    /// Ovde se kombinuju poslovna pravila i pozivi stored procedura.
    /// </summary>
    public sealed class ServisRezervacija
    {
        private readonly ProcedureIPogledi _procedure;

        public ServisRezervacija()
        {
            // vežemo slojeve: konekcija → pomoćnik → procedure
            var baza = new BazaKonekcija("Ambulanta");
            var db = new DbPomocnik(baza);
            _procedure = new ProcedureIPogledi(db);
        }

        /// <summary>
        /// Zakazuje novi termin (koristi stored proceduru ZakaziT).
        /// </summary>
        public void Zakazi(Rezervacija r)
        {
            // 1. Validacija
            if (!r.Datum.HasValue || !r.Vreme.HasValue)
                throw new PoslovnoPraviloIzuzetak("Unesite datum i vreme.");

            if (!PravilaLogike.ProveriDatumUBuducnosti(r.Datum.Value))
                throw new PoslovnoPraviloIzuzetak("Ne možete zakazati termin u prošlosti.");

            if (!PravilaLogike.ProveriRadnoVreme(r.Vreme.Value.ToString()))
                throw new PoslovnoPraviloIzuzetak("Termin mora biti između 08:00 i 16:00.");

            // 2. Default vrednosti
            if (string.IsNullOrWhiteSpace(r.Pacijent))
                r.Pacijent = "Nepoznat";
            if (string.IsNullOrWhiteSpace(r.Status))
                r.Status = "Zakazano";
            if (string.IsNullOrWhiteSpace(r.Usluga))
                r.Usluga = "Pregled";

            // 3. Poziv procedure
            bool uspeh = _procedure.ZakaziT(
                r.KorisnikID ?? 0,
                r.Pacijent,
                r.Datum.Value,
                r.Vreme.Value,
                r.Status,
                r.Usluga,
                r.DoktorID,
                r.Cena
            );

            if (!uspeh)
                throw new PoslovnoPraviloIzuzetak("Zakazivanje nije uspelo.");

            System.Diagnostics.Debug.WriteLine(
                $"[DEBUG] Zakazano: ID={r.KorisnikID}, Pacijent={r.Pacijent}, Datum={r.Datum}, Vreme={r.Vreme}, Usluga={r.Usluga}"
            );
        }

        public DataTable UcitajSve()
        {
            return _procedure.UcitajPregledZakazivanja();
        }

        /// <summary>
        /// Učitava podatke za štampu (View_StampaRezervacija).
        /// </summary>
        public DataTable UcitajZaStampu()
        {
            return _procedure.UcitajStampaRezervacija();
        }

        /// <summary>
        /// Ažurira postojeću rezervaciju (poziva stored proceduru DodajRezervaciju ponovo).
        /// </summary>
        public void Izmeni(Rezervacija r)
        {
            try
            {
                if (!PravilaLogike.ProveriDatumUBuducnosti(r.Datum ?? DateTime.Today))
                    throw new PoslovnoPraviloIzuzetak("Ne možete postaviti termin u prošlosti.");

                if (!PravilaLogike.ProveriRadnoVreme((r.Vreme ?? TimeSpan.Zero).ToString()))
                    throw new PoslovnoPraviloIzuzetak("Termin mora biti između 08:00 i 16:00.");

                if (string.IsNullOrWhiteSpace(r.Pacijent))
                    r.Pacijent = "Nepoznat";

                bool uspeh = _procedure.Izmeni(r);

                if (!uspeh)
                    throw new PoslovnoPraviloIzuzetak("Ažuriranje nije uspelo.");

                System.Diagnostics.Debug.WriteLine($"[DEBUG] Ažurirano ID={r.RezervacijaID} → {r.Pacijent}, {r.Datum}, {r.Vreme}");
            }
            catch (PoslovnoPraviloIzuzetak ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Poslovno pravilo: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Neočekivana greška: {ex.Message}");
                throw new PoslovnoPraviloIzuzetak("Greška pri ažuriranju podataka.");
            }

        }



        public void Obrisi(int id)
        {
            bool uspeh = _procedure.ObrisiRezervaciju(id);

            if (!uspeh)
                throw new PoslovnoPraviloIzuzetak("Brisanje rezervacije nije uspelo. Pokušajte ponovo.");
        }
        public DataTable FiltrirajRezervacije(string doktor, string usluga, DateTime? datumOd, DateTime? datumDo, TimeSpan? vreme)
        {
            var baza = new BazaKonekcija("Ambulanta");
            var db = new DbPomocnik(baza);
            var repo = new FilterRepozitorijum(db);

            return repo.FiltriraneRezervacije(doktor, usluga, datumOd, datumDo, vreme);
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
        public List<Rezervacija> DohvatiSveRezervacije()
        {
            var tabela = _procedure.UcitajPregledZakazivanja();
            return tabela.AsEnumerable().Select(r => new Rezervacija
            {
                RezervacijaID = Convert.ToInt32(r["RezervacijaID"]),
                Pacijent = r["Pacijent"].ToString(),
                Usluga = r["Usluga"].ToString(),
                Status = r["Status"].ToString(),
                Datum = r["Datum"] as DateTime?,
                Vreme = r["Vreme"] as TimeSpan?
            }).ToList();
        }


    }
}
