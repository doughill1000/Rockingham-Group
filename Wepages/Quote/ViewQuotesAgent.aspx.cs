using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.Configuration;

public partial class Wepages_ViewQuotesAgent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        Guid userID = new Guid(Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey.ToString());
        Session["UserID"] = userID;

       /* try
        {
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);

            SqlDataSource sdsViewQuotes = new SqlDataSource();
            //sdsViewQuotes.SelectCommandType = SqlDataSourceCommandType.Text;
            sdsViewQuotes.SelectCommand = "SELECT reference#, InsuranceType, LastName + ', ' + FirstName as Name, DateCreated, "
	        + "case(active) When 0 THEN 'Inactive' When 1 THEN 'Active'END as Status "
			+ "from Quote "
			+ "WHERE Reactivation IS NULL "
			+ "AND ACTIVE = 1 "
			+ "AND AgentID = '"+userID+"' ORDER BY Reference# ASC";
            //cmd.CommandText = "SELECT * FROM AGENT WHERE AGENTID = '{a013e5db-9105-4912-ba8b-4fc8ba743cd7}'";
            //sdsViewQuotes.SelectParameters.Add("@UserID", Session["UserID"].ToString());
            sdsViewQuotes.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;  
            sdsViewQuotes.ID = "sdsViewQuotes";
            Page.Controls.Add(sdsViewQuotes);
            sdsViewQuotes.Select(DataSourceSelectArguments.Empty);   
            //sdsViewQuotes.DataBind();

            gvViewQuotes.DataSourceID = "sdsViewQuotes";
            gvViewQuotes.DataBind();
        }
        catch
        {
            throw;
        }*/
        
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
                gvViewQuotes.Rows[i].Cells[4].Controls.Add(hyperlink);
            }
            else
                gvViewQuotes.Rows[i].Cells[4].Controls.Add(hyperlink);

            // add view pdf btn
            /*System.Web.UI.WebControls.Button btnView = new System.Web.UI.WebControls.Button();
            btnView.ID = "btnView";
            btnView.Text = "View PDF";
            btnView.Click += new System.EventHandler((s, ea) => viewPDF(s, ea, referenceNum));
            gvViewQuotes.Rows[i].Cells[5].Controls.Add(btnView);*/
            
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
                    gvViewQuotes.Rows[i].Cells[5].Controls.Add(btnView);
                }
            }
            catch { }
            conn.Close();

            // add view pdf btn
            System.Web.UI.WebControls.Button btnFinalize = new System.Web.UI.WebControls.Button();
            btnFinalize.ID = "btnFinalize";
            btnFinalize.Text = "Finalize";
            btnFinalize.OnClientClick = "return confirm('You are Finalizing this quote.  Click Ok to confirm.');";
            btnFinalize.Click += new System.EventHandler((s, ea) => btnFinalize_Click(s, ea, referenceNum));
            if(!submitted)
                gvViewQuotes.Rows[i].Cells[6].Controls.Add(btnFinalize);
        }


        if (gvViewQuotes.Rows.Count < 1)
        {
            
            try
            {
                lblSearchInput.Text = "No results found.  Please fill out your Agency Information if you have not done so.";
            }
            catch { }
            
        }
    }
    protected void sdsViewQuotes_OnDataBound(object sender, GridViewRowEventArgs e)
    {

        /*MembershipUser currentUser = Membership.GetUser();
        Guid userID = (Guid)currentUser.ProviderUserKey;
        e.Command.Parameters["@UserID"].Value = userID;*/
    }
    protected void btnFinalize_Click(object s, EventArgs ea, string num)
    {
        try
        {
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            conn.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE Quote set submitted = 1 WHERE reference# = " + Convert.ToInt32(num);
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("ViewQuotesAgent.aspx");
        }
        catch { }
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