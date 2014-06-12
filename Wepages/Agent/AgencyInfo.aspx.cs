using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Wepages_Agent_AgencyInfo : System.Web.UI.Page
{
    static Agent agent = new Agent();
    static Agency agency = new Agency();
    Guid userID;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Only happens first time page is loaded
        if (!IsPostBack)
        {

            agent = new Agent();
            //Pulls information from existing agent. Uses reference number to identify the quote being requested
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            try
            {
                cmd.CommandText = "GetAgentInfo";
                SqlDataReader reader;
                conn.Open();
                userID = new Guid(Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey.ToString());
                cmd.Parameters.AddWithValue("@UserID", userID);
                reader = cmd.ExecuteReader();
                reader.Read();

                //Put all values from database into quote class
                agent.agentID = reader.GetGuid(0);
                agent.phone = Website.getSafeString(reader, 1);
                agent.email = Website.getSafeString(reader, 2);
                agent.agency = Website.getSafeString(reader, 3);
                agent.firstName = Website.getSafeString(reader, 4);
                agent.lastName = Website.getSafeString(reader, 5);
                agent.region = Website.getSafeString(reader, 6);
                agent.manager = reader.GetGuid(7);

                conn.Close();
                conn.Open();

                //get agency info
                SqlCommand cmd2 = Website.getSQLCommand(conn);
                cmd2.CommandText = "select * from agency where name = '" + agent.agency + "'";
                SqlDataReader rdrAgency;
                cmd2.CommandType = System.Data.CommandType.Text;
                rdrAgency = cmd2.ExecuteReader();
                rdrAgency.Read();

                //agency
                agency.name = rdrAgency.GetString(0);
                agency.streetaddress = Website.getSafeString(rdrAgency,1);
                agency.city = rdrAgency.GetString(2);
                agency.state = rdrAgency.GetString(3);
                agency.zip = rdrAgency.GetString(4);
            }
            catch
            {
                //agent.agentID = Guid.NewGuid();
                agent.agentID = userID;
                agent.phone = "";
                agent.email = "";
                agent.agency = "";
                agent.firstName = "";
                agent.lastName = "";
            }

            conn.Close();

            
            //Put all attributes from class into data fields

            
            txtFirstName.Text = agent.firstName;
            txtLastName.Text = agent.lastName;
            txtAgencyName.Text = agent.agency;
            txtEmail.Text = agent.email;
            if (agency.state == "Virginia")//va
            {
                ddlRegionVA.Visible = true;
                ddlRegionVA.SelectedValue = agent.region;
                rfvRegionVA.Visible = true;
            }
            else//pa
            {
                ddlRegionPA.Visible = true;
                ddlRegionPA.SelectedValue = agent.region;
                rfvRegionPA.Visible = true;
            }
            txtPhone.Text = agent.phone;
            ddlAgencyName.DataBind();
            ddlAgencyName.SelectedValue = agent.agency;
            ddlAgencyName_SelectedIndexChanged(null, null);
            /*for(int i = 0;i<ddlAgencyName.Items.Count;i++)
            {
                if (ddlAgencyName.Items[i].Text == agent.agency)
                {
                    ddlAgencyName.SelectedIndex = i;
                    break;
                }
            }*/
            
        }
        //Validates the page
        Page.Validate();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        //Checks if page is valid
        if (!Page.IsValid)
        {
            lblRequiredFieldError.Text = "Please enter a value for all required fields";
        }
        else
        {
            try
            {
                /*if ((HttpContext.Current.User.IsInRole("agency_manager")))
                {
                    Guid tmp = agent.agentID;
                    agent.manager = tmp;
                }
                else
                    agent.manager = Guid.Empty;*/
                userID = new Guid(Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey.ToString());
                agent.agentID = userID;
                if (ddlAgencyName.SelectedIndex == 0)
                    agent.agency = txtAgencyName.Text;
                else
                    agent.agency = ddlAgencyName.SelectedItem.Value;
                agent.firstName = txtFirstName.Text;
                agent.lastName = txtLastName.Text;
                agent.phone = txtPhone.Text;
                agent.email = txtEmail.Text;
                if (ddlRegionVA.Visible == true) //va
                {
                    agent.region = ddlRegionVA.SelectedItem.Value;
                }
                else//pa
                {
                    agent.region = ddlRegionPA.SelectedItem.Value;
                }
                if(ddlManager.Items.Count != 0)
                    agent.manager = new Guid(ddlManager.SelectedItem.Value);
                if ((HttpContext.Current.User.IsInRole("agency_manager")))
                {
                    Guid tmp = agent.agentID;
                    agent.manager = tmp;
                }
                if (pnlNewAgency.Visible == true)//make new agency
                {
                    Agency agency = new Agency();
                    agency.name = txtAgencyName.Text;
                    agency.streetaddress = txtStreetAddress.Text;
                    agency.state = ddlState.SelectedItem.Text;
                    agency.zip = txtZipCode.Text;
                    agency.city = txtCity.Text;
                    agency.setAgencyInfo();
                }
                agent.setAgentInfo();
                Session["agent"] = agent;
                Session["UserID"] = agent.agentID;

                if ((HttpContext.Current.User.IsInRole("agency_manager")))
                {

                    //test to see if there is another manager.  If the this manager is THE ONLY manager, the system will assign his agentID to their manager columns
                    SqlConnection conn = Website.getSQLConnection();
                    SqlCommand cmd = Website.getSQLCommand(conn);
                    cmd.CommandText = "SELECT * FROM Agent where agency = '" + agent.agency + "' and agentid = manager";
                    cmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader reader;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    int counter = 0;
                    while (reader.Read())
                    {
                        counter++;
                    }
                    conn.Close();
                    if (counter == 1)//if there is not another manager, system assigns agent's manager column equal to manager's agentID
                    {
                        conn.Open();
                        cmd.CommandText = "UPDATE agent SET manager = '" + agent.agentID + "' Where agency = '" + agent.agency + "'";
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                
            }
            catch (SqlException ex)
            {
                lblRequiredFieldError.Text = ex.ToString();
            }
        }
        btnSave.Text = "Saved";
        btnSave.Enabled = false;
        btnAddAgency.Enabled = false;
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex == 1) //va
        {
            ddlRegionPA.Visible = false;
            ddlRegionVA.Visible = true;
            rfvRegionVA.Visible = true;
            rfvRegionPA.Visible = false;
        }
        else if (ddlState.SelectedIndex == 2)//pa
        {
            ddlRegionPA.Visible = true;
            ddlRegionVA.Visible = false;
            rfvRegionVA.Visible = false;
            rfvRegionPA.Visible = true;
        }
        else
        {
            ddlRegionPA.Visible = false;
            ddlRegionVA.Visible = false;
            rfvRegionVA.Visible = false;
            rfvRegionPA.Visible = false;
        }

    }
    protected void ddlAgencyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);

        cmd.CommandText = "SELECT Agentid, Lastname + ', ' + firstName from agent where AgentiD = Manager AND agency = '"
            + ddlAgencyName.SelectedItem.Text + "'";
        cmd.CommandType = System.Data.CommandType.Text;
        SqlDataReader manReader;
        conn.Open();
        manReader = cmd.ExecuteReader();
        ddlManager.Items.Clear();
        while(manReader.Read())
        {
            Guid id = (Guid)manReader.GetGuid(0);
            string info = manReader.GetString(1);
            ListItem item = new ListItem();
            item.Text = info;
            item.Value = id.ToString();
            ddlManager.Items.Add(item);
            
        }
        conn.Close();
        ddlManager.DataBind();

        try
        {
            cmd.CommandText = "SELECT [State] from Agency Where [Name] = '" + ddlAgencyName.SelectedItem.Text + "'";
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string state = reader.GetString(0);
                agency.state = state;
                if (state == "Virginia")//show va drop down
                {
                    ddlRegionPA.Visible = false;
                    ddlRegionVA.Visible = true;
                    rfvRegionVA.Visible = true;
                    rfvRegionPA.Visible = false;
                }
                else//show pa
                {
                    ddlRegionPA.Visible = true;
                    ddlRegionVA.Visible = false;
                    rfvRegionVA.Visible = false;
                    rfvRegionPA.Visible = true;
                }
            }
        }
        catch { throw; };

        /*if (pnlNewAgency.Visible == false)
            pnlNewAgency.Visible = true;*/



    }
    protected void btnAddAgency_Click(object sender, EventArgs e)
    {
        if (pnlNewAgency.Visible)
        {
            pnlNewAgency.Visible = false;
            btnAddAgency.Text = "Add Agency";
            rfvAgencyName1.Visible = true;
        }
        else
        {
            pnlNewAgency.Visible = true;
            btnAddAgency.Text = "Remove Agency";
            rfvAgencyName1.Visible = false;
            ddlAgencyName.SelectedIndex = 0;
            ddlManager.Items.Clear();
        }
        Page.Validate();
    }
    protected void ddlAgencyName_Databound(Object sender, EventArgs e)
    {
        ddlAgencyName.Items.Insert(0, "Select One");
        ddlAgencyName.Items[0].Value = "";
        ddlAgencyName.SelectedIndex = 0;
    }
}