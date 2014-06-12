using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Web.Security;

public partial class Wepages_GettingStarted : System.Web.UI.Page
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
            lblRequiredFieldError.Text = "Please enter a value for all required fields";
        }
        else
        {
            try
            {
                Quote quote = new Quote();
                Session["quote"] = quote;
                quote.quoteID = Guid.NewGuid();
                quote.insuranceType = ddlTypeOfInsurance.SelectedItem.Text;
                if (HttpContext.Current.User.Identity.IsAuthenticated == true)
                {
                    quote.userID = new Guid(Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey.ToString());
                }
                else
                {
                    quote.userID = new Guid(Guid.Empty.ToString());
                }
                quote.createQuote();
                Session["quote"] = quote;

                switch (ddlContinue.SelectedValue)
                {
                    case "1":
                        Response.Redirect("Applicant.aspx");
                        break;
                }
            }
            catch (SqlException ex)
            {
                Response.Write(ex.ToString());
            }
            catch (Exception ex)
            {
                lblRequiredFieldError.Text = "Sorry, we have are unable to reach the database. Please check your connection.";
            }
            lblRequiredFieldError.Text = "";
        }
    }
    protected void ddlContinue_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (ddlContinue.SelectedValue)
        {
            case "2":
                lblUnableToContinue.Text = "Sorry, but unless you consent to this information disclosure we will be unable to accurately quote you. Please contact an agent directly.";
                break;
            default:
                lblUnableToContinue.Text = "";
                break;
        }
    }
    protected void cuvContinue_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (args.Value == "1")
            args.IsValid = true;
        else
            args.IsValid = false;
    }
}