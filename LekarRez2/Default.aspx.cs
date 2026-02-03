using System;
using System.Web;
using System.Web.UI;

namespace LekarRez2
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // validacija prijave
            if (Session["PacijentID"] == null)
            {
                Response.Redirect("~/login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                var ime = Convert.ToString(Session["PacijentIme"]);
                var prezime = Convert.ToString(Session["PacijentPrezime"]);
                lblDobrodosli.Text = $"Dobrodošli, {ime} {prezime}!";
            }

            // spreči keš pa Back ne vraća ovu stranu nakon logou­ta (opciono)
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnPregledaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pregled.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnZakazi_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Zakaz.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnStampaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Stampa.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnCenovnik_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cenovnik.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
