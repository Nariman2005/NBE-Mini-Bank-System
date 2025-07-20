using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;


namespace insallahfinalproject
{
    public partial class Login : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["NBEConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LoginUser();
            }
        }
        private void LoginUser()
        {
            string email = txt_username.Text.Trim();
            string password = txt_password.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        string query = @"
                SELECT ID, Fname, Lname, Passwords, UserTypeID, AccountStatusID
                FROM Users
                WHERE Email = @Email";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Email", email);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string storedHash = reader["Passwords"].ToString();
                                    int accountStatus = Convert.ToInt32(reader["AccountStatusID"]);
                                    int userTypeID = Convert.ToInt32(reader["UserTypeID"]);

                                    if (VerifyPassword(password, storedHash))
                                    {
                                        int userId = Convert.ToInt32(reader["ID"]);
                                        string userName = reader["Fname"] + " " + reader["Lname"];

                                        switch (accountStatus)
                                        {
                                            case 1:
                                                ShowMessage("❌ Your account is pending admin approval.", false);
                                                break;
                                            case 2:
                                                // Save user session
                                                Session["ID"] = userId;

                                                Session["UserName"] = userName;
                                                Session["UserTypeID"] = userTypeID;

                                                // Log the login action
                                                reader.Close(); // Close before using the connection again
                                                LogUserAction(userId, "Login", $"User {userName} logged in.", conn, transaction);
                                                transaction.Commit();

                                                // Check user type and redirect accordingly
                                                if (userTypeID == 1) // Employee/Admin
                                                {
                                                    Response.Redirect("Adminpanel.aspx");
                                                }
                                                else if (userTypeID == 2) // Client
                                                {
                                                    // Check if the client already has an account
                                                    if (!UserHasAccount(userId))
                                                        Response.Redirect("CompleteRegistration.aspx");
                                                    else
                                                        Response.Redirect("homepage.aspx");
                                                }
                                                else
                                                {
                                                    ShowMessage("❌ Unknown user type. Please contact support.", false);
                                                }
                                                break;
                                            case 3:
                                                ShowMessage("❌ Your account has been suspended or rejected.", false);
                                                break;
                                            default:
                                                ShowMessage("❌ Unknown account status. Please contact support.", false);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        ShowMessage("❌ Invalid email or password.", false);
                                    }
                                }
                                else
                                {
                                    ShowMessage("❌ No account found with that email.", false);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("❌ An error occurred during login: " + ex.Message, false);
            }
            ClearForm();
        }



        private bool UserHasAccount(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Accounts WHERE UserID  = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void LogUserAction(int userID, string actionType, string details, SqlConnection conn, SqlTransaction transaction)
        {
            try
            {
                string logQuery = @"
                    INSERT INTO Logs (SessionID, UserID, ActionType, Timestamp, Details, IPAddress) 
                    VALUES (@SessionID, @UserID, @ActionType, GETDATE(), @Details, @IPAddress)";

                using (SqlCommand logCmd = new SqlCommand(logQuery, conn, transaction))
                {
                    logCmd.Parameters.AddWithValue("@SessionID", Session.SessionID);
                    logCmd.Parameters.AddWithValue("@UserID", userID);
                    logCmd.Parameters.AddWithValue("@ActionType", actionType);
                    logCmd.Parameters.AddWithValue("@Details", details);
                    logCmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
                    logCmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Log silently fails
            }
        }
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // If using plain text (not recommended), just compare
            //return enteredPassword == storedHash;

            // If using hashed passwords
            return HashPassword(enteredPassword) == storedHash;
        }

        private string HashPassword(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            pnl_message.Visible = true;
            lbl_message.Text = message;
            lbl_message.CssClass = isSuccess ? "success-message" : "error-message";
        }

        private void ClearForm()
        {
            txt_username.Text = "";
            txt_password.Text = "";
        }
    }
}