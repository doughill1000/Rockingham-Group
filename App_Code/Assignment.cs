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
/// Summary description for Assignment
/// </summary>
/// 


public class Assignment
{
    public Guid driverID { get; set; }
    public Guid vehicleID { get; set; }
    public Guid quoteID { get; set; }
    public bool primary { get; set; }
    public static List<Assignment> assignments = new List<Assignment>();
    public static int assignmentCount;

	public Assignment()
	{
        assignmentCount++;
	}

    public void setAssignment()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = "SetAssignment";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@driverID", this.driverID);
        cmd.Parameters.AddWithValue("@vehicleID", this.vehicleID);
        cmd.Parameters.AddWithValue("@quoteID", this.quoteID);
        cmd.Parameters.AddWithValue("@primary", this.primary);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    /*public void getAssignments()
    {

    }*/
}