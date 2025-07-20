<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="insallahfinalproject.Register" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NBE - User Registration</title>
       <style type="text/css">
     body {
    background-color: #f5f5f5;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    margin: 0;
    padding: 0;
}

.form-container {
    padding: 40px;
    border: 2px solid #006341; /* NBE green */
    border-radius: 10px;
    margin: 30px auto;
    background-color: #ffffff;
    max-width: 600px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

.form-title {
    text-align: center;
    font-size: 30px;
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
    width: 180px;
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
    border-color: #f6b21b; /* gold */
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
        <div class="form-container">
            <div class="form-title">Welcome to NBE - Register</div>
            
            <!-- Message Panel -->
            <asp:Panel ID="pnl_message" runat="server" Visible="false">
                <asp:Label ID="lbl_message" runat="server"></asp:Label>
            </asp:Panel>
            
            <table class="auto-style1">
               <tr class="form-row">
    <td>First Name <span class="required">*</span></td>
    <td>
        <asp:TextBox ID="txt_fname" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfv_fname" runat="server" 
            ControlToValidate="txt_fname" 
            ErrorMessage="First Name is required" 
            CssClass="validation-error" Display="Dynamic" />
    </td>
</tr> 
               <tr class="form-row">
    <td>Last Name <span class="required">*</span></td>
    <td>
        <asp:TextBox ID="txt_lname" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfv_lname" runat="server" 
            ControlToValidate="txt_lname" 
            ErrorMessage="Last Name is required" 
            CssClass="validation-error" Display="Dynamic" />
               </td>
             </tr><tr class="form-row">
            <td>Email <span class="required">*</span></td>
    <td>
        <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" 
            TextMode="Email" MaxLength="100"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfv_email" runat="server" 
            ControlToValidate="txt_email" 
            ErrorMessage="Email is required" 
            CssClass="validation-error" Display="Dynamic" />
    </td>
</tr>
                <tr class="form-row">
                    <td>Phone Number<span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txt_phone" runat="server" CssClass="form-control" 
                            MaxLength="15" placeholder="e.g., 01001234567"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_phone" runat="server" 
             ControlToValidate="txt_phone" 
            ErrorMessage="Phone Number is required" 
            CssClass="validation-error" Display="Dynamic" />
                
                    </td>
                </tr>
                <tr class="form-row">
                    <td>Address<span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txt_address" runat="server" CssClass="form-control" 
                            MaxLength="30" placeholder="City, Area"></asp:TextBox>
               
                    <asp:RequiredFieldValidator ID="rvf_address" runat="server" 
 ControlToValidate="txt_address" 
ErrorMessage="address is required" 
CssClass="validation-error" Display="Dynamic" />
                    </td>
                </tr>
                <tr class="form-row">
                    <td>Password <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txt_password" runat="server" CssClass="form-control" 
                            TextMode="Password" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_password" runat="server" 
 ControlToValidate="txt_password" 
ErrorMessage="password is required" 
CssClass="validation-error" Display="Dynamic" />
                     
                    </td>
                </tr>
                <tr class="form-row">
     <td>Confirm Password <span class="required">*</span></td>
     <td>
         <asp:TextBox ID="txt_confirm_password" runat="server" CssClass="form-control" 
             TextMode="Password" MaxLength="50"></asp:TextBox>
         <asp:CompareValidator ID="cv_password" runat="server" 
             ControlToValidate="txt_confirm_password" ControlToCompare="txt_password" 
             ErrorMessage="Passwords do not match" 
             CssClass="validation-error" Display="Dynamic" />
     </td>

                </tr>
                <tr class="form-row">
                    <td colspan="2" style="text-align: center; padding-top: 20px;">
                        <asp:Button ID="btn_Submit" runat="server" Text="Register" 
                            CssClass="btn-submit" OnClick="btn_Submit_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>











