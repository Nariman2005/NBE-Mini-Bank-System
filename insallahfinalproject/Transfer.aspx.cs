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
    public partial class Transfer : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["NBEConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ID"]  != null)
                {
                    int userId = Convert.ToInt32(Session["ID"]);

                    // Optional: Show sender phone
                    txtSenderPhone.Text = GetUserPhone(userId);

                    // Load user's own accounts into sender dropdown
                    LoadSenderAccounts(userId);
                }
                else
                {
                    // Redirect to login if session expired
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void LoadSenderAccounts(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT AccountID, 
                    
                   CONCAT('Account #', AccountID, ' - Balance: ', Balance, ' EGP', CASE 
                        WHEN AccountTypeID = 1 THEN '-Current'
                        WHEN AccountTypeID = 2 THEN '-Saving'
                    END) AS DisplayText 
                   
            FROM Accounts 
            WHERE UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddlSenderAccounts.Items.Clear();
                    if (reader.HasRows)
                    {
                        ddlSenderAccounts.DataSource = reader;
                        ddlSenderAccounts.DataTextField = "DisplayText";
                        ddlSenderAccounts.DataValueField = "AccountID";
                        ddlSenderAccounts.DataBind();
                    }
                    else
                    {
                        ddlSenderAccounts.Items.Add(new ListItem("No accounts found", ""));
                    }
                }
            }
        }

        private string GetUserPhone(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT PhoneNumber FROM Users WHERE ID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "";
                }
            }
        }
      

        protected void txtRecipientPhone_TextChanged(object sender, EventArgs e)
        {
            string phone = txtRecipientPhone.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT a.AccountID, 
                   CONCAT('Account #', a.AccountID, ' - ', u.Fname, ' ', u.Lname) AS DisplayText
            FROM Accounts a
            INNER JOIN Users u ON a.UserID = u.ID
            WHERE u.PhoneNumber = @Phone";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddlRecipientAccounts.Items.Clear();
                    if (reader.HasRows)
                    {
                        ddlRecipientAccounts.DataSource = reader;
                        ddlRecipientAccounts.DataTextField = "DisplayText";
                        ddlRecipientAccounts.DataValueField = "AccountID";
                        ddlRecipientAccounts.DataBind();
                    }
                    else
                    {
                        ddlRecipientAccounts.Items.Add(new ListItem("No accounts found", ""));
                    }
                }
            }
        }



        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            int senderAccountId = int.Parse(ddlSenderAccounts.SelectedValue);

            int recipientAccountId = int.Parse(ddlRecipientAccounts.SelectedValue);
            decimal amount;

          

            if (!decimal.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                lblMessage.Text = "❌ Enter a valid positive amount.";
                lblMessage.CssClass = "message error";
                return;
            }

            if (senderAccountId == recipientAccountId)
            {
                lblMessage.Text = "❌ Cannot transfer to the same account.";
                lblMessage.CssClass = "message error";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Get sender balance
                    decimal senderBalance = 0;
                    string checkBalanceQuery = "SELECT Balance FROM Accounts WHERE AccountID = @SenderAccountID";
                    using (SqlCommand checkCmd = new SqlCommand(checkBalanceQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@SenderAccountID", senderAccountId);
                        object result = checkCmd.ExecuteScalar();
                        if (result != null)
                            senderBalance = Convert.ToDecimal(result);
                        else
                        {
                            lblMessage.Text = "❌ Sender account not found.";
                            lblMessage.CssClass = "message error";
                            transaction.Rollback();
                            return;
                        }
                    }

                    if (senderBalance < amount)
                    {
                        lblMessage.Text = "❌ Insufficient balance.";
                        lblMessage.CssClass = "message error";
                        transaction.Rollback();
                        return;
                    }

                    // Deduct from sender
                    string deductQuery = "UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountID = @SenderAccountID";
                    using (SqlCommand cmd = new SqlCommand(deductQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@SenderAccountID", senderAccountId);
                        cmd.ExecuteNonQuery();
                    }

                    // Add to recipient
                    string creditQuery = "UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountID = @RecipientAccountID";
                    using (SqlCommand cmd = new SqlCommand(creditQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@RecipientAccountID", recipientAccountId);
                        cmd.ExecuteNonQuery();
                    }

                    // Insert into Transactions table (both debit and credit in one row)
                    string insertTransactionQuery = @"
                INSERT INTO Transactions (Amount, TransactionTypeID, SenderAccountID, ReceiverAccountID)
                VALUES (@Amount, @TransactionTypeID, @SenderAccountID, @ReceiverAccountID)";
                    using (SqlCommand cmd = new SqlCommand(insertTransactionQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@TransactionTypeID", 2); 
                        cmd.Parameters.AddWithValue("@SenderAccountID", senderAccountId);
                        cmd.Parameters.AddWithValue("@ReceiverAccountID", recipientAccountId);
                        cmd.ExecuteNonQuery();
                    }

                    // Get sender user ID
                    int senderUserId = 0;
                    string getUserQuery = @"SELECT UserID FROM Accounts WHERE AccountID = @SenderAccountID";
                    using (SqlCommand cmd = new SqlCommand(getUserQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@SenderAccountID", senderAccountId);
                        senderUserId = (int)cmd.ExecuteScalar();
                    }

                    string logDetails = $"Transferred {amount} EGP from Account #{senderAccountId} to Account #{recipientAccountId}";

                    LogUserAction(senderUserId, "Transfer", logDetails, conn, transaction);

                    transaction.Commit();
                    lblMessage.Text = "✅ Transfer completed";
                    lblMessage.CssClass = "message success";
                    Response.Redirect("homepage.aspx");
                }
                catch (Exception ex)
                {
             
                    lblMessage.Text = "❌ Error: " + ex.Message;
                    lblMessage.CssClass = "message error";
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
                // Optional: You can log this failure to a file or ignore silently.
            }
        }







    }
}