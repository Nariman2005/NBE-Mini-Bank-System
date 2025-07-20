<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="insallahfinalproject.homepage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Homepage - NBE</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            text-align: center;
        }

        .header {
            margin-top: 40px;
        }

        .logo {
            height: 80px;
            margin-bottom: 20px;
        }

        .welcome {
            font-size: 28px;
            color: #006341;
            font-weight: bold;
            margin-bottom: 50px;
        }

        .button-container {
            display: flex;
            justify-content: center;
            gap: 40px;
            flex-wrap: wrap;
        }

        .btn {
            padding: 15px 30px;
            font-size: 18px;
            background-color: #006341;
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            transition: background 0.3s;
            min-width: 200px;
        }

        .btn:hover {
            background-color: #004d2a;
        }

        .footer {
            margin-top: 80px;
            font-size: 12px;
            color: #777;
        }
        .footer {
    margin-top: 100px;
    font-size: 12px;
    color: #777;
    padding-bottom: 40px;
    text-align: center;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Logo Section -->
        <div class="header">
            <img src="images/nbe-logo.png" alt="NBE Logo" class="logo" />
            <div class="welcome">Welcome, <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>!</div>
        </div>




        <!-- Action Buttons -->
        <div class="button-container">
            <asp:Button ID="btnTransfer" runat="server" Text="💸 Transfer Money" CssClass="btn" OnClick="btnTransfer_Click" />
            <asp:Button ID="btnCheckBalance" runat="server" Text="💰 Check Balance" CssClass="btn" OnClick="btnCheckBalance_Click" />
            <asp:Button ID="btnBankstatment" runat="server" Text="🏦 Bank Statment" CssClass="btn" OnClick="btnBankStatment_Click" />
            <asp:Button ID="btnAddAccount" runat="server" Text="➕ Add Account" CssClass="btn" OnClick="btnAddAccount_Click" />
        </div>
       <div class="footer">
    <asp:Button ID="btnLogout" runat="server" Text="🚪 Exit" CssClass="btn" OnClick="btnLogout_Click" />
    <br />
    <span>© 2025 National Bank of Egypt. All rights reserved.</span>
</div>

    </form>
</body>
</html>
