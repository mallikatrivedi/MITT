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
    public partial class WebForm3 : System.Web.UI.Page
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

            DateTime dt = DateTime.Now;
            DateTime dt1 = dt.Date;

            int displayCriteria = 4;
            int objectId = 1;// Convert.ToInt32(Session["criteriaid"].ToString());

            

            Label1.Text =  "--" +Convert.ToInt32(Session["criteriaid"].ToString());
            string objectValue = Session["criteriaValue"].ToString();
           // string strqry = " ";

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
                    lblIssueCount.Text = "no issues";

                else
                {
                    lblIssueCount.Text = table.Rows.Count.ToString();
                    issuesGrid.DataSource = table;

                    issuesGrid.DataBind();
                }
            }
            catch (SqlException ex)
            {
                Class1.WriteLog(ex.ToString());
            }

        }
        protected void issuesGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}