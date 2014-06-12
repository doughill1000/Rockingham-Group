<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewQuotesManager.aspx.cs" Inherits="Wepages_Quote_ViewQuotesManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Quotes</h2>
    <asp:Label ID="lblSearchInput" runat="server"></asp:Label>
    <asp:GridView ID="gvViewQuotes" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsViewQuotes" 
        DataKeyNames="QuoteID" CssClass = "mGrid">
        <Columns>
            <asp:BoundField DataField="Reference#" HeaderText="Reference #" 
                SortExpression="Reference#" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="InsuranceType" HeaderText="Insurance Type" 
                SortExpression="InsuranceType" />
            <asp:BoundField DataField="Name" HeaderText="Last, First" 
                SortExpression="Name" />
            <asp:BoundField DataField="DateCreated" HeaderText="Date Created" 
                SortExpression="DateCreated" DataFormatString="{0:d}" />
            <asp:BoundField DataField="Status" HeaderText="Status" 
                SortExpression="Status" />
            <asp:HyperLinkField />
            <asp:ButtonField />
            <asp:ButtonField />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsViewQuotes" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        
        SelectCommand="Select QuoteID, reference# as 'Reference#', InsuranceType, LastName + ', ' + FirstName as Name, DateCreated, 
	        case(active) When 0 THEN 'Inactive' When 1 THEN 'Active'END as Status from Quote WHERE Reactivation IS NULL ORDER BY Reference#" SelectCommandType="Text" >
        
    </asp:SqlDataSource>

    <h2>Quotes Requested for Reactivation</h2>
    <asp:Label ID="lblReqQuotes" runat="server"></asp:Label>
    <asp:GridView ID="gvReactivate" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsViewReactivateQuotes" CssClass = "mGrid"
        DataKeyNames="QuoteID">
        <Columns>
            <asp:BoundField DataField="Reference#" HeaderText="Reference #" 
                SortExpression="Reference#" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="InsuranceType" HeaderText="Insurance Type" 
                SortExpression="InsuranceType" />
            <asp:BoundField DataField="Name" HeaderText="Last, First" 
                SortExpression="Name" />
            <asp:BoundField DataField="DateCreated" HeaderText="Date Created" 
                SortExpression="DateCreated" DataFormatString="{0:d}" />
            <asp:BoundField DataField="Status" HeaderText="Status" 
                SortExpression="Status" />
            <asp:ButtonField />
            <asp:ButtonField />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsViewReactivateQuotes" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        
        SelectCommand="Select QuoteID, reference# as 'Reference#', InsuranceType, LastName + ', ' + FirstName as Name, DateCreated, 
	        case(active) When 0 THEN 'Inactive' When 1 THEN 'Active'END as Status from Quote WHERE Reactivation IS NOT NULL" SelectCommandType="Text" >
        
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>