using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace insallahfinalproject
{
    public partial class CompleteRegistration : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NBEConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAccountTypes();
                LoadBranches();
            }
        }

     
        private void LoadAccountTypes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT AccountTypeID, AccountTypeName FROM AccountType";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ddlAccountType.DataSource = reader;
                    ddlAccountType.DataTextField = "AccountTypeName";
                    ddlAccountType.DataValueField = "AccountTypeID";
                    ddlAccountType.DataBind();
                }
            }
        }

        private void LoadBranches()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT BranchID, BranchName FROM Branch";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ddlBranch.DataSource = reader;
                    ddlBranch.DataTextField = "BranchName";
                    ddlBranch.DataValueField = "BranchID";
                    ddlBranch.DataBind();
                }
            }
        }


        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (Session["ID"] == null)
            {
                lbl_message.Text = "Session expired. Please log in again.";
                Response.Redirect("Default.aspx");
                return;
            }

            int userID = Convert.ToInt32(Session["ID"]);
            int accountTypeID = Convert.ToInt32(ddlAccountType.SelectedValue);
            int branchID = Convert.ToInt32(ddlBranch.SelectedValue);
            decimal balance;

            if (!decimal.TryParse(txtBalance.Text, out balance) || balance < 0)
            {
                lbl_message.Text = "Please enter a valid initial balance.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // ✅ Check if this account type already exists for the user
                    string checkQuery = @"
                SELECT COUNT(*) 
                FROM Accounts 
                WHERE UserID = @UserID AND AccountTypeID = @AccountTypeID";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@UserID", userID);
                        checkCmd.Parameters.AddWithValue("@AccountTypeID", accountTypeID);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            lbl_message.Text = "❌ You already have an account of this type.";
                            transaction.Rollback();
                            return;
                        }
                    }

                    // ✅ Insert account if not duplicate
                    string insertQuery = @"
                INSERT INTO Accounts (UserID, AccountTypeID, Balance, StatusID, BranchID)
                VALUES (@UserID, @AccountTypeID, @Balance, 1, @BranchID)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@AccountTypeID", accountTypeID);
                        cmd.Parameters.AddWithValue("@Balance", balance);
                        cmd.Parameters.AddWithValue("@BranchID", branchID);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            string userName = Session["UserName"]?.ToString();
                            LogUserAction(userID, "Create Account", $"User {userName} completed account registration.", conn, transaction);

                            transaction.Commit();
                            lbl_message.ForeColor = System.Drawing.Color.Green;
                            lbl_message.Text = "🎉 Account created successfully!";
                            Response.Redirect("homepage.aspx");
                        }
                        else
                        {
                            transaction.Rollback();
                            lbl_message.Text = "❌ Failed to create account. Please try again.";
                        }
                    }
                }
                catch (Exception ex)
                {

                    lbl_message.Text = "❌ Error: " + ex.Message;
                }
            }
        }

        private void LogUserAction(int userID, string actionType, string details, SqlConnection conn, SqlTransaction transaction)
        {
            try
            {
                string query = @"
            INSERT INTO Logs (SessionID, UserID, ActionType, Timestamp, Details, IPAddress)
            VALUES (@SessionID, @UserID, @ActionType, GETDATE(), @Details, @IPAddress)";

                using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@SessionID", Session.SessionID);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ActionType", actionType);
                    cmd.Parameters.AddWithValue("@Details", details);
                    cmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Fail silently
            }
        }


    }
}