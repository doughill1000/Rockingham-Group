using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;
using System.Drawing;

public partial class Wepages_Authentication_Password_RetrieveUserName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblResponse.Visible = false;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblResponse.Visible = true;
        string username =  Membership.GetUserNameByEmail(txtEmailAddress.Text);
        if (username == null)
        {
            lblResponse.ForeColor = Color.Red;
            lblResponse.Text = "E-mail address " + Server.HtmlEncode(txtEmailAddress.Text) + " was not found. Please try again.";
        }
        else
        {
            MailMessage message = new MailMessage();
            message.To.Add(Membership.GetUser(username).Email);
            message.Subject = "UserName Retrieval";
            message.From = new MailAddress("CIS484Project@gmail.com");
            message.Body = "Your username is " + Server.HtmlEncode(username);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Send(message);
            lblResponse.ForeColor = Color.Black;
            lblResponse.Text = "Your username has been sent to you in an email.";
        }
    }
}