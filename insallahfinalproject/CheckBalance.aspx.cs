using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace insallahfinalproject
{
    public partial class CheckBalance : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NBEConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Session validation
                if (Session["ID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                LoadBalance();
                LoadBalancefordiffaccountstype();
            }
        }

        private void LoadBalance()
        {
            int userId = Convert.ToInt32(Session["ID"]);
            string userName = Session["UserName"]?.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT SUM(Balance) FROM Accounts WHERE UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal totalBalance = Convert.ToDecimal(result);
                        lblBalance.Text = $"💰 Your total balance across all accounts is:<br/>EGP {totalBalance:N2}";

                        LogUserAction(userId, "Check Balance", $"User {userName} checked their total balance.", conn);
                    }
                   
                }
            }
        }

        private void LoadBalancefordiffaccountstype()
        {
            int userId = Convert.ToInt32(Session["ID"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT AccountID, AccountTypeID, Balance
            FROM Accounts
            WHERE UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    rptAccounts.DataSource = reader;
                    rptAccounts.DataBind();
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
                // Ignore logging errors
            }
        }
    }
}
