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
    public partial class findCat : System.Web.UI.Page
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

            lblQCount.Visible = false;
            DataTable table = new DataTable();

            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            myConn.Open();
            int index = int.Parse(Session["drpIndex"].ToString());
            string name = Session["nameCriteria"].ToString();
            
            string strqry;
            SqlCommand myCom = new SqlCommand();
            
            if (index == 0) //list all
            {   //to prevent sql injection
                strqry = "Select * from queue where id > 0 and name like @name";
                myCom = new SqlCommand(strqry, myConn);
                myCom.Parameters.AddWithValue("@name", '%');
            }
            else if (index == 1) //starts with
             
            
            {
                
                strqry = "Select * from queue where id > 0 and name like '%'+@name+'%'";
                myCom = new SqlCommand(strqry, myConn);
                myCom.Parameters.AddWithValue("@name",name);
            }
           
            using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
            }

            if (table.Rows.Count == 0)
            {
                lblQCount.Text = "No queues match the criteria";
                lblQCount.Visible = true;
            }
            else
            {
                GridView1.DataSource = table;
                GridView1.DataBind();
            }
            
        }
        

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            
            SqlCommand myCom = new SqlCommand();
            int catID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string cat = e.Values[1].ToString();

            myCom.CommandType = CommandType.StoredProcedure;
            myCom.CommandText = "[dbo].[DeleteCategory-Queue]";
            myCom.Parameters.AddWithValue("@CatID", catID);
            myCom.Parameters.AddWithValue("@Cat", cat);
            myCom.Connection = myConn;
            myConn.Open();            
            myCom.ExecuteNonQuery();
           
            myConn.Close();

            
            GridView1.EditIndex = -1;
            bindgrid();
            //add coded to handle Foreign key constraint..
            //cannot delete a category if there are issues or SR attached to it
            //or replace category of all such issues with Deleted category
            //and remove the entry in the repxqueue
        }
        
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex; //takes appropriate row number from the grid to update mode
            bindgrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string name = e.NewValues[0].ToString();
            int isActive = Convert.ToInt32(e.NewValues[1].ToString());
            
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";

            string Nstrqry = "update queue set Name = @name, isActive=@isActive where ID = @ID";
            SqlCommand myCom = new SqlCommand(Nstrqry, myConn);

            myCom.Parameters.AddWithValue("@ID", ID);
            myCom.Parameters.AddWithValue("@name", name);
            myCom.Parameters.AddWithValue("@isActive", isActive);
            myConn.Open();
            myCom.ExecuteNonQuery();
            myConn.Close();

            GridView1.EditIndex = -1;
            bindgrid();
        }

        protected void GridView1_cancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            bindgrid();
        }
    }
}