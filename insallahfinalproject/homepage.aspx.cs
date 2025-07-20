using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace insallahfinalproject
{
    public partial class homepage : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NBEConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ID"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                int userID = Convert.ToInt32(Session["ID"]);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    Fname, Lname, AccountStatusID
                FROM Users u
                WHERE u.ID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string fullName = reader["Fname"] + " " + reader["Lname"];
                                lblUsername.Text = fullName;

                                // Optionally save to Session for later logging
                                Session["UserName"] = fullName;
                            }
                            else
                            {
                                Response.Redirect("Default.aspx");
                                return;
                            }
                        }
                    }
                }
            }
            LogUserAction((int)Session["ID"],"Entered the homepage", $"{Session["UserName"]} investigate the home page");
        }



        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            LogUserAction((int)Session["ID"], "Transfer Click", $"{Session["UserName"]}clicked on Transfer button.");
            Response.Redirect("Transfer.aspx");
        }
        protected void btnAddAccount_Click(object sender, EventArgs e)
        {
            LogUserAction((int)Session["ID"], "Add Acount Click", $"{Session["UserName"]}clicked on Add Account button.");
            Response.Redirect("CompleteRegistration.aspx");
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            LogUserAction((int)Session["ID"], "Exit Click", $"{Session["UserName"]}clicked on Exit button.");
            Session.Clear();       // Remove all session data
            Session.Abandon();     // Terminate the session
            Response.Redirect("Default.aspx"); // Redirect to login or default page
        }
        protected void btnBankStatment_Click(object sender, EventArgs e)
        {
            LogUserAction((int)Session["ID"],"Bank Statment Click",$"{ Session["UserName"]} clicked on Bank Statment button");
            Response.Redirect("Bankstatment.aspx");
        }

        protected void btnCheckBalance_Click(object sender, EventArgs e)
        {
            LogUserAction((int)Session["ID"], "Check Balance Click", $"{Session["UserName"]} clicked on Check Balance button.");
            Response.Redirect("CheckBalance.aspx");
        }
        protected void btnMyAccount_Click(object sender, EventArgs e)
        {
            LogUserAction((int)Session["ID"], "My Accounts Click", $"{Session["UserName"]}clicked on My Account button.");
            Response.Redirect("myaccounts.aspx"); 
        }

        private void LogUserAction(int userID, string actionType, string details)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string logQuery = @"
                        INSERT INTO Logs (SessionID, UserID, ActionType, Timestamp, Details, IPAddress) 
                        VALUES (@SessionID, @UserID, @ActionType, GETDATE(), @Details, @IPAddress)";

                    using (SqlCommand logCmd = new SqlCommand(logQuery, conn))
                    {
                        logCmd.Parameters.AddWithValue("@SessionID", Session.SessionID);
                        logCmd.Parameters.AddWithValue("@UserID", userID);
                        logCmd.Parameters.AddWithValue("@ActionType", actionType);
                        logCmd.Parameters.AddWithValue("@Details", details);
                        logCmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
                        logCmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
              
            }
        }
    }
}
