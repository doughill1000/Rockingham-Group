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
/// Summary description for Vehicle
/// </summary>
public class Vehicle : Quote
{
    public Guid vehicleID { get; set; }
    public String vehicleType { get; set; }
    public String vinNumber { get; set; }
    public int year { get; set; }
    public String make { get; set; }
    public String model { get; set; }
    public String bodyType { get; set; }
    public int? engineCyl { get; set; }
    public String awayAtSchool { get; set; }
    public String inStateZipCode { get; set; }
    public String outStateCollegeName { get; set; }
    public String outStateCollegePop { get; set; }
    public Boolean garaged { get; set; }
    public String noGarageZipCode { get; set; }
    public String antiTheft { get; set; }
    public String antiLockBrake { get; set; }
    public String airbags { get; set; }
    public String vehicleUsage { get; set; }
    public String otherThanCollisionDeductible { get; set; }
    public String collisionDeductible { get; set; }
    public String rentalReimburse { get; set; }
    public String rentalReimbursementLimit { get; set; }
    public static List<Vehicle>vehicles = new List<Vehicle>();
    public static int vehicleCount;

    public Vehicle()
    {
        vehicleCount++;
    }

    public void setVehicleInfo()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("SetVehicleInfo");
        cmd.Parameters.AddWithValue("@quoteID", this.quoteID);
        cmd.Parameters.AddWithValue("@vehicleID", this.vehicleID);

        cmd.Parameters.AddWithValue("@vehicleType", this.vehicleType);
        cmd.Parameters.AddWithValue("@vin#", this.vinNumber);
        cmd.Parameters.AddWithValue("@year", this.year);
        cmd.Parameters.AddWithValue("@make", this.make);
        cmd.Parameters.AddWithValue("@model", this.model);
        cmd.Parameters.AddWithValue("@bodyType", this.bodyType);
        if (this.engineCyl == -1)
        {
            cmd.Parameters.AddWithValue("@engineCyl", DBNull.Value);
        }
        else
        {
            cmd.Parameters.AddWithValue("@engineCyl", this.engineCyl);
        }
        cmd.Parameters.AddWithValue("@awayAtSchool", this.awayAtSchool);
        cmd.Parameters.AddWithValue("@inStateZipCode", this.inStateZipCode);
        cmd.Parameters.AddWithValue("@outStateCollegeName", this.outStateCollegeName);
        cmd.Parameters.AddWithValue("@outStateCollegePop", this.outStateCollegePop);
        cmd.Parameters.AddWithValue("@garaged", this.garaged);
        cmd.Parameters.AddWithValue("@noGarageZipCode", this.noGarageZipCode);
        cmd.Parameters.AddWithValue("@antiTheft", this.antiTheft);
        cmd.Parameters.AddWithValue("@antiLockBrake", this.antiLockBrake);
        cmd.Parameters.AddWithValue("@airbags", this.airbags);
        cmd.Parameters.AddWithValue("@vehicleUsage", this.vehicleUsage);
        cmd.Parameters.AddWithValue("@otherThanCollisionDeductible", this.otherThanCollisionDeductible);
        cmd.Parameters.AddWithValue("@collisionDeductible", this.collisionDeductible);
        cmd.Parameters.AddWithValue("@rentalReimburse", this.rentalReimburse);
        cmd.Parameters.AddWithValue("@rentalReimbursementLimit", this.rentalReimbursementLimit);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}