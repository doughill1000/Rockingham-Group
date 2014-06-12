using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Net.Mail;

public partial class Wepages_PDFTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        //http://www.4guysfromrolla.com/articles/030911-1.aspx

        // Create a Document object
        var document = new Document(PageSize.A4, 50, 50, 25, 25);

        // Create a new PdfWriter object, specifying the output stream
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);

        // Open the Document for writing
        document.Open();

        var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/logo_small.png"));
        logo.SetAbsolutePosition(400, 760);
        document.Add(logo);

        // Create a new Paragraph object with the text, "Hello, World!"
        Paragraph welcomeParagraph = new Paragraph("Hello, World!");

        // Add the Paragraph object to the document
        document.Add(welcomeParagraph);

        document.Close();

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment;filename=test.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.BinaryWrite(output.ToArray());

        /*System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        //message.To.Add("");
        message.To.Add("hansonwg@dukes.jmu.edu");
        message.Subject = "Quote Submission Confirmation";
        message.From = new MailAddress("CIS484Project@gmail.com");
        message.Body = "Thank you for submitting a quote, you will be contacted by an agent within the next week.";
        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        message.Attachments.Add(new Attachment (new MemoryStream(output.ToArray()), "Reference#.pdf"));
        smtp.Send(message);*/
        

        


    }

}