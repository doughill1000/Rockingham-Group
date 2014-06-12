using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Wepages_Auto_AssignmentInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Validate();
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (!Page.IsValid)
        {

        }
        else
        {
            /*Quote quote = new Quote();
            quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
            Session["quote"] = quote.quoteID;*/
            Response.Redirect("AssignmentDisplay.aspx");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DriverDisplay.aspx");
    }

}