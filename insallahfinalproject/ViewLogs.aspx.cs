using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace insallahfinalproject
{
    public partial class ViewLogs : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NBEConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLogs();
            }
        }

        private void LoadLogs()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        LogID,
                        SessionID,
                        UserID,
                        ActionType,
                        FORMAT(Timestamp, 'yyyy-MM-dd HH:mm:ss') AS Timestamp,
                        Details,
                        IPAddress
                    FROM Logs
                    ORDER BY Timestamp DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    gvLogs.DataSource = dt;
                    gvLogs.DataBind();
                }
            }
        }
    }
}
