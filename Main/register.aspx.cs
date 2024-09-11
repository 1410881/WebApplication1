using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblErrorContrasenia.Text = "";
        
        }
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        void Limpiar()
        {
            tbNombres.Text = "";
            tbApellidos.Text = "";
            tbFechaNacimiento.Text = "";
            tbUsuario.Text = "";
            tbConfirmarContrasenia.Text = "";
            tbContrasenia.Text = "";
            lblError.Text = "";
            lblErrorContrasenia.Text = "";

        }
        protected void BtnRegistrar_Click(Object sender, EventArgs e)
        {
            int tamanioimagen = int.Parse(FUImage.FileContent.Length.ToString());
            string contraseniasinverificar = tbContrasenia.Text;
            Regex letras = new Regex(@"[a-zA-Z]");
            Regex numeros = new Regex(@"[0-9]");
            Regex especiales = new Regex("[!\"#\\$%&'()*+,-./:;=?@\\[\\]{|}~]");
            conexion.Open();
            SqlCommand usuario = new SqlCommand("ContarUsuario", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            usuario.Parameters.Add("@USuario", SqlDbType.VarChar).Value = tbUsuario.Text;
            int user = Convert.ToInt32(usuario.ExecuteScalar());
            if (tbNombres.Text == "" || tbApellidos.Text == "" || tbFechaNacimiento.Text == "" || tbUsuario.Text == "")
            {
                lblError.Text = "Los campos no pueden quedar vacíos!";
            }
            else if (user >= 1)
            {
                lblError.Text = "El usuario " + tbUsuario.Text + " ya existe!";
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
            else if (!FUImage.HasFile)
            {
                lblError.Text = "No se ha cargado una imagen de perfil!";
            }
            else if (tamanioimagen >= 2097151000)
            {
                lblError.Text = "El tamaño de la imagen no puede ser mayor a 10 Mb!";
            }
            else
            {
                byte[] imagen = FUImage.FileBytes;
                string patron = "facebook";
                using (conexion)
                {
                    using (SqlCommand cmd = new SqlCommand("ps_sentAgregaa", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = tbNombres.Text;
                        cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar).Value = tbApellidos.Text;
                        cmd.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = tbFechaNacimiento.Text;
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = tbUsuario.Text;
                        cmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar).Value = tbContrasenia.Text;
                        cmd.Parameters.Add("@Patron", SqlDbType.VarChar).Value = patron;
                        cmd.Parameters.Add("@Imagen", SqlDbType.Image).Value = imagen;
                        cmd.Parameters.Add("@IDsuarios", SqlDbType.Int).Value = 0;
                        cmd.ExecuteNonQuery();
                        Limpiar();
          

                    }
                    conexion.Close();
                    Response.Redirect("facebook.aspx");
                }
                
               
            }


        }
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("facebook.aspx");
        }
        
    } 

}