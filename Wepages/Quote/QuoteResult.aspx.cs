using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail; //Used for email
using System.Web.Security;
using System.Drawing;
using System.Globalization; //Used for currency
/*PDFS*/
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Net;
using System.Data;
using System.Text;
/******/

public partial class Wepages_QuoteResult : System.Web.UI.Page
{
    string type;
    static int referenceNumber;
    protected void Page_Load(object sender, EventArgs e)
    {
        Quote quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
        if (!IsPostBack)
        {
            jsMap.Text = "";
            loadMap(Convert.ToInt32(quote.zipcode));
            if (quote.insuranceType == "Home")
            {
                Home home = (Home)(System.Web.HttpContext.Current.Session["home"]);
                double premium = calcHomeQuote(quote, home);
                CultureInfo usaCulture = new CultureInfo("en-US");
                lblannualPremAmount.Text = premium.ToString("C", usaCulture);
                double monthlyPremAmount = premium / 12.0;
                lblmonthlyPremAmount.Text = monthlyPremAmount.ToString("C", usaCulture);
                type = "home";
                htmlSteps.Text = @"<div style=""padding:15px"">
                        <link type=""text/css"" href=""Styles\StyleSheet.css"" rel=""Stylesheet"" />
                        <div class=""stepComplete"">Step One:<br />Applicant</div>
                        <div class=""stepComplete"">Step Two:<br />Property</div>
                        <div class=""stepComplete"">Step Three:<br />Coverage</div>
                        <div class=""stepComplete"">Step Four:<br />Discounts</div>
                        <div class=""stepComplete"" style=""border-color:#ffffff; border-width:5px;"">Step Five:<br />Quote Result</div>
                        </div>";
            }
            else
            {
                AutoPolicy policy = (AutoPolicy)(System.Web.HttpContext.Current.Session["autopolicy"]);
                double premium = calculateAutoQuote(quote, policy);
                CultureInfo usaCulture = new CultureInfo("en-US");
                lblannualPremAmount.Text = premium.ToString("C", usaCulture);
                double monthlyPremAmount = premium / 12.0;
                lblmonthlyPremAmount.Text = monthlyPremAmount.ToString("C", usaCulture);
                type = "auto";
                htmlSteps.Text = @"<div style=""padding:15px"">
                        <link type=""text/css"" href=""Styles\StyleSheet.css"" rel=""Stylesheet"" />
                        <div class=""stepComplete"">Step One:<br />Applicant</div>
                        <div class=""stepComplete"">Step Two:<br />General Information</div>
                        <div class=""stepComplete"">Step Three:<br />Policy</div>
                        <div class=""stepComplete"">Step Four:<br />Assignments</div>
                        <div class=""stepComplete"" style=""border-color:#ffffff; border-width:5px;"">Step Five:<br />Quote Result</div>
                        </div>";
            }

            try
            {
                //agents in region
                SqlConnection conn = Website.getSQLConnection();
                SqlCommand cmd = Website.getSQLCommand(conn);
                conn.Open();
                SqlDataReader readerRefNumber;
                cmd.CommandType = System.Data.CommandType.Text;
                //Select the reference number and display
                cmd.CommandText = "select reference# from quote where quoteID = '" + quote.quoteID + "'";
                readerRefNumber = cmd.ExecuteReader();
                readerRefNumber.Read();
                referenceNumber = readerRefNumber.GetInt32(0);
                lblQuoteRefShow.Text = referenceNumber.ToString();
                conn.Close();
                //Show the date of the quote
                lblDateShow.Text = DateTime.Today.ToString("d");
                cmd.CommandText = "select AgentID, firstName + ' ' + lastname + ', ' + agency from agent WHERE region = '" + quote.region + "' AND Agency is not null ORDER BY Agency";
                SqlDataReader reader;
                conn.Open();
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Guid id = (Guid)reader.GetGuid(0);
                    string info = reader.GetString(1);
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                    item.Text = info;
                    item.Value = id.ToString();
                    ddlAgentRegion.Items.Add(item);
                }
                conn.Close();

                SqlDataReader readerAllAgents;
                //all OTHER agents in that state
                conn.Open();
                cmd.CommandText = "select AgentID, firstName + ' ' + lastname + ', ' + agency from agent INNER JOIN Agency on agent.agency = agency.name " +/*WHERE region not in ('" + quote.region + "') AND*/ " WHERE State = '" + quote.state + "' AND Agency IS NOT NULL ORDER BY Agency";
                cmd.ExecuteNonQuery();
                readerAllAgents = cmd.ExecuteReader();

                while (readerAllAgents.Read())
                {
                    Guid id = (Guid)readerAllAgents.GetGuid(0);
                    string info = readerAllAgents.GetString(1);
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                    item.Text = info;
                    item.Value = id.ToString();
                    ddlAllAgents.Items.Add(item);
                }
                ddlAgentRegion.DataBind();
                ddlAllAgents.DataBind();
            }
            catch { };
            if (ddlAgentRegion.Items.FindByValue(quote.agentID.ToString()) != null)
            {
                ddlAgentRegion.SelectedValue = quote.agentID.ToString();
            }
            else if (ddlAllAgents.Items.FindByValue(quote.agentID.ToString()) != null)
            {
                ddlAllAgents.SelectedValue = quote.agentID.ToString();
            }
            else
            {
                ddlAllAgents.SelectedIndex = 0;
                ddlAgentRegion.SelectedIndex = 0;
            }
            if (quote.methodOfContact != "")
            {
                ddlContactMethod.Visible = true;
                ddlContactMethod.SelectedValue = quote.methodOfContact;
            }
            if (quote.contactEmail != "")
            {
                txtEmail.Visible = true;
                txtEmail.Text = quote.contactEmail;
            }
            if (quote.contactPhone != "")
            {
                txtPhone.Visible = true;
                txtPhone.Text = quote.contactPhone;
            }
            if (quote.preferredCallTime != "")
            {
                ddlPreferredCallTime.Visible = true;
                ddlPreferredCallTime.SelectedValue = quote.preferredCallTime;
            }
            
            //see if agent has been chosen
            try
            {
                SqlConnection conn = Website.getSQLConnection();
                SqlCommand cmd = Website.getSQLCommand(conn);
                conn.Close();
                conn.Open();
                SqlDataReader readerAgentID;
                cmd.CommandText = "select agentID From quote where quoteid = '" + quote.quoteID + "'";
                cmd.CommandType = System.Data.CommandType.Text;
                readerAgentID = cmd.ExecuteReader();
                readerAgentID.Read();
                Guid agentID = readerAgentID.GetGuid(0);
                quote.agentID = agentID;
                conn.Close();
                if (quote.agentID != Guid.Empty)
                {
                    conn.Open();
                    SqlDataReader readerAgentInfo;
                    cmd.CommandText = "select * From agent where agentID = '" + quote.agentID + "'";
                    cmd.CommandType = System.Data.CommandType.Text;
                    readerAgentInfo = cmd.ExecuteReader();
                    readerAgentInfo.Read();
                    string first = readerAgentInfo.GetString(4);
                    string last = readerAgentInfo.GetString(5);
                    string agency = readerAgentInfo.GetString(3);
                    string phone = readerAgentInfo.GetString(1);
                    string email = readerAgentInfo.GetString(2);
                    string info = first + " " + last + ", " + agency + "<br>" + phone + " | " + email;
                    lblAgentShow.Text = info;

                    lblAgentShow.Visible = true;
                    hylHome.Visible = true;
                    //lblAgentShowBottom.Visible = true;
                    lblAgent.Visible = true;
                    pnlAgentStuff.Visible = false;
                    btnSendToAgent.Visible = false;
                    btnQuotePDF.Visible = true;
                    btnQuotePDF.Text = "Update and View PDF";
                    /*try
                    {
                        ddlAllAgents.SelectedValue = quote.agentID.ToString();
                    }
                    catch
                    {
                        ddlAgentRegion.SelectedValue = quote.agentID.ToString();
                    }
                    
                    pnlContact.Visible = true;
                    ddlAgentRegion.Enabled = false;
                    ddlAllAgents.Enabled = false;
                    ddlContact.SelectedIndex = 2;
                    ddlContact.Enabled = false;*/
                    
                }
                conn.Close();
            }
            catch //no agentid assigned
            {
                
            }
        }
        if (type == "auto")
            lblBreadcrumbs.Text = "<a href=\"../Applicant.aspx\">Applicant</a> » <a href=\"../Auto/GeneralInformation.aspx\">General Information</a> » <a href=\"../Auto/Policy.aspx\">Policy</a> » <a href=\"../Auto/AssignmentHub.aspx\">Assignments</a> » Quote Results<br />";
        else
            lblBreadcrumbs.Text = "<a href=\"../Applicant.aspx\">Applicant</a> » <a href=\"../Home/Property.aspx\">Property</a> » <a href=\"../Home/Coverage.aspx\">Coverage</a> » <a href=\"../Home/Discounts.aspx\">Discounts</a> » Quote Results<br />";

        

        Page.Validate();
    }
    protected void ddlContact_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlContact.SelectedIndex == 2)
        {
            pnlContact.Visible = true;
        }
        else
        {
            pnlContact.Visible = false;
            pnlPhone.Visible = false;
            pnlEmail.Visible = false;
        }
    }
    protected void ddlContactMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlContactMethod.SelectedIndex == 1) //email
        {
            pnlEmail.Visible = true;
            pnlPhone.Visible = false;
        }
        else if (ddlContactMethod.SelectedIndex == 2) //phone
        {
            pnlPhone.Visible = true;
            pnlEmail.Visible = false;
        }
        else
        {
            pnlEmail.Visible = false;
            pnlPhone.Visible = false;
        }
    }
    protected void ddlAgentRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAgentRegion.SelectedIndex != 0)
        {
            ddlAllAgents.SelectedIndex = 0;
            rfvAllAgents.Visible = false;
        }
        else
        {
            if (ddlAllAgents.SelectedIndex == 0)
            {
                rfvAllAgents.Visible = true;
                rfvAgentRegion.Visible = true;
            }
        }
    }
    protected void ddlAllAgents_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAllAgents.SelectedIndex != 0)
        {
            ddlAgentRegion.SelectedIndex = 0;
            rfvAgentRegion.Visible = false;
        }
        else
        {
            if (ddlAgentRegion.SelectedIndex == 0)
            {
                rfvAllAgents.Visible = true;
                rfvAgentRegion.Visible = true;
            }
        }
    }

    protected void ddlAgentRegion_DataBound(Object sender, EventArgs e)
    {
        ddlAgentRegion.Items.Insert(0, "Select One");
        ddlAgentRegion.Items[0].Value = "";
        ddlAgentRegion.SelectedIndex = 0;
    }

    protected void ddlAllAgents_DataBound(Object sender, EventArgs e)
    {
        ddlAllAgents.Items.Insert(0, "Select One");
        ddlAllAgents.Items[0].Value = "";
        ddlAllAgents.SelectedIndex = 0;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Quote quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
        if (quote.insuranceType == "Auto")//auto
            Response.Redirect("~/Wepages/Auto/AssignmentHub.aspx");

        else
            Response.Redirect("~/Wepages/Home/Discounts.aspx");
    }
    protected void btnSendToAgent_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Quote quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);

            quote.methodOfContact = ddlContactMethod.SelectedItem.Value;
            quote.contactEmail = txtEmail.Text;
            quote.contactPhone = txtPhone.Text;
            quote.preferredCallTime = ddlPreferredCallTime.SelectedItem.Value;
            if (ddlAllAgents.SelectedIndex != 0)
            {
                quote.agentID = new Guid(ddlAllAgents.SelectedValue);
            }
            else
                quote.agentID = new Guid(ddlAgentRegion.SelectedValue);
            quote.monthlyPremium = lblmonthlyPremAmount.Text;
            quote.annualPremium = lblannualPremAmount.Text;

            Session["quote"] = quote;
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@QuoteID", quote.quoteID);
            cmd.Parameters.AddWithValue("@AgentID", quote.agentID);
            cmd.Parameters.AddWithValue("@methodOfContact", quote.methodOfContact);
            cmd.Parameters.AddWithValue("@contactEmail", quote.contactEmail);
            cmd.Parameters.AddWithValue("@contactPhone", quote.contactPhone);
            cmd.Parameters.AddWithValue("@preferredcallTime", quote.preferredCallTime);
            cmd.Parameters.AddWithValue("@monthlyPremium", quote.monthlyPremium);
            cmd.Parameters.AddWithValue("@annualPremium", quote.annualPremium);
            cmd.CommandText = "SetAgentToQuote";
            cmd.ExecuteNonQuery();
            conn.Close();

            lblSubmission.Text = "Thank you for submitting a quote. You will be contacted by an agent soon. ";
            hylReturnToQuotes.Visible = true;
            btnBack.Visible = false;
            btnSendToAgent.Visible = false;
            btnQuotePDF.Visible = true;

/*********************************************************************************************************************************************
////////////////////////////////////////////////////////////Create, Store, Email PDF/////////////////////////////////////////////////////////
/*********************************************************************************************************************************************/

            //Create PDF
            MemoryStream ms = new MemoryStream(); //Server.MapPath leads to website folder
            Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            /* (NEEDS UPDATING)*/
            //Add contents 
            doc.AddTitle("Rockingham Group");
            //Add logo
            iTextSharp.text.Image imgLogo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/logo_small.png")); //Rockingham logo
            imgLogo.SpacingBefore = 75;
            doc.Add(imgLogo); 
            
            //Add Quote Result header
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 16);
            Paragraph p = new Paragraph("\nQuote Result", times);
            p.Alignment = Element.ALIGN_CENTER;
            doc.Add(p);
            
            //Add premium amounts
            doc.Add(new Paragraph("\nMonthly Policy Premium: " + lblmonthlyPremAmount.Text + "\nAnnual Policy Premium: " + lblannualPremAmount.Text));
            
            //Add reference number and date
            doc.Add(new Paragraph("\nReference: #" + referenceNumber + "\n" + "Date: " + DateTime.Today.Date.ToString("d")));

            PdfPTable orderInfoTable = new PdfPTable(2);
            orderInfoTable.HorizontalAlignment = 0;
            orderInfoTable.SpacingBefore = 10;
            orderInfoTable.SpacingAfter = 10;
            orderInfoTable.DefaultCell.Border = 0;
            orderInfoTable.SetWidths(new int[] { 1, 4 });

            //PDF Tables
            /*orderInfoTable.AddCell(new Phrase("Order:", boldTableFont));
            orderInfoTable.AddCell(txtOrderID.Text);
            orderInfoTable.AddCell(new Phrase("Price:", boldTableFont));
            orderInfoTable.AddCell(Convert.ToDecimal(txtTotalPrice.Text).ToString("c"));

            document.Add(orderInfoTable);*/


            //CLose document, send to bit array
            writer.CloseStream = false; //Make sure stream doesn't close with doc
            doc.Close();
            byte[] bytes = ms.ToArray();
            ms.Close();

            //Insert pdf into database
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "IF NOT EXISTS(select * from QuotePDFs where pdfID = @pdfID) "
            + "BEGIN insert into QuotePDFs(pdfID, pdfFile) VALUES (@pdfID, @pdfFile) END ELSE BEGIN UPDATE QuotePDFs SET pdfFile = @pdfFile where pdfID = "
            + "@pdfID END";
            cmd.Parameters.AddWithValue("@pdfID", quote.quoteID);
            cmd.Parameters.AddWithValue("@pdfFile", bytes);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            SqlDataReader readerAgent;
            cmd.CommandText = "select email from agent where agentID = '" + quote.agentID + "'";
            readerAgent = cmd.ExecuteReader();
            readerAgent.Read();
            string email = Website.getSafeString(readerAgent, 0);
            conn.Close();
            MailMessage message = new MailMessage();
            message.To.Add(email);
            message.Subject = "Rockingham Group Quote Deliverable";
            message.From = new MailAddress("CIS484Project@gmail.com");
            message.Body = "You have been selected for a quote!  The reference number is " + referenceNumber;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");

            //Attach PDF to email
            message.Attachments.Add(new Attachment(new MemoryStream(bytes), "Reference#" + referenceNumber + ".pdf"));

            smtp.Send(message);

            //Sends email confirmation to customer with PDF attachment
            if (ddlContactMethod.SelectedIndex == 1)//email chosen
            {
                message = new MailMessage();
                message.To.Add(txtEmail.Text);
                message.Subject = "Quote Submission Confirmation";
                message.From = new MailAddress("CIS484Project@gmail.com");
                message.Body = "Thank you for submitting a quote, you will be contacted by an agent within the next week.";
                smtp = new SmtpClient("smtp.gmail.com");

                //Attach PDF to email
                message.Attachments.Add(new Attachment(new MemoryStream(bytes), "Reference#" + referenceNumber + ".pdf"));

                smtp.Send(message);
            }

            else //phone chosen
            {

            }

            
        }
        else
        {
            lblSubmission.Text = "";
            hylReturnToQuotes.Visible = false;
        }
    }
    //Opens a quote from the database 
    protected void btnQuotePDF_Click(object sender, EventArgs e)
    {
        Quote quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
        SqlConnection conn = Website.getSQLConnection();
        SqlCommand cmd = Website.getSQLCommand(conn);
        //Create PDF
        MemoryStream ms = new MemoryStream(); //Server.MapPath leads to website folder
        Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
        PdfWriter writer = PdfWriter.GetInstance(doc, ms);
        doc.Open();

        /* (NEEDS UPDATING)*/
        //Add contents 
        doc.AddTitle("Rockingham Group");
        //Add logo
        iTextSharp.text.Image imgLogo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/logo_small.png")); //Rockingham logo
        imgLogo.SpacingBefore = 75;
        doc.Add(imgLogo);

        //Add Quote Result header
        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 16);
        Paragraph p = new Paragraph("\nQuote Result", times);
        p.Alignment = Element.ALIGN_CENTER;
        doc.Add(p);

        //Add premium amounts
        doc.Add(new Paragraph("\nMonthly Policy Premium: " + lblmonthlyPremAmount.Text + "\nAnnual Policy Premium: " + lblannualPremAmount.Text));

        //Add reference number and date
        doc.Add(new Paragraph("\nReference: #" + referenceNumber + "\n" + "Date: " + DateTime.Today.Date.ToString("d")));

        PdfPTable orderInfoTable = new PdfPTable(2);
        orderInfoTable.HorizontalAlignment = 0;
        orderInfoTable.SpacingBefore = 10;
        orderInfoTable.SpacingAfter = 10;
        orderInfoTable.DefaultCell.Border = 0;
        orderInfoTable.SetWidths(new int[] { 1, 4 });

        //PDF Tables
        /*orderInfoTable.AddCell(new Phrase("Order:", boldTableFont));
        orderInfoTable.AddCell(txtOrderID.Text);
        orderInfoTable.AddCell(new Phrase("Price:", boldTableFont));
        orderInfoTable.AddCell(Convert.ToDecimal(txtTotalPrice.Text).ToString("c"));

        document.Add(orderInfoTable);*/


        //CLose document, send to bit array
        writer.CloseStream = false; //Make sure stream doesn't close with doc
        doc.Close();
        byte[] bytes = ms.ToArray();
        ms.Close();

        //Insert pdf into database
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "IF NOT EXISTS(select * from QuotePDFs where pdfID = @pdfID) "
        + "BEGIN insert into QuotePDFs(pdfID, pdfFile) VALUES (@pdfID, @pdfFile) END ELSE BEGIN UPDATE QuotePDFs SET pdfFile = @pdfFile where pdfID = "
        + "@pdfID END";
        cmd.Parameters.AddWithValue("@pdfID", quote.quoteID);
        cmd.Parameters.AddWithValue("@pdfFile", bytes);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Guid quoteID = (Guid)(HttpContext.Current.Session["quoteID"]);
        cmd.CommandType = System.Data.CommandType.Text;
        conn.Open();

        using (cmd = new SqlCommand("select pdfFile from QuotePDFs  where pdfID = '" + quoteID  + "'" , conn))
        {

            using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.Default))
            {

                if (dr.Read())
                {
                    byte[] fileData = (byte[])dr.GetValue(0);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Reference#" + referenceNumber + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(fileData);
                }
                else
                {
                    Response.Write("Sorry but we do not have any records for your pdf");
                }
            }
        }
    }

    /********************************************************************************************************************
     * ////////////////////////////////////////CALCULATE HOME QUOTE/////////////////////////////////////////////////////
     * ******************************************************************************************************************/

    protected double calcHomeQuote(Quote quote, Home home)
    {
        double quoteAmount = 500;


        //Credit Rating
        if (Convert.ToInt32(quote.creditRating) >= 720 & (Convert.ToInt32(quote.creditRating) <= 850))
        {
            quoteAmount += 5;
        }
        else if (Convert.ToInt32(quote.creditRating) >= 690 & (Convert.ToInt32(quote.creditRating) < 720))
        {
            quoteAmount += 50;
        }
        else if (Convert.ToInt32(quote.creditRating) >= 650 & (Convert.ToInt32(quote.creditRating) < 690))
        {
            quoteAmount += 100;
        }
        else if (Convert.ToInt32(quote.creditRating) >= 350 & (Convert.ToInt32(quote.creditRating) < 650))
        {
            quoteAmount += 200;
        }

        //Region of home location
        switch (home.region)
        {
            case "Northern Virginia":
                quoteAmount += 50;
                break;
            case "Central Virginia":
                quoteAmount += 11;
                break;
            case "Southern Virginia":
                quoteAmount += 5;
                break;
            case "Chesapeake Bay":
                quoteAmount += 40;
                break;
            case "Coastal Virginia - Hampton Road":
                quoteAmount += 42;
                break;
            case "Coastal Virginia - Eastern Shore":
                quoteAmount += 51;
                break;
            case "Shenandoah Valley":
                quoteAmount += 5;
                break;
            case "Blue Ridge Highlands":
                quoteAmount += 5;
                break;
            case "Heart of Appalachia":
                quoteAmount += 3;
                break;
        }

        //Year home was built

        if (Convert.ToInt32(home.yearBuilt) >= 2000)
        {
            quoteAmount += 50;
        }
        else if ((Convert.ToInt32(home.yearBuilt) < 2000 & (Convert.ToInt32(home.yearBuilt) >= 1975)))
        {
            quoteAmount += 100;
        }
        else if ((Convert.ToInt32(home.yearBuilt) < 1975 & (Convert.ToInt32(home.yearBuilt) >= 1950)))
        {
            quoteAmount += 150;
        }
        else if ((Convert.ToInt32(home.yearBuilt) < 1949) & (Convert.ToInt32(home.yearBuilt) >= 1900))
        {
            quoteAmount += 200;
        }
        else if (Convert.ToInt32(home.yearBuilt) < 1900)
        {
            quoteAmount += 300;
        }

        //Construction Type
        if (home.constructionType == "Brick/Block/Stone")
        {
            quoteAmount += 15;
        }
        else if (home.constructionType == "Wood/Vinyl Siding & Log")
        {
            quoteAmount += 30;
        }

        //Distance to nearest fire station & fire hydrant within 1,000 ft
        if (home.distanceToFireStation == "Within 5 miles")
        {
            quoteAmount -= 30;
        }
        else if (home.fireHydrant)
        {
            quoteAmount -= 20;
        }
        else if (home.distanceToFireStation == "Over 5 miles")
        {
            quoteAmount += 100;
        }

        //Newly Purchased
        if (home.newlyPurchased)
        {
            quoteAmount -= 15;
        }
        else
        {
            quoteAmount += 15;
        }

        //If not newly purchased, then current insurance on home
        if (home.currentInsurance == "Yes with Rockingham Group")
        {
            quoteAmount -= 5;
        }
        else if (home.currentInsurance == "Yes with another insurance company")
        {
            quoteAmount -= 10;
        }
        else if (home.currentInsurance == "No prior home cancelled")
        {
            quoteAmount += 10;
        }
        else if (home.currentInsurance == "No insurance on current home")
        {
            quoteAmount += 20;
        }

        //Number of paid losses in past three years
        if (home.paidLosses3Years == "0")
        {
            quoteAmount -= 25;
        }
        else if (home.paidLosses3Years == "1")
        {
            quoteAmount += 35;
        }
        else if (home.paidLosses3Years == "2")
        {
            quoteAmount += 50;
        }
        else if (home.paidLosses3Years == "3")
        {
            quoteAmount += 100;
        }
        else if (home.paidLosses3Years == "4")
        {
            quoteAmount += 150;
        }

        //coverage A,B,C,D
        quoteAmount += (Double.Parse(home.desiredInsurance) - 50000) * .004;

        //coverage L
        switch (home.coverageL)
        {
            case 100000:
                quoteAmount += 0;
                break;
            case 300000:
                quoteAmount += 15;
                break;
            case 500000:
                quoteAmount += 30;
                break;
        }

        //coverage M
        switch (home.coverageM)
        {
            case 1000:
                quoteAmount += 0;
                break;
            case 2000:
                quoteAmount += 5;
                break;
            case 3000:
                quoteAmount += 10;
                break;
            case 4000:
                quoteAmount += 15;
                break;
            case 5000:
                quoteAmount += 36;
                break;
        }

        //Policy Deductible
        switch (home.policyDeductible)
        {
            case 500:
                quoteAmount -= 15;
                break;
            case 1000:
                quoteAmount -= 30;
                break;
            case 2500:
                quoteAmount -= 50;
                break;
            case 5000:
                quoteAmount -= 80;
                break;
            case 10000:
                quoteAmount -= 130;
                break;
        }

        //Policy Discounts
        if (home.smokeAlarms) //Smoke Alarms
        {
            quoteAmount -= 5;
        }
        else
        {
            quoteAmount += 5;
        }
        if (home.fireExtinguishers) //Fire Extinguishers
        {
            quoteAmount -= 5;
        }
        else
        {
            quoteAmount += 13;
        }
        if (home.deadBolts) //Deadbolts
        {
            quoteAmount -= 13;
        }
        else
        {
            quoteAmount += 5;
        }
        if (home.fireAlarmMonitoringCenter) //Fire alarm report to central center
        {
            quoteAmount -= 5;
        }
        else
        {
            quoteAmount += 13;
        }
        if (home.burglarAlarm) //Burglar alarm report to central center
        {
            quoteAmount -= 13;
        }
        else
        {
            quoteAmount += 13;
        }
        if (home.sprinklerSystem) //Sprinkler System on all floors
        {
            quoteAmount -= 13;
        }
        else
        {
            quoteAmount += 13;
        }


        //Auto Policy with Rockingham 
        //THIRD OPTION NEEDS TO BE UPDATED WHEN THE WEBSITE UPDATES
        if (home.autoPolicyRockinghamAuto == "No")
        {
            quoteAmount += 25;
        }
        else if (home.autoPolicyRockinghamAuto == "Yes")
        {
            quoteAmount = quoteAmount * .90;
        }
        else if (home.autoPolicyRockinghamAuto == "No, but planning to my coverage to Rockingham")
        {
            quoteAmount = (quoteAmount * .92);
        }

        return quoteAmount;
    }

    /********************************************************************************************************************
    * ////////////////////////////////////////CALCULATE AUTO QUOTE/////////////////////////////////////////////////////
    * ******************************************************************************************************************/

    protected double calculateAutoQuote(Quote quote, AutoPolicy autoPolicy)
    {
        double quoteResult = 650;

        switch (quote.region)
        {
            case "Northern Virginia":
                quoteResult += 50;
                break;
        }

        //discount if multiple drivers share a car
        if (Vehicle.vehicles.Count > Driver.drivers.Count)
        {
            quoteResult -= 25;
        }
        else if (Driver.drivers.Count > Vehicle.vehicles.Count)
        {
            quoteResult -= 25;
        }

        //Credit Rating
        if (Convert.ToInt32(quote.creditRating) >= 720 & (Convert.ToInt32(quote.creditRating) <= 850))
        {
            quoteResult += 5;
        }
        else if (Convert.ToInt32(quote.creditRating) >= 690 & (Convert.ToInt32(quote.creditRating) < 720))
        {
            quoteResult += 50;
        }
        else if (Convert.ToInt32(quote.creditRating) >= 650 & (Convert.ToInt32(quote.creditRating) < 690))
        {
            quoteResult += 100;
        }
        else if (Convert.ToInt32(quote.creditRating) >= 350 & (Convert.ToInt32(quote.creditRating) < 650))
        {
            quoteResult += 200;
        }


        //Accident Forgiveness - Per Quote
        if (autoPolicy.accidentForgiveness == true)
        {
            quoteResult += 200;
        }
        else
        {
            quoteResult += 0;
        }

        //Child Under 6 - Per Quote
        if (autoPolicy.ageUnderSix == true)
        {
            quoteResult -= 25;
        }
        else
        {
            quoteResult += 10;
        }

        //Home Policy with Rockingham - Per Quote
        switch (autoPolicy.homePolicy)
        {
            case "No":
                quoteResult += 0;
                break;
            case "Yes":
                quoteResult -= (quoteResult * .08);
                break;
            case "No, but planning to my coverage to Rockingham":
                quoteResult -= (quoteResult * .05);
                break;
        }

        //Number of years with current policy - Per Quote
        switch (autoPolicy.yearsCurrentPolicy)
        {
            case "0":
                quoteResult += 200;
                break;
            case "1":
                quoteResult += 150;
                break;
            case "2":
                quoteResult += 100;
                break;
            case "3":
                quoteResult += 50;
                break;
            case "4":
                quoteResult += 25;
                break;
            case "5":
                quoteResult += 5;
                break;
            case "6":
                quoteResult -= -10;
                break;
            case "7":
                quoteResult -= 50;
                break;
            case "8":
                quoteResult -= 60;
                break;
            case "9":
                quoteResult -= 70;
                break;
            case "10":
                quoteResult -= 100;
                break;
        }



        //Bodily Injury Limit - Per Quote
        switch (autoPolicy.bodilyInjuryLimit)
        {
            case "$25,000/$50,000":
                quoteResult += 100;
                break;
            case "$50,000/$100,000":
                quoteResult += 125;
                break;
            case "$100,000/$200,000":
                quoteResult += 150;
                break;
            case "$100,000/$300,000":
                quoteResult += 165;
                break;
            case "$300,000/$300,000":
                quoteResult += 180;
                break;
            case "$250,000/$500,000":
                quoteResult += 195;
                break;
            case "$500,000/$500,000":
                quoteResult += 215;
                break;
            case "$70,000":
                quoteResult += 130;
                break;
            case "$75,000":
                quoteResult += 135;
                break;
            case "$100,000":
                quoteResult += 155;
                break;
            case "$200,000":
                quoteResult += 160;
                break;
            case "$300,000":
                quoteResult += 175;
                break;
        }

        //Property Damage Limit - Per Quote
        switch (autoPolicy.propertyDamageLimit)
        {
            case "$20,000":
                quoteResult += 100;
                break;
            case "$25,000":
                quoteResult += 110;
                break;
            case "$40,000":
                quoteResult += 130;
                break;
            case "$50,000":
                quoteResult += 180;
                break;
            case "$100,000":
                quoteResult += 250;
                break;
            case "$150,000":
                quoteResult += 265;
                break;
            case "$200,000":
                quoteResult += 285;
                break;
            case "$250,000":
                quoteResult += 300;
                break;
            case "$500,000":
                quoteResult += 350;
                break;
        }

        //Medical Expenses - Per Quote
        switch (autoPolicy.medicalExpense)
        {
            case "none":
                quoteResult += 0;
                break;
            case "$1,000":
                quoteResult += 25;
                break;
            case "$2,000":
                quoteResult += 30;
                break;
            case "$5,000":
                quoteResult += 40;
                break;
            case "$10,000":
                quoteResult += 50;
                break;
            case "$25,000":
                quoteResult += 65;
                break;
            case "$50,000":
                quoteResult += 80;
                break;
        }

        //IncomeLoss - Per Quote
        if (autoPolicy.incomeLoss == false)
            quoteResult -= 15;

        else
            quoteResult += 30;


        ////////////////////////////////////////////////// VEHICLE //////////////////////////////////////////////////

        for (int v = 0; v < Assignment.assignments.Count; v++)
        {
            Guid vehicleID = Assignment.assignments[v].vehicleID;
            Vehicle vehicle = new Vehicle();
            for (int j = 0; j < Vehicle.vehicles.Count; j++)
            {
                if (Vehicle.vehicles[j].vehicleID == vehicleID)
                {
                    vehicle = Vehicle.vehicles[j];
                    break;
                }
            }

            //Other than collision deductible - Per Quote
            switch (vehicle.otherThanCollisionDeductible)
            {
                case "No Cov":
                    quoteResult += 75;
                    break;
                case "$100":
                    quoteResult += 60;
                    break;
                case "$250":
                    quoteResult += 45;
                    break;
                case "$500":
                    quoteResult += 30;
                    break;
                case "$1000":
                    quoteResult += 15;
                    break;
                case "$2500":
                    quoteResult += 5;
                    break;
            }

            //Collision deductible - Per Quote
            switch (vehicle.collisionDeductible)
            {
                case "No Cov":
                    quoteResult += 75;
                    break;
                case "$100":
                    quoteResult += 60;
                    break;
                case "$250":
                    quoteResult += 45;
                    break;
                case "$500":
                    quoteResult += 30;
                    break;
                case "$1000":
                    quoteResult += 15;
                    break;
                case "$2500":
                    quoteResult += 5;
                    break;
            }

            //Rental Reimbursement Limit - Per Quote
            switch (vehicle.rentalReimbursementLimit)
            {
                case "$600":
                    quoteResult += 15;
                    break;
                case "$900":
                    quoteResult += 20;
                    break;
                case "$1200":
                    quoteResult += 25;
                    break;
                case "$1500":
                    quoteResult += 30;
                    break;
            }


            // LOOP FOR EACH VEHICLE

            //Vehicle models
            if (vehicle.model == "ESCALADE" || vehicle.model == "CTS" || vehicle.model == "CORVETTE" || vehicle.model == "CAMARO" || vehicle.model == "MUSTANG")
            {
                quoteResult += 100;
            }
            else if (vehicle.model == "CAMRY" || vehicle.model == "PILOT" || vehicle.model == "TOWN & COU" || vehicle.model == "ODYSSEY")
            {
                quoteResult -= 100;
            }

            //Vehile makes
            if (vehicle.make == "Bmw" || vehicle.model == "Jaguar" || vehicle.model == "Lexus" || vehicle.model == "Mazda" || vehicle.model == "Mercedez Benz" || vehicle.model == "Porsche" || vehicle.model == "Rover" || vehicle.model == "Lotus")
            {
                quoteResult += 100;
            }
            else if (vehicle.make == "Volvo" || vehicle.make == "Volkswagen" || vehicle.make == "Kia")
            {
                quoteResult -= 100;
            }




            //AntiTheft - Per Vehicle
            switch (vehicle.antiTheft)
            {
                case "No":
                    quoteResult += 150;
                    break;
                case "Alarm Only":
                    quoteResult += 50;
                    break;
                case "Passive & Active Device":
                    quoteResult -= 150;
                    break;
            }

            //4Wheel Driver - Per Vehicle
            if (vehicle.antiLockBrake == "4-wheel")
            {
                quoteResult -= 25;
            }
            else
            {
                quoteResult += 25;
            }


            //Vehicle Airbags - Per Vehicle
            switch (vehicle.airbags)
            {
                case "No":
                    quoteResult += 150;
                    break;
                case "Driver Only Airbag":
                    quoteResult += 50;
                    break;
                case "Passenger Airbag": //This should be Driver + Passenger
                    quoteResult -= 100;
                    break;
            }

            //Vehicle Away At School - Per Vehicle
            switch (vehicle.awayAtSchool)
            {
                case "No":
                    quoteResult += 0;
                    break;
                case "In-State":
                    quoteResult += 25;
                    break;
                case "Out-of State":
                    quoteResult += 40;
                    break;
            }

            //Population of Out Of State college - Per Vehicle
            switch (vehicle.outStateCollegePop)
            {
                case "less than 30,000":
                    quoteResult += 5;
                    break;
                case "30,000-249,999":
                    quoteResult += 10;
                    break;
                case "325,000 or greater":
                    quoteResult += 15;
                    break;
            }

            //Vehicle garaged at current address - Per Vehicle
            if (vehicle.garaged == true)
            {
                quoteResult += 5;

            }
            else
            {
                quoteResult += 15;
            }

            //Vehicle Usage - Per Vehicle
            switch (vehicle.vehicleUsage)
            {
                case "Pleasure (No Work Use)":
                    quoteResult += 50;
                    break;
                case "Work Use (Drive to work or schoool)":
                    quoteResult += 100;
                    break;
                case "Business Use & Farm Use":
                    quoteResult += 180;
                    break;
            }

        }

        ///////////////////////////////////////////////////// DRIVER //////////////////////////////////////////////////
        for (int v = 0; v < Assignment.assignments.Count; v++)
        {
            Guid driverID = Assignment.assignments[v].driverID;
            Driver driver = new Driver();
            for (int j = 0; j < Driver.drivers.Count; j++)
            {
                if (Driver.drivers[j].driverID == driverID)
                {
                    driver = Driver.drivers[j];
                    break;
                }
            }

            //Number of years licensed - Per Driver
            if (driver.yearLicensed <= 2)
            {
                quoteResult += 700;
            }
            else if (driver.yearLicensed > 2 & driver.yearLicensed <= 5)
            {
                quoteResult += 250;
            }
            else if (driver.yearLicensed > 5 & driver.yearLicensed <= 10)
            {
                quoteResult += 100;
            }
            else if (driver.yearLicensed > 10 & driver.yearLicensed <= 16)
            {
                quoteResult += 0;
            }
            else if (driver.yearLicensed > 16 & driver.yearLicensed <= 21)
            {
                quoteResult -= 50;
            }
            else if (driver.yearLicensed >= 22)
            {
                quoteResult -= 150;
            }


            //Accidents, tickets, or violoation in last 3 years - Per Driver
            if (driver.violationsLast3Years == true)
            {
                quoteResult += 300;
            }
            else
            {
                quoteResult -= 50;
            }

            //Age - Per Driver
            if (driver.age > 15 & driver.age <= 24)
            {
                quoteResult += 1300;
            }
            if (driver.age > 24 & driver.age <= 29)
            {
                quoteResult += 650;
            }
            if (driver.age > 29 & driver.age <= 39)
            {
                quoteResult += 325;
            }
            if (driver.age > 39 & driver.age <= 49)
            {
                quoteResult += 125;
            }
            if (driver.age > 49 & driver.age <= 59)
            {
                quoteResult += 0;
            }
            if (driver.age > 60)
            {
                quoteResult -= 50;
            }

            //Marital status - Per Driver
            switch (driver.maritalStatus)
            {
                case "Single":
                    quoteResult += 20;
                    break;
                case "Married":
                    quoteResult += 5;
                    break;

            }

            //Gender - Per Driver
            switch (driver.maritalStatus)
            {
                case "Male":
                    quoteResult += 20;
                    break;
                case "Female":
                    quoteResult += 5;
                    break;
            }

            //Quote Minimum
            if (quoteResult < 100)
            {
                quoteResult = 100;
            }
        }
        return quoteResult;
    }

    protected void loadMap(int zipcode)
    {
        try
        {
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            Quote quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
            cmd.CommandText = "SELECT * FROM AGENCY WHere state = '" + quote.state + "'";
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataReader rdrAgencies;
            conn.Open();
            rdrAgencies = cmd.ExecuteReader();

            string markers = "";
            string mylatlng = getLatLng(zipcode);
            // applicant's marker
            markers += @" var coords = new google.maps.LatLng(" + mylatlng + @");
                var image = '../../Images/map_marker_home.png';
                var markerHome = new google.maps.Marker({
                    position: coords,
                    map: map,
                    title: markerHome,
                    icon: image
                }); 
                var agency_icon = '../../Images/map_agency_icon.png';";
            int counter = 0;
            while (rdrAgencies.Read())
            {
                string agencyName = rdrAgencies.GetString(0);
                //agencyName = agencyName.Substring(0, agencyName.IndexOf(" "));
                string streetaddress = Website.getSafeString(rdrAgencies, 1);
                string city = rdrAgencies.GetString(2);
                string state = rdrAgencies.GetString(3);
                string zip = rdrAgencies.GetString(4);

                string latlng = getLatLng(Convert.ToInt32(zip));

                //agency markers (looped)
                markers += @" var contentString" + counter + @" = '<div id=""test""><p><strong>Agency Name: " + agencyName + @"</strong></p><p>" + streetaddress + @"</p><p>" + city + ", " + state + " " + zip + @"</p><img src=""../../Images/stars.png""<p></p></div>';
            
                
                var iw" + counter + @" = new google.maps.InfoWindow({
                    content: contentString" + counter + @"
                });
            
                var coords = new google.maps.LatLng(" + latlng + @");
                var marker" + counter + @" = new google.maps.Marker({
                    position: coords,
                    map: map,
                    icon: agency_icon,
                    title: '" + counter + @"'
                });
                var circle" + counter + @" = new google.maps.Circle({
                  center:coords,
                  radius:80467.2,
                  strokeColor:""#0000FF"",strokeOpacity:0.9,strokeWeight:1,fillColor:""#0000FF"",fillOpacity:0.1
                });
                circle" + counter + @".setMap(map);
                google.maps.event.addListener(marker" + counter + @", 'click', function () {
                    
                    if (infowindow) infowindow.close();
                      infowindow = new google.maps.InfoWindow({content: contentString" + counter + @"});
                      infowindow.open(map, marker" + counter + @");
                    map.position = marker" + counter + @".position;
                    
                });";
                counter++;
            }
            conn.Close();

            
            string map = @"<script>var infowindow; function initialize() {
            var myLatlng = new google.maps.LatLng(" + mylatlng + ");" +
                @"var mapOptions = {
                zoom: 7,
                center: myLatlng
            };

            var map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);";

            
            map += markers;

            map += @" }
            google.maps.event.addDomListener(window, 'load', initialize);

            
            </script>";
            jsMap.Text = map;
        }
        catch (Exception excp)
        {
            throw;
        }
    }

    protected string getLatLng(int postalcode)
    {
            string url = "http://maps.google.com/maps/api/geocode/xml?address=" + postalcode + "&sensor=false";
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    try
                    {
                        foreach (DataRow row in dsResult.Tables["result"].Rows)
                        {
                            string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                            DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                            string latitude = location["lat"].ToString();
                            string longitude = location["lng"].ToString();
                            return latitude + ", " + longitude;
                        }
                    }
                    catch { return "38.4661199, -78.7888860"; }
                }
            }
            return "38.4661199, -78.7888860";
        
    }
}