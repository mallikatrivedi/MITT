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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnNewMemo.Visible = true;
            txtNewMemo.Visible = false;
            btnSaveMemo.Visible = false;
            string Sissueid = Session["issueid"].ToString();
            int issueid = Convert.ToInt32(Sissueid);
            //int issueid = issuid;
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            String strqry;
            strqry = "Select * from issue where id = @issueid";
            SqlCommand myCom1 = new SqlCommand(strqry, myConn);

            myCom1.Parameters.AddWithValue("@issueid", issueid);

            using (SqlDataAdapter ad1 = new SqlDataAdapter(myCom1))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table1.Clear();
                ad1.Fill(table1);
                issueGrid.DataSource = table1;
                issueGrid.DataBind();
            }
            SqlCommand myCom = new SqlCommand();
            myCom.CommandType = CommandType.StoredProcedure;
            myCom.CommandText = "[dbo].[GetMemo]";
            myCom.Parameters.AddWithValue("@issueid", issueid);
            myCom.Connection = myConn;
            myConn.Open();

            myCom.ExecuteNonQuery();
            myConn.Close();
           

            using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
                
            }
            txtMemo.Text= "";
            for (int i = 0; i <= table.Rows.Count -1; i++)
            {
                txtMemo.Text = txtMemo.Text + table.Rows[i][1].ToString() + "  updated on " + txtMemo.Text + table.Rows[i][0].ToString() + table.Rows[i][2].ToString();
                txtMemo.Text = txtMemo.Text+"\r\n";
            }


        }


        protected void btnNewMemo_Click(object sender, EventArgs e)
        {
            txtNewMemo.Visible =true;
            txtNewMemo.Focus();
            btnSaveMemo.Visible = true;
            txtNewMemo.Text = "";
            btnSaveMemo.Enabled = true;
        }

        protected void btnSaveMemo_Click(object sender, EventArgs e)
        {   String Sissueid = Session["issueid"].ToString();
            int issueid = Convert.ToInt32(Sissueid);
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
            }

            btnSaveMemo.Enabled = false;
        }

        protected void issueGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        }
    }
