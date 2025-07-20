<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transfer.aspx.cs" Inherits="insallahfinalproject.Transfer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Funds - NBE</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #ecf0f1;
            margin: 0;
            padding: 0;
        }

        .container {
            background-color: #ffffff;
            max-width: 550px;
            margin: 50px auto;
            padding: 35px;
            border-radius: 12px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.08);
            border-top: 5px solid #1abc9c;
        }

        h2 {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 30px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        label {
            font-weight: 600;
            display: block;
            margin-bottom: 6px;
            color: #2c3e50;
        }

        input, select {
            width: 100%;
            padding: 10px 12px;
            font-size: 14px;
            border-radius: 6px;
            border: 1px solid #bdc3c7;
            transition: border-color 0.3s;
        }

        input:focus, select:focus {
            border-color: #1abc9c;
            outline: none;
        }

        .btn {
            background-color: #1abc9c;
            color: white;
            padding: 12px;
            width: 100%;
            border: none;
            border-radius: 6px;
            font-size: 16px;
            font-weight: bold;
            margin-top: 15px;
            transition: background-color 0.3s;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #16a085;
        }

        .message {
            font-weight: bold;
            text-align: center;
            margin-top: 20px;
            padding: 10px;
            border-radius: 5px;
        }

        .success {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }

        .error {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>💸 Transfer Money</h2>

            <!-- Sender Phone -->
            <div class="form-group">
                <label>📞 Sender Phone:</label>
                <asp:TextBox ID="txtSenderPhone" runat="server" ReadOnly="true" />
            </div>

            <!-- Sender Account -->
            <div class="form-group">
                <label>🏦 Choose Sender Account:</label>
                <asp:DropDownList ID="ddlSenderAccounts" runat="server" />
            </div>

            <!-- Recipient Phone -->
            <div class="form-group">
                <label>📱 Recipient Phone Number:</label>
                <asp:TextBox ID="txtRecipientPhone" runat="server" AutoPostBack="true" OnTextChanged="txtRecipientPhone_TextChanged" />
            </div>

            <!-- Recipient Account -->
            <div class="form-group">
                <label>💳 Recipient Account:</label>
                <asp:DropDownList ID="ddlRecipientAccounts" runat="server" />
            </div>

            <!-- Amount -->
            <div class="form-group">
                <label>💰 Amount (EGP):</label>
                <asp:TextBox ID="txtAmount" runat="server" />
            </div>

            <!-- Submit Button -->
            <asp:Button ID="btnTransfer" runat="server" Text="🔁 Transfer Now" CssClass="btn" OnClick="btnTransfer_Click" />

            <!-- Message -->
            <asp:Label ID="lblMessage" runat="server" CssClass="message" />
        </div>
    </form>
</body>
</html>
