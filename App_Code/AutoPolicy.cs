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

/// <summary>
/// Summary description for AutoPolicy
/// </summary>
public class AutoPolicy : Quote
{
    public DateTime startDate { get; set; }
    public String scheduledPayment { get; set; }
    public bool? accidentForgiveness { get; set; }
    public bool? ageUnderSix { get; set; }
    public DateTime? childBirthDate { get; set; }
    public String homePolicy { get; set; }
    public String yearsCurrentPolicy { get; set; }
    public String bodilyInjuryLimit { get; set; }
    public String propertyDamageLimit { get; set; }
    public String uninsuredBodilyInjuryLimit { get; set; }
    public String uninsuredPropertyDamageLimit { get; set; }
    public String medicalExpense { get; set; }
    public bool? incomeLoss { get; set; }

	public AutoPolicy()
	{
        
	}
    public void setAutoGeneralInfo()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("SetAutoGeneralInfo");
        cmd.Parameters.AddWithValue("@startDate", this.startDate);
        cmd.Parameters.AddWithValue("@scheduledPayment", this.scheduledPayment);
        cmd.Parameters.AddWithValue("@accidentForgiveness", this.accidentForgiveness);
        cmd.Parameters.AddWithValue("@ageUnderSix", this.ageUnderSix);
        if (this.ageUnderSix == false)
        {
            cmd.Parameters.AddWithValue("@childBirthDate", DBNull.Value);
        }
        else
        {
            cmd.Parameters.AddWithValue("@childBirthDate", this.childBirthDate);
        }
        cmd.Parameters.AddWithValue("@yearsCurrentPolicy", this.yearsCurrentPolicy);
        cmd.Parameters.AddWithValue("@homePolicy", this.homePolicy);
        cmd.Parameters.AddWithValue("@reference", this.reference);
        cmd.Parameters.AddWithValue("@QuoteID", this.quoteID);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    public void setAutoPolicy()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("SetAutoPolicyInfo");
        cmd.Parameters.AddWithValue("@bodilyInjuryLimit", this.bodilyInjuryLimit);
        cmd.Parameters.AddWithValue("@propertyDamageLimit", this.propertyDamageLimit);
        cmd.Parameters.AddWithValue("@uninsuredBodilyInjuryLimit", this.uninsuredBodilyInjuryLimit);
        cmd.Parameters.AddWithValue("@uninsuredPropertyDamageLimit", this.uninsuredPropertyDamageLimit);
        cmd.Parameters.AddWithValue("@medicalExpense", this.medicalExpense);
        cmd.Parameters.AddWithValue("@incomeLoss", this.incomeLoss);

        cmd.Parameters.AddWithValue("@QuoteID", this.quoteID);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}