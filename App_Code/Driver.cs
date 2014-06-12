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
/// Summary description for Driver
/// </summary>
public class Driver : Quote
{
    public Guid driverID { get; set; }
    public String firstName { get; set; }
    public String middleName { get; set; }
    public String lastName { get; set; }
    public String suffix { get; set; }
    public DateTime dateOfBirth { get; set; }
    public int age { get; set; }
    public String gender { get; set; }
    public String maritalStatus { get; set; }
    public DateTime dateFirstDriversLicense { get; set; }
    public int yearLicensed { get; set; }
    public bool violationsLast3Years { get; set; }
    public static List<Driver>drivers = new List<Driver>();
    public static int driverCount;

    public Driver()
    {
        driverCount++;
    }

    public void setDriverInfo()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("setDriverInfo");
        cmd.Parameters.AddWithValue("@driverID", this.driverID);
        cmd.Parameters.AddWithValue("@quoteID", this.quoteID);
        cmd.Parameters.AddWithValue("@firstName", this.firstName);
        cmd.Parameters.AddWithValue("@middleName", this.middleName);
        cmd.Parameters.AddWithValue("@lastName", this.lastName);
        cmd.Parameters.AddWithValue("@suffix", this.suffix);
        cmd.Parameters.AddWithValue("@dateOfBirth", this.dateOfBirth);
        cmd.Parameters.AddWithValue("@age", this.age);
        cmd.Parameters.AddWithValue("@gender", this.gender);
        cmd.Parameters.AddWithValue("@maritalStatus", this.maritalStatus);
        cmd.Parameters.AddWithValue("@dateFirstDriversLicense", this.dateFirstDriversLicense);
        cmd.Parameters.AddWithValue("@yearLicensed", this.yearLicensed);
        cmd.Parameters.AddWithValue("@violationsLast3Years", this.violationsLast3Years);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

}