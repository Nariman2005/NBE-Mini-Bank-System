using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace insallahfinalproject
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btn_Login_Click(object sender, EventArgs e)
        {
            string selectedRole = rblRole.SelectedValue;



            // Redirect to login page with role parameter
            Response.Redirect($"Login.aspx?role={selectedRole}");
        }

        protected void btn_Register_Click(object sender, EventArgs e)
        {
            string selectedRole = rblRole.SelectedValue;

            // Redirect to registration page
            Response.Redirect("Register.aspx");
        }
        protected void rblRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRole = rblRole.SelectedValue;

            if (selectedRole == "client")
            {
                lblMessage.Text = "Selected: <strong>Client</strong> - Login or register for banking services";
                btn_Register.Visible = true;
            }
            else if (selectedRole == "admin")
            {
                lblMessage.Text = "Selected: <strong>Admin</strong> - Login with admin credentials";
                btn_Register.Visible = false;
            }

            lblMessage.Visible = true;
        }

    }
}