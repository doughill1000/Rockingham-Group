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
/// Summary description for Quote
/// </summary>
public class Quote
{
    public Guid quoteID { get; set; }
    public String insuranceType { get; set; }
    public Guid userID { get; set; }
    public String firstName { get; set; }
    public String middleName { get; set; }
    public String lastName { get; set; }
    public String suffix { get; set; }
    public DateTime dateOfBirth { get; set; }
    public String SSN { get; set; }
    public String streetAddress1 { get; set; }
    public String streetAddress2 { get; set; }
    public String aptLot { get; set; }
    public String city { get; set; }
    public String state { get; set; }
    public String zipcode { get; set; }
    public String reference { get; set; }
    public String region { get; set; }
    public String methodOfContact { get; set; }
    public String contactEmail {get; set;}
    public String contactPhone { get; set;}
    public String preferredCallTime {get;set;}
    public int creditRating {get;set;}
    public Guid agentID { get; set; }
    public String monthlyPremium { get; set; }
    public String annualPremium { get; set; }

    public Quote()
    {

    }

    public void createQuote()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("CreateQuote");
        cmd.Parameters.AddWithValue("@QuoteID", this.quoteID);
        cmd.Parameters.AddWithValue("@InsuranceType", this.insuranceType);
        cmd.Parameters.AddWithValue("@UserID", this.userID);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void setApplicantInfo()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("SetApplicantInfo");
        cmd.Parameters.AddWithValue("@firstName", this.firstName);
        cmd.Parameters.AddWithValue("@middleName", this.middleName);
        cmd.Parameters.AddWithValue("@lastName", this.lastName);
        cmd.Parameters.AddWithValue("@suffix", this.suffix);
        cmd.Parameters.AddWithValue("@dateOfBirth", this.dateOfBirth.Date);
        cmd.Parameters.AddWithValue("@SSN", this.SSN);
        cmd.Parameters.AddWithValue("@StreetAddress1", this.streetAddress1);
        cmd.Parameters.AddWithValue("@StreetAddress2", this.streetAddress2);
        cmd.Parameters.AddWithValue("@Apt#Lot#", this.aptLot);
        cmd.Parameters.AddWithValue("@Zipcode", this.zipcode);
        cmd.Parameters.AddWithValue("@City", this.city);
        cmd.Parameters.AddWithValue("@State", this.state);
        cmd.Parameters.AddWithValue("@QuoteID", this.quoteID);
        cmd.Parameters.AddWithValue("@Region", this.region);
        cmd.Parameters.AddWithValue("@CreditRating",this.creditRating);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}