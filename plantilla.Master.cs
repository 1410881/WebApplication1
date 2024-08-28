using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class plantilla : System.Web.UI.MasterPage
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuariodatos"] != null)
            {
                int id = int.Parse(Session["usuariodatos"].ToString());
                using (conexion)
                {
                    using (SqlCommand cmd = new SqlCommand("Perfil", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                        conexion.Open();
                        SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        dr.Read();
                        this.lblUsuario.Text = dr["Apellidos"].ToString() + ", " + dr["Nombres"].ToString();
                        imgPerfil.ImageUrl = "Imagen.aspx?id=" + id;
                    }
                }

            }
            else
            {
                Response.Redirect("facebook.aspx");
            }

        }
        protected void Perfil_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("Perfil.aspx");
        }
        protected void Salir_click(object sender, EventArgs e)
        {
            Session.Remove("usuariodatos");
            Response.Redirect("index1.aspx");
        }
    }
}