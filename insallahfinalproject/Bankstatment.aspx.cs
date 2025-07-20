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
    public partial class Bankstatment : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["NBEConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ID"] == null)
                    Response.Redirect("Login.aspx");

                LoadAccounts();
            }
        }

        private void LoadAccounts()
        {
            int userId = Convert.ToInt32(Session["ID"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT 
 AccountID,
    CONCAT(AccountID, 
        CASE 
            WHEN AccountTypeID = 1 THEN ' -Current'
            WHEN AccountTypeID = 2 THEN ' -Saving'
        END
    ) AS DisplayText
FROM Accounts 
WHERE UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddlAccounts.DataSource = reader;
                    ddlAccounts.DataTextField = "DisplayText";
                    ddlAccounts.DataValueField = "AccountID";
                    ddlAccounts.DataBind();
                }
            }

            if (ddlAccounts.Items.Count > 0)
                LoadTransactions(Convert.ToInt32(ddlAccounts.SelectedValue));
        }

        protected void ddlAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTransactions(Convert.ToInt32(ddlAccounts.SelectedValue));
        }

        private void LoadTransactions(int accountId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                SELECT 
                    t.TransactionID,
                    t.Timestamp,
                    t.Amount,
                    CASE 
                        WHEN t.SenderAccountID = @AccountID THEN 'Debit'
                        WHEN t.ReceiverAccountID = @AccountID THEN 'Credit'
                        ELSE 'Other'
                    END AS Type,
                    u.Email AS Counterparty
                FROM Transactions t
                JOIN Accounts a1 ON 
                    (t.SenderAccountID = @AccountID AND t.ReceiverAccountID = a1.AccountID)
                    OR (t.ReceiverAccountID = @AccountID AND t.SenderAccountID = a1.AccountID)
                JOIN Users u ON a1.UserID = u.ID
                WHERE t.SenderAccountID = @AccountID OR t.ReceiverAccountID = @AccountID
                ORDER BY t.Timestamp DESC;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    gvTransactions.DataSource = dt;
                    gvTransactions.DataBind();


                    int userId = Convert.ToInt32(Session["ID"]);
                    string userName = Session["UserName"]?.ToString();
                    LogUserAction(userId, "View Statement", $"User {userName} viewed transactions for Account #{accountId}", conn);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("homepage.aspx");
        }

        private void LogUserAction(int userID, string actionType, string details, SqlConnection conn)
        {
            try
            {
                string query = @"
                    INSERT INTO Logs (SessionID, UserID, ActionType, Timestamp, Details, IPAddress)
                    VALUES (@SessionID, @UserID, @ActionType, GETDATE(), @Details, @IPAddress)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
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
                // Optional: log failure to log to a file or ignore
            }
        }
    }
}
    