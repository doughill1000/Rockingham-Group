<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SearchResults.aspx.cs" Inherits="SearchResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Search Quotes</h2>
    <asp:Label ID="lblSearchInput" runat="server"></asp:Label>
    <asp:GridView ID="gvViewQuotes" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsViewQuotes" CssClass = "mGrid">
        <Columns>
            <asp:BoundField DataField="Reference#" HeaderText="Reference #" 
                SortExpression="Reference#" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="InsuranceType" HeaderText="Insurance Type" 
                SortExpression="InsuranceType" />
            <asp:BoundField DataField="Name" HeaderText="Last, First" 
                SortExpression="Name" ReadOnly="True" />
            <asp:BoundField DataField="DateCreated" HeaderText="Date Created" 
                SortExpression="DateCreated" DataFormatString="{0:d}"/>
            <asp:BoundField DataField="Status" HeaderText="Status" 
                SortExpression="Status" ReadOnly="True" />
            <asp:HyperLinkField />
            <asp:ButtonField />
            <asp:ButtonField />

        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsViewQuotes" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        
        SelectCommand="SearchResultsCustomer" SelectCommandType="StoredProcedure" >
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="input" SessionField="searchinput" />
            <asp:SessionParameter DefaultValue="" Name="UserID" SessionField="UserID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
