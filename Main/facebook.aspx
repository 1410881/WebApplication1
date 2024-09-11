<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="facebook.aspx.cs" Inherits="WebApplication1.facebook" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="login.css" rel="stylesheet" />
    <link href="estilos/login.css" rel="stylesheet" />
    <title>FloreBook</title>
    <link rel="shortcut icon" href="images/Logo_de_Facebook.png" type="image/x-icon"/>
</head>
<body>

    <div class="wrapper">
        <div class="formcontent">
            <form id="form_login" runat="server">
                <div class="form-control">
                    <div class="col-md-6 text-center mb-5">
                        <asp:Label CssClass="lblBienvenida" ID="lblBienvenida" runat="server"  Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Iniciar &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sesión"></asp:Label>
                    </div>
                    <div>
                        
                        <asp:TextBox ID="tbUsuario" CssClass="form-control" runat="server" placeholder="Nombre Usuario"></asp:TextBox>
                    </div>
                    <div>
                        
                        <asp:TextBox ID="tbPassword" CssClass="form-control" TextMode="Password" runat="server" placeholder="Password"></asp:TextBox>
                    </div>
                    <br/>
                    <div class="row">
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblError"></asp:Label>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Button ID="BtnIngresar" CssClass="btn btn-primary" runat="server" Text="Ingresar" OnClick="BtnIngresar_Click" />
                    </div>
                    <br />
                    <div class="row">
                        <asp:Button runat="server" ID="BtnRegistro" CssClass="btn btn-secondary" Text="Registrarse" OnClick="BtnRegistro_Click"/>
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
