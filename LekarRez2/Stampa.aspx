<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stampa.aspx.cs" Inherits="LekarRez2.Stampa" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Štampa rezervacija</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f4f6f8;
            padding: 30px;
        }
        .container {
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            max-width: 1000px;
            margin: 0 auto;
        }
        h2 {
            text-align: center;
            margin-bottom: 25px;
            font-weight: 600;
            color: #333;
        }
        .filter-container {
            margin-bottom: 20px;
            text-align: center;
        }
        .filter-container .form-control, .filter-container .form-select {
            display: inline-block;
            width: auto;
            margin: 5px;
        }
        .btn-group {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Štampa rezervacija</h2>

            <div class="filter-container">
                <asp:DropDownList ID="ddlDoktor" runat="server" CssClass="form-select"></asp:DropDownList>
                <asp:DropDownList ID="ddlUsluga" runat="server" CssClass="form-select"></asp:DropDownList>
                <asp:TextBox ID="txtDatumOd" runat="server" CssClass="form-control" placeholder="Datum od (YYYY-MM-DD)"></asp:TextBox>
                <asp:TextBox ID="txtDatumDo" runat="server" CssClass="form-control" placeholder="Datum do (YYYY-MM-DD)"></asp:TextBox>
                <asp:TextBox ID="txtVreme" runat="server" CssClass="form-control" placeholder="Vreme (hh:mm)"></asp:TextBox>
                <div class="btn-group">
                    <asp:Button ID="btnFiltriraj" runat="server" CssClass="btn btn-primary" Text="Filtriraj" OnClick="btnFiltriraj_Click" />
                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary" Text="Prikaži sve" OnClick="btnReset_Click" />
                </div>
            </div>

            <asp:GridView ID="gvRezervacije" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="True"></asp:GridView>
            <asp:Label ID="lblPoruka" runat="server" CssClass="fw-bold d-block text-center mt-2"></asp:Label>
        </div>

        <div class="text-center mt-3">
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Početna" CssClass="btn btn-success w-50" />
        </div>
    </form>
</body>
</html>
