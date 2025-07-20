<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBalance.aspx.cs" Inherits="insallahfinalproject.CheckBalance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check Account Balance - NBE</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #ecf0f1;
            margin: 0;
            padding: 0;
        }

        .container {
            background-color: #ffffff;
            max-width: 500px;
            margin: 80px auto;
            padding: 35px;
            border-radius: 12px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            border-top: 5px solid #1abc9c;
            text-align: center;
        }

        h2 {
            color: #2c3e50;
            margin-bottom: 25px;
        }

        .balance-label {
            font-size: 22px;
            font-weight: bold;
            color: #27ae60;
            margin-bottom: 15px;
            display: block;
        }

        .error-label {
            color: #c0392b;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .btn {
            background-color: #3498db;
            color: white;
            padding: 12px 25px;
            font-size: 16px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .btn:hover {
            background-color: #2980b9;
        }
        .card {
    background-color: #f8f9fa;
    padding: 20px;
    margin-bottom: 20px;
    border-radius: 10px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    text-align: left;
}
.card h4 {
    color: #2c3e50;
    margin-bottom: 8px;
}
.card p {
    margin: 5px 0;
    color: #34495e;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>🔍 Account Balance</h2>

            <asp:Label ID="lblMessage" runat="server" CssClass="error-label" />
            <asp:Label ID="lblBalance" runat="server" CssClass="balance-label" />

            <br /><br />

            <asp:Repeater ID="rptAccounts" runat="server">
    <ItemTemplate>
        <div class="card">
            <h4>Account ID: <%# Eval("AccountID") %></h4>
            <p>
                Type:
                <%# Convert.ToInt32(Eval("AccountTypeID")) == 1 ? "Current" : "Saving" %>
            </p>
            <p>Balance: EGP <%# String.Format("{0:N2}", Eval("Balance")) %></p>
        </div>
    </ItemTemplate>
</asp:Repeater>

            <asp:Button ID="btnBack" runat="server" Text="🏠 Back to Homepage" CssClass="btn" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
