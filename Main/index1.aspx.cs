using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class index1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarPublicaciones();
            List<Contacto> contactos = ObtenerContactosDesdeBaseDeDatos();
            rptContacts.DataSource = contactos;
            rptContacts.DataBind();
        }
        public class Contacto
        {
            public int Id { get; set; }
            public string Nombres { get; set; }
            public string AvatarUrl { get; set; }
        }

        protected List<Contacto> ObtenerContactosDesdeBaseDeDatos()
        {
            List<Contacto> contactos = new List<Contacto>();
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ObtenerContactosConImagenes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Contacto contacto = new Contacto();
                        contacto.Id = (int)reader["Id"];
                        contacto.Nombres = (string)reader["Nombres"];
                        byte[] bytes = (byte[])reader["AvatarUrl"];
                        string base64String = Convert.ToBase64String(bytes);
                        contacto.AvatarUrl = "data:image/jpeg;base64," + base64String;

                        contactos.Add(contacto);
                    }
                }
            }

            return contactos;
        }



        private DataTable GetDataFromStoredProcedure(string procedureName)
        {
            string conString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        private void CargarPublicaciones()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT P.PostId, U.Nombres AS NombreAutor, P.Contenido, P.FechaPublicacion ,P.ImagenUrl
                    FROM Publicaciones P
                    JOIN Usuarir U ON P.AutorId = U.id";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    da.Fill(dt);
                    rptPublicaciones.DataSource = dt;
                    rptPublicaciones.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Redirect("index1.aspx");
                }
            }
        }
        protected void btnCrearPublicacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("publicaciones.aspx");
        }


    }
}
