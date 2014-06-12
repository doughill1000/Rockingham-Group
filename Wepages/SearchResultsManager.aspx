<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchResultsManager.aspx.cs" Inherits="Wepages_SearchResultsManager" %>

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
            <asp:BoundField DataField="name" HeaderText="Last, First" 
                SortExpression="name" ReadOnly="True" />
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
        
        SelectCommand="SearchResultsManager" SelectCommandType="StoredProcedure" >
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="input" SessionField="searchinput" />
        </SelectParameters>
    </asp:SqlDataSource>

    <h2>Quotes Requested for Reactivation</h2>
    <asp:Label ID="lblReqQuotes" runat="server"></asp:Label>
    <asp:GridView ID="gvReactivate" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsViewReactivateQuotes" CssClass = "mGrid">
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
        SelectCommand="SearchResultsManagerReactivation" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="input" SessionField="searchinput" />
        </SelectParameters>
        
        
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

