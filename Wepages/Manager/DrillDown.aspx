<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DrillDown.aspx.cs" Inherits="Wepages_DrillDown" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Drill Down</h2>
    <asp:Chart ID="total" runat="server" DataSourceID="QuoteTypeSum" Width="800px" OnClick="On_Click"
        Visible="false" EnableViewState="True">
        <Series>
            <asp:Series ChartArea="total" Name="Series1" XValueMember="InsuranceType" YValueMembers="TotalQuotes"
                PostBackValue="#VALX">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="total">
                <AxisY Title="TotalQuotes">
                </AxisY>
                <AxisX Title="Name">
                </AxisX>
            </asp:ChartArea>
        </ChartAreas>
        <Titles>
            <asp:Title Name="InitialChart">
            </asp:Title>
        </Titles>
    </asp:Chart>
    <asp:Chart ID="yearChart" runat="server" DataSourceID="YearDataSource" OnClick="On_Click" Width="260px" Visible="false">
        <Series>
            <asp:Series Name="Series1" XValueMember="Year" YValueMembers="TotalQuotes" PostBackValue="#VALX">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="total">
                <AxisY Title="TotalQuotes">
                </AxisY>
                <AxisX Title="Year">
                </AxisX>
            </asp:ChartArea>
        </ChartAreas>
        <Titles>
            <asp:Title Name="yearChart">
            </asp:Title>
        </Titles>
    </asp:Chart>
    <asp:Chart ID="quarterChart" runat="server" DataSourceID="QuarterDataSource" OnClick="On_Click" Width="260px" Visible="false">
        <Series>
            <asp:Series Name="Series1" XValueMember="Quarter" YValueMembers="TotalQuotes" PostBackValue="#VALX">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="total">
                <AxisY Title="TotalQuotes">
                </AxisY>
                <AxisX Title="Quarter">
                </AxisX>
            </asp:ChartArea>
        </ChartAreas>
        <Titles>
            <asp:Title Name="quarterChart">
            </asp:Title>
        </Titles>
    </asp:Chart>
    <asp:Chart ID="monthChart" runat="server" DataSourceID="MonthDataSource" OnClick="On_Click" Width="260px" Visible = "false">
        <Series>
            <asp:Series Name="Series1" XValueMember="Month" YValueMembers="TotalQuotes">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="total">
                <AxisY Title="TotalQuotes">
                </AxisY>
                <AxisX Title="Month">
                </AxisX>
            </asp:ChartArea>
        </ChartAreas>
        <Titles>
            <asp:Title Name="monthChart">
            </asp:Title>
        </Titles>
    </asp:Chart>
    <asp:SqlDataSource ID="QuoteTypeSum" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT count(QuoteID) As TotalQuotes, InsuranceType
        FROM Quote group by InsuranceType"></asp:SqlDataSource>
    <asp:SqlDataSource ID="YearDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT DISTINCT DATEPART(YYYY, DateCreated) AS Year, count(QuoteID) As TotalQuotes FROM Quote 
        WHERE InsuranceType = @Quote
        group by DATEPART(YYYY, DateCreated) 
        order by DATEPART(YYYY, DateCreated)">
        <SelectParameters>
            <asp:Parameter Name="Quote" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="QuarterDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT DISTINCT DATEPART(Q, DateCreated) AS Quarter, count(QuoteID) As TotalQuotes FROM Quote 
        WHERE InsuranceType = @Quote and DATEPART(YYYY, DateCreated) = @Year
        group by DATEPART(Q, DateCreated) 
        order by DATEPART(Q, DateCreated)">
        <SelectParameters>
            <asp:Parameter Name="Quote" Type="String" />
            <asp:Parameter Name="Year" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="MonthDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT
        CASE DATEPART(M, DateCreated)
        WHEN '1' THEN 'JAN'
		WHEN '2' THEN 'FEB'
		WHEN '3' THEN 'MAR'
		WHEN '4' THEN 'APR'
		WHEN '5' THEN 'MAY'
		WHEN '6' THEN 'JUN'
		WHEN '7' THEN 'JUL'
		WHEN '8' THEN 'AUG'
		WHEN '9' THEN 'SEP'
		WHEN '10' THEN 'OCT'
		WHEN '11' THEN 'NOV'
		WHEN '12' THEN 'DEC'
		END AS Month, Count(QuoteID) AS TotalQuotes
        FROM Quote
        WHERE InsuranceType = @Quote AND DATEPART(YYYY, DateCreated) = @Year and DATEPART(Q, DateCreated) = @Quarter
        GROUP BY Datepart(M, DateCreated) 
        ORDER BY Datepart(M, DateCreated)">
        <SelectParameters>
            <asp:Parameter Name="Quote" Type="String" />
            <asp:Parameter Name="Year" Type="Int32" />
            <asp:Parameter Name="Quarter" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" />
    <br />
</asp:Content>
