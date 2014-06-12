using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Agent
/// </summary>
public class Agency
{

    public string name { get; set; }
    public string streetaddress { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zip { get; set; }

    public Agency()
    {

    }

    public void setAgencyInfo()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("SetAgencyInfo");
        cmd.Parameters.AddWithValue("@name", this.name);
        cmd.Parameters.AddWithValue("@streetaddress", this.streetaddress);
        cmd.Parameters.AddWithValue("@city", this.city);
        cmd.Parameters.AddWithValue("@state", this.state);
        cmd.Parameters.AddWithValue("@zip", this.zip);
        
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}