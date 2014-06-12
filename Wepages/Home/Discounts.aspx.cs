using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Wepages_Home_Discounts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Home home = (Home)(System.Web.HttpContext.Current.Session["home"]);
                SqlConnection conn = Website.getSQLConnection();
                SqlCommand cmd = Website.getSQLCommand(conn);
                cmd.CommandText = "GetDiscountInfo";
                SqlDataReader reader;
                conn.Open();
                cmd.Parameters.AddWithValue("@QuoteID", home.quoteID);
                reader = cmd.ExecuteReader();
                reader.Read();
                try
                {
                    home.smokeAlarms = reader.GetBoolean(0);
                }
                catch { }
                try
                {
                    home.fireExtinguishers = reader.GetBoolean(1);
                }
                catch { }
                try
                {
                    home.deadBolts = reader.GetBoolean(2);
                }
                catch { }
                try
                {
                    home.fireAlarmMonitoringCenter = reader.GetBoolean(3);
                }
                catch { }
                try
                {
                    home.burglarAlarm = reader.GetBoolean(4);
                }
                catch { }
                try
                {
                    home.sprinklerSystem = reader.GetBoolean(5);
                }
                catch { }
                try
                {
                    home.autoPolicyRockinghamAuto = Website.getSafeString(reader, 6);
                }
                catch { }

                //Check any radio buttons that are marked to true
                if (home.smokeAlarms)
                {
                    rbSmokeAlarmsYes.Checked = true;
                }
                else
                {
                    rbSmokeAlarmsNo.Checked = true;
                }
                if (home.fireExtinguishers)
                {
                    rbFireExtinguisherYes.Checked = true;
                }
                else
                {
                    rbFireExtinguisherNo.Checked = true;
                }
                if (home.deadBolts)
                {
                    rbDeadBoltsYes.Checked = true;
                }
                else
                {
                    rbDeadBoltsNo.Checked = true;
                }
                if (home.fireAlarmMonitoringCenter)
                {
                    rbFireAlarmReportYes.Checked = true;
                }
                else
                {
                    rbFireAlarmReportNo.Checked = true;
                }
                if (home.burglarAlarm)
                {
                    rbBuglarReportYes.Checked = true;
                }
                else
                {
                    rbBurglarReportNo.Checked = true;
                }
                if (home.sprinklerSystem)
                {
                    rbSprinklersYes.Checked = true;
                }
                else
                {
                    rbSprinklersNo.Checked = true;
                }
                ddlAutoPolicy.SelectedValue = home.autoPolicyRockinghamAuto;
            }
            catch { }
            
        }
        Page.Validate();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Coverage.aspx");
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                Home home = new Home();
                home = (Home)(System.Web.HttpContext.Current.Session["home"]);
                home.smokeAlarms = Discounts(rbSmokeAlarmsYes);
                home.fireExtinguishers = Discounts(rbFireExtinguisherYes);
                home.deadBolts = Discounts(rbDeadBoltsYes);
                home.fireAlarmMonitoringCenter = Discounts(rbFireAlarmReportYes);
                home.burglarAlarm = Discounts(rbBuglarReportYes);
                home.sprinklerSystem = Discounts(rbSprinklersYes);
                home.autoPolicyRockinghamAuto = ddlAutoPolicy.SelectedItem.Text;
                home.setDiscountInfo();
                Response.Redirect("~/Wepages/Quote/QuoteResult.aspx");
            }
            catch (SqlException ex)
            {
                Response.Write(ex.ToString());
            }
        }
        else
        {
            lblRequiredFieldError.Text = "Please enter a value for all required fields";
        }
    }
    protected static bool Discounts(RadioButton rb)
    {
        if (rb.Checked)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}