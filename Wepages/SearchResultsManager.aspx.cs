using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Wepages_SearchResultsManager : System.Web.UI.Page
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

            System.Web.UI.WebControls.Button reactivate = new System.Web.UI.WebControls.Button();
            reactivate.ID = "btnReac";
            reactivate.OnClientClick = "return confirm('You are Reactivating this quote.  Click Ok to confirm.');";
            reactivate.Text = "Reactivate";
            reactivate.Click += new System.EventHandler((s, ea) => reactivateClicked(s, ea, referenceNum));
            if (submitted)
            {

            }
            else if (gvViewQuotes.Rows[i].Cells[4].Text == "Active")
                gvViewQuotes.Rows[i].Cells[6].Controls.Add(deactivate);
            else
                gvViewQuotes.Rows[i].Cells[6].Controls.Add(reactivate);

            /*System.Web.UI.WebControls.Button btnView = new System.Web.UI.WebControls.Button();
            btnView.ID = "btnView";
            btnView.Text = "View PDF";
            btnView.Click += new System.EventHandler((s, ea) => viewPDF(s, ea, referenceNum));
            gvViewQuotes.Rows[i].Cells[7].Controls.Add(btnView);*/

            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "select * from QuotePDFs where pdfID = (select quoteID from quote where reference# = " + referenceNum + ")";
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

        for (int i = 0; i < gvReactivate.Rows.Count; i++)
        {
            string referenceNum = gvReactivate.Rows[i].Cells[0].Text;

            System.Web.UI.WebControls.Button deactivate = new System.Web.UI.WebControls.Button();
            deactivate.ID = "btnDeac";
            deactivate.Text = "Deactivate";
            deactivate.OnClientClick = "return confirm('You are Deactivating this quote.  Click Ok to confirm.');";
            deactivate.Click += new System.EventHandler((s, ea) => deactivateClicked(s, ea, referenceNum));

            System.Web.UI.WebControls.Button reactivate = new System.Web.UI.WebControls.Button();
            reactivate.ID = "btnReac";
            reactivate.OnClientClick = "return confirm('You are Reactivating this quote.  Click Ok to confirm.');";
            reactivate.Text = "Reactivate";
            reactivate.Click += new System.EventHandler((s, ea) => reactivateClicked(s, ea, referenceNum));
            if (gvReactivate.Rows[i].Cells[4].Text == "Active")
                gvReactivate.Rows[i].Cells[5].Controls.Add(deactivate);
            else
                gvReactivate.Rows[i].Cells[5].Controls.Add(reactivate);

            /*System.Web.UI.WebControls.Button btnView = new System.Web.UI.WebControls.Button();
            btnView.ID = "btnView";
            btnView.Text = "View";
            btnView.Click += new System.EventHandler((s, ea) => viewPDF(s, ea, referenceNum));
            gvReactivate.Rows[i].Cells[6].Controls.Add(btnView);*/

            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "select * from QuotePDFs where pdfID = (select quoteID from quote where reference# = " + referenceNum + ")";
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
                    gvReactivate.Rows[i].Cells[6].Controls.Add(btnView);
                }
            }
            catch { }
            conn.Close();
        }

        if (gvReactivate.Rows.Count < 1)
        {
            lblReqQuotes.Text = "No quotes to be reactivated.";
        }
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
            Response.Redirect("SearchResultsManager.aspx");
        }
        catch (Exception e)
        {
            throw;
        }
    }
    protected void reactivateClicked(object s, EventArgs ea, string num)
    {
        try
        {
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "ReactivateQuote";
            conn.Open();
            cmd.Parameters.AddWithValue("@ReferenceNum", num);
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("SearchResultsManager.aspx");
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