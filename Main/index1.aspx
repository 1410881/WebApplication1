<%@ Page Title="" Language="C#" MasterPageFile="~/plantilla.Master" AutoEventWireup="true" CodeBehind="index1.aspx.cs" Inherits="WebApplication1.index1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Página Principal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>

.acciones {
    display: flex;
    align-items: center;
}

.btn-like,
.btn-comentar,
.btn-compartir {
    display: flex;
    align-items: center;
    background: transparent;
    border: none;
    cursor: pointer;
    font-size: 14px;
    font-weight: bold;
    color: #4b4f56;
    padding: 8px;
    transition: color 0.3s;
}

.btn-like:hover,
.btn-comentar:hover,
.btn-compartir:hover {
    color: #1877f2;
}


.icono {
    width: 16px;
    height: 16px;
    margin-right: 5px;
    background-size: 16px 16px;
}

.like {
    background-image: url('images/like-icon-vector-illustration.jpg'); 
}

.comentar {
    background-image: url('images/1230203.png'); 
}

.compartir {
    background-image: url('images/share-icon-ui-icon-vector.jpg'); 
}


  
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}


body {
    font-family: Arial, sans-serif;
    background-color: #f0f2f5;
}


.container-main {
    display: flex;
    justify-content: space-between;
    margin: 20px;
}


.main-content {
    width: 60%;
}


.publicacion {
    background-color: #fff;
    border: 1px solid #dddfe2;
    border-radius: 8px;
    margin-bottom: 20px;
    padding: 15px;
}


.autor {
    font-weight: bold;
    color: #050505;
    margin-bottom: 5px;
}


.contenido {
    margin-bottom: 10px;
}


.imagen {
    max-width: 100%;
    height: auto;
    margin-bottom: 10px;
    border-radius: 8px;
}


.btn-accion {
    background-color: #4267b2;
    color: #fff;
    border: none;
    padding: 5px 10px;
    cursor: pointer;
    margin-right: 10px;
    border-radius: 5px;
}


.btn-accion:hover {
    background-color: #2e4a86;
}


.sidebar {
    width: 35%;
}


.contact-list {
    list-style: none;
    padding: 0;
}

.contact-item {
    display: flex;
    align-items: center;
    padding: 10px;
    margin-bottom: 10px;
    border-radius: 5px;
    background-color: #fff;
    box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
}


.contact-avatar {
    width: 40px;
    height: 40px;
    border-radius: 50%;
    margin-right: 10px;
}


.contact-name {
    font-weight: bold;
}
.btn-accion {
    background-color: #4267b2;
    color: #fff;
    border: none;
    padding: 8px 12px;
    cursor: pointer;
    margin-right: 10px;
    border-radius: 5px;
    font-size: 14px;
    font-weight: bold;
    transition: background-color 0.3s;
}


.btn-accion:hover {
    background-color: #365899;
}

    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
   
    <div class="container-main">
        <div class="main-content">
                    <asp:Button ID="btnCrearPublicacion" runat="server" Text="Crear Publicación" OnClick="btnCrearPublicacion_Click" />

             <h2>Publicaciones</h2>
  <asp:Repeater ID="rptPublicaciones" runat="server">
    <ItemTemplate>
        <div class="publicacion">
                        <div class="fecha">Fecha de Publicación: <%# Eval("FechaPublicacion", "{0:dd/MM/yyyy}") %></div>
            <div class="autor"><%# Eval("NombreAutor") %></div>
            <div class="contenido"><%# Eval("Contenido") %></div>
            <asp:Image ID="imgPublicacion" runat="server" ImageUrl='<%# Eval("ImagenUrl") %>' CssClass="imagen" style="max-width: 200px; max-height: 200px;" />

           <div class="acciones">
                <button class="btn-like">
                    <span class="icono like"></span>
                    Me gusta
                </button>
                <button class="btn-comentar">
                    <span class="icono comentar"></span>
                    Comentar
                </button>
                <button class="btn-compartir">
                    <span class="icono compartir"></span>
                    Compartir
                </button>
            </div>
    </div>
    </ItemTemplate>
</asp:Repeater>
        </div>

        <div class="sidebar">

            <h2>Contactos</h2>
            <asp:Repeater ID="rptContacts" runat="server">
                <ItemTemplate>
                    <div class="contact-item" onclick="toggleChat('<%# Eval("Id") %>')">
                        <img src='<%# Eval("AvatarUrl") %>'style="border-radius: 50%;max-width: 60px; max-height: 60px; alt='<%# Eval("Nombres") %>' class="contact-avatar" />
                        <%# Eval("Nombres") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>