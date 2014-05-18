using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;



namespace MITT
{
    public partial class IssueDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            
            lblDetails.Text = Request.QueryString["id"];
            int issuid = Convert.ToInt32(Request.QueryString["id"]);
            int issueid = issuid;
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                       
            string strqry = "select M.memodate as [DATE], U.name as [AUTHOR], M.memo as [MEMO] from Memo M inner join Users U on M.memoauthor = u.id WHERE M.issueid = @issuid";
                //"Select * from memos where issueid =@issuid";

            SqlCommand myCom = new SqlCommand(strqry, myConn);
            myCom.Parameters.AddWithValue("@issuid", issuid);
            using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
            }
            GridView1.DataSource = table;
            GridView1.DataBind();

            strqry = "Select * from issue where id = @issueid";
            SqlCommand myCom1 = new SqlCommand(strqry, myConn);
            
            myCom1.Parameters.AddWithValue("@issueid", issueid);

            using (SqlDataAdapter ad1 = new SqlDataAdapter(myCom1))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table1.Clear();
                ad1.Fill(table1);
            }
            int  priority, urgency, status;
            string requesterName;

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


                
            }
            
        }

        
           
        protected void Button1_Click(object sender, EventArgs e)
        {
            //IMPROVE - get system email and password from DB rather than hardcoding

            string issuid = (Request.QueryString["id"]).ToString();
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                       
            string strqry = "Select email from users where id = (select requesterid from issue where id = @issueid)";
            SqlCommand myCom1 = new SqlCommand(strqry, myConn);

            myCom1.Parameters.AddWithValue("@issueid", issuid);
            myConn.Open();
           string email = myCom1.ExecuteScalar().ToString();

           string strqrySysMail = "Select EmailAddress from MITEmail where id = 1";
           SqlCommand myComSysMail = new SqlCommand(strqrySysMail, myConn);

           string Sysemail = myComSysMail.ExecuteScalar().ToString();

           string strqryPwd = "Select Password from MITEmail where id = 1";
           SqlCommand myComPwd = new SqlCommand(strqryPwd, myConn);

           string Pwd = myComPwd.ExecuteScalar().ToString();
           myConn.Close();
           txtEmail.Text = email;
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;
 
            MailAddress from = new MailAddress(Sysemail);

            MailAddress to = new MailAddress(email);
            //txtEmail.Text

            MailMessage message = new MailMessage(from, to);

            //Attachment att = new Attachment("C:\\Reports\\Tulips.jpg");

            message.Body = TextBox5.Text;//"This is a test e-mail message sent using gmail as a relay server ";

            message.Subject = "Issue #" + issuid + " Issue titled "+txtTitle .Text+" logged on " +txtDtLogged.Text;//"Gmail test email with SSL and Credentials";
            //message.Attachments.Add(att);
            NetworkCredential myCreds = new NetworkCredential(Sysemail, "dalroti4", "");

            client.Credentials = myCreds;

            try
            {

                client.Send(message);
              //int   linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":line") + 5));

            }

            catch (Exception ex)
            {

                Console.WriteLine("Exception is:" + ex.ToString());
                TextBox5.Text = "Exception is:" + ex.ToString();

                Class1.WriteLog(ex.ToString());
            }

            Console.WriteLine("Goodbye.");
            Button1.Visible = false;
        }

        protected void gridMemos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        }
    }
