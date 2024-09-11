using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public partial class Perfil : System.Web.UI.Page
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        public static int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = int.Parse(Session["usuariodatos"].ToString());
            if (Session["usuariodatos"]== null)
            {
                Response.Redirect("facebook.aspx");
            }
            else 
            {
      
                    using (SqlCommand cmd = new SqlCommand("Perfil",conexion))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        conexion.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            image.ImageUrl = "Imagen.aspx?id=" + id;
                            this.tbNombres.Text = dr["Nombres"].ToString();
                            this.tbApellidos.Text = dr["Apellidos"].ToString();
                            this.tbFechaNacimiento.Text = dr["FechaNacimiento"].ToString();
                            //tbFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                            this.tbUsuario.Text = dr["Usuario"].ToString();
                            dr.Close();
                        }
                        conexion.Close();
                    }
                }
            }
            void MetodoOcultar()
            {
                if (contrasenia.Visible == false)
                {
                    contrasenia.Visible = true;
                    BtnGuardar.Visible = true;
                    BtnCambiar.Text = "Cancelar";
                    lblErrorClave.Text = "";
                }
                else
                {
                    contrasenia.Visible = false;
                    BtnGuardar.Visible = false;
                    BtnCambiar.Text = "Cambiar contraseña";
                    lblErrorClave.Text = "";
                }
            }
             protected void BtnAplicar_Click(object sender , EventArgs e)
            {
                int tamanioarchivo;
                byte[] imagen = FUImage.FileBytes;
                tamanioarchivo = int.Parse(FUImage.FileContent.Length.ToString());
                if (tamanioarchivo >= 2097151000)
                {
                    lblError.Text = "El tamaño de la imagen debe ser menor a 10 Mb!";
                }
                else if (FUImage.HasFile)
                {
                    SqlCommand cmd = new SqlCommand("CambiarImagen", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Imagen", SqlDbType.Image).Value = imagen;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    lblError.Text = "";
                    conexion.Close();
                SqlCommand cmd2 = new SqlCommand("cambiarNombre",conexion);
                { 
                
                }
                }
                else
                {
                    lblError.Text = "No se ha cargado una imagen de perfil nueva!";
                }
             }
        protected void BtnCambiar_Click(object sender, EventArgs e)
        {
            MetodoOcultar();
        }

        protected void Eliminar(object sender, EventArgs e)
        {
            using (conexion)
            {
                using (SqlCommand cmd = new SqlCommand("Eliminar", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                    Session.Remove("usuariologueado");
                    Response.Redirect("facebook.aspx");
                }
            }
        }
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            string contraseniasinverificar = tbContrasenia.Text;
            Regex letras = new Regex(@"[a-zA-Z]");
            Regex numeros = new Regex(@"[0-9]");
            Regex especiales = new Regex("[!\"#\\$%&'()*+,-./:;=?@\\[\\]{|}~]");

            if (tbContrasenia.Text == "" || tbConfirmarContrasenia.Text == "")
            {
                lblError.Text = "Los campos no pueden quedar vacíos!";
            }
            else if (tbContrasenia.Text != tbConfirmarContrasenia.Text)
            {
                lblError.Text = "Los contraseñas no coinciden!";
            }
            else if (!letras.IsMatch(contraseniasinverificar))
            {
                lblError.Text = "Los contraseña debe contener letras!";
            }
            else if (!numeros.IsMatch(contraseniasinverificar))
            {
                lblError.Text = "Los contraseña debe contener números!";
            }
            else if (!especiales.IsMatch(contraseniasinverificar))
            {
                lblError.Text = "Los contraseña debe contener caracteres especiales!";
            }
            else
            {
                try
                {
                    using (conexion)
                    {
                        using (SqlCommand cmd = new SqlCommand("CambiarContrasenia", conexion))
                        {
                            string patron = "InfoToolsSV";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            cmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar).Value = tbContrasenia.Text;
                            cmd.Parameters.Add("@Patron", SqlDbType.VarChar).Value = patron;
                            conexion.Open();
                            cmd.ExecuteNonQuery();
                            conexion.Close();
                            MetodoOcultar();
                            lblErrorClave.Text = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErrorClave.Text = ex.Message;
                }


            }
        }
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("index1.aspx");
        }
    }
}
