using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class publicaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Text = "";
                CargarUsuarioLogueado();
            }
        }

        private void CargarUsuarioLogueado()
        {
            if (Session["usuariodatos"] != null)
            {
                int usuarioId = int.Parse(Session["usuariodatos"].ToString());
                lblNombreUsuario.Text = ObtenerNombreUsuario(usuarioId);
            }
            else
            {
                Response.Redirect("index1.aspx");
            }
        }

        private string ObtenerNombreUsuario(int usuarioId)
        {
            string nombreUsuario = "";
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Nombres + ' ' + Apellidos AS NombreCompleto FROM Usuarir WHERE id = @UsuarioId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                try
                {
                    connection.Open();
                    nombreUsuario = cmd.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al obtener el nombre del usuario: " + ex.Message;
                }
            }
            return nombreUsuario;
        }

        protected void btnAgregarPublicacion_Click(object sender, EventArgs e)
        {
            if (Session["usuariodatos"] != null)
            {
                string imagenUrl = "";

                if (fileImagen.HasFile)
                {
                        
                    string fileName = Path.GetFileName(fileImagen.PostedFile.FileName);
                    string uploadFolder = Server.MapPath("~/Images/");
                    string filePath = Path.Combine(uploadFolder, fileName);
                    fileImagen.SaveAs(filePath);


                    imagenUrl = "~/Images/" + fileName;
                }


                int autorId = int.Parse(Session["usuariodatos"].ToString());
                if (!string.IsNullOrEmpty(txtContenido.Text))
                {

                    string contenido = txtContenido.Text;
                    DateTime fechaPublicacion = DateTime.Now;

                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("AgregarPublicacionConImagen", connection);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AutorId", autorId);
                        cmd.Parameters.AddWithValue("@Contenido", contenido);
                        cmd.Parameters.AddWithValue("@ImagenUrl", imagenUrl);
                        cmd.Parameters.AddWithValue("@FechaPublicacion", fechaPublicacion);

                        try
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            lblMensaje.Text = "Publicación agregada exitosamente.";
                            Response.Redirect("index1.aspx");
                        }
                        catch (Exception ex)
                        {
                            lblMensaje.Text = "Error al agregar la publicación: " + ex.Message;
                        }
                    }
                }
                else
                {
                    lblMensaje.Text = "Por favor, ingresa un contenido.";
                }
            }
            else
            {
                Response.Redirect("index1.aspx");
            }
        }
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("index1.aspx");
        }


    }
}