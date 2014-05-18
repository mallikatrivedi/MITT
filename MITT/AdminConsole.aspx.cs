using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace MITT
{
    public partial class AdminConsole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrMsg.Visible = false;
            if (!IsPostBack)
            {
                //this has to be implemented
            }
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            txtEmail.Enabled = false;
            txtPassword.Enabled = false;
            lblConfirmPwd.Visible = false;
            txtConfPwd.Visible = false;
            // add verification for system email account format- javascript
            try
            {
                SqlConnection myConn = new SqlConnection();
                myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                string strqry = "Update MITEmail set EmailAddress = @email, Password = @pwd where id = 1";
                SqlCommand myCom = new SqlCommand(strqry, myConn);
                myCom.Parameters.AddWithValue("@email", txtEmail.Text);
                myCom.Parameters.AddWithValue("@pwd", txtPassword.Text);
                myConn.Open();
                myCom.ExecuteNonQuery();
                myConn.Close();
            }
            catch (SqlException sqex)
            {
                Class1.WriteLog(sqex.ToString());
                lblErrMsg.Visible = true;
            }
            lblSubmit.Visible = true;
       }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
            txtEmail.Enabled = false;
            txtPassword.Enabled = false;
            lblConfirmPwd.Visible = false;
            txtConfPwd.Visible = false;
            btnSubmit.Enabled = false;
            btnEditConfPwd.Visible = false;
            lblSubmit.Visible = false;
            try
            {
                SqlConnection myConn = new SqlConnection();
                myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                myConn.Open();
                string strqry = "Select EmailAddress from MITEmail where id = 1";
            
                 SqlCommand myCom = new SqlCommand(strqry, myConn);
                 txtEmail.Text = (String)myCom.ExecuteScalar();
                 myConn.Close();
            }
            catch (SqlException sqlExc)
            {
                Console.WriteLine(sqlExc.Message); 
                lblErrMsg.Visible = true;
            }
            catch (Exception ex)
            {
                Class1.WriteLog(ex.ToString());
                lblErrMsg.Visible = true;
            }
        }

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            pnlCreateUser.Visible = true;
            rdUserType.Items[0].Selected = true;
            txtName.Text = String.Empty;
            txtUserEmail.Text = String.Empty;
            txtPh.Text = String.Empty;
            txtCompany.Text = String.Empty;
            txtPwd.Text = String.Empty;
            txtConfirmPwd.Text = String.Empty;
            pnlFindUser.Visible = false;
            lblAssignRoles.Visible = false;
            lblUserName.Visible = false;
            txtName.Focus();
        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            Session["Datelogged"] = DateTime.Now;
            Session["criteriaid"] = "0";
            Session["criteriaValue"] ="*";
            btnSearchIssue.Visible = false;
        }

        protected void btnUser_Click(object sender, EventArgs e)
        { 
            MultiView1.ActiveViewIndex = 0;
            pnlCreateUser.Visible = false;
            pnlFindUser.Visible = false;
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {   
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            SqlCommand myCom = new SqlCommand();
            try
            {
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.CommandText = "[dbo].[AddUser]";
                myCom.Parameters.AddWithValue("@Name", txtName.Text);
                myCom.Parameters.AddWithValue("@Email", txtUserEmail.Text);
                myCom.Parameters.AddWithValue("@CompanyName", txtCompany.Text);
                myCom.Parameters.AddWithValue("@password", txtPwd.Text);
                myCom.Parameters.AddWithValue("@phNo", txtPh.Text);
                int Role;
                if (rdUserType.SelectedIndex == 0)
                    Role = 2;
                else if (rdUserType.SelectedIndex == 1)
                    Role = 1;
                else Role = 0;
                myCom.Parameters.AddWithValue("@Roles", Role);
                myCom.Connection = myConn;
                myConn.Open();
                myCom.ExecuteNonQuery();
                lblUserName.Text = txtName.Text + " has been added";
                lblUserName.Visible = true;
                if (Role == 1)
                    lblAssignRoles.Visible = true;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2601://unique key constraint
                        lblUserName.Text = "Duplicate  names or email addresses are not allowed, Please enter a unique name and email address";
                        break;
                    default:
                        Class1.WriteLog(ex.ToString());
                        break;
                }
            }
            finally
            {
                myConn.Close();
            }
        }

        protected void btnCategory_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            lblMsgQAdded.Visible = false;
        }

        protected void btnRoles_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            drpUser.Visible = false;
            btnAssign.Enabled = false;
            btnUpdAssign.Visible = false;
            chkQueue.Visible = false;
        }

        protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkQueue.Visible = false;
            btnUpdAssign.Visible = false;
        }

        protected void btnSelectUser_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            string strqry = "Select * from users where id > 0 and roles = 1 ";
            SqlCommand myCom = new SqlCommand(strqry, myConn);
            using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
            }
            drpUser.Visible = true;
            btnAssign.Enabled = true;
            drpUser.DataSource = table;
            drpUser.DataTextField = "Name";
            drpUser.DataValueField = "id";
            drpUser.DataBind();
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            List<int> queues = new List<int>();
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            chkQueue.Visible = true;
            chkQueue.Enabled = true;
            btnUpdAssign.Visible = true;
            btnUpdAssign.Enabled = true;
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            string strqry = "Select * from queue";
            SqlCommand myCom = new SqlCommand(strqry, myConn);
            using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                table.Clear();
                ad.Fill(table);
            }
            chkQueue.DataSource = table;
            chkQueue.DataTextField = "Name";
            chkQueue.DataValueField = "id";
            chkQueue.DataBind();
            String Uname = drpUser.SelectedItem.Text;
            String strqry1 = "select id from users where name=@Uname";
            SqlCommand myCom2 = new SqlCommand(strqry1, myConn);
            myCom2.Parameters.AddWithValue("@Uname", Uname);
            myConn.Open();
            int rid = (int)myCom2.ExecuteScalar();
            myConn.Close();

            strqry = "select queueid from repxqueue where repid = @rid";
            SqlCommand myCom1 = new SqlCommand(strqry, myConn);
            myCom1.Parameters.AddWithValue("@rid", rid);
            using (SqlDataAdapter ad1 = new SqlDataAdapter(myCom1))
            {
                table1.Clear();
                ad1.Fill(table1);
            }
            try
            {
                int pos = 0;
                foreach (DataRow dtrow in table1.Rows)
                {
                    int i = (int)table1.Rows[pos][0];
                    queues.Add(i);
                    pos++;
                }
                foreach (ListItem chk1 in chkQueue.Items)
                {
                    chk1.Selected = false;
                }

                int queueid;
                for (int i = 0; i < queues.Count; i++)
                {
                    queueid = queues[i];
                    int qid; int ctr = 0; //int qpos;
                    foreach (DataRow dtrow in table.Rows)
                    {
                        qid = (int)table.Rows[ctr][0];
                        if (qid == queueid) break;
                        ctr++;
                    }

                    chkQueue.Items[ctr].Selected = true;
                }

                //remove the 0th entry
                //try using css class
                chkQueue.Items[0].Text = " ";
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

        protected void btnUpdAssign_Click(object sender, EventArgs e)
        {
            {  // removing of assignemnts yet to be handled.....
                //one way call a stored procedure to first delete all present assignments and then insert 
                // or google out table valued parameters to pass a list as a parameter
                SqlConnection myConn = new SqlConnection();
                myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                String Uname = drpUser.SelectedItem.Text;
                String strqry1 = "select id from users where name=@Uname";
                SqlCommand myCom2 = new SqlCommand(strqry1, myConn);
                myCom2.Parameters.AddWithValue("@Uname", Uname);
                myConn.Open();
                int rid = (int)myCom2.ExecuteScalar();
                myConn.Close();

                String strqry = "delete from repxqueue where repid= @rid";
                SqlCommand myCom1 = new SqlCommand(strqry, myConn);
                myCom1.Parameters.AddWithValue("@rid", rid);
                myConn.Open();
                myCom1.ExecuteNonQuery();
                myConn.Close();
                string queuename;
                for (int i = 0; i < chkQueue.Items.Count; i++)
                {
                    if (chkQueue.Items[i].Selected == true)
                    {
                        SqlCommand myCom = new SqlCommand();
                        myCom.CommandType = CommandType.StoredProcedure;
                        myCom.CommandText = "[dbo].[AddRoles]";
                        myCom.Parameters.AddWithValue("@repid", rid);
                        queuename = chkQueue.Items[i].Text;
                        myCom.Parameters.AddWithValue("@queuename", queuename);
                        myCom.Connection = myConn;
                        myConn.Open();
                        myCom.ExecuteNonQuery();
                        myConn.Close();
                        //IMPORTANT - DISCUSS WITH MITHU- using selectedIndex for rid was giving
                        //foreign key constraint after deleting some users
                        //so obtain the id of user selected from the dropdown
                        //so username cannot be duplicated
                    }


                }

                btnUpdAssign.Enabled = false;
                chkQueue.Enabled = false;
            }

        }
        protected void btnNewCat_Click(object sender, EventArgs e)
        {
            lblQName.Visible = true;
            txtQName.Visible = true;
            txtQName.Text = string.Empty;
            txtQName.Enabled = true;
            btnAddQueue.Visible = true;
            btnAddQueue.Enabled = true;
            lblMsgQAdded.Visible = true;
            btnTryAgain.Visible = false;
            drpCatCriteria.Visible = false;
            txtCatName.Visible = false;
            btnFindCatQueue.Visible = false;
        }

        protected void btnAddQueue_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //write code to blank out all user fields - rqd if 2 users added consecutively
                SqlConnection myConn = new SqlConnection();
                myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                SqlCommand myCom = new SqlCommand();
                try
                {
                    myCom.CommandType = CommandType.StoredProcedure;
                    myCom.CommandText = "[dbo].[AddQueue]";
                    myCom.Parameters.AddWithValue("@Name", txtQName.Text);
                    myCom.Connection = myConn;
                    myConn.Open();

                    myCom.ExecuteNonQuery();
                    lblMsgQAdded.Text = "has been added";
                }

                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 2601:
                            lblMsgQAdded.Text = "Duplicate Queue names are not allowed, Please enter a unique queue name";
                            btnTryAgain.Visible = true;
                            break;
                        default:
                            Class1.WriteLog(ex.ToString());
                            break;
                    }

                }
                myConn.Close();


                txtQName.Enabled = false;
                btnAddQueue.Enabled = false;

            }
        }

       

        protected void btnTimeIssue_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = true;
            drpCriteria.Visible = false; 
            Session["criteria"] = 2;
            btnSearchIssue.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";

            //myConn.Open();
            string strqry = "Select * from users where roles=2";
            SqlCommand myCom = new SqlCommand(strqry, myConn);

            using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
            }
            Calendar1.Visible = false;
            drpCriteria.Items.Clear();
            drpCriteria.Visible = true;
            drpCriteria.DataSource = table;
            drpCriteria.DataTextField = "Name";
            drpCriteria.DataValueField = "id";
            drpCriteria.DataBind();
            Session["criteria"] = 1;
            drpCriteria.SelectedIndex = 0;
            Session["criteriaid"] = drpCriteria.SelectedIndex;
            btnSearchIssue.Visible = true;
        }

        protected void btnPriority_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = false;
            drpCriteria.Items.Clear();
            drpCriteria.Visible = true;
            drpCriteria.Items.Add("Select Priority");
            drpCriteria.Items.Add("Low");
            drpCriteria.Items.Add("Medium");
            drpCriteria.Items.Add("High");
            drpCriteria.Items.Add("Critical");
            Session["criteria"] = 3;
            btnSearchIssue.Visible = true;
        }

        protected void btnCatg_Click(object sender, EventArgs e)
        {
            drpCriteria.Visible = true;
            drpCriteria.Items.Clear();
            Calendar1.Visible = false;
            DataTable table = new DataTable();

            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";

            //myConn.Open();
            string strqry = "Select * from queue";
            SqlCommand myCom = new SqlCommand(strqry, myConn);

            using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
            }
            Calendar1.Visible = false;
            drpCriteria.Items.Clear();
            drpCriteria.Visible = true;
            drpCriteria.DataSource = table;
            drpCriteria.DataTextField = "Name";
            drpCriteria.DataValueField = "id";
            drpCriteria.DataBind();
            Session["criteria"] = 4;
            btnSearchIssue.Visible = true;
        }

        protected void btnSearchIssue_Click(object sender, EventArgs e)
        {   
           Response.Redirect("displayIssues.aspx");
        }

        protected void drpCriteria_SelectedIndexChanged(object sender, EventArgs e)
        { 

            //HAD A LOT OF ISSUES FIGURING THIS OUT, THE SESSION VARIABLE HAS TO  BE STORED AT THIS PLACE
            Session["Datelogged"] = DateTime.Now;
            Session["criteriaid"] = drpCriteria.SelectedIndex;
            Session["criteriaValue"] = drpCriteria.SelectedItem.Text;
            
            //First you will need to populate and databing the list only in a if( !IsPostBack ) { ... statement.

//Next - Session["ddlState"] is never being populated, not sure where you want 
            //to populate this but you could do it in the selected index changed as you stated. this should work if you are not calling DataBind() on the postback.
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dt = Calendar1.SelectedDate;
            Session["DateLogged"] = dt;
            Session["criteriaid"] = -1;
        }

        protected void drpCatCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpCatCriteria.SelectedIndex == 1) txtCatName.Visible =true;
        }

       
        protected void Go_Click(object sender, EventArgs e)
        {
            Session["drpIndex"] = drpListUser.SelectedIndex;
            Session["nameCriteria"] = txtUserName.Text;
            Response.Redirect("FindUsers.aspx");
        }

        protected void btnFindCatQueue_Click1(object sender, EventArgs e)
        {
            Session["drpIndex"] = drpCatCriteria.SelectedIndex;
            Session["nameCriteria"] = txtCatName.Text;
            Response.Redirect("findCategory.aspx");
        }

        protected void btnFindCat_Click(object sender, EventArgs e)
        {
            lblQName.Visible = false;
            txtQName.Visible = false;
            btnAddQueue.Visible = false;
            lblMsgQAdded.Visible = false;
            btnTryAgain.Visible = false;
            drpCatCriteria.Visible = true;
            txtCatName.Visible = true;
            btnFindCatQueue.Visible = true;
            txtCatName.Visible = true;
            
        }

        protected void btnTryAgain_Click(object sender, EventArgs e)
        {
            txtQName.Focus();
            txtQName.Enabled = true;
            btnTryAgain.Visible = false;
            btnAddQueue.Enabled = true;
        }


        protected void drpListUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEditEmail_Click(object sender, EventArgs e)
        {
            txtEmail.Enabled = true;
            btnSubmit.Enabled = true;
        }

        protected void btnEditPwd_Click(object sender, EventArgs e)
        {
            txtPassword.Enabled = true;
            lblConfirmPwd.Visible = true;
            txtConfPwd.Visible = true;
            btnSubmit.Enabled = true;
        }

        protected void btnFindUser_Click(object sender, EventArgs e)
        {
            pnlCreateUser.Visible = false;
            pnlFindUser.Visible = true;
            drpListUser.SelectedIndex = 0;
        }

        protected void txtCatName_TextChanged(object sender, EventArgs e)
        {
            btnAddQueue.Enabled = true;
        }

        protected void txtQName_TextChanged(object sender, EventArgs e)
        {
            btnAddQueue.Enabled = true;
        }

        protected void rdUserType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}