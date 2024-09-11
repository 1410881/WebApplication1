using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class Imagen : System.Web.UI.Page
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuariodatos"] == null)
            {
                Response.Redirect("facebook.aspx");
            }
            else
            {
                using (conexion)
                {
                    using (SqlCommand cmd = new SqlCommand("CargarImagen", conexion))
                    { 
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                        conexion.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            byte[] imagen = (byte[])dr["Imagen"];
                            Response.BinaryWrite(imagen);
                        }
                    }
                }
            }
        }
    }
}