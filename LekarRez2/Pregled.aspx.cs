using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using LekarRez2.Modeli;
using LekarRez2.PoslovnaLogika.Servisi;
using LekarRez2.PoslovnaLogika.Izuzeci;
using LekarRez2.PoslovnaLogika.PravilaSistema;

namespace LekarRez2
{
    public partial class Zakazivanje : Page
    {
        private readonly ServisRezervacija _servis = new ServisRezervacija();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UcitajMrezu();
                UcitajUsluge();
            }
        }

        // -------------------- UČITAVANJE REZERVACIJA --------------------
        private void UcitajMrezu()
        {
            try
            {
                DataTable tabela = _servis.UcitajSve();
                GridViewRezervacije.DataSource = tabela;
                GridViewRezervacije.DataBind();
            }
            catch (Exception ex)
            {
                PrikaziPoruku("Greška pri učitavanju: " + ex.Message, true);
            }
        }

        // -------------------- FILTRIRANJE --------------------
        private void UcitajUsluge()
        {
            try
            {
                DataTable tabela = _servis.UcitajSve();
                var usluge = tabela.AsEnumerable()
                                   .Select(r => r["Usluga"].ToString())
                                   .Distinct()
                                   .OrderBy(u => u)
                                   .ToList();

                ddlUsluge.Items.Clear();
                ddlUsluge.Items.Add("— Izaberi uslugu —");
                foreach (var u in usluge)
                    ddlUsluge.Items.Add(u);
            }
            catch
            {
                ddlUsluge.Items.Clear();
                ddlUsluge.Items.Add("Greška pri učitavanju usluga");
            }
        }

        protected void btnFiltriraj_Click(object sender, EventArgs e)
        {
            try
            {
                string usluga = ddlUsluge.SelectedValue;
                if (usluga == "— Izaberi uslugu —")
                {
                    PrikaziPoruku("Izaberite uslugu za filtriranje.", true);
                    return;
                }

                DataTable tabela = _servis.UcitajSve();
                DataView view = new DataView(tabela);
                view.RowFilter = $"Usluga = '{usluga.Replace("'", "''")}'";

                GridViewRezervacije.DataSource = view;
                GridViewRezervacije.DataBind();

                PrikaziPoruku($"Prikazane su rezervacije za uslugu: {usluga}");
            }
            catch (Exception ex)
            {
                PrikaziPoruku("Greška pri filtriranju: " + ex.Message, true);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            UcitajMrezu();
            ddlUsluge.SelectedIndex = 0;
            PrikaziPoruku("Prikazane su sve rezervacije.");
        }

        // -------------------- DOGAĐAJI GRID-A --------------------
        protected void GridViewRezervacije_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRezervacije.EditIndex = e.NewEditIndex;
            UcitajMrezu();
        }

        protected void GridViewRezervacije_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRezervacije.EditIndex = -1;
            UcitajMrezu();
        }

        protected void GridViewRezervacije_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rezervacijaId = Convert.ToInt32(GridViewRezervacije.DataKeys[e.RowIndex].Value);
                GridViewRow red = GridViewRezervacije.Rows[e.RowIndex];

                string datumTekst = Procitaj(red, 1);
                string vremeTekst = Procitaj(red, 2);
                string pacijent = Procitaj(red, 3);
                string status = Procitaj(red, 4);
                string usluga = Procitaj(red, 5);

                if (!DateTime.TryParse(datumTekst, out DateTime datum))
                {
                    PrikaziPoruku("Unesite ispravan datum (npr. 2025-10-06).", true);
                    return;
                }

                if (!TimeSpan.TryParse(vremeTekst, out TimeSpan vreme))
                {
                    PrikaziPoruku("Unesite ispravno vreme (hh:mm).", true);
                    return;
                }

                Rezervacija r = new Rezervacija
                {
                    RezervacijaID = rezervacijaId,
                    Datum = datum,
                    Vreme = vreme,
                    Pacijent = pacijent,
                    Usluga = usluga,
                    Status = status
                };

                _servis.Izmeni(r);
                GridViewRezervacije.EditIndex = -1;
                UcitajMrezu();
                PrikaziPoruku("✅ Rezervacija uspešno ažurirana.");
            }
            catch (PoslovnoPraviloIzuzetak ex)
            {
                PrikaziPoruku(ex.Message, true);
            }
            catch (Exception ex)
            {
                PrikaziPoruku("Greška pri ažuriranju: " + ex.Message, true);
            }
        }

        protected void GridViewRezervacije_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GridViewRezervacije.DataKeys[e.RowIndex].Value);
                _servis.Obrisi(id);
                UcitajMrezu();
                PrikaziPoruku("Rezervacija je obrisana.");
            }
            catch (Exception ex)
            {
                PrikaziPoruku("Greška pri brisanju: " + ex.Message, true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        // -------------------- POMOĆNE METODE --------------------
        private string Procitaj(GridViewRow red, int index)
        {
            if (red.Cells[index].Controls.Count > 0 && red.Cells[index].Controls[0] is TextBox tb)
                return tb.Text.Trim();
            return red.Cells[index].Text.Trim();
        }

        private void PrikaziPoruku(string poruka, bool jeGreska = false)
        {
            lblPoruka.Text = poruka;
            lblPoruka.ForeColor = jeGreska ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }
    }
}
