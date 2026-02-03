using System;
using System.Web.UI;
using LekarRez2.Modeli;
using LekarRez2.PoslovnaLogika.Servisi;
using LekarRez2.PoslovnaLogika.PravilaSistema;

namespace LekarRez2
{
    public partial class signup : Page
    {
        private readonly ServisKorisnik _servis = new ServisKorisnik();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Validacija unosa
                if (!PravilaLogike.ProveriPoljaRegistracija(
                        Txtime.Text.Trim(),
                        txtPrezime.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtLozinka.Text.Trim(),
                        DropDownList1.SelectedValue))
                {
                    PrikaziPoruku("Sva polja su obavezna.", true);
                    return;
                }

                // Provera jačine lozinke
                if (!PravilaLogike.ProveriJacinuLozinke(txtLozinka.Text.Trim()))
                {
                    PrikaziPoruku("Lozinka mora imati bar 6 karaktera.", true);
                    return;
                }

                // Kreiramo model korisnika
                var korisnik = new Korisnik
                {
                    Ime = Txtime.Text.Trim(),
                    Prezime = txtPrezime.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Lozinka = txtLozinka.Text.Trim(),
                    Uloga = DropDownList1.SelectedValue
                };

                // Poziv servisa
                bool uspeh = _servis.Registruj(korisnik);

                if (uspeh)
                {
                    PrikaziPoruku("✅ Uspešno ste registrovani!", false);
                    OcistiFormu();
                }
                else
                {
                    PrikaziPoruku("Registracija nije uspela. Pokušajte ponovo.", true);
                }
            }
            catch (Exception ex)
            {
                PrikaziPoruku("Greška prilikom registracije: " + ex.Message, true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        // -------------------- POMOĆNE METODE --------------------

        private void PrikaziPoruku(string poruka, bool jeGreska)
        {
            lblPoruka.Text = poruka;
            lblPoruka.ForeColor = jeGreska ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }

        private void OcistiFormu()
        {
            Txtime.Text = "";
            txtPrezime.Text = "";
            txtEmail.Text = "";
            txtLozinka.Text = "";
            DropDownList1.SelectedIndex = 0;
        }
    }
}
