<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="AssignmentInformation.aspx.cs" Inherits="Wepages_Auto_AssignmentInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Assignments</h2>
    <fieldset class="fieldset">
        <legend>
            <h3>
                Driver Vehicle Assignment Detail</h3>
        </legend>
        <ul class="ul">
            <li>
                <asp:Label ID="lblDriver" runat="server" Text="Driver: " Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlDriver" runat="server">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsDriver" runat="server" 
                    
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="GetAssignmentDrivers" SelectCommandType="StoredProcedure"
                    >
                    <SelectParameters>
                        <asp:Parameter Name="QuoteID" Type="Object" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </li><li>
                <asp:Label ID="lblVehicle" runat="server" Text="Vehicle: " Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlVehicle" runat="server">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsVehicle" runat="server"></asp:SqlDataSource>
            </li><li>
                <asp:Label ID="lblPrimaryDriver" runat="server" Text="Primary Driver of Vehicle: " Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlPrimaryDriver" runat="server">
                <asp:ListItem Text = "Select One" Value = ""></asp:ListItem>
                <asp:ListItem Text = "No" Value = "false"></asp:ListItem>
                <asp:ListItem Text = "Yes" Value = "true"></asp:ListItem>
                </asp:DropDownList>
            </li>
        </ul>
    </fieldset>
    <br /><br />
    <asp:Button ID = "btnBack" runat  = "server" Text = "Back" 
        onclick="btnBack_Click" />
    <asp:Button ID = "btnContinue" runat = "server" Text = "Continue" 
        onclick="btnContinue_Click" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
