using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Windows.Forms;
using System.Data.SqlClient;

public partial class Wepages_Quote_ViewQuotes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < gvViewQuotes.Rows.Count; i++)
        {
            bool submitted = false;
            string referenceNum = gvViewQuotes.Rows[i].Cells[0].Text;

            //check to see if the quote is submitted.  if it is, the hyperlink will be disabled and the text will be: "submitted"
            SqlConnection connSubmitted = Website.getSQLConnection();
            SqlCommand cmdSubmitted = Website.getSQLCommand(connSubmitted);
            cmdSubmitted.CommandText = "SELECT * FROM quote where Reference# = " + Convert.ToInt32(referenceNum) + "AND Submitted = 1";
            cmdSubmitted.CommandType = System.Data.CommandType.Text;
            SqlDataReader readerSubmitted;
            connSubmitted.Open();
            readerSubmitted = cmdSubmitted.ExecuteReader();
            int counterSubmitted = 0;
            while (readerSubmitted.Read())
            {
                counterSubmitted++;
            }
            if (counterSubmitted > 0)
                submitted = true;
            connSubmitted.Close();

            HyperLink hyperlink = new HyperLink();
            hyperlink.Text = "Edit";
            hyperlink.NavigateUrl = "~/Wepages/Applicant.aspx/?ReferenceNum=" + referenceNum;
            if (submitted)
            {
                hyperlink.Text = "Submitted";
                hyperlink.Enabled = false;
                gvViewQuotes.Rows[i].Cells[5].Controls.Add(hyperlink);
            }
            else if (gvViewQuotes.Rows[i].Cells[4].Text == "Active")
                gvViewQuotes.Rows[i].Cells[5].Controls.Add(hyperlink);
            else
            {
                hyperlink.Enabled = false;
                gvViewQuotes.Rows[i].Cells[5].Controls.Add(hyperlink);
            }

            System.Web.UI.WebControls.Button deactivate = new System.Web.UI.WebControls.Button();
            deactivate.ID = "btnDeac";
            deactivate.Text = "Deactivate";
            deactivate.OnClientClick = "return confirm('You are Deactivating this quote.  Click Ok to confirm.');";
            deactivate.Click += new System.EventHandler((s, ea) => deactivateClicked(s, ea, referenceNum));

            System.Web.UI.WebControls.Button reqReactivate = new System.Web.UI.WebControls.Button();
            reqReactivate.ID = "btnReac";
            reqReactivate.OnClientClick = "return confirm('You are requesting to reactivate this quote.  Click Ok to confirm.');";
            reqReactivate.Text = "Request Reactivation";
            reqReactivate.Click += new System.EventHandler((s, ea) => reqReactivateClicked(s, ea, referenceNum));
            if (submitted)
            {
                
            }
            else if (gvViewQuotes.Rows[i].Cells[4].Text == "Active")
                gvViewQuotes.Rows[i].Cells[6].Controls.Add(deactivate);
            else
                gvViewQuotes.Rows[i].Cells[6].Controls.Add(reqReactivate);
            

            //go through each quote and look to see if reactivation was requested.
            //test to see if there is another manager.  If the this manager is THE ONLY manager, the system will assign his agentID to their manager columns
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "SELECT * FROM quote where Reference# = " + Convert.ToInt32(referenceNum) + " and reactivation = 1";
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
            if (counter == 1)//if the quote is requested to be reactivated, disable the button and change its text.
            {
                reqReactivate.Text = "Reactivation Requested";
                reqReactivate.Enabled = false; 
            }
            
            cmd.CommandText = "select * from QuotePDFs where pdfID = (select quoteID from quote where reference# = "+referenceNum+")";
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataReader readerPDF;
            conn.Open();
            readerPDF = cmd.ExecuteReader();
            int count = 0;
            while (readerPDF.Read())
                count++;
            try
            {
                if (count == 1)
                {
                    System.Web.UI.WebControls.Button btnView = new System.Web.UI.WebControls.Button();
                    btnView.ID = "btnView";
                    btnView.Text = "View PDF";
                    btnView.Click += new System.EventHandler((s, ea) => viewPDF(s, ea, referenceNum));
                    gvViewQuotes.Rows[i].Cells[7].Controls.Add(btnView);
                }
            }
            catch { }
            conn.Close();
        }


        if (gvViewQuotes.Rows.Count < 1)
        {
            lblSearchInput.Text = "No results found.";
        }
    }
    protected void sdsViewQuotes_OnDataBound(object sender, GridViewRowEventArgs e)
    {

        /*MembershipUser currentUser = Membership.GetUser();
        Guid userID = (Guid)currentUser.ProviderUserKey;
        e.Command.Parameters["@UserID"].Value = userID;*/
    }

    protected void sdsViewQuotes_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void deactivateClicked(object s, EventArgs ea, string num)
    {
        try
        {
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "DeactivateQuote";
            conn.Open();
            cmd.Parameters.AddWithValue("@ReferenceNum", num);
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("ViewQuotes.aspx");
        }
        catch (Exception e)
        {
            throw;
        }
    }
    protected void reqReactivateClicked(object s, EventArgs ea, string num)
    {
        try
        {
            // send email to manager in appropriate territory to request reactivation
            
            // flag quote as reactivation requested
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "RequestReactivateQuote";
            conn.Open();
            cmd.Parameters.AddWithValue("@ReferenceNum", num);
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("ViewQuotes.aspx");
        }
        catch (Exception e)
        {
            throw;
        }
    }

    protected void viewPDF(object s, EventArgs ea, string num)
    {
        
        Quote quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
        SqlConnection conn = Website.getSQLConnection();
        conn.Open();
        SqlCommand cmd = Website.getSQLCommand(conn);
        cmd.CommandType = System.Data.CommandType.Text;
        using (cmd = new SqlCommand("select pdfFile from QuotePDFs  where pdfID = (select quoteid from quote where reference# = '" + num + "')", conn))
        {

            using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.Default))
            {

                if (dr.Read())
                {
                    byte[] fileData = (byte[])dr.GetValue(0);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Reference#" + num + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(fileData);
                }
                else
                {
                    Response.Write("Sorry but we do not have any records for your pdf");
                }
            }
        }
        conn.Close();
    }
}
