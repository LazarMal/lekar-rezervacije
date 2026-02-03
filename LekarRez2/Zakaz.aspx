<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zakaz.aspx.cs" Inherits="LekarRez2.Zakaz" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rezervacija termina nove usluge</title>
    <meta charset="utf-8" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background-color:#f9f9f9; padding:30px; }
        .form-container {
            max-width:520px; margin:0 auto; background:#fff; padding:24px;
            border-radius:12px; box-shadow:0 6px 16px rgba(0,0,0,.08);
        }
        h2 { margin-bottom:22px; font-weight:600; text-align:center; }
        .form-label { margin-top:10px; font-weight:600; }
        .btn-submit { width:100%; margin-top:16px; }
        .footer-actions { text-align:center; margin-top:16px; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="form-container">
        <h2>Rezervacija termina nove usluge</h2>

        <asp:Label ID="lblPacijent" runat="server" Text="Pacijent:" CssClass="form-label"></asp:Label>
        <asp:TextBox ID="txtPacijent" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

        <asp:Label ID="lblDoktor" runat="server" Text="Doktor:" CssClass="form-label"></asp:Label>
        <asp:DropDownList ID="ddlDoktor" runat="server" CssClass="form-control"></asp:DropDownList>

        <asp:Label ID="lblUsluga" runat="server" Text="Vrsta usluge:" CssClass="form-label"></asp:Label>
       <asp:DropDownList ID="ddlUsluga" runat="server" CssClass="form-control"></asp:DropDownList>

        <asp:Label ID="lblDatum" runat="server" Text="Datum:" CssClass="form-label"></asp:Label>
        <asp:TextBox ID="txtDatum" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>

        <asp:Label ID="lblVreme" runat="server" Text="Vreme (hh:mm):" CssClass="form-label"></asp:Label>
        <asp:TextBox ID="txtVreme" runat="server" CssClass="form-control" Placeholder="10:30"></asp:TextBox>

        <asp:Button ID="btnZakazi" runat="server" Text="Zakaži"
            CssClass="btn btn-primary btn-submit" OnClick="btnZakazi_Click" />

        <asp:Label ID="lblPoruka" runat="server" CssClass="d-block mt-2"></asp:Label>
    </div>

    <div class="footer-actions">
        <asp:Button ID="Button1" runat="server" Text="Početna"
            CssClass="btn btn-outline-primary w-50" OnClick="Button1_Click" />
    </div>
</form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
