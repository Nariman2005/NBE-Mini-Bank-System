z<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="insallahfinalproject.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NBE - User Login</title>
    <style type="text/css">
        body {
            background-color: #f5f5f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
        }
        .logo-container {
            text-align: center;
            margin-top: 40px;
        }
        .logo-container img {
            height: 80px;
        }
        .form-container {
            padding: 40px;
            border: 2px solid #006341;
            border-radius: 10px;
            margin: 30px auto;
            background-color: #ffffff;
            max-width: 600px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }
        .form-title {
            text-align: center;
            font-size: 28px;
            margin-bottom: 30px;
            color: #006341;
            font-weight: bold;
        }
        .form-row {
            margin-bottom: 15px;
        }
        .form-row td {
            padding: 8px;
            vertical-align: middle;
        }
        .form-row td:first-child {
            font-weight: bold;
            width: 160px;
            color: #004d2a;
        }
        .form-control {
            width: 100%;
            max-width: 300px;
            padding: 8px;
            border: 1px solid #bdc3c7;
            border-radius: 4px;
            font-size: 14px;
        }
        .form-control:focus {
            border-color: #f6b21b;
            outline: none;
            box-shadow: 0 0 6px rgba(246, 178, 27, 0.4);
        }
        .btn-submit {
            background-color: #006341;
            color: white;
            padding: 12px 30px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            transition: background 0.3s;
        }
        .btn-submit:hover {
            background-color: #004d2a;
        }
        .required {
            color: red;
            font-weight: bold;
        }
        .validation-error {
            color: red;
            font-size: 12px;
        }
        .success-message {
            background-color: #d4edda;
            color: #155724;
            padding: 10px;
            border: 1px solid #c3e6cb;
            border-radius: 4px;
            margin-bottom: 15px;
            text-align: center;
        }
        .error-message {
            background-color: #f8d7da;
            color: #721c24;
            padding: 10px;
            border: 1px solid #f5c6cb;
            border-radius: 4px;
            margin-bottom: 15px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- NBE Logo -->
        <div class="logo-container">
            <img src="images/nbe-logo.png" alt="NBE Logo" />
        </div>

        <div class="form-container">
            <div class="form-title">Welcome to NBE - Login</div>

            <!-- Message Panel -->
            <asp:Panel ID="pnl_message" runat="server" Visible="false">
                <asp:Label ID="lbl_message" runat="server" CssClass="success-message"></asp:Label>
            </asp:Panel>

            <table class="auto-style1">
                <tr class="form-row">
                    <td>Email / Username<span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txt_username" runat="server" CssClass="form-control" MaxLength="50" placeholder="Enter your email"></asp:TextBox>
                    </td>
                </tr>
                <tr class="form-row">
                    <td>Password <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txt_password" runat="server" CssClass="form-control" TextMode="Password" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr class="form-row">
                    <td colspan="2" style="text-align: center; padding-top: 20px;">
                        <asp:Button ID="btn_Submit" runat="server" Text="Login" CssClass="btn-submit" OnClick="btn_Submit_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" CssClass="validation-error" />
                    </td>
                </tr>
            </table>
        </div>

        <!-- Optional Footer -->
        <div style="text-align:center; margin-top:30px; font-size:12px; color:#777;">
            © 2025 National Bank of Egypt. All rights reserved.
        </div>
    </form>
</body>
</html>