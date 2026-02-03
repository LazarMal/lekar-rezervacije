<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cenovnik.aspx.cs" Inherits="LekarRez2.Cenovnik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cenovnik usluga</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f4f6f8;
            padding: 30px;
        }
        .cenovnik-container {
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.15);
            max-width: 800px;
            margin: 0 auto;
        }
        h2 {
            margin-bottom: 25px;
            font-weight: 600;
            text-align: center;
            color: #333;
        }
        table {
            width: 100%;
        }
        .table th, .table td {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="cenovnik-container">
            <h2>Cenovnik usluga</h2>
            <asp:GridView ID="GridViewCenovnik" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Usluga" HeaderText="Naziv usluge" />
                    
                    <asp:BoundField DataField="Cena" HeaderText="Cena (RSD)" DataFormatString="{0:N2}" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Početna" CssClass="btn btn-primary w-50" />
        </div>
    </form>
</body>
</html>
