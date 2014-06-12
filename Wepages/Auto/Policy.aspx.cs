using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Wepages_Auto_Policy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //Retrieve autopolicy from session from general information page
                AutoPolicy policy = (AutoPolicy)(System.Web.HttpContext.Current.Session["autoPolicy"]);
                SqlConnection conn = Website.getSQLConnection();
                SqlCommand cmd = Website.getSQLCommand(conn);
                cmd.CommandText = "GetAutoPolicyInfo";
                SqlDataReader reader;
                conn.Open();
                cmd.Parameters.AddWithValue("@QuoteID", policy.quoteID);
                reader = cmd.ExecuteReader();
                reader.Read();
                policy.bodilyInjuryLimit = Website.getSafeString(reader, 0);
                policy.propertyDamageLimit = Website.getSafeString(reader, 1);
                policy.uninsuredBodilyInjuryLimit = Website.getSafeString(reader, 2);
                policy.uninsuredPropertyDamageLimit = Website.getSafeString(reader, 3);
                policy.medicalExpense = Website.getSafeString(reader, 4);
                try
                {
                    policy.incomeLoss = reader.GetBoolean(5);
                }
                catch (Exception ex)
                {
                    
                }
                
                ddlBodilyInjuryLimit.SelectedValue = policy.bodilyInjuryLimit;
                ddlPropertyDamageLimit.SelectedValue = policy.propertyDamageLimit;
                lblUninsuredMotoristBodyAmount.Text = policy.uninsuredBodilyInjuryLimit;
                lblUninsuredPropertyDamageAmount.Text = policy.uninsuredPropertyDamageLimit;
                ddlMedicalExpense.SelectedValue = policy.medicalExpense; 
                try
                {
                    if (policy.incomeLoss == false)
                    {
                        ddlIncomeLoss.SelectedIndex = 1;
                    }
                    else if (policy.incomeLoss == true)
                    {
                        ddlIncomeLoss.SelectedIndex = 2;
                    }
                    else
                    {
                        ddlIncomeLoss.SelectedIndex = 0;
                    }
                }
                catch { }

            }
            catch (Exception ex)
            {

            }
        }
        Page.Validate();
    }
    
    protected void SetUninsuredMotoristBodyLimit(object sender, EventArgs e)
    {
        lblUninsuredMotoristBodyAmount.Text = ddlBodilyInjuryLimit.SelectedItem.Text;
    }
    protected void SetUninsuredPropertyLimit(object sender, EventArgs e)
    {
        lblUninsuredPropertyDamageAmount.Text = ddlPropertyDamageLimit.SelectedItem.Text;
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
                Guid quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
                AutoPolicy autoPolicy = (AutoPolicy)(System.Web.HttpContext.Current.Session["autoPolicy"]);
                autoPolicy.quoteID = quoteID;
                autoPolicy.bodilyInjuryLimit = ddlBodilyInjuryLimit.SelectedItem.Text;
                autoPolicy.propertyDamageLimit = ddlPropertyDamageLimit.SelectedItem.Text;
                autoPolicy.uninsuredBodilyInjuryLimit = lblUninsuredMotoristBodyAmount.Text;
                autoPolicy.uninsuredPropertyDamageLimit = lblUninsuredPropertyDamageAmount.Text;
                autoPolicy.medicalExpense = ddlMedicalExpense.SelectedItem.Text;
                try
                {
                    autoPolicy.incomeLoss = Convert.ToBoolean(ddlIncomeLoss.SelectedItem.Value);
                }
                catch
                {
                    
                }
                autoPolicy.setAutoPolicy();
                //Check to see if any vehicles exist for a quote
                //If vehicle exists, user is directed to vehicle display page
                //If no vehicles exist, the user is directed to the vehicle information page

                    Response.Redirect("AssignmentHub.aspx");
            }
            catch (SqlException ex)
            {
                lblRequiredFieldError.Text = ex.ToString();
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("GeneralInformation.aspx");
    }
}