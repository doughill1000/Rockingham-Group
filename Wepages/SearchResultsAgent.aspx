<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchResultsAgent.aspx.cs" Inherits="Wepages_SearchResultsAgent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Search Quotes</h2>
    <asp:Label ID="lblSearchInput" runat="server" CssClass="failureNotification"></asp:Label>
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
            <asp:HyperLinkField />
            <asp:ButtonField />
            <asp:ButtonField />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsViewQuotes" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        
        SelectCommand="SearchResultsAgent" SelectCommandType="StoredProcedure" >
        <SelectParameters>
            <asp:SessionParameter Name="input" SessionField="searchInput" Type="String" />
            <asp:SessionParameter Name="UserID" SessionField="UserID" />
        </SelectParameters>
        
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
