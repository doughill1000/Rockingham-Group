using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Wepages_Auto_DriverInformation : System.Web.UI.Page
{
    //Driver driver = new Driver();
    protected void Page_Load(object sender, EventArgs e)
    {
        cvFirstLicense2.ValueToCompare = DateTime.Now.ToShortDateString();
        if (!IsPostBack)
        {
            //Autofill driver information if there are no previous drivers
            if (Driver.driverCount == 0)
            {
                Quote quote = (Quote)(HttpContext.Current.Session["quote"]);
                txtFirstName.Text = quote.firstName;
                txtMiddleName.Text = quote.middleName;
                txtLastName.Text = quote.lastName;
                txtSuffix.Text = quote.suffix;
                txtDateOfBirth.Text = quote.dateOfBirth.ToString("d");
                if (Website.calculateYearsInt(txtDateOfBirth.Text) >= 0)
                {
                    lblAgeCalculated.Text = Website.calculateYearsString(txtDateOfBirth.Text);
                    if (lblAgeCalculated.Text != "" && cuvDateOfBirthOld.IsValid && cuvDateOfBirthYoung.IsValid && cvDateOfBirth.IsValid)
                    {
                        lblAge.Visible = true;
                        lblAgeCalculated.Visible = true;
                    }
                    else
                    {
                        lblAge.Visible = false;
                        lblAgeCalculated.Visible = false;
                    }
                }
            }
            //Fill information for editing a driver
            else
            {

                try
                {
                    String strDriver = Request.QueryString["Index"];
                    if (strDriver != null)
                    {
                        int driversIndex = Convert.ToInt32(strDriver);
                        Driver driver = Driver.drivers[driversIndex];
                        txtFirstName.Text = driver.firstName;
                        txtMiddleName.Text = driver.middleName;
                        txtLastName.Text = driver.lastName;
                        txtSuffix.Text = driver.suffix;
                        if (driver.dateOfBirth == DateTime.MinValue)
                        {
                            txtDateOfBirth.Text = "";
                        }
                        else
                        {
                            txtDateOfBirth.Text = driver.dateOfBirth.ToString("d");
                        }
                        lblAge.Visible = true;
                        lblAgeCalculated.Visible = true;
                        lblAgeCalculated.Text = Website.checkForInts(driver.age);
                        ddlGender.SelectedValue = driver.gender;
                        ddlMaritalStatus.SelectedValue = driver.maritalStatus;
                        if (driver.dateOfBirth == DateTime.MinValue)
                        {
                            txtFirstLicense.Text = "";
                        }
                        else
                        {
                            txtFirstLicense.Text = driver.dateFirstDriversLicense.ToString("d");
                        }
                        lblYearsLicensed.Visible = true;
                        lblYearsLicensedCalculated.Visible = true;
                        lblYearsLicensed.Text = "Years Licensed:";
                        lblYearsLicensedCalculated.Text = Website.checkForInts(driver.yearLicensed);
                        ddlIncidentLast3Years.SelectedIndex = Website.checkForBooleans(driver.violationsLast3Years);
                    }
                }
                catch (Exception ex)
                { }
            }
        }
        Page.Validate();
    }
    protected void txtDateOfBirth_TextChanged(object sender, EventArgs e)
    {
        //Check if birthdate is valid
        if (Website.calculateYearsInt(txtDateOfBirth.Text) >= 0)
        {
            lblAgeCalculated.Text = Website.calculateYearsString(txtDateOfBirth.Text);
            if (lblAgeCalculated.Text != "" && cuvDateOfBirthOld.IsValid && cuvDateOfBirthYoung.IsValid && cvDateOfBirth.IsValid)
            {
                lblAge.Visible = true;
                lblAgeCalculated.Visible = true;
            }
            else
            {
                lblAge.Visible = false;
                lblAgeCalculated.Visible = false;
            }
        }
        ddlGender.Focus();
    }
    protected void txtFirstLicense_TextChanged(object sender, EventArgs e)
    {
        lblYearsLicensedCalculated.Text = Website.calculateYearsString(txtFirstLicense.Text);
        cuvFirstLicense2.Validate();
        if (!cvFirstLicense.IsValid)
        {
            cvFirstLicense2.Enabled = false;
        }
        else
        {
            cvFirstLicense2.Enabled = true;
        }
        if (lblYearsLicensedCalculated.Text != "" && cuvFirstLicense.IsValid && cvFirstLicense.IsValid && cvFirstLicense2.IsValid && cuvFirstLicense2.IsValid)
        {
            if (Convert.ToInt32(lblYearsLicensedCalculated.Text) >= 0)
            {
                lblYearsLicensed.Visible = true;
                lblYearsLicensedCalculated.Visible = true;
            }

            else
            {
                lblYearsLicensed.Visible = false;
                lblYearsLicensedCalculated.Visible = false;
            }
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
                Guid quoteID = (Guid)(System.Web.HttpContext.Current.Session["quoteID"]);
                Driver driver;
                if (Request.QueryString["Index"] == null)
                {
                    driver = new Driver();
                    driver.driverID = Guid.NewGuid();
                }
                else
                {
                    driver = Driver.drivers[Convert.ToInt32(Request.QueryString["Index"])];
                }
                driver.quoteID = quoteID;
                driver.firstName = txtFirstName.Text;
                driver.middleName = txtMiddleName.Text;
                driver.lastName = txtLastName.Text;
                driver.suffix = txtSuffix.Text;
                driver.dateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text);
                driver.age = Convert.ToInt32(lblAgeCalculated.Text);
                driver.gender = ddlGender.SelectedItem.Text;
                driver.maritalStatus = ddlMaritalStatus.SelectedItem.Text;
                driver.dateFirstDriversLicense = Convert.ToDateTime(txtFirstLicense.Text);
                driver.yearLicensed = Convert.ToInt32(lblYearsLicensedCalculated.Text);
                driver.violationsLast3Years = Convert.ToBoolean(ddlIncidentLast3Years.SelectedValue);
                //Put information into database through driver
                driver.setDriverInfo();

                if (Request.QueryString["Index"] == null)
                {
                    Driver.drivers.Add(driver);
                }
                else
                {
                    Driver.drivers[Convert.ToInt32(Request.QueryString["Index"])] = driver;
                }

                Response.Redirect("~/Wepages/Auto/AssignmentHub.aspx");
            }
            catch (SqlException ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
    //Calculates the number  of years between two dates
    //Returns the value as a String

    protected void ddlIncidentLast3Years_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIncidentLast3Years.SelectedValue == "true")
        {
            lblIncidentWarning.Text = "<strong>Note</strong>: Because of these incidents, your quote has the potential to increase.";
        }
        else
        {
            lblIncidentWarning.Text = "";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Wepages/Auto/AssignmentHub.aspx");
    }
    protected void cuvDateOfBirthYoung_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (cvDateOfBirth.IsValid)
        {
            if (Website.calculateYearsInt(args.Value) < 15.5)
            {
                args.IsValid = false;
                lblAge.Visible = false;
                lblAgeCalculated.Visible = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
    protected void cuvDateOfBirthOld_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if (Website.calculateYearsInt(args.Value) > 100)
        {
            args.IsValid = false;
            lblAge.Visible = false;
            lblAgeCalculated.Visible = false;

        }
        else
        {
            args.IsValid = true;
        }
    }
    protected void cuvFirstLicense_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {

            if (Website.calculateYearsInt(args.Value) > 85)
            {
                args.IsValid = false;
                lblYearsLicensed.Visible = false;
                lblYearsLicensedCalculated.Visible = false;

            }
            else
            {
                args.IsValid = true;
                lblYearsLicensed.Visible = true;
                lblYearsLicensedCalculated.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void cuvFirstLicense2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            int calculatedYears = Convert.ToDateTime(txtFirstLicense.Text).Year - Convert.ToDateTime(txtDateOfBirth.Text).Year;
            if (Convert.ToDateTime(txtDateOfBirth.Text) > Convert.ToDateTime(txtFirstLicense.Text).AddYears(-calculatedYears))
                calculatedYears--;

            if (calculatedYears < 15)
            {
                args.IsValid = false;
                lblYearsLicensed.Visible = false;
                lblYearsLicensedCalculated.Visible = false;
            }
            else
            {
                args.IsValid = true;
                lblYearsLicensed.Visible = true;
                lblYearsLicensedCalculated.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }

}