<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewLogs.aspx.cs" Inherits="insallahfinalproject.ViewLogs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activity Logs</title>
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #f4f6f8;
            margin: 0;
            padding: 20px;
        }

        .container {
            background-color: white;
            max-width: 1100px;
            margin: auto;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 0 10px #ccc;
        }

        h2 {
            text-align: center;
            color: #2c3e50;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .gridview th, .gridview td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: center;
        }

        .gridview th {
            background-color: #34495e;
            color: white;
        }

        .gridview tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .gridview tr:hover {
            background-color: #f1f1f1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>🔍 System Logs</h2>
            <asp:GridView ID="gvLogs" runat="server" AutoGenerateColumns="true" CssClass="gridview" EmptyDataText="No logs found." />
        </div>
    </form>
</body>
</html>
