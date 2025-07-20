<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bankstatment.aspx.cs" Inherits="insallahfinalproject.Bankstatment" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Statement</title>
    <style>
        body {
            font-family: Arial;
            background-color: #f4f4f4;
        }

        .container {
            max-width: 900px;
            margin: 50px auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 10px #ccc;
        }

        h2 {
            text-align: center;
            color: #006341;
        }

        .account-select {
            margin-bottom: 20px;
            text-align: center;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
        }

        .gridview th, .gridview td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: center;
        }

        .gridview th {
            background-color: #006341;
            color: white;
        }

        .back-btn {
            margin-top: 30px;
            text-align: center;
        }

        .btn {
            padding: 10px 25px;
            background-color: #006341;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
        }

        .btn:hover {
            background-color: #004d2a;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>📄 Bank Statement</h2>

            <div class="account-select">
                <asp:DropDownList ID="ddlAccounts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAccounts_SelectedIndexChanged" />
            </div>

            <asp:GridView ID="gvTransactions" runat="server" CssClass="gridview" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" />
                    <asp:BoundField DataField="Timestamp" HeaderText="Date & Time" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount (EGP)" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="Counterparty" HeaderText="Counterparty Account" />
                </Columns>
            </asp:GridView>

            <div class="back-btn">
                <asp:Button ID="btnBack" runat="server" Text="⬅ Back to Homepage" CssClass="btn" OnClick="btnBack_Click" />
            </div>
        </div>
    </form>
</body>
</html>
