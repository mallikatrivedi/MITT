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
    public partial class SRConsole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrMsg.Visible = false;
            //at present nothing required to be done at Page_Load
        }

        protected void btnWorkload_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            drpQueueAssign.Visible = false;
            btnSRSearchIssue.Visible = false;
            issueGrid.Visible = false;
            lblIssueCount.Visible = false;
            lblNoQ.Visible = false;
            pnlIssue.Visible = false;
        }

        protected void btnTrackIssue_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            btnNewMemo.Visible = false;
            txtNewMemo.Visible = false;
            btnSaveMemo.Visible = false;
            issueGrid0.Visible = false;
            lblIssueMsg.Visible = false;
            chkClose.Visible = false;
            txtMemo.Visible = false;
        }
        
        protected void drpQueueAssign_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSRSearchIssue.Enabled = true;
            issueGrid.Visible = false;
            pnlIssue.Visible = false;
        }

        protected void btnSRSearchIssue_Click(object sender, EventArgs e)
        {
            if (drpQueueAssign.SelectedIndex >= 0)//ensure this index isnot undefined
            {   
                lblIssueCount.Visible = true;
                DateTime dt = DateTime.Now;
                DateTime dt1 = dt.Date;
                int displayCriteria = 4;
                int objectId = 1;// any value can be sent to SP when display criteria = 4
                string objectValue = drpQueueAssign.SelectedItem.Text;//passing queue name to SP
                DataTable table = new DataTable();
                
                SqlConnection myConn = new SqlConnection();
                SqlCommand myCom = new SqlCommand();
                myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                try
                {
                    myCom.CommandType = CommandType.StoredProcedure;
                    myCom.CommandText = "[dbo].[GetIssue]";
                    myCom.Parameters.AddWithValue("@displaycriteria ", displayCriteria);
                    myCom.Parameters.AddWithValue("@objectvalue", objectValue);
                    myCom.Parameters.AddWithValue("@objectid", objectId);
                    myCom.Parameters.AddWithValue("@dt1", dt1);
                    myCom.Parameters.AddWithValue("@issueid", 0);
                    myCom.Connection = myConn;
                    myConn.Open();
                    myCom.ExecuteNonQuery();
                    using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
                    {   
                        table.Clear();
                        ad.Fill(table);
                        issueGrid.Visible = true;
                        issueGrid.DataSource = table;
                        issueGrid.DataBind();
                        myConn.Close();
                        pnlIssue.Visible = true;
                    }
                    if (table.Rows.Count == 0)
                        lblIssueCount.Text = "No issues found in this queue ";
                     else
                    {
                        lblIssueCount.Text = table.Rows.Count.ToString() + " issues in this queue";
                    }
                }
                catch (SqlException sqex)
                {
                    Class1.WriteLog(sqex.ToString());
                }
                catch (Exception ex)
                {
                    Class1.WriteLog(ex.ToString());
                }
                
            }
            else
            {

                btnSRSearchIssue.Enabled = false;
                lblSelectQueue.Text = "Plese select a queue";
            }
        }

        protected void btnAssignedQueues_Click(object sender, EventArgs e)
        {
            drpQueueAssign.Visible = true;
            btnSRSearchIssue.Visible = true;
            string Suserid = (Session["user_id"].ToString());// getting the value of the SR logged in from login.aspx
            int userid = Convert.ToInt32(Suserid);
            DataTable table = new DataTable();

            try
            {
                SqlConnection myConn = new SqlConnection();
                myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                string strqry = "Select * from queue where id in(select queueid from repxqueue where repid = @userid)";
                SqlCommand myCom = new SqlCommand(strqry, myConn);
                myCom.Parameters.AddWithValue("@userid", userid);

                using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
                {
                    // fire Fill method to fetch the data and fill into DataTable 
                    table.Clear();
                    ad.Fill(table);
                }
                drpQueueAssign.DataSource = table;//give source to dropdown list
                drpQueueAssign.DataTextField = "Name"; // assign values to dropdown object properties
                drpQueueAssign.DataValueField = "id";
                drpQueueAssign.DataBind();
            }

            catch (SqlException sqex)
            {
                Class1.WriteLog(sqex.ToString());
            }
            catch (Exception ex)
            {
                Class1.WriteLog(ex.ToString());
            }
            finally
            {
                if (table.Rows.Count == 0)
                    lblNoQ.Visible = true;
            }
}
        protected void IssueGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            btnNewMemo.Visible = true;
            txtNewMemo.Visible = false;
            btnSaveMemo.Visible = false;
            int issueid = Convert.ToInt32(txtIssueNo.Text);
            DateTime dt = DateTime.Now;
            DateTime dt1 = dt.Date;
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            myConn.Open(); 
            try
            {
                SqlCommand myCom = new SqlCommand();
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.CommandText = "[dbo].[GetIssue]"; //CALL STORED PROC WITH DISPLAY CRITERIA =5 AND PASS OTHER VALUES
                myCom.Parameters.AddWithValue("@displaycriteria ", 5);
                myCom.Parameters.AddWithValue("@objectvalue", "0");
                myCom.Parameters.AddWithValue("@objectid", 0);
                myCom.Parameters.AddWithValue("@dt1", dt1);
                myCom.Parameters.AddWithValue("@issueid", issueid);
                myCom.Connection = myConn;
                myCom.ExecuteNonQuery();
                using (SqlDataAdapter ad1 = new SqlDataAdapter(myCom))
                {
                    // fire Fill method to fetch the data and fill into DataTable 
                    table1.Clear();
                    ad1.Fill(table1);
                    issueGrid0.DataSource = table1;
                    issueGrid0.DataBind();
                }
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

            if (table1.Rows.Count == 0)
                lblIssueMsg.Visible = true;
            else
            {
                issueGrid0.Visible = true;
                txtMemo.Visible = true;
                txtMemo.Enabled = false;
                chkClose.Visible = true;
                if (table1.Rows[0][5].ToString() == "Closed")
                    chkClose.Checked = true;
                else
                    chkClose.Checked = false;
             try
                {
                    SqlCommand myCom1 = new SqlCommand();
                    myCom1.CommandType = CommandType.StoredProcedure;
                    myCom1.CommandText = "[dbo].[GetMemo]";
                    myCom1.Parameters.AddWithValue("@issueid", issueid);
                    myCom1.Connection = myConn;
                    myCom1.ExecuteNonQuery();
                    using (SqlDataAdapter ad = new SqlDataAdapter(myCom1))
                    {
                        table.Clear();
                        ad.Fill(table);
                    }
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
                finally
                {
                    myConn.Close();
                }
                txtMemo.Text = "";
                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    txtMemo.Text = txtMemo.Text + table.Rows[i][1].ToString() + "  updated on " + table.Rows[i][0].ToString() + table.Rows[i][2].ToString();
                    txtMemo.Text = txtMemo.Text + "\r\n";
                }
            } 
        }

        protected void btnNewMemo_Click(object sender, EventArgs e)
        {
            txtNewMemo.Visible =true;
            txtNewMemo.Focus();
            btnSaveMemo.Visible = true;
            txtNewMemo.Text = "";
            btnSaveMemo.Enabled = true;
            txtNewMemo.Enabled = true;
        }

        protected void btnSaveMemo_Click(object sender, EventArgs e)
        {  
            int issueid= Convert.ToInt32(txtIssueNo.Text);
            txtMemo.Enabled = true;
            txtMemo.Text = "";
            string sUserId = (Session["user_id"].ToString());
            int userid = Convert.ToInt32(sUserId);

            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            SqlCommand myCom = new SqlCommand();
            try
            {
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.CommandText = "[dbo].[InsertMemo]";
                myCom.Parameters.AddWithValue("@issueid", issueid);
                myCom.Parameters.AddWithValue("@memoauthor", userid);
                myCom.Parameters.AddWithValue("@memo", txtNewMemo.Text);
                myCom.Connection = myConn;
                myConn.Open();
                myCom.ExecuteNonQuery();
                myConn.Close();
            }

            catch (SqlException sqlExc)
            {
                Class1.WriteLog(sqlExc.ToString());
                lblErrMsg.Visible = true;
            }
            catch (Exception ex)
            {
                Class1.WriteLog(ex.ToString());
                lblErrMsg.Visible = true;
            }
            finally
            {
                btnSaveMemo.Enabled = false;
                txtMemo.Visible = false;
                txtNewMemo.Enabled = false;
            }
        }

        protected void issueGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtIssueNo_TextChanged(object sender, EventArgs e)
        {
            btnGetDetails.Enabled = true;
        }

        protected void chkClose_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";

            SqlCommand myCom = new SqlCommand();
            int statusValue;
            int issueid = Convert.ToInt32(txtIssueNo.Text);
            if (chkClose.Checked == true)
                statusValue = 1;
            else
                statusValue = 0;
            try
            { 
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.CommandText = "[dbo].[UpdateStatus]";
                myCom.Parameters.AddWithValue("@issueid", issueid);
                myCom.Parameters.AddWithValue("@status", statusValue);
                myCom.Connection = myConn;
                myConn.Open();
                myCom.ExecuteNonQuery();
                myConn.Close();
            }
            catch (SqlException sqlExc)
            {
                Class1.WriteLog(sqlExc.ToString());
                lblErrMsg.Visible = true;
            }
            catch (Exception ex)
            {
                Class1.WriteLog(ex.ToString());
                lblErrMsg.Visible = true;
            }
        }
       }
     }

        
                

