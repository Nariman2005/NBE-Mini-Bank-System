using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace insallahfinalproject
{
    public partial class Adminpanel : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NBEConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ID"] == null)
                {
                    lbl_message.Text = "Session expired. Please log in again.";
                    Response.Redirect("Default.aspx");
                    return;
                }
                LoadPendingUsers();

            }
        }

        private void LoadPendingUsers()

        {
           
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE AccountStatusID = 1"; // Only pending users
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gv_pendingUsers.DataSource = dt;
                gv_pendingUsers.DataBind();
            }
        }
        protected void btnViewLogs_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewLogs.aspx");
        }


        protected void gv_pendingUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gv_pendingUsers.Rows[rowIndex];
                int userId = Convert.ToInt32(gv_pendingUsers.DataKeys[rowIndex].Value);

                int newStatus = (e.CommandName == "Approve") ? 2 : 3; // 2: Approved, 3: Rejected

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE Users SET AccountStatusID = @Status WHERE ID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lbl_message.Text = $"User ID {userId} has been {(newStatus == 2 ? "approved" : "rejected")}.";
                lbl_message.ForeColor = (newStatus == 2) ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                LoadPendingUsers(); // Refresh the grid
            }
        }
    }
}