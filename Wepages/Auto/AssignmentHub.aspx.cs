using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Wepages_Auto_AssignmentHub : System.Web.UI.Page
{
    const int VMAX = 12;
    const int DMAX = 8;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        Guid quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
        Vehicle.vehicles = new List<Vehicle>();
        Vehicle.vehicleCount = 0;
        Driver.drivers = new List<Driver>();
        Driver.driverCount = 0;
        Assignment.assignments = new List<Assignment>();
        Assignment.assignmentCount = 0;

        /**************************CHECK DRIVERS***********************/
        for (int i = 0; i < gvDriver.Rows.Count; i++)
        {
            int index = i;
            HyperLink hyperlink = new HyperLink();
            hyperlink.Text = "Edit";
            hyperlink.NavigateUrl = "~/Wepages/Auto/DriverInformation.aspx/?Index=" + index;
            gvDriver.Rows[i].Cells[2].Controls.Add(hyperlink);
            
            System.Web.UI.WebControls.Button btnDelete_Driver = new System.Web.UI.WebControls.Button();
            btnDelete_Driver.ID = "btnDeleteDriver";
            btnDelete_Driver.Text = "Delete";
            btnDelete_Driver.OnClientClick = "return confirm('You are deleting this driver. This will also delete any assignment(s) associated with this driver. Click Ok to confirm.');";
            btnDelete_Driver.Click += new System.EventHandler((s, ea) => btnDelete_Driver_Clicked(s, ea, index));
            gvDriver.Rows[i].Cells[3].Controls.Add(btnDelete_Driver);
        }
        if (gvDriver.Rows.Count > 0)
        {
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "GetDriverInfo";
            SqlDataReader reader;
            conn.Open();
            cmd.Parameters.AddWithValue("@QuoteID", quoteID);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Driver driver = new Driver();
                driver.firstName = Website.getSafeString(reader, 0);
                driver.middleName = Website.getSafeString(reader, 1);
                driver.lastName = Website.getSafeString(reader, 2);
                driver.suffix = Website.getSafeString(reader, 3);
                try
                {
                    driver.dateOfBirth = reader.GetDateTime(4);

                }
                catch (Exception ex)
                {
                    driver.dateOfBirth = DateTime.MinValue;
                }
                try
                {
                    driver.age = reader.GetInt32(5);
                }
                catch { driver.age = -1; }
                driver.gender = Website.getSafeString(reader, 6);
                driver.maritalStatus = Website.getSafeString(reader, 7);
                try
                {
                    driver.dateFirstDriversLicense = reader.GetDateTime(8);

                }
                catch (Exception ex)
                {
                    driver.dateFirstDriversLicense = DateTime.MinValue;
                }
                try
                {
                    driver.yearLicensed = reader.GetInt32(9);
                }
                catch { driver.yearLicensed = -1; }
                driver.violationsLast3Years = reader.GetBoolean(10);
                driver.driverID = reader.GetGuid(11);
                Driver.drivers.Add(driver);
                if (Driver.drivers.Count == DMAX)
                {
                    lblError.Text = "You have reached the maximum number of drivers. Please delete one to add another.";
                    btnDriver.Visible = false;
                }
                else
                {
                    btnDriver.Visible = true;
                }

            }
        }
        

        /**************************CHECK VEHICLES***********************/
        for (int i = 0; i < gvVehicle.Rows.Count; i++)
        {
            int index = i;
            HyperLink hyperlink = new HyperLink();
            hyperlink.Text = "Edit";
            hyperlink.NavigateUrl = "~/Wepages/Auto/VehicleInformation.aspx/?Index=" + index;
            gvVehicle.Rows[i].Cells[4].Controls.Add(hyperlink);

            System.Web.UI.WebControls.Button btnDelete_Vehicle = new System.Web.UI.WebControls.Button();
            btnDelete_Vehicle.ID = "btnDeleteVehicle";
            btnDelete_Vehicle.Text = "Delete";
            btnDelete_Vehicle.OnClientClick = "return confirm('You are deleting this vehicle. This will also delete any assignment(s) associated with this vehicle. Click Ok to confirm.');";
            btnDelete_Vehicle.Click += new System.EventHandler((s, ea) => btnDelete_Vehicle_Clicked(s, ea, index));
            gvVehicle.Rows[i].Cells[5].Controls.Add(btnDelete_Vehicle);
        }
        if (gvVehicle.Rows.Count > 0)
        {   // if there are drivers, populate the array
            quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "GetVehicleInfo";
            SqlDataReader reader;
            conn.Open();
            cmd.Parameters.AddWithValue("@QuoteID", quoteID);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Vehicle vehicle = new Vehicle();
                vehicle.vehicleType = Website.getSafeString(reader, 0);
                vehicle.vinNumber = Website.getSafeString(reader, 1);
                try
                {
                    vehicle.year = reader.GetInt32(2);
                }
                catch { vehicle.year = -1; }
                vehicle.make = Website.getSafeString(reader, 3);
                vehicle.model = Website.getSafeString(reader, 4);
                vehicle.bodyType = Website.getSafeString(reader, 5);
                try
                {

                    vehicle.engineCyl = reader.GetInt32(6);
                }
                catch (Exception ex)
                {
                    vehicle.engineCyl = -1;
                }
                vehicle.awayAtSchool = Website.getSafeString(reader, 7);
                vehicle.inStateZipCode = Website.getSafeString(reader, 8);
                vehicle.outStateCollegeName = Website.getSafeString(reader, 9);
                vehicle.outStateCollegePop = Website.getSafeString(reader, 10);
                try
                {
                    vehicle.garaged = reader.GetBoolean(11);
                }
                catch { }
                vehicle.noGarageZipCode = Website.getSafeString(reader, 12);
                vehicle.antiTheft = Website.getSafeString(reader, 13);
                vehicle.antiLockBrake = Website.getSafeString(reader, 14);
                vehicle.airbags = Website.getSafeString(reader, 15);
                vehicle.vehicleUsage = Website.getSafeString(reader, 16);
                vehicle.otherThanCollisionDeductible = Website.getSafeString(reader, 17);
                vehicle.collisionDeductible = Website.getSafeString(reader, 18);
                vehicle.rentalReimburse = Website.getSafeString(reader, 19);
                vehicle.rentalReimbursementLimit = Website.getSafeString(reader, 20);
                vehicle.vehicleID = reader.GetGuid(21);
                Vehicle.vehicles.Add(vehicle);
                if (Vehicle.vehicles.Count == VMAX)
                {
                    lblError.Text += "<br/>You have reached the maximum number of vehicles. Please delete one to add another.";
                    btnVehicle.Visible = false;
                }
                else
                {
                    btnVehicle.Visible = true;
                }

            }
        }

        
        /**************************CHECK ASSIGNMENTS***********************/
        for (int i = 0; i < gvAssignment.Rows.Count; i++)
        {
            int index = i;
            System.Web.UI.WebControls.Button btnDelete_Assignment = new System.Web.UI.WebControls.Button();
            btnDelete_Assignment.ID = "btnDeleteAssignment";
            btnDelete_Assignment.Text = "Delete";
            btnDelete_Assignment.OnClientClick = "return confirm('You are Deleting this Assignment.  Click Ok to confirm.');";
            btnDelete_Assignment.Click += new System.EventHandler((s, ea) => btnDelete_Assignment_Clicked(s, ea, index));
            gvAssignment.Rows[i].Cells[3].Controls.Add(btnDelete_Assignment);
        }
        if (gvAssignment.Rows.Count > 0)
        {   // if there are assignments, populate the array
            quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "GetAssignmentInfo";
            SqlDataReader reader;
            conn.Open();
            cmd.Parameters.AddWithValue("@QuoteID", quoteID);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Assignment assignment = new Assignment();
                assignment.vehicleID = reader.GetGuid(0);
                assignment.quoteID = reader.GetGuid(1);
                assignment.driverID = reader.GetGuid(2);
                assignment.primary = reader.GetBoolean(3);
                Assignment.assignments.Add(assignment);
            }

            /*// Fill assignments tab with Drivers and Vehicles
            for (int vehicle = 0; vehicle < Vehicle.vehicleCount; vehicle++)
            {
                string temp = Vehicle.vehicles[vehicle].year + " " + Vehicle.vehicles[vehicle].make 
                    + " " + Vehicle.vehicles[vehicle].model;
                ddlVehicle.Items.Add(new ListItem(temp, temp));
            }
            for (int driver = 0; driver < Driver.driverCount; driver++)
            {
                string temp = Driver.drivers[driver].firstName + " " + Driver.drivers[driver].lastName;
                ddlDriver.Items.Add(new ListItem(temp, temp));
            }*/
        }
    }
    protected void btnAssignmentSave_Click(object sender, EventArgs e)
    {
        try
        {
            //verify that the selected index is not 0 for both ddl's
            if (ddlDriver.SelectedIndex != 0 && ddlVehicle.SelectedIndex != 0)
            {

                // save the assignment to the db. validate that an appropriate driver/ vehicle combo is selected.
                int vehicleIndex = ddlVehicle.SelectedIndex;
                int driverIndex = ddlDriver.SelectedIndex;

                bool match = false;
                if (chkPrimary.Checked)
                {
                    for (int i = 0; i < Assignment.assignments.Count; i++)
                    {
                        if (Assignment.assignments[i].primary == true) //if assignment has primary
                        {
                            if (Assignment.assignments[i].vehicleID == Vehicle.vehicles[vehicleIndex - 1].vehicleID)
                            {
                                match = true;
                                lblError.Text = "The vehicle has already been assigned a primary driver.";
                                break;
                            }
                        }
                    }
                }
                if (!match)
                {
                    Assignment assignment = new Assignment();
                    Guid vID = Vehicle.vehicles[vehicleIndex - 1].vehicleID;
                    Guid dID = Driver.drivers[driverIndex - 1].driverID;
                    assignment.driverID = dID;
                    assignment.vehicleID = vID;
                    assignment.quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
                    if (chkPrimary.Checked)
                        assignment.primary = true;
                    else
                        assignment.primary = false;
                    assignment.setAssignment();
                    chkPrimary.Checked = false;
                    Assignment.assignments.Add(assignment);
                    Response.Redirect("AssignmentHub.aspx");
                }
            }
            else
            {
                lblError.Text = "Please select a driver and vehicle to assign.";
            }
            
        }
        catch(Exception ex)
        {
            lblError.Text = "No duplicate assignments allowed.";
        }

    }
    protected void btnDelete_Driver_Clicked(object s, EventArgs ea, int index)
    {
        try
        {

            //delete driver associated with this quote
            Guid driverID = Driver.drivers[index].driverID;
            Driver.drivers.RemoveAt(index);

            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.CommandText = "DELETE FROM Driver WHERE DriverID = @driverID";
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            Response.Redirect("AssignmentHub.aspx");
        }
        catch
        {
            throw;
        }
    }
    protected void btnDelete_Vehicle_Clicked(object s, EventArgs ea, int index)
    {
        try
        {
            //delete driver vehicle with this quote
            Guid vehicleID = Vehicle.vehicles[index].vehicleID;
            Vehicle.vehicles.RemoveAt(index);

            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.Parameters.AddWithValue("@vehicleID", vehicleID);
            cmd.CommandText = "DELETE FROM Vehicle WHERE VehicleID = @vehicleID";
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            Response.Redirect("AssignmentHub.aspx");
        }
        catch
        {
            throw;
            lblError.Text = "";
        }
    }
    protected void btnDelete_Assignment_Clicked(object s, EventArgs ea, int index)
    {
        try
        {
            //delete assignment associated with this quote
            Guid vehicleID = Assignment.assignments[index].vehicleID;
            Guid driverID = Assignment.assignments[index].driverID;
            Assignment.assignments.RemoveAt(index);

            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@vehicleID", vehicleID);
            cmd.CommandText = "DELETE FROM Assignment WHERE DriverID = @driverID AND VehicleID = @vehicleID";
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            Response.Redirect("AssignmentHub.aspx");
        }
        catch
        {

        }
    }

    protected void ddlDriver_DataBound(object sender, EventArgs e)
    {
        ddlDriver.Items.Insert(0, "Select one");
        ddlDriver.Items[0].Value = "";
        ddlDriver.SelectedIndex = 0;
        
    }

    protected void ddlVehicle_DataBound(object sender, EventArgs e)
    {
        ddlVehicle.Items.Insert(0, "Select one");
        ddlVehicle.Items[0].Value = "";
        ddlVehicle.SelectedIndex = 0;
        
    }


    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (Vehicle.vehicles.Count > 0 && Driver.drivers.Count > 0 && Assignment.assignments.Count > 0)
        {
            //verify
            //test to ensure every vehicle has a primary driver
            bool isAssigned = false;
            for (int i = 0; i < Vehicle.vehicles.Count; i++)
            {
                isAssigned = false;
                for (int j = 0; j < Assignment.assignments.Count; j++)
                {
                    if (Vehicle.vehicles[i].vehicleID == Assignment.assignments[j].vehicleID)
                    {
                        isAssigned = true;
                        break;
                    }
                }
                if (!isAssigned)
                {
                    lblError.Text = "All vehicles must be assigned to a driver.";
                    break;
                }
            }

            //ensure all asigned vehicles have a primary driver
            bool hasPrimary = false;
            for (int i = 0; i < Vehicle.vehicles.Count; i++)
            {
                hasPrimary = false;
                for (int j = 0; j < Assignment.assignments.Count; j++)
                {
                    if (Vehicle.vehicles[i].vehicleID == Assignment.assignments[j].vehicleID)
                    {
                        if (Assignment.assignments[j].primary == true)
                        {
                            hasPrimary = true;
                            break;
                        }
                    }
                }
                if (!hasPrimary) // if all assignments and there is no match, exit loop
                {
                    //hasPrimary = false;
                    break;
                }
            }
            if (!hasPrimary)
            {
                lblError.Text += "<br/>All vehicles must be assigned a primary driver.";
            }
            else if (isAssigned && hasPrimary)
                Response.Redirect("~/Wepages/Quote/QuoteResult.aspx");
        }
        else
        {
            lblError.Text = "At least one driver, vehicle, and assignment must be created.";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Wepages/Auto/Policy.aspx");
    }

}