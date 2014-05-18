using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MITT
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string SuserType = (Session["Roles"].ToString());
            int UserType = Convert.ToInt32(SuserType);
            if (UserType == 0)
            Response.Redirect("AdminConsole.aspx");
            else
                if (UserType ==1)

                    Response.Redirect("SRConsole.aspx");
                else

                    Response.Redirect("UserConsole.aspx");
        }

        protected void btnLogOut_Click1(object sender, EventArgs e)
        {

            //go to login page 
            Response.Redirect("Login.aspx");
            Session.RemoveAll();//to clear all session variables
            //or  if(Session["strlcaNo"]!=null) Session.Remove("strIcaNo"); for individuals at the time oflogin

        }

        protected void btnHome_Click(object sender, ImageClickEventArgs e)
        {
            
            string Roles = (Session["Roles"].ToString());
            int iRoles = Convert.ToInt32(Roles);
            if (iRoles == 0)
                Response.Redirect("AdminConsole.aspx");
             else if (iRoles == 1)
                Response.Redirect("SRConsole.aspx");
            else Response.Redirect("UserConsole.aspx");
        }
    }
}