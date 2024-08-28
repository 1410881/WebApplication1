<%@ Page Title="" Language="C#" MasterPageFile="~/plantilla.Master" AutoEventWireup="true" CodeBehind="publicaciones.aspx.cs" Inherits="WebApplication1.publicaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
      <style>
   
        .publicaciones-container {
            max-width: 600px;
            margin: auto;
            padding: 20px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .publicaciones-form-group {
            margin-bottom: 20px;
        }

        .publicaciones-form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .publicaciones-form-group input[type="text"],
        .publicaciones-form-group textarea {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .publicaciones-form-group input[type="file"] {
            width: auto;
            padding: 8px;
            border: none;
            background-color: #f0f2f5;
            border-radius: 4px;
            cursor: pointer;
        }

        .publicaciones-btn-agregar {
            background-color: #1877f2;
            color: #fff;
            border: none;
            padding: 10px 20px;
            border-radius: 4px;
            cursor: pointer;
            font-weight: bold;
            transition: background-color 0.3s;
        }

        .publicaciones-btn-agregar:hover {
            background-color: #1655b8;
        }
    </style>
   <script type="text/javascript">
       function mostrarPrevisualizacion(input) {
           if (input.files && input.files[0]) {
               var reader = new FileReader();

               reader.onload = function (e) {
                   var imagenPrevia = document.getElementById('imagenPrevia');
                   imagenPrevia.src = e.target.result;
                   imagenPrevia.style.display = 'block';
               }

               reader.readAsDataURL(input.files[0]);
           }
       }
   </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

 <div class="publicaciones-container">
        <h2>Crear nueva publicación</h2>
        <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <div class="publicaciones-form-group">
            <label for="txtContenido">Contenido:</label>
            <asp:TextBox ID="txtContenido" runat="server" TextMode="MultiLine" Rows="5" Columns="50"></asp:TextBox>
        </div>
        <div class="publicaciones-form-group">
            <label for="fileImagen">Imagen:</label>
            <asp:FileUpload ID="fileImagen" runat="server" onchange="mostrarPrevisualizacion(this);" />
        </div>
        <div class="publicaciones-form-group">
            <img id="imagenPrevia" src="images/tu-imagen-aqui.jpg" alt="Previsualización de la imagen" style="max-width: 500px; max-height: 500px;"/>
        </div>
        <asp:Button ID="btnAgregarPublicacion" runat="server" Text="Agregar Publicación" CssClass="publicaciones-btn-agregar" OnClick="btnAgregarPublicacion_Click" />
        <div>
<asp:Button runat="server" ID="BtnRegresar" Text="Regresar" CssClass="publicaciones-btn-agregar" OnClick="BtnRegresar_Click" />

        </div>
 </div>

</asp:Content>
