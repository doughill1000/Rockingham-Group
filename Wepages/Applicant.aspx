<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="Applicant.aspx.cs" Inherits="Wepages_Applicant" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Used for Calendar --%>
    Applicant
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <h2>
        Application</h2>
    <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification"
        Text="" />
    <fieldset class="fieldset">
        <legend>
            <h3>
                Applicant Information</h3>
        </legend>
        <ul class="ul">
            <li>
                <asp:Label ID="lblFirstName" runat="server" Text="First Name:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                    ValidationGroup="applicationInformation" Display="dynamic" CssClass="failureNotification"
                    EnableClientScript="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revFirstName" runat="server" ErrorMessage="Please enter a valid first name"
                    ValidationExpression="^[a-zA-Z'.\s]{1,50}" ControlToValidate="txtFirstName" CssClass="failureNotification"
                    Display="Dynamic" ValidationGroup="applicationInformation"></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtMiddleName" runat="server" Width="200px"></asp:TextBox>
            </li>
            <li>
                <asp:Label ID="lblLastName" runat="server" Text="Last Name:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="*" ControlToValidate="txtLastName"
                    ValidationGroup="applicationInformation" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revLastName" runat="server" ErrorMessage="Please enter a valid last name"
                    ValidationExpression="^[a-zA-Z'.\s]{1,50}" ControlToValidate="txtLastName" CssClass="failureNotification"
                    Display="Dynamic" ValidationGroup="applicationInformation"></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblSuffix" runat="server" Text="Suffix:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtSuffix" runat="server" Width="200px"></asp:TextBox>
            </li>
            <li>
                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtDateOfBirth" runat="server"
                    ></asp:TextBox> <i>(mm-dd-yyyy)</i>
                <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ErrorMessage="*" ControlToValidate="txtDateOfBirth"
                    ValidationGroup="applicationInformation" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvDateOfBirth" runat="server" Type="Date" Operator="DataTypeCheck"
                    ControlToValidate="txtDateOfBirth" Display="Dynamic" CssClass="failureNotification"
                    ErrorMessage=" Please enter a date in the valid format" ValidationGroup="applicationInformation" EnableClientScript="false"></asp:CompareValidator>
                <asp:CustomValidator ID="cuvDateOfBirth" runat="server" ErrorMessage="Applicant must be 18 or older to submit a quote."
                    CssClass="failureNotification" EnableClientScript="false" OnServerValidate="cuvDateOfBirth_ServerValidate"
                    ControlToValidate="txtDateOfBirth" ValidationGroup="applicationInformation"></asp:CustomValidator>
                <asp:CustomValidator ID="cuvDateOfBirthOld" runat="server" ErrorMessage="Driver cannot be over 100 years old"
                    CssClass="failureNotification" EnableClientScript="false" OnServerValidate="cuvDateOfBirthOld_ServerValidate"
                    ControlToValidate="txtDateOfBirth" ValidationGroup="applicationInformation"></asp:CustomValidator>
            </li>
            <li>
                <asp:Label ID="lblSSN" runat="server" Text="Social Security Number:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtSSN" runat="server" ToolTip="xxx-xx-xxxx"></asp:TextBox>&nbsp;<i>(xxx-xx-xxxx)</i>
                <asp:RegularExpressionValidator ID="revSSN" runat="server" ValidationExpression="\d{3}-\d{2}-\d{4}"
                    ControlToValidate="txtSSN" ErrorMessage=" Please enter a valid social security number"
                    Display="Dynamic" CssClass="failureNotification" ValidationGroup="applicationInformation"></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblAddresses" runat="server" Text="Addresses:" Width="200px" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddlAddresses" runat="server" Width="400px" 
                    Visible="false" OnDataBound="ddlAddresses_DataBound" AutoPostBack="true"
                    onselectedindexchanged="ddlAddresses_SelectedIndexChanged"></asp:DropDownList>
            </li>
            <li>
                <asp:Label ID="lblStreetAddress1" runat="server" Text="Street Address Line 1:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtStreetAddress1" runat="server" Width="300px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvStreetAddress1" runat="server" ControlToValidate="txtStreetAddress1"
                    ValidationGroup="applicationInformation" Display="dynamic" CssClass="failureNotification"
                    EnableClientScript="false" ErrorMessage="*"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblStreetAddress2" runat="server" Text="Street Address Line 2:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtStreetAddress2" runat="server" Width="300px"></asp:TextBox>
                <br />
                <asp:Label ID="lblAptLot" runat="server" Text="Apt#/Lot#:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtAptLot" runat="server"></asp:TextBox>
            </li>
            <li>
                <asp:Label ID="lblZipCode" runat="server" Text="Zip Code:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ErrorMessage="*" ControlToValidate="txtZipCode"
                    ValidationGroup="applicationInformation" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revZipCode" runat="server" ErrorMessage="Please enter a valid VA or PA zip code"
                    ValidationExpression="\d{5}-?(\d{4})?$" ControlToValidate="txtZipCode" CssClass="failureNotification"></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblCity" runat="server" Width="200px" Text="City:"></asp:Label>
                <asp:TextBox ID="txtCity" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="*" ControlToValidate="txtCity"
                    ValidationGroup="applicationInformation" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revCity" runat="server" ErrorMessage="Please enter a valid city"
                    ValidationExpression="^[a-zA-Z'.\s]{1,50}" ControlToValidate="txtCity" CssClass="failureNotification"
                    Display="Dynamic" ValidationGroup="applicationInformation"></asp:RegularExpressionValidator>
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
                    ControlToValidate="ddlState" EnableClientScript="false" ValidationGroup="applicationInformation"></asp:RequiredFieldValidator>
            </li>
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
                    ControlToValidate="ddlRegionVA" EnableClientScript="false" Visible="false" ValidationGroup="applicationInformation"></asp:RequiredFieldValidator>
                    
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
                    ControlToValidate="ddlRegionPA" EnableClientScript="false" Visible="false" ValidationGroup="applicationInformation"></asp:RequiredFieldValidator>
            </li>
            <li></li>
            <li>
                <asp:Label ID="lblCreditRating" runat="server" Text="Credit Score:" Width="200px"></asp:Label>
                <asp:TextBox ID="txtCreditRating" runat="server" ToolTip="300-850"></asp:TextBox> <i>(350 - 800)</i>
                <asp:RequiredFieldValidator ID="rfvCreditRating" runat="server" ErrorMessage="*" ControlToValidate="txtCreditRating"
                    ValidationGroup="applicationInformation" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="revCreditRating" type="Integer" runat="server" ErrorMessage="Please enter a valid credit rating"
                    MaximumValue="850" MinimumValue="300"  ControlToValidate="txtCreditRating" CssClass="failureNotification"></asp:RangeValidator>
            </li>
        </ul>
    </fieldset>
    <br />
    <br /> 
    <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click"
        ToolTip="Press this button to continue" />
        </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Literal ID="htmlSteps" runat="server"></asp:Literal>
</asp:Content>



