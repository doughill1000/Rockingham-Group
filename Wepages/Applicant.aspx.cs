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

public partial class Wepages_Applicant : System.Web.UI.Page
{
    static Quote quote = new Quote();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Only happens first time page is loaded
        if (!IsPostBack)
        {
            String referenceNum = Request.QueryString["ReferenceNum"];
            //new quote
            if (referenceNum == null) 
            {
                quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
                //Has attributes QuoteID, InsuranceType, UserID (if there is one)
            }
            //Pulls information from existing quote. Uses reference number to identify the quote being requested
            else
            {
                quote = new Quote();

                SqlConnection conn = Website.getSQLConnection();
                SqlCommand cmd = Website.getSQLCommand(conn);
                cmd.CommandText = "GetApplicantInfo";
                SqlDataReader reader;
                conn.Open();
                cmd.Parameters.AddWithValue("@Reference#", Convert.ToInt32(referenceNum));
                reader = cmd.ExecuteReader();
                reader.Read();

                //Put all values from database into quote class
                quote.quoteID = reader.GetGuid(0);
                quote.insuranceType = Website.getSafeString(reader, 1);
                quote.userID = reader.GetGuid(2);
                quote.firstName = Website.getSafeString(reader, 3);
                quote.middleName = Website.getSafeString(reader, 4);
                quote.lastName = Website.getSafeString(reader, 5);
                quote.suffix = Website.getSafeString(reader, 6);
                try
                {
                    quote.dateOfBirth = reader.GetDateTime(7);

                }
                catch (Exception ex)
                {
                    quote.dateOfBirth = DateTime.MinValue;
                }
                quote.SSN = Website.getSafeString(reader, 8);
                quote.streetAddress1 = Website.getSafeString(reader, 9);
                quote.streetAddress2 = Website.getSafeString(reader, 10);
                quote.aptLot = Website.getSafeString(reader, 11);
                quote.city = Website.getSafeString(reader, 12);
                quote.state = Website.getSafeString(reader, 13);
                quote.zipcode = Website.getSafeString(reader, 14);
                quote.reference = Website.getSafeString(reader, 15);
                quote.region = Website.getSafeString(reader, 16);
                /*try
                {
                    quote.userID = reader.GetGuid(16);
                }
                catch
                {

                }*/
                try
                {
                    quote.creditRating = reader.GetInt32(17);
                }
                catch (Exception exc) { quote.creditRating = 0; }
                
                
                conn.Close();
            }
            //Put all attributes from class into data fields
            txtFirstName.Text = quote.firstName;
            txtMiddleName.Text = quote.middleName;
            txtLastName.Text = quote.lastName;
            txtSuffix.Text = quote.suffix;
            if (quote.dateOfBirth == DateTime.MinValue)
            {
                txtDateOfBirth.Text = "";
            }
            else
            {
                txtDateOfBirth.Text = quote.dateOfBirth.Date.ToString("d");
            }
            txtSSN.Text = quote.SSN;
            txtStreetAddress1.Text = quote.streetAddress1;
            txtStreetAddress2.Text = quote.streetAddress2;
            txtAptLot.Text = quote.aptLot;
            txtZipCode.Text = quote.zipcode;
            txtCity.Text = quote.city;
            ddlState.SelectedValue = quote.state;
            if (quote.state == "Virginia")//va
            {
                ddlRegionVA.Visible = true;
                ddlRegionVA.SelectedValue = quote.region;
                rfvRegionVA.Visible = true;
            }
            else//pa
            {
                ddlRegionPA.Visible = true;
                ddlRegionPA.SelectedValue = quote.region;
                rfvRegionPA.Visible = true;
            }
            if (quote.creditRating == 0)
                txtCreditRating.Text = "";
            else
                txtCreditRating.Text = quote.creditRating.ToString();
            //Validates the page
            
            //find if there are stored addresses
            try
            {
                SqlConnection connAddress = Website.getSQLConnection();
                SqlCommand cmdAddress = Website.getSQLCommand(connAddress);
                string userId = Membership.GetUser().ProviderUserKey.ToString();
                cmdAddress.CommandText = "SELECT DISTINCT streetaddress1, streetaddress2, [apt#/lot#], zipcode, city, state FROM Quote Where USERID = '" + userId + "' AND streetaddress1 is not null";
                cmdAddress.CommandType = System.Data.CommandType.Text;
                SqlDataReader readerAddress;
                connAddress.Open();
                readerAddress = cmdAddress.ExecuteReader();
                int counter = 0;
                while (readerAddress.Read())
                {
                    counter++;
                }
                connAddress.Close();
                if (counter > 0)//if there are addresses, fill the array and populate the drop down list
                {
                    //clear addresses
                    Address.addresses.Clear();
                    connAddress.Open();
                    readerAddress = cmdAddress.ExecuteReader();
                    for (int i = 0; i < counter; i++)
                    {
                        readerAddress.Read();
                        Address.addresses.Add(new Address());
                        Address.addresses[i].addressLine1 = readerAddress.GetString(0);
                        Address.addresses[i].addressLine2 = readerAddress.GetString(1);
                        Address.addresses[i].aptLot = readerAddress.GetString(2);
                        Address.addresses[i].zip = readerAddress.GetString(3);
                        Address.addresses[i].city = readerAddress.GetString(4);
                        Address.addresses[i].state = readerAddress.GetString(5);

                        //add concatenated string to drop down list.
                        string address = "";
                        if (Address.addresses[i].addressLine2.Trim() == "")
                        {
                            address += Address.addresses[i].addressLine1.Trim();
                        }
                        else
                        {
                            address += Address.addresses[i].addressLine1.Trim() + ", " + Address.addresses[i].addressLine2.Trim();
                        }
                        if (Address.addresses[i].aptLot.Trim() != "")
                        {
                            address += ", Apt/Lot " + Address.addresses[i].aptLot.Trim();
                        }
                        address += ", " + Address.addresses[i].city.Trim() + ", " + Address.addresses[i].state.Trim() + " " + Address.addresses[i].zip.Trim();
                        ListItem item = new ListItem(address);
                        ddlAddresses.Items.Add(item);
                    }
                    connAddress.Close();
                    ddlAddresses.DataBind();
                    ddlAddresses.Visible = true;
                    lblAddresses.Visible = true;
                }
                else //no addresses? hide adddresses
                {
                    ddlAddresses.Visible = false;
                    lblAddresses.Visible = false;
                }
            }
            catch { }
            if (quote.insuranceType == "Home")
                htmlSteps.Text = @"<div style=""padding:15px"">
                        <link type=""text/css"" href=""Styles\StyleSheet.css"" rel=""Stylesheet"" />
                        <div class=""stepComplete"" style=""border-color:#ffffff; border-width:5px;"">Step One:<br />Applicant</div>
                        <div class=""stepIncomplete"">Step Two:<br />Property</div>
                        <div class=""stepIncomplete"">Step Three:<br />Coverage</div>
                        <div class=""stepIncomplete"">Step Four:<br />Discounts</div>
                        <div class=""stepIncomplete"">Step Five:<br />Quote Result</div>
                        </div>";
            else
                htmlSteps.Text = @"<div style=""padding:15px"">
                        <link type=""text/css"" href=""Styles\StyleSheet.css"" rel=""Stylesheet"" />
                        <div class=""stepComplete"" style=""border-color:#ffffff; border-width:5px;"">Step One:<br />Applicant</div>
                        <div class=""stepIncomplete"">Step Two:<br />General Information</div>
                        <div class=""stepIncomplete"">Step Three:<br />Policy</div>
                        <div class=""stepIncomplete"">Step Four:<br />Assignments</div>
                        <div class=""stepIncomplete"">Step Five:<br />Quote Result</div>
                        </div>";

        }
        Page.Validate();
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        Page.Validate();
        //Checks if page is valid
        if (!Page.IsValid)
        {
            lblRequiredFieldError.Text = "* Please enter a value for all required fields";
        }
        else if (!validZipCode(txtZipCode.Text.TrimEnd(), ddlState.SelectedItem.Value))
        {
            //show error if zip code does not exist
            lblRequiredFieldError.Text = "* Please enter a valid " + ddlState.SelectedItem.Value + " ZIP code";
        }
        else
        {
            try
            {
                quote.firstName = txtFirstName.Text;
                quote.middleName = txtMiddleName.Text;
                quote.lastName = txtLastName.Text;
                quote.suffix = txtSuffix.Text;
                quote.dateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text).Date;
                quote.SSN = txtSSN.Text;
                quote.streetAddress1 = txtStreetAddress1.Text;
                quote.streetAddress2 = txtStreetAddress2.Text;
                quote.aptLot = txtAptLot.Text;
                quote.city = txtCity.Text;
                quote.state = ddlState.SelectedItem.Value;
                if (ddlRegionVA.Visible == true) //va
                {
                    quote.region = ddlRegionVA.SelectedItem.Value;
                }
                else//pa
                {
                    quote.region = ddlRegionPA.SelectedItem.Value;
                }
                quote.creditRating = Convert.ToInt32(txtCreditRating.Text);
                quote.zipcode = txtZipCode.Text.Trim();
                quote.setApplicantInfo();
                Session["quote"] = quote;
                Session["quoteID"] = quote.quoteID;
                //Redirects to either auto or home based on insurance type
                switch (quote.insuranceType)
                {
                    case "Auto":
                        Response.Redirect("~/Wepages/Auto/GeneralInformation.aspx");
                        break;
                    case "Home":
                        Response.Redirect("~/Wepages/Home/Property.aspx");
                        break;
                }
            }
            catch (SqlException ex)
            {
                lblRequiredFieldError.Text = ex.ToString();
            }
        }
    }

    protected void cuvDateOfBirth_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //Checks if applicant is 18 or older
        if (cvDateOfBirth.IsValid)
        {
            if (Website.calculateYearsInt(args.Value) < 18)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
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

    protected void ddlAddresses_DataBound(Object sender, EventArgs e)
    {
        ddlAddresses.Items.Insert(0, "Stored Addresses");
        ddlAddresses.Items[0].Value = "";
        ddlAddresses.SelectedIndex = 0;
    }
    protected void ddlAddresses_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAddresses.SelectedIndex != 0)
        {
            txtStreetAddress1.Text = Address.addresses[ddlAddresses.SelectedIndex-1].addressLine1.Trim();
            txtStreetAddress2.Text = Address.addresses[ddlAddresses.SelectedIndex-1].addressLine2.Trim();
            txtAptLot.Text = Address.addresses[ddlAddresses.SelectedIndex-1].aptLot;
            txtZipCode.Text = Address.addresses[ddlAddresses.SelectedIndex-1].zip;
            txtCity.Text = Address.addresses[ddlAddresses.SelectedIndex-1].city;
            if (Address.addresses[ddlAddresses.SelectedIndex - 1].state == "Virginia")
            {
                ddlState.SelectedIndex = 1;
                ddlRegionPA.Visible = false;
                ddlRegionVA.Visible = true;
                ddlRegionVA.SelectedIndex = 0;
                rfvRegionVA.Visible = true;
                rfvRegionPA.Visible = false;
            }
            else //pa
            {
                ddlState.SelectedIndex = 2;
                ddlRegionPA.Visible = true;
                ddlRegionVA.Visible = false;
                ddlRegionPA.SelectedIndex = 0;
                rfvRegionVA.Visible = false;
                rfvRegionPA.Visible = true;
            }
            Page.Validate();
        }
    }

    private bool validZipCode(string zip, string statetmp)
    {
        string state = "";
        if (statetmp == "Virginia")
            state = "VA";
        else
            state = "PA";
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        //string userId = Membership.GetUser().ProviderUserKey.ToString();
        cmd.CommandText = "SELECT * FROM ZIP WHERE Zip = " + zip + " AND State = '" + state + "'";
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
        if (counter > 0)
            return true;
        else
            return false;
    }
    protected void cuvDateOfBirthOld_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (cvDateOfBirth.IsValid)
        {
            if (Website.calculateYearsInt(args.Value) > 100)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}