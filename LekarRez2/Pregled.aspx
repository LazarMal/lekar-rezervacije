<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pregled.aspx.cs" Inherits="LekarRez2.Zakazivanje" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pregled rezervacija</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f4f6f8;
            padding: 30px;
        }

        .table-container {
            background-color: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.1);
            max-width: 950px;
            margin: 0 auto;
        }

        h2 {
            margin-bottom: 20px;
            font-weight: 600;
            color: #333;
            text-align: center;
        }

        .filter-container {
            text-align: center;
            margin-bottom: 20px;
        }

        .filter-container select,
        .filter-container button {
            margin: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="table-container">
            <h2>Pregled rezervacija</h2>

            <div class="filter-container">
                <asp:DropDownList ID="ddlUsluge" runat="server" CssClass="form-select w-50 d-inline-block"></asp:DropDownList>
                <asp:Button ID="btnFiltriraj" runat="server" Text="Filtriraj" CssClass="btn btn-primary" OnClick="btnFiltriraj_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Prikaži sve" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
            </div>

            <asp:GridView ID="GridViewRezervacije" runat="server" CssClass="table table-striped"
                AutoGenerateColumns="False"
                DataKeyNames="RezervacijaID"
                OnRowEditing="GridViewRezervacije_RowEditing"
                OnRowUpdating="GridViewRezervacije_RowUpdating"
                OnRowCancelingEdit="GridViewRezervacije_RowCancelingEdit"
                OnRowDeleting="GridViewRezervacije_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="RezervacijaID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Datum" HeaderText="Datum" />
                    <asp:BoundField DataField="Vreme" HeaderText="Vreme" />
                    <asp:BoundField DataField="Pacijent" HeaderText="Pacijent" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="Usluga" HeaderText="Usluga" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblPoruka" runat="server" CssClass="fw-bold d-block text-center mt-2"></asp:Label>
        </div>

        <div class="text-center mt-3">
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Početna" CssClass="btn btn-success w-50" />
        </div>
    </form>
</body>
</html>
