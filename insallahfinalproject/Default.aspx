<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="insallahfinalproject.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NBE Welcome</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 800px;
            margin: 80px auto;
            text-align: center;
        }

        .logo {
            font-size: 34px;
            font-weight: bold;
            color: #006341;
            margin-bottom: 10px;
        }

        .subtitle {
            font-size: 18px;
            color: #f6b21b;
            margin-bottom: 30px;
        }

        .card-container {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 30px;
        }

        .role-card {
            background-color: white;
            border: 2px solid #006341;
            border-radius: 12px;
            padding: 30px;
            width: 280px;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }

        .role-card:hover {
            background-color: #e6f5ef;
            transform: translateY(-5px);
        }

        .role-title {
            font-size: 22px;
            font-weight: bold;
            color: #006341;
            margin-bottom: 10px;
        }

        .role-desc {
            font-size: 15px;
            color: #444;
            margin-bottom: 20px;
        }

        .btn {
            padding: 10px 20px;
            font-size: 15px;
            background-color: #006341;
            color: white;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            margin: 5px;
        }

        .btn:hover {
            background-color: #004d2a;
        }

        .btn-register {
            background-color: #f6b21b;
            color: white;
        }

        .btn-register:hover {
            background-color: #d49e18;
        }

        .message {
            margin-top: 25px;
            color: #006341;
            font-weight: bold;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="logo">National Bank of Egypt</div>
            <div class="subtitle">Choose your role to continue</div>

            <asp:RadioButtonList ID="rblRole" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblRole_SelectedIndexChanged" CssClass="card-container">
                <asp:ListItem Value="client" Text="" Selected="True">
                    <div class="role-card">
                        <div class="role-title">Client</div>
                        <div class="role-desc">Access your banking services</div>
                    </div>
                </asp:ListItem>
                <asp:ListItem Value="admin" Text="">
                    <div class="role-card">
                        <div class="role-title">Admin</div>
                        <div class="role-desc">Manage users and system</div>
                    </div>
                </asp:ListItem>
            </asp:RadioButtonList>

            <div class="message">
                <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
            </div>

            <div style="margin-top: 30px;">
                <asp:Button ID="btn_Login" runat="server" Text="Login" CssClass="btn" OnClick="btn_Login_Click" />
                <asp:Button ID="btn_Register" runat="server" Text="Register" CssClass="btn btn-register" OnClick="btn_Register_Click" />
            </div>
        </div>
    </form>
</body>
</html>
