<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="LekarRez2.signup" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registracija korisnika</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }
        .signup-container {
            max-width: 350px;
            margin: 50px auto;
            padding: 25px;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            max-height:500px;
        }
        .form-label {
            font-weight: 600;
        }
        .auto-style1 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="signup-container">
            <h3 class="text-center mb-4">Registracija</h3>

            <asp:Panel ID="Panel1" runat="server" CssClass="form-group mb-3">
                <asp:Label ID="Label1" runat="server" Text="Ime" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="Txtime" runat="server" CssClass="auto-style1" Width="310px" />
            </asp:Panel>

            <asp:Panel ID="Panel2" runat="server" CssClass="form-group mb-3">
                <asp:Label ID="Label2" runat="server" Text="Prezime" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtPrezime" runat="server" CssClass="auto-style1" Width="308px" />
            </asp:Panel>

            <asp:Panel ID="Panel3" runat="server" CssClass="form-group mb-3" Width="537px">
                <asp:Label ID="Label3" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="auto-style1" TextMode="Email" Width="308px" />
            </asp:Panel>

            <asp:Panel ID="Panel4" runat="server" CssClass="form-group mb-3">
                <asp:Label ID="Label4" runat="server" Text="Lozinka" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtLozinka" runat="server" CssClass="auto-style1" TextMode="Password" Width="311px" />
            </asp:Panel>

            <asp:Panel ID="Panel5" runat="server" CssClass="form-group mb-4">
                <asp:Label ID="Label5" runat="server" Text="Pol" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-select" Height="33px" Width="212px">
                    
                    
                    <asp:ListItem>Pacijent Musko</asp:ListItem>
                    <asp:ListItem>Pacijent Zensko</asp:ListItem>
                  
                </asp:DropDownList>
            </asp:Panel>

            <asp:Button ID="btnSubmit" runat="server" Text="Registruj se" CssClass="btn btn-primary w-100" OnClick="btnSubmit_Click" />
            <asp:Label ID="lblPoruka" runat="server" Text=""></asp:Label>
        </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Log in" CssClass="btn btn-primary w-100" />
        </p>
    </form>

    </body>
</html>
