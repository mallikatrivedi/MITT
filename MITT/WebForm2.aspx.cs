using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;
namespace MITT
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblDetails.Text = Request.QueryString["id"];
            int issuid = Convert.ToInt32(Request.QueryString["id"]);
            int issueid = issuid;
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=HD285\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            String strqry;
            strqry = "Select * from issue where id = @issueid";
            SqlCommand myCom1 = new SqlCommand(strqry, myConn);

            myCom1.Parameters.AddWithValue("@issueid", issueid);

            using (SqlDataAdapter ad1 = new SqlDataAdapter(myCom1))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table1.Clear();
                ad1.Fill(table1);
            }
            int priority, urgency, status;

            if (table1.Rows.Count > 0)
            {
                txtTitle.Text = table1.Rows[0][1].ToString();
                txtCategory.Text = table1.Rows[0][3].ToString();
                txtDtLogged.Text = table1.Rows[0][4].ToString();
                priority = Convert.ToInt32(table1.Rows[0][6]);
                switch (priority)
                {
                    case 1: txtPriority.Text = "Low"; break;
                    case 2: txtPriority.Text = "Medium"; break;
                    case 3: txtPriority.Text = "High"; break;
                    case 4: txtPriority.Text = "Critical"; break;

                }

                urgency = Convert.ToInt32(table1.Rows[0][8]);
                switch (urgency)
                {
                    case 0: txtUrgency.Text = "Low"; break;
                    case 1: txtUrgency.Text = "Medium"; break;
                    case 2: txtUrgency.Text = "High"; break;
                    case 3: txtUrgency.Text = "Critical"; break;

                }
                status = Convert.ToInt32(table1.Rows[0][9]);
                switch (status)
                {
                    case 0: txtStatus.Text = "Open"; break;
                    case -1: txtStatus.Text = "Closed"; break;
                }
                myConn.Open();
                string strqrySysMail = "Select EmailAddress from MITEmail where id = 1";
                SqlCommand myComSysMail = new SqlCommand(strqrySysMail, myConn);
                
                string Sysemail = myComSysMail.ExecuteScalar().ToString();
                txtEmail.Text = Sysemail;
                myConn.Close();
            }
        }
    }
}