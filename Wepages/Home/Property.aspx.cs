using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Wepages_Home_Property : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {    
        cvYearHomeBuilt.ValueToCompare = DateTime.Now.Year.ToString();
        if (!IsPostBack)
        {
            try
            {
                //Pull quoteID from session entered in application.aspx
                Guid quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
                Home home = new Home();
                //Set the home ID equal to the quoteID pulled
                home.quoteID = quoteID;
                SqlConnection conn = Website.getSQLConnection();
                SqlCommand cmd = Website.getSQLCommand(conn);
                cmd.CommandText = "GetPropertyInfo";
                SqlDataReader reader;
                conn.Open();
                cmd.Parameters.AddWithValue("@QuoteID", home.quoteID);
                reader = cmd.ExecuteReader();

                //Get data from dbase
                reader.Read();
                //Doesn't allow null value to be read
                try
                {
                    home.yearBuilt = reader.GetInt32(0);
                }catch(Exception ex) {
                    home.yearBuilt = -1;
                }
                home.constructionType = Website.getSafeString(reader, 1);
                home.distanceToFireStation = Website.getSafeString(reader, 2);
                try
                {
                    home.fireHydrant = reader.GetBoolean(3);
                }
                catch { }
                try
                {
                    home.newlyPurchased = reader.GetBoolean(4);
                }
                catch { }
                home.currentInsurance = Website.getSafeString(reader, 5);
                home.currentInsuranceCompany = Website.getSafeString(reader, 6);
                home.desiredInsurance = Website.getSafeString(reader, 7);
                home.paidLosses3Years = Website.getSafeString(reader, 8);

                //Fill data fields
                txtYearHomeBuilt.Text = Website.checkForInts(home.yearBuilt);
                ddlConstructionType.SelectedValue = home.constructionType;
                ddlNearestFireStation.SelectedValue = home.distanceToFireStation;
                ddlFireHydrant.SelectedIndex = Website.checkForBooleans(home.fireHydrant);
                ddlNewlyPurchased.SelectedIndex = Website.checkForBooleans(home.newlyPurchased);
                ddlCurrentInsurance.SelectedValue = home.currentInsurance;
                txtCurrentInsuranceCompany.Text = home.currentInsuranceCompany;
                txtDesiredAmount.Text = home.desiredInsurance;
                ddlPaidLosses3Years.SelectedValue = home.paidLosses3Years;
            }
            catch { }
                 
        }
        Page.Validate();
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            lblRequiredFieldError.Text = "Please enter a value for all required fields";
        }
        else
        {
            try
            {
                Home home = new Home();
                home.quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
                home.yearBuilt = Convert.ToInt32(txtYearHomeBuilt.Text);
                home.constructionType = ddlConstructionType.SelectedItem.Text;
                home.distanceToFireStation = ddlNearestFireStation.Text;
                if (ddlFireHydrant.SelectedValue != "")
                {
                    home.fireHydrant = Convert.ToBoolean(ddlFireHydrant.SelectedValue);
                }
                else
                {
                    home.fireHydrant = false;
                }
                if(ddlNewlyPurchased.SelectedValue != "")
                {
                    home.newlyPurchased = Convert.ToBoolean(ddlNewlyPurchased.SelectedValue);
                }else
                {
                    home.newlyPurchased = false;
                }
                home.currentInsurance = ddlCurrentInsurance.SelectedValue;
                home.currentInsuranceCompany = txtCurrentInsuranceCompany.Text;
                home.desiredInsurance = txtDesiredAmount.Text;
                home.paidLosses3Years = ddlPaidLosses3Years.SelectedValue;
                home.setPropertyInfo();
                Session["DesiredInsurance"] = txtDesiredAmount.Text;
                Session["home"] = home;
                Response.Redirect("Coverage.aspx");
            }
            catch (SqlException ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
    protected void ddlNearestFireStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNearestFireStation.SelectedValue == "Within 5 miles")
        {
            lblFireHydrant.Visible = true;
            ddlFireHydrant.Visible = true;
            rfvFireHydrant.Visible = true;
        }
        else
        {
            lblFireHydrant.Visible = false;
            ddlFireHydrant.Visible = false;
            rfvFireHydrant.Visible = false;
            ddlFireHydrant.SelectedValue = "";
        }
    }
    protected void ddlNewlyPurchased_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNewlyPurchased.SelectedValue == "false")
        {
            lblCurrentInsurance.Visible = true;
            ddlCurrentInsurance.Visible = true;
            rfvCurrentInsurance.Visible = true;
        }
        else
        {
            lblCurrentInsurance.Visible = false;
            ddlCurrentInsurance.Visible = false;
            rfvCurrentInsurance.Visible = false;
            ddlCurrentInsurance.SelectedItem.Value = "";
        }
    }

    protected void ddlCurrentInsurance_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrentInsurance.SelectedValue == "Yes with another insurance company")
        {
            lblCurrentInsuranceCompany.Visible = true;
            txtCurrentInsuranceCompany.Visible = true;
        }
        else
        {
            lblCurrentInsuranceCompany.Visible = false;
            txtCurrentInsuranceCompany.Visible = false;
            txtCurrentInsuranceCompany.Text = "";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Wepages/Applicant.aspx");
    }
    protected void txtDesiredAmount_TextChanged(object sender, EventArgs e)
    {
        if (!revDesiredInsuranceAmount.IsValid)
        {
            cvDesiredAmount.IsValid = true;
        }
    }
}