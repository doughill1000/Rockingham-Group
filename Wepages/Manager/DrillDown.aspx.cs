using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;

public partial class Wepages_DrillDown : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        total.Visible = true;
        total.ChartAreas["total"].AxisX.Interval = 1;
        total.ChartAreas["total"].AxisX.Title = "Insurance Type";
        total.ChartAreas["total"].AxisY.Title = "Total Quotes";
        total.Titles["InitialChart"].Text = "Total Auto Quotes vs. Total Home Quotes";
        total.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
        foreach (DataPoint data in this.total.Series["Series1"].Points)
        {
            data.PostBackValue = "#VALX,#VALY";
        }
    }

    protected void On_Click(object sender, ImageMapEventArgs e)
    {
        String[] postback = e.PostBackValue.Split(',');

        if (sender.Equals(total))
        {
            clearParameters();
            yearChart.Visible = true;
            YearDataSource.SelectParameters["Quote"].DefaultValue = postback[0];
            YearDataSource.DataBind();
            yearChart.Titles["yearChart"].Text = postback[0];
            yearChart.DataBind();
        }
        else if (sender.Equals(yearChart))
        {
            clearParameters();
            yearChart.Visible = true;
            quarterChart.Visible = true;
            String quote = YearDataSource.SelectParameters["Quote"].DefaultValue;
            QuarterDataSource.SelectParameters["Quote"].DefaultValue = quote;
            QuarterDataSource.SelectParameters["Year"].DefaultValue = postback[0];
            QuarterDataSource.DataBind();
            quarterChart.Titles["quarterChart"].Text = quote + "-" + postback[0];
            quarterChart.DataBind();
        }

        else if (sender.Equals(quarterChart))
        {
            clearParameters();
            yearChart.Visible = true;
            quarterChart.Visible = true;
            monthChart.Visible = true;
            String quote = YearDataSource.SelectParameters["Quote"].DefaultValue;
            String year = QuarterDataSource.SelectParameters["Year"].DefaultValue;
            MonthDataSource.SelectParameters["Quote"].DefaultValue = quote;
            MonthDataSource.SelectParameters["Year"].DefaultValue = year;
            MonthDataSource.SelectParameters["Quarter"].DefaultValue = postback[0];
            MonthDataSource.DataBind();
            monthChart.Titles["monthChart"].Text = quote + "-" + year + ":Q" + postback[0];
            monthChart.DataBind();
        }
    }
    protected void clearParameters()
    {
        yearChart.Visible = false;
        quarterChart.Visible = false;
        monthChart.Visible = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        yearChart.Visible = false;
        quarterChart.Visible = false;
        monthChart.Visible = false;
    }
}