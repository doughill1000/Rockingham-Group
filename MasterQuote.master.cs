using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class MasterQuote : System.Web.UI.MasterPage
{
    Quote quote = new Quote();
    SqlDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            quote = (Quote)(System.Web.HttpContext.Current.Session["quote"]);
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            conn.Open();
            String referenceNum = Request.QueryString["ReferenceNum"];
            if (referenceNum == null)
            {
                cmd.Parameters.AddWithValue("@QuoteID", quote.quoteID);
                cmd.CommandText = "GetReferenceNumber";
                reader = cmd.ExecuteReader();
                reader.Read();
                lblReference.Text = "Reference# " + reader.GetInt32(0) + " ";
                conn.Close();
            }
            else
            {
                lblReference.Text = "Reference# " + referenceNum;
            }

        }
        catch (SqlException ex)
        {
            Response.Redirect(ex.ToString());
        }

    }
    protected void imbLogo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Wepages/Home.aspx");
    }
}
