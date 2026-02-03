using System;
using System.Data;
using LekarRez2.PoslovnaLogika.Servisi;

namespace LekarRez2
{
    public partial class Cenovnik : System.Web.UI.Page
    {
        private readonly ServisCenovnik _servis = new ServisCenovnik();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                UcitajCenovnik();
        }

        private void UcitajCenovnik()
        {
            try
            {
                DataTable dt = _servis.UcitajCenovnik();

                GridViewCenovnik.DataSource = dt;
                GridViewCenovnik.DataBind();

                if (dt.Rows.Count == 0)
                {
                    // Ako nema podataka
                    GridViewCenovnik.EmptyDataText = "Trenutno nema podataka o cenovniku.";
                    GridViewCenovnik.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Greška pri učitavanju cenovnika: " + ex.Message + "');</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
