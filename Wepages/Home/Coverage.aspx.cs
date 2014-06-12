using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Wepages_Home_Coverage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        int coverageA = Convert.ToInt32((String)(System.Web.HttpContext.Current.Session["DesiredInsurance"]));
        lblCoverageAAmount.Text = "$" + coverageA;
        lblCoverageBAmount.Text = "$" + (int)(coverageA * .1);
        lblCoverageCAmount.Text = "$" + (int)(coverageA * .7);
        lblCoverageDAmount.Text = "$" + (int)(coverageA * .2);

        if (!IsPostBack)
        {
            try
            {
                Home home = (Home)(System.Web.HttpContext.Current.Session["home"]);
                SqlConnection conn = Website.getSQLConnection();
                SqlCommand cmd = Website.getSQLCommand(conn);
                cmd.CommandText = "GetCoverageInfo";
                SqlDataReader reader;
                conn.Open();
                cmd.Parameters.AddWithValue("@QuoteID", home.quoteID);
                reader = cmd.ExecuteReader();
                reader.Read();
                try
                {
                    home.coverageA = reader.GetInt32(0);
                } catch { }
                try
                {
                    home.coverageB = reader.GetInt32(1);
                } catch { }
                try
                {
                    home.coverageC = reader.GetInt32(2);
                } catch { }
                try
                {
                    home.coverageD = reader.GetInt32(3);
                } catch { }
                try
                {
                    home.coverageL = reader.GetInt32(4);
                } catch { }
                try
                {
                    home.coverageM = reader.GetInt32(5);
                } catch { }
                try
                {
                    home.policyDeductible = reader.GetInt32(6);
                } catch { }

                /*lblCoverageAAmount.Text = "$" + Website.checkForInts(home.coverageA);
                lblCoverageBAmount.Text = "$" + Website.checkForInts(home.coverageB);
                lblCoverageCAmount.Text = "$" + Website.checkForInts(home.coverageC);
                lblCoverageDAmount.Text = "$" + Website.checkForInts(home.coverageD);*/
                ddlCoverageL.SelectedValue = Website.checkForInts(home.coverageL);
                ddlCoverageM.SelectedValue = Website.checkForInts(home.coverageM);
                ddlPolicyDeductible.SelectedValue = Website.checkForInts(home.policyDeductible);
            }
            catch { }
            
        }
        Page.Validate();
        
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            Home home = new Home();
            home = (Home)(System.Web.HttpContext.Current.Session["home"]);
            int coverageA = Convert.ToInt32((String)(System.Web.HttpContext.Current.Session["DesiredInsurance"]));
            home.coverageA = coverageA;
            home.coverageB = (int)(coverageA * .1);
            home.coverageC = (int)(coverageA * .7);
            home.coverageD = (int)(coverageA * .2);
            home.coverageL = Convert.ToInt32(ddlCoverageL.SelectedValue);
            home.coverageM = Convert.ToInt32(ddlCoverageM.SelectedValue);
            home.policyDeductible = Convert.ToInt32(ddlPolicyDeductible.SelectedValue);
            home.setCoverageInfo();
            Session["home"] = home;
            Response.Redirect("Discounts.aspx");
        }
        else
        {
            lblRequiredFieldError.Text = "Please enter a value for all required fields";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Property.aspx");
    }
}