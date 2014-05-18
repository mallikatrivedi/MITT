using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace MITT
{
    public partial class UserConsole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {               
              //no code required here as the one drop down is static and other is databound
            }
        }

        protected void btnNewIssue_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            pnlIssueNo.Visible = false;
            pnlLogNewIssue.Visible = true;
            lblUrIssue.Visible = false;
            lblLogged.Visible = false;
            lblIssueNo.Visible = false;
            txtDetails.Text = String.Empty;
            txtTitle.Text = String.Empty;
            drpUrgency.SelectedIndex = 0;
            txtTitle.Focus();
         }

        protected void btnIssueHistory_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            string Suserid = (Session["user_id"].ToString());//passing session variables to other page
            int userid = Convert.ToInt32(Suserid);
            DataTable table = new DataTable();
            try
            {
                SqlConnection myConn = new SqlConnection();
                myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                string strqry = "Select id, title, timelogged,  (CASE WHEN status=1 Then 'Closed' Else 'Open' END) as status   from issue  where requesterid = @userid and id > 0";
                SqlCommand myCom = new SqlCommand(strqry, myConn);
                myCom.Parameters.AddWithValue("@userid", userid);

                using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
                {
                    // fire Fill method to fetch the data and fill into DataTable 
                    table.Clear();
                    ad.Fill(table);
                }
                if (table.Rows.Count == 0)
                    lblZeroRecords.Text = "No issues logged till date";
                else
                {
                    GridView1.DataSource = table;
                    GridView1.DataBind();
                }
                myConn.Open();
                string strqrySysMail = "Select EmailAddress from MITEmail where id = 1";
                SqlCommand myComSysMail = new SqlCommand(strqrySysMail, myConn);

                string Sysemail = myComSysMail.ExecuteScalar().ToString();
                lblMITTEmail.Text = Sysemail;
                myConn.Close();

            }
            catch (SqlException sqex)
            {
                Class1.WriteLog(sqex.ToString());
                lblErrMsg.Visible = true;
            }
            catch (Exception ex)
            {
                Class1.WriteLog(ex.ToString());
                lblErrMsg.Visible = true;
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            lblChngProfile.Visible = false;
            txtPh.Enabled = false;
            txtcompany.Enabled = false;
            txtPwd.Enabled = false;
            lblConfirmPwd.Visible = false;
            txtConfirmPwd.Visible = false;
            btnProfileSave.Enabled = false;
            string Suserid = (Session["user_id"].ToString());
            int userid = Convert.ToInt32(Suserid);
            DataTable table = new DataTable();

            try
            {
                SqlConnection myConn = new SqlConnection();
                myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                string strqry = "Select * from Users where id = @userid";
                SqlCommand myCom = new SqlCommand(strqry, myConn);
                myCom.Parameters.AddWithValue("@userid", userid);
            
                using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
                {
                    table.Clear();
                    ad.Fill(table);
                }

                if (table.Rows.Count > 0)
                {
                   lblName.Text = table.Rows[0][1].ToString();
                   lblEmail.Text = table.Rows[0][2].ToString();
                   txtPh.Text  = table.Rows[0][6].ToString();
                   txtcompany.Text =  table.Rows[0][3].ToString();
                   txtPwd.Text = table.Rows[0][5].ToString();
                }
                myConn.Close();
             }
            catch (SqlException sqex)
                {
                    Class1.WriteLog(sqex.ToString());
                    lblErrMsg.Visible = true;
                }
                catch (Exception ex)
                {
                    Class1.WriteLog(ex.ToString());
                    lblErrMsg.Visible = true;
                }
}
        protected void btnProfileSave_Click(object sender, EventArgs e)
        {
            string Suserid = (Session["user_id"].ToString());
            int userid = Convert.ToInt32(Suserid);
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";

            SqlCommand myCom = new SqlCommand();

            myCom.CommandType = CommandType.StoredProcedure;
            myCom.CommandText = "[dbo].[UpdateUserProfile]";
            myCom.Parameters.AddWithValue("@Userid",userid);
            myCom.Parameters.AddWithValue("@Pwd", txtPwd.Text);
            myCom.Parameters.AddWithValue("@CompanyName", txtcompany.Text);
            myCom.Parameters.AddWithValue("@PhNo", txtPh.Text);
            //myCom.Parameters.AddWithValue("@id", 0);
            //myCom.Parameters["@id"].Direction = ParameterDirection.Output;
            myCom.Connection = myConn;
            myConn.Open();
            try
            {
                myCom.ExecuteNonQuery();
                //lblIssueNo.Text = myCom.Parameters["@id"].Value.ToString();
                myConn.Close();
                btnProfileSave.Enabled = false;
                lblChngProfile.Visible = true;
                lblChngProfile.Text = "Your profile has been updated";
                //add try catch
                txtcompany.Enabled = false;
                txtPh.Enabled = false;
                txtPwd.Enabled = false;
                txtConfirmPwd.Enabled = false;
             }
            catch (SqlException sqex)
            {
                Class1.WriteLog(sqex.ToString());
                lblErrMsg.Visible = true;
            }
            catch (Exception ex)
            {
                Class1.WriteLog(ex.ToString());
                lblErrMsg.Visible = true;
            }
        }

        protected void btnEditComp_Click(object sender, EventArgs e)
        {
            txtcompany.Enabled = true;
            txtcompany.Focus();
            btnProfileSave.Enabled = true;
        }

        protected void btnEditPwd_Click(object sender, EventArgs e)
        {
            lblConfirmPwd.Visible = true;
            txtConfirmPwd.Visible = true;
            txtPwd.Enabled = true;
            txtPwd.Focus();
            btnProfileSave.Enabled = true;
        }

        protected void txtTitle_TextChanged(object sender, EventArgs e)
        {
            btnLogIssue.Enabled = true;
        }

        protected void drpCat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpUrgency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void btnEditPh_Click(object sender, EventArgs e)
        {
            txtPh.Enabled = true;
            txtPh.Focus();
            btnProfileSave.Enabled = true;
            
        }

        protected void btnLogIssue_Click(object sender, EventArgs e)
        {
            string sUserId = (Session["user_id"].ToString());
            int userid = Convert.ToInt32(sUserId);

            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";

            SqlCommand myCom = new SqlCommand();

            try
            {
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.CommandText = "[dbo].[InsertIssue]";
                myCom.Parameters.AddWithValue("@title", txtTitle.Text);
                myCom.Parameters.AddWithValue("@Requesterid", userid);
                myCom.Parameters.AddWithValue("@Category", drpCat.SelectedItem.Value);
                myCom.Parameters.AddWithValue("@details", txtDetails.Text);
                myCom.Parameters.AddWithValue("@UserUrgency", drpUrgency.SelectedIndex);
                myCom.Parameters.AddWithValue("@id", 0);
                myCom.Parameters["@id"].Direction = ParameterDirection.Output;
                myCom.Connection = myConn;
                myConn.Open();

                myCom.ExecuteNonQuery();
                lblIssueNo.Text = "#" + myCom.Parameters["@id"].Value.ToString();
                myConn.Close();
                lblUrIssue.Visible = true;
                lblLogged.Visible = true;
                lblIssueNo.Visible = true;
                pnlIssueNo.Visible = true;
                pnlIssueNo.Enabled = false;
                pnlLogNewIssue.Visible = false;
            }

            catch (SqlException sqex)
            {
                Class1.WriteLog(sqex.ToString());
                lblErrMsg.Visible = true;
            }
            catch (Exception ex)
            {
                Class1.WriteLog(ex.ToString());
                lblErrMsg.Visible = true;
            }
        }

        protected void txtPh_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtcompany_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnPhEdit_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }  
           
   }
}
