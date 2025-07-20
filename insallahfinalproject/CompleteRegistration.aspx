<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompleteRegistration.aspx.cs" Inherits="insallahfinalproject.CompleteRegistration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Complete Your Registration</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f1f2f6;
            margin: 0;
            padding: 0;
        }

        .form-container {
            width: 100%;
            max-width: 500px;
            margin: 80px auto;
            padding: 30px;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }

        h2 {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 30px;
        }

        label, .form-container asp\:label {
            font-weight: bold;
            color: #34495e;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #bdc3c7;
            border-radius: 5px;
        }

        .form-control:focus {
            outline: none;
            border-color: #3498db;
            box-shadow: 0 0 5px rgba(52,152,219,0.3);
        }

        .btn-submit {
            background-color: #27ae60;
            color: white;
            font-weight: bold;
            border: none;
            padding: 12px;
            width: 100%;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
        }

        .btn-submit:hover {
            background-color: #1e8449;
        }

        .error-message {
            color: red;
            font-size: 14px;
            text-align: center;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Complete Registration</h2>

            <asp:Label ID="lbl_message" runat="server" CssClass="error-message"></asp:Label>

            <div class="form-group">
                <asp:Label Text="Account Type:" runat="server" />
                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <asp:Label Text="Initial Balance:" runat="server" />
                <asp:TextBox ID="txtBalance" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <asp:Label Text="Select Branch:" runat="server" />
                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" />
            </div>

            <asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" CssClass="btn-submit" OnClick="btnCreateAccount_Click" />
        </div>
    </form>
</body>
</html>
