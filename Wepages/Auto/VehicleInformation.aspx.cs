using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Wepages_Auto_VehicleInformation : System.Web.UI.Page
{
    //static Vehicle vehicle = new Vehicle();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Validate();
        if (!IsPostBack)
        {
            try
            {
                String strVehiclesIndex = Request.QueryString["Index"];
                if (strVehiclesIndex != null) //edit vehicle
                {
                    int vehiclesIndex = Convert.ToInt32(strVehiclesIndex);
                    Vehicle vehicle = Vehicle.vehicles[vehiclesIndex];
                    ddlVehicleType.SelectedValue = vehicle.vehicleType;
                    txtVIN.Text = vehicle.vinNumber;
                    ddlYear.DataBind();
                    ddlYear.SelectedValue = vehicle.year.ToString();

                    lblMake.Visible = true;
                    ddlMake.Visible = true;
                    rfvMake.Visible = true;
                    sdsMake.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
                    ddlMake.DataBind();
                    ddlMake.SelectedValue = vehicle.make;

                    lblModel.Visible = true;
                    ddlModel.Visible = true;
                    rfvModel.Visible = true;
                    sdsModel.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
                    sdsModel.SelectParameters["Make"].DefaultValue = ddlMake.SelectedValue;
                    ddlModel.DataBind();
                    ddlModel.SelectedValue = vehicle.model;

                    lblBodyType.Visible = true;
                    ddlBodyType.Visible = true;
                    sdsBodyType.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
                    sdsBodyType.SelectParameters["Make"].DefaultValue = ddlMake.SelectedValue;
                    sdsBodyType.SelectParameters["Model"].DefaultValue = ddlModel.SelectedValue;
                    ddlBodyType.DataBind();
                    ddlBodyType.SelectedValue = vehicle.bodyType;

                    lblEngineCylPartialVIN.Visible = true;
                    ddlEngineCylPartialVIN.Visible = true;
                    sdsEngineCylPartialVIN.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
                    sdsEngineCylPartialVIN.SelectParameters["MAKE"].DefaultValue = ddlMake.SelectedValue;
                    sdsEngineCylPartialVIN.SelectParameters["MODEL"].DefaultValue = ddlModel.SelectedValue;
                    sdsEngineCylPartialVIN.SelectParameters["BODY"].DefaultValue = ddlBodyType.SelectedValue;
                    
                    ddlEngineCylPartialVIN.DataBind();
                    ddlEngineCylPartialVIN.SelectedValue = vehicle.engineCyl.ToString();

                    ddlAwayAtSchool.SelectedValue = vehicle.awayAtSchool;
                    txtInStateZipCode.Text = vehicle.inStateZipCode;
                    txtCollegeName.Text = vehicle.outStateCollegeName;
                    ddlCollegePop.SelectedValue = vehicle.outStateCollegePop;
                    ddlGaragedAtCurrentAddress.SelectedValue = Website.checkForBooleans(vehicle.garaged).ToString();
                    txtGarageZipCode.Text = vehicle.noGarageZipCode;
                    ddlAntiTheftDevices.SelectedValue = vehicle.antiTheft;
                    ddlAntiLockBrakes.SelectedValue = vehicle.antiLockBrake;
                    ddlAirbags.SelectedValue = vehicle.airbags;
                    ddlUse.SelectedValue = vehicle.vehicleUsage;
                    ddlOtherThanCollisionDeductable.SelectedValue = vehicle.otherThanCollisionDeductible;
                    ddlOtherThanCollisionDeductable.DataBind();
                    if (ddlOtherThanCollisionDeductable.SelectedValue == "0")
                    {
                        lblCollisionDeductible.Visible = true;
                        ddlCollisionDeductible.Visible = true;
                        lblRentalReimbursement.Visible = true;
                        ddlRentalReimbursement.Visible = true;
                    }
                    ddlCollisionDeductible.SelectedValue = vehicle.collisionDeductible;
                    ddlRentalReimbursement.DataBind();
                    ddlRentalReimbursement.SelectedValue = vehicle.rentalReimburse;
                    if (ddlRentalReimbursement.SelectedValue == "Option 1")
                    {
                        ddlRentalReimbursementLimit.SelectedValue = vehicle.rentalReimbursementLimit;
                        lblRentalReimbursementLimit.Visible = true;
                        ddlRentalReimbursementLimit.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {  }
        }
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (!Page.IsValid)
        {
            lblRequiredFieldError.Text = "Please enter a value for all required fields";
        }
        else
        {
            try
            {
                Guid quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]); //Retrieve quoteID from session
                Vehicle vehicle;
                if (Request.QueryString["Index"] == null) //If this is not an edit
                {
                    //Create new vehicle and assign a new vehicleID
                    vehicle = new Vehicle();
                    vehicle.vehicleID = Guid.NewGuid();

                }
                else
                {
                    //Pull the vehicle from the ararylist that is being edited
                    vehicle = Vehicle.vehicles[Convert.ToInt32(Request.QueryString["Index"])]; 
                }
                //Assign all vehicle attributes to data field selected values
                vehicle.quoteID = quoteID;
                vehicle.vehicleType = ddlVehicleType.SelectedItem.Text;
                vehicle.vinNumber = txtVIN.Text;
                vehicle.year = Convert.ToInt32(ddlYear.SelectedItem.Text);
                vehicle.make = ddlMake.SelectedItem.Text;
                vehicle.model = ddlModel.SelectedItem.Text;
                if (ddlBodyType.SelectedValue != "")
                {
                    vehicle.bodyType = ddlBodyType.SelectedItem.Text;
                }
                else
                {
                    vehicle.bodyType = "";
                }
                if (ddlEngineCylPartialVIN.SelectedValue != "")
                {
                    vehicle.engineCyl = Convert.ToInt32(ddlEngineCylPartialVIN.SelectedItem.Text);
                }
                else
                {
                    vehicle.engineCyl = -1;
                }
                vehicle.awayAtSchool = ddlAwayAtSchool.SelectedItem.Text;
                vehicle.inStateZipCode = txtInStateZipCode.Text;
                vehicle.outStateCollegeName = txtCollegeName.Text;
                vehicle.outStateCollegePop = ddlCollegePop.SelectedItem.Text;
                if (ddlGaragedAtCurrentAddress.SelectedValue != "")
                {
                    vehicle.garaged = Convert.ToBoolean(ddlGaragedAtCurrentAddress.SelectedValue);
                }
                else
                {
                    vehicle.garaged = true;
                }
                vehicle.noGarageZipCode = txtGarageZipCode.Text;
                vehicle.antiTheft = ddlAntiTheftDevices.SelectedItem.Text;
                vehicle.antiLockBrake = ddlAntiLockBrakes.SelectedItem.Text;
                vehicle.airbags = ddlAirbags.SelectedItem.Text;
                vehicle.vehicleUsage = ddlUse.SelectedItem.Text;
                vehicle.otherThanCollisionDeductible = ddlOtherThanCollisionDeductable.SelectedValue;
                vehicle.collisionDeductible = ddlCollisionDeductible.SelectedValue;
                vehicle.rentalReimburse = ddlRentalReimbursement.SelectedValue;
                vehicle.rentalReimbursementLimit = ddlRentalReimbursementLimit.SelectedValue;
                vehicle.setVehicleInfo();
                if (Request.QueryString["Index"] == null)
                {
                    //If vehicle is new, add it to the arraylist
                    Vehicle.vehicles.Add(vehicle);
                }
                else
                {
                    //Update the vehicle in the arraylist to the new vehicle
                    Vehicle.vehicles[Convert.ToInt32(Request.QueryString["Index"])] = vehicle;
                }
                Response.Redirect("~/Wepages/Auto/AssignmentHub.aspx");
            }
            catch (SqlException ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
    protected void txtVIN_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlDataReader reader;
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@VIN", txtVIN.Text);
            cmd.CommandText = "MatchVIN";
            reader = cmd.ExecuteReader();
            //If the VIN matches
            if (reader.Read())
            {
                //Make all vehicle fields visible
                lblMake.Visible = true;
                ddlMake.Visible = true;
                rfvMake.Visible = true;
                lblModel.Visible = true;
                ddlModel.Visible = true;
                rfvModel.Visible = true;
                lblBodyType.Visible = true;
                ddlBodyType.Visible = true;
                lblEngineCylPartialVIN.Visible = true;
                ddlEngineCylPartialVIN.Visible = true;
                ddlYear.SelectedValue = reader.GetDouble(1).ToString();
                sdsMake.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
                ddlMake.DataBind();
                ddlMake.SelectedValue = reader.GetString(2);
                sdsModel.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
                sdsModel.SelectParameters["Make"].DefaultValue = ddlMake.SelectedValue;
                ddlModel.DataBind();
                ddlModel.SelectedValue = reader.GetString(3);
                sdsBodyType.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
                sdsBodyType.SelectParameters["Make"].DefaultValue = ddlMake.SelectedValue;
                sdsBodyType.SelectParameters["Model"].DefaultValue = ddlModel.SelectedValue;
                ddlBodyType.DataBind();
                try
                {
                    ddlBodyType.SelectedValue = reader.GetString(4);
                }
                catch (Exception ex)
                {
                    ddlBodyType.SelectedValue = "";
                }
                sdsEngineCylPartialVIN.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
                sdsEngineCylPartialVIN.SelectParameters["Make"].DefaultValue = ddlMake.SelectedValue;
                sdsEngineCylPartialVIN.SelectParameters["Model"].DefaultValue = ddlModel.SelectedValue;
                sdsEngineCylPartialVIN.SelectParameters["Body"].DefaultValue = ddlBodyType.SelectedValue;
                ddlEngineCylPartialVIN.DataBind();
                try
                {

                    ddlEngineCylPartialVIN.SelectedValue = reader.GetDouble(5).ToString();
                }
                catch (Exception ex)
                {
                    ddlEngineCylPartialVIN.SelectedValue = "";
                }
            }
            else
            {
                ddlYear.SelectedIndex = 0;
                lblMake.Visible = false;
                ddlMake.Visible = false;
                rfvMake.Visible = false;
                lblModel.Visible = false;
                ddlModel.Visible = false;
                rfvModel.Visible = false;
                lblBodyType.Visible = false;
                ddlBodyType.Visible = false;
                lblEngineCylPartialVIN.Visible = false;
                ddlEngineCylPartialVIN.Visible = false;
            }
        }
        catch (SqlException ex)
        {
            lblRequiredFieldError.Text = ex.ToString();
        }

    }
    protected void ddlAwayAtSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblInStateZipCode.Visible = false;
        txtInStateZipCode.Visible = false;
        revInStateZipCode.Visible = false;
        txtInStateZipCode.Text = "";
        txtCollegeName.Text = "";
        ddlCollegePop.SelectedValue = "";

        switch (ddlAwayAtSchool.SelectedValue)
        {
            case "In-State":
                lblInStateZipCode.Visible = true;
                txtInStateZipCode.Visible = true;
                revInStateZipCode.Visible = true;
                lblCollegeName.Visible = false;
                txtCollegeName.Visible = false;
                lblCollegePop.Visible = false;
                ddlCollegePop.Visible = false;
                txtCollegeName.Text = "";
                ddlCollegePop.SelectedValue = "";
                break;

            case "Out-of State":
                lblCollegeName.Visible = true;
                txtCollegeName.Visible = true;
                lblCollegePop.Visible = true;
                ddlCollegePop.Visible = true;
                lblInStateZipCode.Visible = false;
                txtInStateZipCode.Visible = false;
                revInStateZipCode.Visible = false;
                lblCollegeName.Visible = true;
                txtCollegeName.Visible = true;
                txtInStateZipCode.Text = "";

                break;

            default:
                lblInStateZipCode.Visible = false;
                txtInStateZipCode.Visible = false;
                revInStateZipCode.Visible = false;
                break;

        }
    }
    protected void ddlGaragedAtCurrentAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblGarageZipCode.Visible = false;
        txtGarageZipCode.Visible = false;
        revGarageZipCode.Visible = false;
        txtGarageZipCode.Text = "";
        if (ddlGaragedAtCurrentAddress.SelectedValue == "true")
        {
            lblGarageZipCode.Visible = true;
            txtGarageZipCode.Visible = true;
            revGarageZipCode.Visible = true;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Wepages/Auto/AssignmentHub.aspx");
    }
    protected void txtInStateZipCode_TextChanged(object sender, EventArgs e)
    {
        revInStateZipCode.Validate();
    }
    protected void txtGarageZipCode_TextChanged(object sender, EventArgs e)
    {
        revGarageZipCode.Validate();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        String year = ddlYear.SelectedValue;
        clearParameters();
        sdsMake.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
        if (ddlYear.SelectedValue != "")
        {
            lblMake.Visible = true;
            ddlMake.Visible = true;
            rfvMake.Visible = true;
            lblModel.Visible = false;
            ddlModel.Visible = false;
            rfvModel.Visible = false;
            lblBodyType.Visible = false;
            ddlBodyType.Visible = false;
            lblEngineCylPartialVIN.Visible = false;
            ddlEngineCylPartialVIN.Visible = false;
            Page.Validate();
        }
        else
        {
            lblMake.Visible = false;
            ddlMake.Visible = false;
            rfvMake.Visible = false;
        }
    }

    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        string year = ddlYear.SelectedValue;
        string make = ddlMake.SelectedValue;
        clearParameters();
        sdsModel.SelectParameters["VYEAR"].DefaultValue = year;
        sdsModel.SelectParameters["MAKE"].DefaultValue = make;
        if (ddlMake.SelectedValue != "")
        {
            lblModel.Visible = true;
            ddlModel.Visible = true;
            rfvModel.Visible = true;
            Page.Validate();
        }
        else
        {
            lblModel.Visible = false;
            ddlModel.Visible = false;
            rfvModel.Visible = false;
        }
        lblBodyType.Visible = false;
        ddlBodyType.Visible = false;
        lblEngineCylPartialVIN.Visible = false;
        ddlEngineCylPartialVIN.Visible = false;
    }
    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clearParameters();
        sdsBodyType.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
        sdsBodyType.SelectParameters["MAKE"].DefaultValue = ddlMake.SelectedValue;
        sdsBodyType.SelectParameters["MODEL"].DefaultValue = ddlModel.SelectedValue;
        if (ddlModel.SelectedValue != "")
        {
            lblBodyType.Visible = true;
            ddlBodyType.Visible = true;
            Page.Validate();
        }
        else
        {
            lblBodyType.Visible = false;
            ddlBodyType.Visible = false;
        }
        lblEngineCylPartialVIN.Visible = false;
        ddlEngineCylPartialVIN.Visible = false;
    }

    protected void ddlBodyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clearParameters();
        sdsEngineCylPartialVIN.SelectParameters["VYEAR"].DefaultValue = ddlYear.SelectedValue;
        sdsEngineCylPartialVIN.SelectParameters["MAKE"].DefaultValue = ddlMake.SelectedValue;
        sdsEngineCylPartialVIN.SelectParameters["MODEL"].DefaultValue = ddlModel.SelectedValue;
        sdsEngineCylPartialVIN.SelectParameters["BODY"].DefaultValue = ddlBodyType.SelectedValue;

        if (ddlBodyType.SelectedValue != "")
        {
            lblEngineCylPartialVIN.Visible = true;
            ddlEngineCylPartialVIN.Visible = true;
            Page.Validate();
        }
        else
        {
            lblEngineCylPartialVIN.Visible = false;
            ddlEngineCylPartialVIN.Visible = false;

        }
    }

    public void clearParameters()
    {
        sdsEngineCylPartialVIN.SelectParameters["VYEAR"].DefaultValue = "";
        sdsEngineCylPartialVIN.SelectParameters["MAKE"].DefaultValue = "";
        sdsEngineCylPartialVIN.SelectParameters["MODEL"].DefaultValue = "";
        sdsEngineCylPartialVIN.SelectParameters["BODY"].DefaultValue = "";
    }

    protected void ddlYear_DataBound(Object sender, EventArgs e)
    {
        ddlYear.Items.Insert(0, "Select One");
        ddlYear.Items[0].Value = "";
        ddlYear.SelectedIndex = 0;
    }

    protected void ddlMake_DataBound(Object sender, EventArgs e)
    {
        ddlMake.Items.Insert(0, "Select One");
        ddlMake.Items[0].Value = "";
        ddlMake.SelectedIndex = 0;
    }

    protected void ddlModel_DataBound(Object sender, EventArgs e)
    {
        ddlModel.Items.Insert(0, "Select One");
        ddlModel.Items[0].Value = "";
        ddlModel.SelectedIndex = 0;
    }

    protected void ddlBodyType_DataBound(Object sender, EventArgs e)
    {
        ddlBodyType.Items.Insert(0, "Select One");
        ddlBodyType.Items[0].Value = "";
        ddlBodyType.SelectedIndex = 0;
    }

    protected void ddlEngineCylPartialVIN_DataBound(Object sender, EventArgs e)
    {
        ddlEngineCylPartialVIN.Items.Insert(0, "Select One");
        ddlEngineCylPartialVIN.Items[0].Value = "";
        ddlEngineCylPartialVIN.SelectedIndex = 0;
    }
    protected void ddlOtherThanCollisionDeductable_SelectedIndexChanged1(object sender, EventArgs e)
    {
        switch (ddlOtherThanCollisionDeductable.SelectedValue)
        {
            case "0":

                lblCollisionDeductible.Visible = true;
                ddlCollisionDeductible.Visible = true;
                lblRentalReimbursement.Visible = true;
                ddlRentalReimbursement.Visible = true;
                break;

            default:

                ddlCollisionDeductible.SelectedValue = "";
                ddlRentalReimbursement.SelectedValue = "";
                lblCollisionDeductible.Visible = false;
                ddlCollisionDeductible.Visible = false;
                lblRentalReimbursement.Visible = false;
                ddlRentalReimbursement.Visible = false;
                lblRentalReimbursementLimit.Visible = false;
                ddlRentalReimbursementLimit.Visible = false;
                break;
        }
    }
    protected void ddlRentalReimbursement_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlRentalReimbursement.SelectedValue == "Option 1")
        {
            lblRentalReimbursementLimit.Visible = true;
            ddlRentalReimbursementLimit.Visible = true;
        }
        else
        {
            ddlRentalReimbursementLimit.SelectedValue = "";
            lblRentalReimbursementLimit.Visible = false;
            ddlRentalReimbursementLimit.Visible = false;
        }
    }
}