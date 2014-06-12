using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;

public partial class Wepages_Auto_GeneralInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cvCorrectStartDate.ValueToCompare = DateTime.Now.ToShortDateString();
        cvChildBirthdate.ValueToCompare = DateTime.Now.ToShortDateString();
        if (!IsPostBack)
        {
            try
            {
                Guid quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
                AutoPolicy policy = new AutoPolicy();
                policy.quoteID = quoteID;
                SqlConnection conn = Website.getSQLConnection();
                SqlCommand cmd = Website.getSQLCommand(conn);
                cmd.CommandText = "GetAutoGeneralInfo";
                SqlDataReader reader;
                conn.Open();
                cmd.Parameters.AddWithValue("@QuoteID", policy.quoteID);
                reader = cmd.ExecuteReader();

                //Fill classes
                reader.Read();
                try
                {
                    policy.startDate = reader.GetDateTime(0); 
                }
                catch (Exception ex)
                {
                    policy.startDate = DateTime.MinValue;
                }
                policy.scheduledPayment = Website.getSafeString(reader, 1);
                try
                {
                    policy.accidentForgiveness = reader.GetBoolean(2);
                }
                catch (Exception ex) { policy.accidentForgiveness = null; }
                try
                {
                    policy.ageUnderSix = reader.GetBoolean(3);
                }
                catch (Exception ex) { policy.ageUnderSix = null; }
                try
                {
                    policy.childBirthDate = reader.GetDateTime(4);
                }
                catch (Exception ex)
                {
                    policy.childBirthDate = DateTime.MinValue;
                } 
                policy.homePolicy = Website.getSafeString(reader, 5);
                policy.yearsCurrentPolicy = Website.getSafeString(reader, 6);

                //Load datafields
                if (policy.startDate == DateTime.MinValue)
                {
                    txtStartDate.Text = "";
                }
                else
                {
                    txtStartDate.Text = policy.startDate.Date.ToString("d");
                }
                ddlScheduledPayments.SelectedValue = policy.scheduledPayment;
                if (policy.accidentForgiveness == null)
                {
                    ddlAccidentForgiveness.SelectedIndex = 0;
                }
                else if (policy.accidentForgiveness == false)
                {
                    ddlAccidentForgiveness.SelectedIndex = 1;
                }
                else
                {
                    ddlAccidentForgiveness.SelectedIndex = 2;
                }
                if (policy.ageUnderSix == null)
                {
                    ddlAgeUnder6.SelectedIndex = 0;
                }
                else if (policy.ageUnderSix == false)
                {
                    ddlAgeUnder6.SelectedIndex = 1;
                }
                else
                {
                    ddlAgeUnder6.SelectedIndex = 2;
                }
                if (policy.childBirthDate == DateTime.MinValue)
                {
                    txtChildBirthDate.Text = "";
                }
                else
                {
                    lblChildBirthDate.Visible = true;
                    txtChildBirthDate.Visible = true;
                    rfvChildBirthDate.Visible = true;
                    cvChildBirthdate.Visible = true;
                    cuvChildBirthDate.Visible = true;
                    DateTime childBirthDate = (DateTime)policy.childBirthDate;
                    txtChildBirthDate.Text = childBirthDate.Date.ToString("d");
                }
                ddlHomePolicy.SelectedValue = policy.homePolicy;
                ddlYearsCurrentPolicy.SelectedValue = policy.yearsCurrentPolicy;
                Quote quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
                
                try
                {
                    ddlHearAboutUs.SelectedValue = ddlHearAboutUs.Items.FindByValue(quote.reference).Value;
                }
                catch (Exception ex)
                {
                    if (quote.reference == null)
                    {
                        ddlHearAboutUs.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlHearAboutUs.SelectedIndex = 14;
                        txtDescribeHearOfUs.Text = quote.reference;
                        txtDescribeHearOfUs.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            Page.Validate();
        }
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
                AutoPolicy autoPolicy = new AutoPolicy();
                autoPolicy.quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
                autoPolicy.startDate = Convert.ToDateTime(txtStartDate.Text);
                autoPolicy.scheduledPayment = ddlScheduledPayments.SelectedItem.Text;
                autoPolicy.accidentForgiveness = Convert.ToBoolean(ddlAccidentForgiveness.SelectedValue);
                autoPolicy.ageUnderSix = Convert.ToBoolean(ddlAgeUnder6.SelectedValue);
                if (txtChildBirthDate.Text != "")
                {
                    autoPolicy.childBirthDate = Convert.ToDateTime(txtChildBirthDate.Text);
                }
                autoPolicy.homePolicy = ddlHomePolicy.SelectedItem.Text;
                autoPolicy.yearsCurrentPolicy = ddlYearsCurrentPolicy.SelectedItem.Text;
                Quote quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
                if (ddlHearAboutUs.SelectedValue == "Other")
                {
                    quote.reference = txtDescribeHearOfUs.Text;
                }
                else
                {
                    quote.reference = ddlHearAboutUs.SelectedItem.Text;
                }
                autoPolicy.reference = quote.reference;
                autoPolicy.setAutoGeneralInfo();
                Session["autoPolicy"] = autoPolicy;
                Session["quote"] = quote;
                Response.Redirect("Policy.aspx");
            }
            catch (SqlException ex)
            {
                lblRequiredFieldError.Text = ex.ToString();
            }

        }
    }
    protected void ddlAgeUnder6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAgeUnder6.SelectedValue == "true")
        {
            txtChildBirthDate.Visible = true;
            lblChildBirthDate.Visible = true;
            rfvChildBirthDate.Visible = true;
        }
        else
        {
            txtChildBirthDate.Visible = false;
            lblChildBirthDate.Visible = false;
            rfvChildBirthDate.Visible = false;

            txtChildBirthDate.Text = "";
        }
    }
    protected void ddlHearAboutUs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHearAboutUs.SelectedValue == "Other")
        {
            txtDescribeHearOfUs.Visible = true;
            lblDescribeHearOfUs.Visible = true;
        }
        else
        {
            txtDescribeHearOfUs.Visible = false;
            lblDescribeHearOfUs.Visible = false;
        }
    }
    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        //cvCorrectStartDate.Validate();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Wepages/Applicant.aspx");
    }
    //Checks if age is older than six
    protected void cuvChildBirthDate_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (cvChildBirthdate.IsValid)
        {
            if (Website.calculateYearsInt(args.Value) > 5)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
    protected void ddlAccidentForgiveness_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}