<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="DriverInformation.aspx.cs" Inherits="Wepages_Auto_DriverInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Driver</h2>
    <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification" />
    <fieldset class="fieldset">
        <legend>
            <h3>
                Driver Information</h3>
        </legend>
        <ul class="ul">
            <li>
                <asp:Label ID="lblFirstName" runat="server" Text="First Name:" Width="325px"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="*" CssClass="failureNotification"
                    ControlToValidate="txtFirstName" EnableClientScript="false"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revFirstName" runat="server" ErrorMessage="Please enter a valid first name"
                    ValidationExpression="^[a-zA-Z'.\s]{1,50}" ControlToValidate="txtFirstName" CssClass="failureNotification"
                    Display="Dynamic" ValidationGroup="applicationInformation" EnableClientScript="false"></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name:" Width="325px"></asp:Label>
                <asp:TextBox ID="txtMiddleName" runat="server"></asp:TextBox>
            </li>
            <li>
                <asp:Label ID="lblLastName" runat="server" Text="Last Name:" Width="325px"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="*" CssClass="failureNotification"
                    ControlToValidate="txtLastName" EnableClientScript="false"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revLastName" runat="server" ErrorMessage="Please enter a valid last name"
                    ValidationExpression="^[a-zA-Z'.\s]{1,50}" ControlToValidate="txtLastName" CssClass="failureNotification"
                    Display="Dynamic" ValidationGroup="applicationInformation" EnableClientScript="false"></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblSuffix" runat="server" Text="Suffix:" Width="325px"></asp:Label>
                <asp:TextBox ID="txtSuffix" runat="server"></asp:TextBox>
            </li>
            <li>
                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth:" Width="325px"></asp:Label>
                <asp:TextBox ID="txtDateOfBirth" runat="server" OnTextChanged="txtDateOfBirth_TextChanged"
                    AutoPostBack="true" ToolTip="Please enter in format mm/dd/yyyy"></asp:TextBox>
                <asp:Image ID="imDateOfBirthQ" runat="server" ToolTip="Please enter in format mm/dd/yyyy"
                    Height="15px" ImageUrl="~/Images/Question.png" />
                <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ErrorMessage="*" ControlToValidate="txtDateOfBirth"
                    Display="dynamic" EnableClientScript="false" CssClass="failureNotification"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvDateOfBirth" runat="server" Type="Date" Operator="DataTypeCheck"
                    ControlToValidate="txtDateOfBirth" Display="Dynamic" CssClass="failureNotification"
                    ErrorMessage=" Please enter a date in the valid format" EnableClientScript="false"></asp:CompareValidator>
                <asp:CustomValidator ID="cuvDateOfBirthYoung" runat="server" ErrorMessage="Driver must be atleast 15 years old"
                    CssClass="failureNotification" EnableClientScript="false" OnServerValidate="cuvDateOfBirthYoung_ServerValidate"
                    ControlToValidate="txtDateOfBirth"></asp:CustomValidator>
                <asp:CustomValidator ID="cuvDateOfBirthOld" runat="server" ErrorMessage="Driver cannot be over 100 years old"
                    CssClass="failureNotification" EnableClientScript="false" OnServerValidate="cuvDateOfBirthOld_ServerValidate"
                    ControlToValidate="txtDateOfBirth"></asp:CustomValidator>
            </li>
            <li>
                <asp:Label ID="lblAge" runat="server" Text="Age:" Width="325px" Visible="false"></asp:Label>
                <asp:Label ID="lblAgeCalculated" runat="server" Width="325px" Visible="false"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblGender" runat="server" Text="Gender:" Width="325px"></asp:Label>
                <asp:DropDownList ID="ddlGender" runat="server">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvGender" runat="server" ErrorMessage="*" CssClass="failureNotification"
                    ControlToValidate="ddlGender" EnableClientScript="false"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblMaritalStatus" runat="server" Text="Marital Status:" Width="325px"></asp:Label>
                <asp:DropDownList ID="ddlMaritalStatus" runat="server">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="Single" Value="Single"></asp:ListItem>
                    <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvMaritalStatus" runat="server" ErrorMessage="*"
                    CssClass="failureNotification" ControlToValidate="ddlMaritalStatus" EnableClientScript="false"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblFirstLicense" runat="server" Text="Date of first license:" Width="325px"></asp:Label>
                <asp:TextBox ID="txtFirstLicense" runat="server" AutoPostBack="true" OnTextChanged="txtFirstLicense_TextChanged"
                    ToolTip="Please enter in format mm/dd/yyyy"></asp:TextBox>
                <asp:Image ID="imFirstLicenseQ" runat="server" ToolTip="Please enter in format mm/dd/yyyy"
                    Height="15px" ImageUrl="~/Images/Question.png" />
                <asp:RequiredFieldValidator ID="rfvFirstLicense" runat="server" ErrorMessage="*"
                    ControlToValidate="txtFirstLicense" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvFirstLicense" runat="server" Type="Date" Operator="DataTypeCheck"
                    ControlToValidate="txtFirstLicense" Display="Dynamic" CssClass="failureNotification"
                    ErrorMessage=" Please enter a date in the valid format" ValidationGroup="applicationInformation"
                    EnableClientScript="false"></asp:CompareValidator>
                <asp:CompareValidator ID="cvFirstLicense2" runat="server" ControlToValidate="txtFirstLicense"
                    ErrorMessage="Date cannot be after today" Operator="LessThanEqual" Type="Date"
                    CssClass="failureNotification" EnableClientScript="false" Display="Dynamic">
                </asp:CompareValidator>
                <asp:CustomValidator ID="cuvFirstLicense" runat="server" ErrorMessage="Driver cannot have obtained licensed over 85 years ago"
                    CssClass="failureNotification" EnableClientScript="false" OnServerValidate="cuvFirstLicense_ServerValidate"
                    ControlToValidate="txtFirstLicense"></asp:CustomValidator>
                <asp:CustomValidator ID="cuvFirstLicense2" runat="server" ErrorMessage="Driver cannot obtain license before 15 years of age"
                    CssClass="failureNotification" EnableClientScript="false" OnServerValidate="cuvFirstLicense2_ServerValidate"
                    ControlToValidate="txtFirstLicense" Display="Dynamic"></asp:CustomValidator>
            </li>
            <li>
                <asp:Label ID="lblYearsLicensed" runat="server" Text="Years Licensed:" Width="325px"
                    Visible="false"></asp:Label>
                <asp:Label ID="lblYearsLicensedCalculated" runat="server" Width="325px" Visible="false"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblIncidentLast3Years" runat="server" Text="Accidents, tickets, or violations in last 3 years:"
                    Width="325px"></asp:Label>
                <asp:DropDownList ID="ddlIncidentLast3Years" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIncidentLast3Years_SelectedIndexChanged">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvIncidentLast3Years" runat="server" ErrorMessage="*"
                    ControlToValidate="ddlIncidentLast3Years" Display="dynamic" EnableClientScript="false"
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblIncidentWarning" runat="server" CssClass="failureNotification"></asp:Label>
            </li>
        </ul>
    </fieldset>
    <br />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
    <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
