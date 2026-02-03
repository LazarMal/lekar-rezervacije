using System;
using System.Web.UI;
using LekarRez2.Modeli;
using LekarRez2.PoslovnaLogika.Servisi;

namespace LekarRez2
{
    public partial class login : Page
    {
        private readonly ServisKorisnik _servis = new ServisKorisnik();

        protected void Page_Load(object sender, EventArgs e) { }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var korisnik = _servis.Prijavi(TxtIme.Text.Trim(), TxtPrezime.Text.Trim(), TxtLozinka.Text.Trim());

                if (korisnik == null)
                {
                    PrikaziGresku("Neispravno ime, prezime ili lozinka.");
                    return;
                }

                // čuvamo korisnika u sesiji
                Session["PacijentID"] = korisnik.KorisnikID;
                Session["PacijentIme"] = korisnik.Ime;
                Session["PacijentPrezime"] = korisnik.Prezime;
                Session["Uloga"] = korisnik.Uloga;

                // redirekcija nakon prijave
                Response.Redirect("Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                PrikaziGresku("Greška prilikom prijave: " + ex.Message);
            }
        }

        protected void BtnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }

        private void PrikaziGresku(string poruka)
        {
            lblPoruka.Text = poruka;
            lblPoruka.CssClass = "text-danger d-block mt-2";
        }
    }
}
