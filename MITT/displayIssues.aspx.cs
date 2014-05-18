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
    public partial class displayIssues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                bindgrid();


            }
        }
        public void bindgrid()
        {

            int index = Convert.ToInt32(Session["criteriaid"].ToString());
            DateTime dt = (DateTime)Session["DateLogged"];
            DateTime dt1 = dt.Date;

            int displayCriteria = Convert.ToInt32(Session["criteria"].ToString());
            int objectId = Convert.ToInt32(Session["criteriaid"].ToString());
            string objectValue = Session["criteriaValue"].ToString();
            if ((displayCriteria == 1 || displayCriteria == 3 || displayCriteria == 4) && index ==0 )
                displayCriteria = 0;
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
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
            }
                if (table.Rows.Count == 0)
                {
                    pnlHeadings.Visible = false;
                    lblIssueCount.Text = "No Issues found with this criteria";
                    }
                else
                {
                    issuesGrid.DataSource = table;
                    issuesGrid.DataBind();
                    lblIssueCount.Text = table.Rows.Count + "issues found";
                }
            myConn.Close();
            
        }

            catch (SqlException ex)

                {
                    Class1.WriteLog(ex.ToString());
            }
    }
       
    }
}