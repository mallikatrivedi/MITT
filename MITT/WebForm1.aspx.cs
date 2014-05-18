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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";

            //myConn.Open();

            string strqry = "Select * from memos";

            SqlCommand myCom = new SqlCommand(strqry, myConn);
           // myCom.Parameters.AddWithValue("@issuid", issuid);
            using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
            }
            GridView1.DataSource = table;
            GridView1.DataBind();
        }
    }
}