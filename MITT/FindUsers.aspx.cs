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
    public partial class FindUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        
        {
             if (!Page.IsPostBack)
            {

                bindgrid(); //for users grid to display latest
                lblErrMsg.Visible = false;
            }
        }

        public void bindgrid()
        {
            DataTable table = new DataTable();
            int index = int.Parse(Session["drpIndex"].ToString());
            string name = Session["nameCriteria"].ToString();
            try
            {
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            //myConn.Open();
                string strqry = "Select id, name, email,companyName, phoneNo, (CASE WHEN Roles=0 Then 'Administrator' WHEN Roles=1 then 'Support Rep' ELSE 'General User' END) as Roles from users where name like @name and id > 0";
            SqlCommand myCom = new SqlCommand(strqry, myConn);
            if (index == 0) //list all
            {
                myCom.Parameters.AddWithValue("@name", '%');
            }
            else if (index == 1) //name contains
            {
                myCom.Parameters.AddWithValue("@name",'%'+ name + '%');
                //strqry = "Select * from ContactList where Name like @name";
            }
            using (SqlDataAdapter ad = new SqlDataAdapter(myCom))
            {
                // fire Fill method to fetch the data and fill into DataTable 
                table.Clear();
                ad.Fill(table);
            }

            if (table.Rows.Count == 0)
            {
                lblUserCount.Text = "No users match the criteria";
            }
            else
            {
                lblUserCount.Text = (table.Rows.Count).ToString() + " users match the criteria";
                GridView1.DataSource = table;
                GridView1.DataBind();
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
        }
        

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            
            SqlCommand myCom = new SqlCommand();
            myCom.CommandType = CommandType.StoredProcedure;
            myCom.CommandText = "[dbo].[DeleteUser]";
            myCom.Parameters.AddWithValue("@ID",ID);
            myCom.Connection = myConn;
            myConn.Open();            
            //before deleting user replace requester id by deleted user
            //remove queue assignments if it is a support rep
           
            myCom.ExecuteNonQuery();
            myConn.Close();
            GridView1.EditIndex = -1;
            bindgrid();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex; //takes appropriate row number from the grid to update mode
            bindgrid();
        }

        protected void GridView1_CancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            bindgrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            //if name or email is blank - give java script message box
            string name = e.NewValues[0].ToString();
            string email = e.NewValues[1].ToString();

            string companyname = (e.NewValues[2] == null) ? " " : e.NewValues[2].ToString();
            
           
            //string companyname = e.NewValues[2].ToString();
            //int Roles = (e.NewValues[3]==null)? 0: Convert.ToInt32(e.NewValues[3].ToString());
            //string password = (e.NewValues[4] == null) ? " " : e.NewValues[4].ToString();
            //string password = e.NewValues[4].ToString();
            //string phoneNo = "**";
            string phoneNo = (e.NewValues[3] == null) ? " " : e.NewValues[3].ToString();
            //string phoneNo = e.NewValues[5].ToString();
            //int isActive = (e.NewValues[3] == null) ? 0 : Convert.ToInt32(e.NewValues[3].ToString());
            //int isActive = Convert.ToInt32(e.NewValues[6].ToString());
            
               SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
            DropDownList ddl1 = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlRoles");
            int Roles = ddl1.SelectedIndex;
            string Nstrqry = "update users set Name = @name, email=@email, companyname=@companyname, phoneNo=@phoneNo,Roles =@roles where ID = @ID";
            SqlCommand myCom = new SqlCommand(Nstrqry, myConn);

            myCom.Parameters.AddWithValue("@ID", ID);
            myCom.Parameters.AddWithValue("@name", name);
            myCom.Parameters.AddWithValue("@email", email);
            myCom.Parameters.AddWithValue("@companyname", companyname);
            myCom.Parameters.AddWithValue("@roles", Roles);
           // myCom.Parameters.AddWithValue("@password", password);
            myCom.Parameters.AddWithValue("@phoneNo", phoneNo);
            
            myConn.Open();
            myCom.ExecuteNonQuery();
            myConn.Close();

            GridView1.EditIndex = -1;
            bindgrid();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {   /*
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlRoles = (e.Row.FindControl("ddlRoles") as DropDownList);
               

                //Add Default Item in the DropDownList
                //ddlRoles.Items.Insert(0, new ListItem("Please select"));

                //Select the Country of Customer in DropDownList
                string Role1 = (e.Row.FindControl("lblCountry") as Label).Text;
                ddlRoles.Items.FindByValue(Role1).Selected = true;
            } */
        }
    }
}