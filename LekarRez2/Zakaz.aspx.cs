using System;
using System.Web.UI;
using LekarRez2.Modeli;
using LekarRez2.PoslovnaLogika.Servisi;
using LekarRez2.PoslovnaLogika.Izuzeci;
using System.Web.UI.WebControls;

namespace LekarRez2
{
    public partial class Zakaz : Page
    {
        private readonly ServisRezervacija _servis;

        public Zakaz()
        {
            _servis = new ServisRezervacija();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PacijentID"] == null)
                {
                    Response.Redirect("login.aspx");
                    return;
                }

                PostaviPacijentaIzSesije();
                UcitajUsluge();
                UcitajDoktore();
            }
        }

        // -------------------- UI POMOĆNE METODE --------------------

        private void PostaviPacijentaIzSesije()
        {
            if (Session["PacijentIme"] == null || Session["PacijentPrezime"] == null)
            {
                txtPacijent.Text = "Nepoznat";
                return;
            }

            string ime = Session["PacijentIme"].ToString();
            string prezime = Session["PacijentPrezime"].ToString();
            txtPacijent.Text = (ime + " " + prezime).Trim();
            txtPacijent.ReadOnly = true;

            System.Diagnostics.Debug.WriteLine($"[DEBUG ZAKAZ] Ucitano iz sesije: {txtPacijent.Text}");
        }


        private void UcitajDoktore()
        {
            ddlDoktor.Items.Clear();
            ddlDoktor.Items.Add(new ListItem("Izaberite doktora", "0"));

            foreach (var d in _servis.DohvatiDoktore())
            {
                ddlDoktor.Items.Add(new ListItem($"{d.Ime} {d.Prezime}", d.KorisnikID.ToString()));
            }
        }

        private void UcitajUsluge()
        {
            ddlUsluga.Items.Clear();
            ddlUsluga.Items.Add(new ListItem("Izaberite uslugu", "0"));

            var servisUsluga = new ServisUsluga();
            var usluge = servisUsluga.VratiSveUsluge();

            foreach (var u in usluge)
            {
                ddlUsluga.Items.Add(new ListItem(u.Naziv, u.UslugaID.ToString()));
            }
        }




        protected void btnZakazi_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDatum.Text) || string.IsNullOrWhiteSpace(txtVreme.Text))
                {
                    PrikaziGresku("Unesite datum i vreme termina.");
                    return;
                }
                string imePac = "";
                if (Session["PacijentIme"] != null && Session["PacijentPrezime"] != null)
                    imePac = $"{Session["PacijentIme"]} {Session["PacijentPrezime"]}";
                else
                    imePac = "Nepoznat";
                // Kreiramo novu rezervaciju na osnovu unetih podataka
                Rezervacija nova = new Rezervacija
                {
                    KorisnikID = Convert.ToInt32(Session["PacijentID"]),
                    Pacijent = txtPacijent.Text.Trim(),
                    DoktorID = ddlDoktor.SelectedIndex > 0 ? (int?)ddlDoktor.SelectedIndex : null,
                    Datum = DateTime.Parse(txtDatum.Text),
                    Vreme = TimeSpan.Parse(txtVreme.Text),
                    Status = "Zakazano",
                    Usluga = ddlUsluga.SelectedValue,
                    Cena = 1500 
                };

                // Pozivamo poslovni servis
                _servis.Zakazi(nova);

                lblPoruka.Text = "✅ Termin uspešno zakazan!";
                lblPoruka.ForeColor = System.Drawing.Color.Green;

                // Očisti polja nakon uspešnog zakazivanja
                txtDatum.Text = "";
                txtVreme.Text = "";
                ddlUsluga.SelectedIndex = 0;
                ddlDoktor.SelectedIndex = 0;
            }
            catch (PoslovnoPraviloIzuzetak ex)
            {
                PrikaziGresku(ex.Message);
            }
            catch (Exception ex)
            {
                PrikaziGresku("Greška u zakazivanju: " + ex.Message);
            }
        }

        // -------------------- DUGME POČETNA --------------------

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        // -------------------- PORUKE --------------------

        private void PrikaziGresku(string poruka)
        {
            lblPoruka.Text = poruka;
            lblPoruka.ForeColor = System.Drawing.Color.Red;
        }
    }
}
