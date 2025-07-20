using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace insallahfinalproject
{
    public partial class Register : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["NBEConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                RegisterUser();
            }
        }

        private void RegisterUser()
        {
            string fname = txt_fname.Text.Trim();
            string lname = txt_lname.Text.Trim();
            string email = txt_email.Text.Trim();
            string phone = txt_phone.Text.Trim();
            string address = txt_address.Text.Trim();
            string password = txt_password.Text;
            int userTypeID = 2; // Client
            int accountStatusID = 1; // Pending approval

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Check if email already exists
                            string checkEmailQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                            using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@Email", email);
                                int emailCount = (int)checkCmd.ExecuteScalar();

                                if (emailCount > 0)
                                {
                                    ShowMessage("Email already exists. Please use a different email address.", false);
                                    transaction.Rollback();
                                    return;
                                }
                            }

                            // Insert new user with pending status
                            string insertUserQuery = @"
                                INSERT INTO Users (Fname, Lname, Email, PhoneNumber, Addresses, Passwords, UserTypeID, AccountStatusID) 
                                VALUES (@Fname, @Lname, @Email, @PhoneNumber, @Address, @Password, @UserTypeID, @AccountStatusID);
                                SELECT SCOPE_IDENTITY();";

                            int newUserID = 0;
                            using (SqlCommand cmd = new SqlCommand(insertUserQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Fname", fname);
                                cmd.Parameters.AddWithValue("@Lname", lname);
                                cmd.Parameters.AddWithValue("@Email", email);
                                cmd.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                                cmd.Parameters.AddWithValue("@Password", HashPassword(password));
                                cmd.Parameters.AddWithValue("@UserTypeID", userTypeID);
                                cmd.Parameters.AddWithValue("@AccountStatusID", accountStatusID);

                                object result = cmd.ExecuteScalar();
                                if (result != null)
                                {
                                    newUserID = Convert.ToInt32(result);
                                }
                                else
                                {
                                    throw new Exception("Failed to create user record.");
                                }
                            }

                            // Log the registration
                            LogUserAction(newUserID, "Registration",
                                $"New user registration: {fname} {lname} ({email})",
                                conn, transaction);

                            transaction.Commit();

                            ShowMessage($"Registration submitted successfully! " +
                                      $"Welcome {fname} {lname}! Your account is pending admin approval. " +
                                      $"You will be able to login once your account is approved. " +
                                      $"Reference ID: {newUserID}", true);

                            ClearForm();

                            Response.Redirect("Default.aspx");
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627)
                {
                    ShowMessage("Email address already exists. Please use a different email.", false);
                }
                else
                {
                    ShowMessage("Database error: " + sqlEx.Message, false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred: " + ex.Message, false);
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

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lbl_message.Text = message;
            pnl_message.CssClass = isSuccess ? "success-message" : "error-message";
            pnl_message.Visible = true;
        }
       
        private void ClearForm()
        {
            txt_fname.Text = "";
            txt_lname.Text = "";
            txt_email.Text = "";
            txt_phone.Text = "";
            txt_address.Text = "";
            txt_password.Text = "";
            txt_confirm_password.Text = "";
        }
    }
}