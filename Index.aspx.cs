using System;


namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["usuariodatos"] != null)
            {
                string usuariodatos = Session["usuariodatos"].ToString();
                lblBienvenida.Text = "Bienvenido/a " + usuariodatos;
            }
            else
            {
                Response.Redirect("facebook.aspx");
            }

        }
        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            Session.Remove("usuariodatos");
            Response.Redirect("facebook.aspx");
        }
    }
}
