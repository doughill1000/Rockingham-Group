<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="AssignmentHub.aspx.cs" Inherits="Wepages_Auto_AssignmentHub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <a href="../Applicant.aspx">Applicant</a> » <a href="GeneralInformation.aspx">General Information</a> » <a href="Policy.aspx">Policy</a> » Assignments <br />
    <asp:Label ID="lblError" runat="server" CssClass="failureNotification" />
    <h2>
        Drivers</h2>
    <asp:GridView ID="gvDriver" runat="server" AutoGenerateColumns="False" DataSourceID="sdsDriver" CssClass = "mGrid">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Last, First" SortExpression="Name" />
            <asp:BoundField DataField="DOB" HeaderText="DOB" SortExpression="DOB" 
                DataFormatString="{0:d}" />
            <asp:HyperLinkField />
            <asp:ButtonField />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsDriver" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT [LastName] + ', ' + [FirstName] as [Name], [DateofBirth] as [DOB] FROM [Driver] WHERE ([QuoteID] = @QuoteID) ORDER BY DRIVERID">
        <SelectParameters>
            <asp:SessionParameter Name="QuoteID" SessionField="QuoteID" Type="Object" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:LinkButton ID="btnDriver" runat="server" PostBackUrl="~/Wepages/Auto/DriverInformation.aspx">Add Driver</asp:LinkButton>
    <h2>
        Vehicles</h2>
    <asp:GridView ID="gvVehicle" runat="server" AutoGenerateColumns="False" DataSourceID="sdsVehicle" CssClass = "mGrid">
        <Columns>
            <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
            <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
            <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
            <asp:BoundField DataField="USE" HeaderText="Use" SortExpression="USE" />
            <asp:HyperLinkField />
            <asp:ButtonField />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsVehicle" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT [Year], [Make], [Model], [VehicleUsage] AS [USE] FROM [Vehicle] WHERE ([QuoteID] = @QuoteID) ORDER BY VEHICLEID">
        <SelectParameters>
            <asp:SessionParameter Name="QuoteID" SessionField="QuoteID" Type="Object" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:LinkButton ID="btnVehicle" runat="server" PostBackUrl="~/Wepages/Auto/VehicleInformation.aspx">Add Vehicle</asp:LinkButton>
    <h2>
        Assignments</h2>
    <asp:GridView ID="gvAssignment" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsAssignment" CssClass = "mGrid">
        <Columns>
            <asp:BoundField DataField="Driver" HeaderText="Driver" SortExpression="Driver" 
                ReadOnly="True" />
            <asp:BoundField DataField="Vehicle" HeaderText="Vehicle" 
                SortExpression="Vehicle" ReadOnly="True" />
            <asp:CheckBoxField DataField="Primary" HeaderText="Primary Driver" 
                SortExpression="Primary" />
            <asp:ButtonField />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsAssignment" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="GetAssignments" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="QuoteID" SessionField="QuoteID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <fieldset class="fieldset">
        <legend>
            <h3>Create Assignment</h3>
        </legend>
    <table>
        <tr>
            <td>
                <asp:DropDownList ID="ddlDriver" runat="server" DataSourceID="sdsDriverAssignment" OnDataBound = "ddlDriver_DataBound"  DataTextField="Driver" DataValueField="Driver">
                    
                </asp:DropDownList>
                <asp:SqlDataSource ID = "sdsDriverAssignment" runat = "server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="Select LastName + ', ' + Firstname As Driver from Driver where QuoteID = @QuoteID">
                    <SelectParameters>
                        <asp:SessionParameter Name="QuoteID" SessionField="quoteID" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:DropDownList ID="ddlVehicle" runat="server" 
                    DataSourceID="sdsVehicleAssignment" OnDataBound = "ddlVehicle_DataBound" DataTextField="Vehicle" DataValueField="Vehicle">
                    <asp:ListItem Value="" Text="Select One" />
                </asp:DropDownList>
                <asp:SqlDataSource ID = "sdsVehicleAssignment" runat = "server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="Select cast([Year] as varchar) + ' ' + Make + ' ' + Model as Vehicle from Vehicle where QuoteID = @quoteID">
                    <SelectParameters>
                        <asp:SessionParameter Name="quoteID" SessionField="quoteID" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <center>
                    <asp:CheckBox ID="chkPrimary" Text="Primary Driver" runat="server" />
                </center>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <center>
                    <asp:Button ID="btnAssignmentSave" runat="server" Text="Save Assignment" OnClick="btnAssignmentSave_Click" />
                </center>
            </td>
        </tr>
    </table>
    </fieldset>
    <br />
    <br />
    <asp:Button runat="server" Text="Back" ID="btnBack" OnClick="btnBack_Click" />
    <asp:Button runat="server" Text="Continue" ID="btnContinue" OnClick="btnContinue_Click" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div style="padding:15px">
    <link type="text/css" href="Styles\StyleSheet.css" rel="Stylesheet" />
    <div class="stepComplete">Step One:<br />Applicant</div>
    <div class="stepComplete">Step Two:<br />General Information</div>
    <div class="stepComplete">Step Three:<br />Policy</div>
    <div class="stepComplete" style="border-color:#ffffff; border-width:5px;">Step Four:<br />Assignments</div>
    <div class="stepIncomplete">Step Five:<br />Quote Result</div>
</div>
</asp:Content>