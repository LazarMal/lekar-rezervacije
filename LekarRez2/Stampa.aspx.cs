using System;
using System.Web.UI;
using LekarRez2.SlojServisa;
using LekarRez2.SlojPodataka;
using LekarRez2.SlojPodataka.Repozitorijumi;
using LekarRez2.PoslovnaLogika.Servisi; // ✅ dodato zbog ServisUsluga

namespace LekarRez2
{
    public partial class Stampa : Page
    {
        private readonly FilterServis _servis;

        public Stampa()
        {
            var baza = new BazaKonekcija("Ambulanta");
            var db = new DbPomocnik(baza);
            var repo = new FilterRepozitorijum(db);
            _servis = new FilterServis(repo);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UcitajFiltre();

                // ✅ Automatski prikaži sve rezervacije odmah po učitavanju
                gvRezervacije.DataSource = _servis.UcitajZaStampu();
                gvRezervacije.DataBind();
            }
        }

        private void UcitajFiltre()
        {
            // ✅ --- Usluge iz nove tabele USLUGA ---
            ddlUsluga.Items.Clear();
            ddlUsluga.Items.Add("Sve");

            try
            {
                var servisUsluga = new ServisUsluga();
                var usluge = servisUsluga.VratiSveUsluge();

                foreach (var u in usluge)
                {
                    ddlUsluga.Items.Add(u.Naziv);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[Greška pri učitavanju usluga] " + ex.Message);
            }

            // ✅ --- Doktori iz postojećeg servisa ---
            ddlDoktor.Items.Clear();
            ddlDoktor.Items.Add("Svi");

            try
            {
                foreach (var d in _servis.DohvatiSveDoktore())
                    ddlDoktor.Items.Add(d);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[Greška pri učitavanju doktora] " + ex.Message);
            }
        }

        protected void btnFiltriraj_Click(object sender, EventArgs e)
        {
            DateTime? datumOd = string.IsNullOrEmpty(txtDatumOd.Text) ? (DateTime?)null : DateTime.Parse(txtDatumOd.Text);
            DateTime? datumDo = string.IsNullOrEmpty(txtDatumDo.Text) ? (DateTime?)null : DateTime.Parse(txtDatumDo.Text);
            TimeSpan? vreme = string.IsNullOrEmpty(txtVreme.Text) ? (TimeSpan?)null : TimeSpan.Parse(txtVreme.Text);

            var tabela = _servis.Filtriraj(
                ddlDoktor.SelectedValue,
                ddlUsluga.SelectedValue,
                datumOd,
                datumDo,
                vreme
            );

            gvRezervacije.DataSource = tabela;
            gvRezervacije.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlDoktor.SelectedIndex = 0;
            ddlUsluga.SelectedIndex = 0;
            txtDatumOd.Text = "";
            txtDatumDo.Text = "";
            txtVreme.Text = "";

            var tabela = _servis.UcitajZaStampu();
            gvRezervacije.DataSource = tabela;
            gvRezervacije.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
