<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adminpanel.aspx.cs" Inherits="insallahfinalproject.Adminpanel" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Panel - Approvals</title>
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #ecf0f1;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 1000px;
            margin: 40px auto;
            background-color: #ffffff;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
        }

        h2, h3 {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 20px;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 40px;
        }

        .gridview th, .gridview td {
            border: 1px solid #ddd;
            padding: 12px;
            text-align: center;
            font-size: 15px;
        }

        .gridview th {
            background-color: #34495e;
            color: white;
        }

        .gridview tr:nth-child(even) {
            background-color: #f8f9fa;
        }

        .gridview tr:hover {
            background-color: #f1f1f1;
        }

        .approve, .reject, .view-logs {
            padding: 8px 15px;
            border: none;
            border-radius: 6px;
            font-size: 14px;
            cursor: pointer;
        }

        .approve {
            background-color: #27ae60;
            color: white;
        }

        .reject {
            background-color: #c0392b;
            color: white;
        }

        .view-logs {
            background-color: #2980b9;
            color: white;
            float: right;
            margin-top: -50px;
        }

        .message {
            margin-top: 10px;
            font-weight: bold;
            text-align: center;
            color: #2c3e50;
        }

        .top-bar {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .section-divider {
            border-top: 1px solid #ccc;
            margin: 40px 0 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="top-bar">
                <h2>Admin Panel - User & Account Approvals</h2>
                <asp:Button ID="btnViewLogs" runat="server" Text="View Logs" CssClass="view-logs" OnClick="btnViewLogs_Click" />
            </div>

            <h3>Pending User Approvals</h3>
            <asp:GridView ID="gv_pendingUsers" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" OnRowCommand="gv_pendingUsers_RowCommand"
                CssClass="gridview" EmptyDataText="✅ No pending users.">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="User ID" />
                    <asp:BoundField DataField="Fname" HeaderText="First Name" />
                    <asp:BoundField DataField="Lname" HeaderText="Last Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" />
                    <asp:BoundField DataField="Addresses" HeaderText="Address" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve"
                                CommandArgument='<%# Container.DataItemIndex %>' CssClass="approve" />
                            &nbsp;
                            <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject"
                                CommandArgument='<%# Container.DataItemIndex %>' CssClass="reject" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="section-divider"></div>

         

            <asp:Label ID="lbl_message" runat="server" CssClass="message" />
        </div>
    </form>
</body>
</html>
