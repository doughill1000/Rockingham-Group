using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Agent
/// </summary>
public class Agent
{

    public Guid agentID { get; set; }
    public string agency { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public string region { get; set; }
    public Guid manager { get; set; }

	public Agent()
	{
		
	}

    public void setAgentInfo()
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        conn.Open();
        cmd.CommandText = ("SetAgentInfo");
        cmd.Parameters.AddWithValue("@agentId", this.agentID);
        cmd.Parameters.AddWithValue("@agency", this.agency);
        cmd.Parameters.AddWithValue("@firstName", this.firstName);
        cmd.Parameters.AddWithValue("@lastName", this.lastName);
        cmd.Parameters.AddWithValue("@phone", this.phone);
        cmd.Parameters.AddWithValue("@email", this.email);
        cmd.Parameters.AddWithValue("@region", this.region);
        cmd.Parameters.AddWithValue("@manager", this.manager);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}