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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultFocus = AppLogin.FindControl("Username").ClientID;//set focus to user name
        }

        protected void AppLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            int Type= 0, userid= 0;
               
            {
              
                if (Session["count"] == null)
                    Session["count"] = 0;
                else
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                
                if (Convert.ToInt32(Session["count"]) > 2)
                    lblLockedMsg.Text = "Your account has been locked. Please contact the administrator";
                else
                {
                    if (ValidateLogin(AppLogin.UserName, AppLogin.Password, ref Type, ref userid))
                    {
                        Session["Roles"] = Type; 
                        Session["user_id"] = userid;
                        if (Type == 0)
                            Response.Redirect("AdminConsole.aspx");
                        else if (Type == 1) Response.Redirect("SRConsole.aspx");
                        else Response.Redirect("UserConsole.aspx");
                    }
                    
                 }
                }

             }
        private bool ValidateLogin(string UserName, string Password, ref int Type, ref int userid)
       
        {
           
                SqlConnection myConn = new SqlConnection();
                myConn.ConnectionString = "Data Source=hd100\\sqlexpress;Initial Catalog=MITT;Integrated Security=True";
                SqlCommand myCom = new SqlCommand();

                try
                {
                    myCom.CommandType = CommandType.StoredProcedure;
                    myCom.CommandText = "[dbo].[Login]";
                    myCom.Parameters.AddWithValue("@Username", UserName);
                    myCom.Parameters.AddWithValue("@UPassword", Password);
                    myCom.Parameters.AddWithValue("@OutRes", 0);
                    myCom.Parameters.AddWithValue("@Type", 0);
                    myCom.Parameters.AddWithValue("@Userid", 0);
                    myCom.Parameters["@OutRes"].Direction = ParameterDirection.Output;
                    myCom.Parameters["@Type"].Direction = ParameterDirection.Output;
                    myCom.Parameters["@Userid"].Direction = ParameterDirection.Output;
                    myCom.Connection = myConn;
                    myConn.Open();
                    myCom.ExecuteNonQuery();
                    myConn.Close();

                    int results = (int)myCom.Parameters["@OutRes"].Value;
                    if (results == 0)
                        return false;
                    else
                        Type = (int)myCom.Parameters["@Type"].Value;
                        userid = (int)myCom.Parameters["@userid"].Value;
                        return true;

                }
                catch (SqlException ex)
                {
                    Class1.WriteLog(ex.ToString());
                    return false;
                }
}
        }
    }
