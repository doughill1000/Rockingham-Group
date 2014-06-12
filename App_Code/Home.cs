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
/// Summary description for Home
/// </summary>
public class Home : Quote
{
    public Guid quoteID { get; set; }
    public int yearBuilt { get; set; }
    public String constructionType { get; set; }
    public String distanceToFireStation { get; set; }
    public Boolean fireHydrant { get; set; }
    public Boolean newlyPurchased { get; set; }
    public String currentInsurance { get; set; }
    public String currentInsuranceCompany { get; set; }
    public String desiredInsurance { get; set; }
    public String paidLosses3Years { get; set; }
    public int coverageA { get; set; }
    public int coverageB { get; set; }
    public int coverageC { get; set; }
    public int coverageD { get; set; }
    public int coverageL { get; set; }
    public int coverageM { get; set; }
    public int policyDeductible { get; set; }
    public Boolean smokeAlarms { get; set; }
    public Boolean fireExtinguishers { get; set; }
    public Boolean deadBolts { get; set; }
    public Boolean fireAlarmMonitoringCenter { get; set; }
    public Boolean burglarAlarm { get; set; }
    public Boolean sprinklerSystem { get; set; }
    public String autoPolicyRockinghamAuto { get; set; }
    
    public Home()
    {

    }

    public void setPropertyInfo()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("SetPropertyInfo");
        cmd.Parameters.AddWithValue("@YearBuilt", this.yearBuilt);
        cmd.Parameters.AddWithValue("@constructionType", this.constructionType);
        cmd.Parameters.AddWithValue("@distanceToFireStation", this.distanceToFireStation);
        cmd.Parameters.AddWithValue("@fireHydrant", this.fireHydrant);
        cmd.Parameters.AddWithValue("@newlyPurchased", this.newlyPurchased);
        cmd.Parameters.AddWithValue("@currentInsurance", this.currentInsurance);
        cmd.Parameters.AddWithValue("@currentInsuranceCompany", this.currentInsuranceCompany);
        cmd.Parameters.AddWithValue("@desiredInsurance", this.desiredInsurance);
        cmd.Parameters.AddWithValue("@paidLosses3Years", this.paidLosses3Years);
        cmd.Parameters.AddWithValue("@QuoteID", this.quoteID);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void setCoverageInfo()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("SetCoverageInfo");
        cmd.Parameters.AddWithValue("@coverageA", this.coverageA);
        cmd.Parameters.AddWithValue("@coverageB", this.coverageB);
        cmd.Parameters.AddWithValue("@coverageC", this.coverageC);
        cmd.Parameters.AddWithValue("@coverageD", this.coverageD);
        cmd.Parameters.AddWithValue("@coverageL", this.coverageL);
        cmd.Parameters.AddWithValue("@coverageM", this.coverageM);
        cmd.Parameters.AddWithValue("@policyDeductible", this.policyDeductible);
        cmd.Parameters.AddWithValue("@QuoteID", this.quoteID);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void setDiscountInfo()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("SetDiscountInfo");
        cmd.Parameters.AddWithValue("@smokeAlarms", this.smokeAlarms);
        cmd.Parameters.AddWithValue("@fireExtinguishers", this.fireExtinguishers);
        cmd.Parameters.AddWithValue("@deadBolts", this.deadBolts);
        cmd.Parameters.AddWithValue("@fireAlarmMonitoringCenter", this.fireAlarmMonitoringCenter);
        cmd.Parameters.AddWithValue("@burglarAlarm", this.burglarAlarm);
        cmd.Parameters.AddWithValue("@sprinklerSystem", this.sprinklerSystem);
        cmd.Parameters.AddWithValue("@autoPolicyRockinghamAuto", this.autoPolicyRockinghamAuto);
        
        cmd.Parameters.AddWithValue("@QuoteID", this.quoteID);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

}