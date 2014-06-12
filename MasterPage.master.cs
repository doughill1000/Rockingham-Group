using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            txtSearchBar.Visible = true;
            btnSearch.Visible = true;
        }
        else
        {
            txtSearchBar.Visible = false;
            btnSearch.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session["UserID"] = (Guid)(Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey);
        Session["searchInput"] = txtSearchBar.Text;
        if(HttpContext.Current.User.IsInRole("manager"))
            Response.Redirect("~/Wepages/SearchResultsManager.aspx");
        else if (HttpContext.Current.User.IsInRole("agent"))
            Response.Redirect("~/Wepages/SearchResultsAgent.aspx");
        else if (HttpContext.Current.User.IsInRole("agency_manager"))
            Response.Redirect("~/Wepages/SearchQuotesAgentManager.aspx");
        else
            Response.Redirect("~/Wepages/SearchResults.aspx");

    }
    protected void imbLogo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Wepages/Home.aspx");
    }
}


