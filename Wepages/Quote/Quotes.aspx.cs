using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Wepages_Quotes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ImbAdd_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Wepages/GettingStarted.aspx");
    }
    protected void imbViewQuote_Click(object sender, ImageClickEventArgs e)
    {
        Session["userID"] = Membership.GetUser().ProviderUserKey.ToString();
        Response.Redirect("ViewQuotes.aspx");   
    }
}