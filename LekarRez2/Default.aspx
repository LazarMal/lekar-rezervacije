<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LekarRez2.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Početna stranica</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background-color: #f8f9fa;
            padding: 40px;
        }
        .container-main {
            max-width: 400px;
            margin: 0 auto;
            text-align: center;
        }
        .welcome-text {
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 30px;
            color: #333;
        }
        .btn-main {
            width: 100%;
            margin-bottom: 15px;
            padding: 12px;
            font-size: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container container-main">
            <div class="welcome-text">
                <asp:Label ID="lblDobrodosli" runat="server" Text="Dobrodošli!"></asp:Label>
            </div>

            <asp:Button ID="btnPregled" runat="server" CssClass="btn btn-danger btn-main" Text="Pregled rezervacija" OnClick="btnPregledaj_Click" />
            <asp:Button ID="btnZakazivanje" runat="server" CssClass="btn btn-danger btn-main" Text="Zakazivanje nove usluge" OnClick="btnZakazi_Click" />
            <asp:Button ID="btnStampa" runat="server" CssClass="btn btn-danger btn-main" Text="Štampa rezervacija" OnClick="btnStampaj_Click" />
            <asp:Button ID="btnCenovnik" runat="server" CssClass="btn btn-danger btn-main" Text="Cenovnik Usluga" OnClick="btnCenovnik_Click" />
            <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-danger btn-main" Text="Odjavi se" OnClick="btnLogout_Click" />
            
        </div>
    </form>
</body>
</html>
