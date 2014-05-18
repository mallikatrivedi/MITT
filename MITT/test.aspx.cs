using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MITT
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = CheckBoxList1.ID;
            Label3.Text = CheckBoxList1.DataTextField;
            //Label2.Text = CheckBoxList1.SelectedValue
            foreach (ListItem li in CheckBoxList1.Items)
            {
                li.Selected = false;
            }
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}