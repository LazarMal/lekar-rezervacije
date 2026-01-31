using System;
using System.Data;
using System.Data.SqlClient;

namespace LekarRez2.SlojPodataka.Repozitorijumi
{
    public sealed class FilterRepozitorijum
    {
        private readonly DbPomocnik _db;

        public FilterRepozitorijum(DbPomocnik db)
        {
            _db = db;
        }

        // ✅ Učitava sve rezervacije iz view-a
        public DataTable SveRezervacije()
        {
            using (var konekcija = _db.Otvori())
            using (var cmd = new SqlCommand("SELECT * FROM View_StampaRezervacija", konekcija))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }
        }

        // ✅ Učitava filtrirane rezervacije iz view-a
        public DataTable FiltriraneRezervacije(string doktor, string usluga, DateTime? datumOd, DateTime? datumDo, TimeSpan? vreme)
        {
            using (var konekcija = _db.Otvori())
            using (var cmd = new SqlCommand("SELECT * FROM View_StampaRezervacija WHERE 1=1", konekcija))
            {
                if (!string.IsNullOrEmpty(doktor) && doktor != "Svi")
                {
                    cmd.CommandText += " AND ISNULL(Doktor, '') = @Doktor";
                    cmd.Parameters.AddWithValue("@Doktor", doktor);
                }

                if (!string.IsNullOrEmpty(usluga) && usluga != "Sve")
                {
                    cmd.CommandText += " AND Usluga = @Usluga";
                    cmd.Parameters.AddWithValue("@Usluga", usluga);
                }

                if (datumOd.HasValue)
                {
                    cmd.CommandText += " AND Datum >= @DatumOd";
                    cmd.Parameters.AddWithValue("@DatumOd", datumOd);
                }

                if (datumDo.HasValue)
                {
                    cmd.CommandText += " AND Datum <= @DatumDo";
                    cmd.Parameters.AddWithValue("@DatumDo", datumDo);
                }

                if (vreme.HasValue)
                {
                    cmd.CommandText += " AND Vreme = @Vreme";
                    cmd.Parameters.AddWithValue("@Vreme", vreme);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }
        }
    }
}
