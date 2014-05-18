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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=HD285\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            SqlCommand myCom = new SqlCommand();
            SqlDataReader reader;
            DataTable table = new DataTable();
            try
            {
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.CommandText = "[dbo].[GetIssues]";
               
                myCom.Connection = myConn;
                myConn.Open();
                myCom.ExecuteNonQuery();
                using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
            }
            GridView1.DataSource = table;
            GridView1.DataBind();
            myConn.Close();
        }

            catch (SqlException ex)

                {
                    Label1.Text = ex.ToString();
            }
               
            }
        
    }
}