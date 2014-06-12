<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgencyInfo.aspx.cs" Inherits="Wepages_Agent_AgencyInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Agency</h2>
    <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification"
        Text="" />
    <fieldset class="fieldset">
        <legend>
            <h3>Agent Information</h3>
        </legend>
        <ul class="ul">
            
            <li>
                <asp:Label ID="lblFirstName" runat="server" Text="Agent First Name:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                    ValidationGroup="agencyInformation" Display="dynamic" CssClass="failureNotification"
                    EnableClientScript="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid first name"
                    ValidationExpression="^[a-zA-Z'.\s]{1,50}" ControlToValidate="txtFirstName" CssClass="failureNotification"
                    Display="Dynamic" ValidationGroup="agencyInformation"></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblLastName" runat="server" Text="Agent Last Name:" 
                    Width="200px" Height="25px"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="*" ControlToValidate="txtLastName"
                    ValidationGroup="agencyInformation" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revLastName" runat="server" ErrorMessage="Please enter a valid last name"
                    ValidationExpression="^[a-zA-Z'.\s]{1,50}" ControlToValidate="txtLastName" CssClass="failureNotification"
                    Display="Dynamic" ValidationGroup="agencyInformation"></asp:RegularExpressionValidator>
            </li>
            
            <li>
                <asp:Label ID="lblPhone" runat="server" Text="Phone Number:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ErrorMessage="*" ControlToValidate="txtPhone"
                    ValidationGroup="agencyInformation" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator    
                    ID="revPhone"  
                    runat="server"    
                    ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"  
                    ControlToValidate="txtPhone"  
                    ErrorMessage=" Please enter a valid phone number" 
                    ValidationGroup="agencyInformation" 
                    CssClass="failureNotification"
                    placeholder="(xxx)xxx-xxxx"
                    ></asp:RegularExpressionValidator>  
            </li>
            <li>
                <asp:Label ID="lblEmail" runat="server" Text="Email:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"
                    ValidationGroup="agencyInformation" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="txtEmail" ErrorMessage=" Please enter a valid email"
                    Display="Dynamic" CssClass="failureNotification" ValidationGroup="agencyInformation"></asp:RegularExpressionValidator>
            </li>
            
            <li>
                <asp:Label ID="lblAgencyName" runat="server" Text="Agency Name:" Width="200px"></asp:Label>
                <asp:DropDownList ID="ddlAgencyName" runat="server" DataSourceID="sdsAgency" 
                    DataTextField="Name" DataValueField="Name" AutoPostBack="true"
                    onselectedindexchanged="ddlAgencyName_SelectedIndexChanged" OnDataBound="ddlAgencyName_Databound">
                    
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsAgency" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="SELECT [Name] FROM [Agency]"></asp:SqlDataSource>
                <asp:RequiredFieldValidator ID="rfvAgencyName1" runat="server" ControlToValidate="ddlAgencyName"
                    ValidationGroup="agencyInformation" Display="dynamic" CssClass="failureNotification"
                    EnableClientScript="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:Button ID="btnAddAgency" runat="server" Text="Add Agency" 
                    onclick="btnAddAgency_Click"/>
                
            </li>
            <li>
                <asp:Label ID = "lblManager" runat = "server" Text = "Manager: " Width="200px"></asp:Label>
                <asp:DropDownList ID = "ddlManager" runat = "server" Width="200px"></asp:DropDownList>
                <!--<asp:RequiredFieldValidator ID="rfvManger" runat="server" ErrorMessage="*" ControlToValidate="txtPhone"
                    ValidationGroup="agencyInformation" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification">*</asp:RequiredFieldValidator>-->
            </li>
            <asp:Panel ID="pnlNewAgency" runat="server" Visible="false">
            <fieldset class="fieldset">
                <legend><h3>Add Agency</h3>
                </legend>
                <li>
                    <asp:Label ID="lblAgencyName2" runat="server" Text="Agency Name:" Width="200px"></asp:Label>
                    <asp:TextBox ID="txtAgencyName" runat="server" Width="200px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAgencyName" runat="server" ControlToValidate="txtAgencyName"
                        ValidationGroup="agencyInformation" Display="dynamic" CssClass="failureNotification"
                        EnableClientScript="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revAgencyNameNew" runat="server" ErrorMessage="Please enter a valid agency name"
                        ValidationExpression="^[a-zA-Z'.\s]{1,50}" ControlToValidate="txtFirstName" CssClass="failureNotification"
                        Display="Dynamic" ValidationGroup="agencyInformation"></asp:RegularExpressionValidator>
                </li>
                <li>
                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code:" Width="200px"></asp:Label>
                    <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ErrorMessage="*" ControlToValidate="txtZipCode"
                        ValidationGroup="agencyInformation" Display="dynamic" EnableClientScript="false"
                        CssClass="failureNotification"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revZipCode" runat="server" ErrorMessage="Please enter a valid zip code"
                        ValidationExpression="\d{5}-?(\d{4})?$" ControlToValidate="txtZipCode" CssClass="failureNotification"></asp:RegularExpressionValidator>
                </li>
                <li>
                    <asp:Label ID="lblStreetAddress" runat="server" Text="Street Address:" Width="200px"></asp:Label>
                    <asp:TextBox ID="txtStreetAddress" runat="server" Width="300px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvStreetAddress" runat="server" ControlToValidate="txtStreetAddress"
                        ValidationGroup="agencyInformation" Display="dynamic" CssClass="failureNotification"
                        EnableClientScript="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Label ID="lblCity" runat="server" Width="200px" Text="City:"></asp:Label>
                    <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="*" ControlToValidate="txtCity"
                        ValidationGroup="agencyInformation" Display="dynamic" EnableClientScript="false"
                        CssClass="failureNotification">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Label ID="lblState" runat="server" Text="State: " Width="200px"></asp:Label>
                    <asp:DropDownList ID="ddlState" runat="server" Visible="true" 
                        AutoPostBack="true" onselectedindexchanged="ddlState_SelectedIndexChanged">
                        <asp:ListItem Value="" Text="Select One"></asp:ListItem>
                        <asp:ListItem Value="Virginia" Text="Virginia"></asp:ListItem>
                        <asp:ListItem Value="Pennsylvania" Text="Pennsylvania"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="*" CssClass="failureNotification"
                        ControlToValidate="ddlstate" EnableClientScript="false"></asp:RequiredFieldValidator>
                </li>
                </fieldset>
            </asp:Panel>
            <li>
                <asp:Label ID="lblRegion" runat="server" Text="Region: " Width="200px"></asp:Label>
                <asp:DropDownList ID="ddlRegionVA" runat="server" Visible="false">
                    <asp:ListItem Value="" Text="Select One"></asp:ListItem>
                    <asp:ListItem Value="Northern Virginia" Text="Northern Virginia"></asp:ListItem>
                    <asp:ListItem Value="Central Virginia" Text="Central Virginia"></asp:ListItem>
                    <asp:ListItem Value="Southern Virginia" Text="Southern Virginia"></asp:ListItem>
                    <asp:ListItem Value="Chesapeake Bay" Text="Chesapeake Bay"></asp:ListItem>
                    <asp:ListItem Value="Coastal Virginia - Hampton Roads" Text="Coastal Virginia - Hampton Roads"></asp:ListItem>
                    <asp:ListItem Value="Coastal Virginia - Eastern Shore" Text="Coastal Virginia - Eastern Shore"></asp:ListItem>
                    <asp:ListItem Value="Shenandoah Valley" Text="Shenandoah Valley"></asp:ListItem>
                    <asp:ListItem Value="Blue Ridge Highlands" Text="Blue Ridge Highlands"></asp:ListItem>
                    <asp:ListItem Value="Heart of Appalachia" Text="Heart of Appalachia"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvRegionVA" runat="server" ErrorMessage="*" CssClass="failureNotification"
                    ControlToValidate="ddlRegionVA" EnableClientScript="false" Visible="false"></asp:RequiredFieldValidator>
                    
                <asp:DropDownList ID="ddlRegionPA" runat="server" Visible="false">
                    <asp:ListItem Value="" Text="Select One"></asp:ListItem>
                    <asp:ListItem Value="Great Lakes and Erie" Text="Great Lakes and Erie"></asp:ListItem>
                    <asp:ListItem Value="Pennsylvania Wilds" Text="Pennsylvania Wilds"></asp:ListItem>
                    <asp:ListItem Value="Susquehanna Valley" Text="Susquehanna Valley"></asp:ListItem>
                    <asp:ListItem Value="Northeast Mountains - Poconos" Text="Northeast Mountains - Poconos"></asp:ListItem>
                    <asp:ListItem Value="Pittsburgh - Laurel Highlands" Text="Pittsburgh - Laurel Highlands"></asp:ListItem>
                    <asp:ListItem Value="Allegheny Mts & Valleys" Text="Allegheny Mts & Valleys"></asp:ListItem>
                    <asp:ListItem Value="PA Dutch - Amish Country" Text="PA Dutch - Amish Country"></asp:ListItem>
                    <asp:ListItem Value="Philadelphia - Lehigh Valley" Text="Philadelphia - Lehigh Valley"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvRegionPA" runat="server" ErrorMessage="*" CssClass="failureNotification"
                    ControlToValidate="ddlRegionPA" EnableClientScript="false" Visible="false"></asp:RequiredFieldValidator>
            </li>
        </ul>
    </fieldset>
    <br />
    <br /> 
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
        ValidationGroup="agencyInformation" ToolTip="Press this button to save" />
        </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

