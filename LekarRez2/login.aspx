<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="LekarRez2.login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prijava</title>
    <meta charset="utf-8" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background-color:#f8f9fa; }
        .login-container {
            max-width:360px; margin:60px auto; padding:24px; background:#fff;
            border-radius:10px; box-shadow:0 6px 16px rgba(0,0,0,.08);
        }
        .form-label{font-weight:600;}
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="login-container">
        <h3 class="text-center mb-4">Prijava</h3>

        <div class="mb-3">
            <asp:Label ID="Label1" runat="server" Text="Ime" CssClass="form-label" />
            <asp:TextBox ID="TxtIme" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <asp:Label ID="Label2" runat="server" Text="Prezime" CssClass="form-label" />
            <asp:TextBox ID="TxtPrezime" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <asp:Label ID="Label3" runat="server" Text="Lozinka" CssClass="form-label" />
            <asp:TextBox ID="TxtLozinka" runat="server" CssClass="form-control" TextMode="Password" />
        </div>

        <asp:Button ID="btnLogin" runat="server" Text="Prijavi se"
            CssClass="btn btn-primary w-100" OnClick="BtnLogin_Click" UseSubmitBehavior="false" />

        <asp:Label ID="lblPoruka" runat="server" CssClass="d-block mt-2"></asp:Label>

        <hr class="my-3" />
        <asp:Button ID="btnSignup" runat="server" Text="Napravi nalog"
            CssClass="btn btn-outline-secondary w-100" OnClick="BtnSignup_Click" />
    </div>
</form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
