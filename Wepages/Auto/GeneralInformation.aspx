<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="GeneralInformation.aspx.cs" Inherits="Wepages_Auto_GeneralInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:Panel ID = "pnlGeneralInformation" runat = "server" DefaultButton = "btnContinue">
    <a href="../Applicant.aspx">Applicant</a> » General Information
        <h2>General Information</h2>
    <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification" />
    <fieldset class="fieldset">
        <legend>
            <h3>
                General Questions</h3>
        </legend>
        <%-- Used for Calendar --%>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <ul class="ul">
            <li>
                <asp:Label ID="lblStartDate" runat="server" Text="Desired policy start date:" Width="325px"></asp:Label>
                <asp:TextBox ID="txtStartDate" runat="server" Width="168px" AutoPostBack="false" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="*" ControlToValidate="txtStartDate"
                    Display="Static" EnableClientScript="false" CssClass="failureNotification"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvStartDate" runat="server" Type="Date" Operator="DataTypeCheck"
                    ControlToValidate="txtStartDate" Display="Dynamic" CssClass="failureNotification"
                    ErrorMessage=" Please enter a valid date. " EnableClientScript="false"></asp:CompareValidator>
                <asp:CompareValidator ID="cvCorrectStartDate" runat="server" ControlToValidate="txtStartDate"
                    ErrorMessage="Date cannot be before today" Operator="GreaterThanEqual" Type="Date"
                    CssClass="failureNotification" EnableClientScript="false" Display="Dynamic">
                </asp:CompareValidator>
                <asp:CalendarExtender ID="ceStartDate" TargetControlID="txtStartDate" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblScheduledPayments" runat="server" Text="Scheduled Payments: " Width="325px"></asp:Label>
                <asp:DropDownList ID="ddlScheduledPayments" runat="server">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="Annual" Value="Annual"></asp:ListItem>
                    <asp:ListItem Text="Semi-Annual" Value="Semi-Annual"></asp:ListItem>
                    <asp:ListItem Text="Quarterly" Value="Quarterly"></asp:ListItem>
                    <asp:ListItem Text="EFT" Value="EFT"></asp:ListItem>
                    <asp:ListItem Text="9-Pay" Value="9-Pay"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvScheduledPayments" runat="server" ErrorMessage="*"
                    ControlToValidate="ddlScheduledPayments" Display="Dynamic" EnableClientScript="false"
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblAccidentForgiveness" runat="server" Text="Accident Forgiveness:"
                    Width="325px"></asp:Label>
                <asp:DropDownList ID="ddlAccidentForgiveness" runat="server" Style="height: 25px"
                    OnSelectedIndexChanged="ddlAccidentForgiveness_SelectedIndexChanged">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvAccidentForgiveness" runat="server" ErrorMessage="*"
                    ControlToValidate="ddlAccidentForgiveness" Display="Dynamic" EnableClientScript="false"
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblAgeUnder6" runat="server" Text="Any Children under the age of six:"
                    Width="325px"></asp:Label>
                <asp:DropDownList ID="ddlAgeUnder6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAgeUnder6_SelectedIndexChanged">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvAgeUnder6" runat="server" ErrorMessage="*" ControlToValidate="ddlAgeUnder6"
                    Display="Dynamic" EnableClientScript="false" CssClass="failureNotification"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblChildBirthDate" runat="server" Width="325px" Text="Child Birth Date:"
                    Visible="false"></asp:Label>
                <asp:TextBox ID="txtChildBirthDate" runat="server" Visible="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvChildBirthDate" runat="server" ErrorMessage="*"
                    ControlToValidate="txtChildBirthDate" Display="Dynamic" EnableClientScript="false"
                    CssClass="failureNotification" Visible="false"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvChildBirthdate" runat="server" ControlToValidate="txtChildBirthDate"
                    ErrorMessage="Please enter a valid birthdate" Operator="LessThanEqual" Type="Date"
                    CssClass="failureNotification" EnableClientScript="false" Display="Dynamic">
                </asp:CompareValidator>
                <asp:CustomValidator ID="cuvChildBirthDate" runat="server" ErrorMessage="Child is not under the age of six"
                    CssClass="failureNotification" ControlToValidate="txtChildBirthDate" OnServerValidate="cuvChildBirthDate_ServerValidate"
                    Display="Dynamic" EnableClientScript="false"></asp:CustomValidator>
                <asp:CalendarExtender ID="ceChildBirthDate" TargetControlID="txtChildBirthDate" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblHomePolicy" runat="server" Text="Home policy with Rockingham: "
                    Width="325px"></asp:Label>
                <asp:DropDownList ID="ddlHomePolicy" runat="server">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                    <asp:ListItem Text="No, but planning to my coverage to Rockingham" Value="No, but planning to my coverage to Rockingham"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvHomePolicy" runat="server" ErrorMessage="*" ControlToValidate="ddlHomePolicy"
                    Display="Dynamic" EnableClientScript="false" CssClass="failureNotification"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblYearsCurrentPolicy" runat="server" Text="Years with current policy"
                    Width="325px"></asp:Label>
                <asp:DropDownList ID="ddlYearsCurrentPolicy" runat="server">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                    <asp:ListItem Text="10+" Value="10+"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvYearsCurrentPolicy" runat="server" ErrorMessage="*"
                    ControlToValidate="ddlYearsCurrentPolicy" Display="Dynamic" EnableClientScript="false"
                    CssClass="failureNotification"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblHearAboutUs" runat="server" Text="How did you hear about us?" Width="325px"></asp:Label>
                <asp:DropDownList ID="ddlHearAboutUs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHearAboutUs_SelectedIndexChanged">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="Radio" Value="Radio"></asp:ListItem>
                    <asp:ListItem Text="TV" Value="TV"></asp:ListItem>
                    <asp:ListItem Text="Facebook" Value="Facebook"></asp:ListItem>
                    <asp:ListItem Text="Twitter" Value="Twitter"></asp:ListItem>
                    <asp:ListItem Text="Internet Ad" Value="Internet Ad"></asp:ListItem>
                    <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
                    <asp:ListItem Text="Billboard" Value="Billboard"></asp:ListItem>
                    <asp:ListItem Text="Newspaper" Value="Newspaper"></asp:ListItem>
                    <asp:ListItem Text="Mail" Value="Mail"></asp:ListItem>
                    <asp:ListItem Text="Community Event" Value="Community Event"></asp:ListItem>
                    <asp:ListItem Text="Word of Mouth" Value="Word of Mouth"></asp:ListItem>
                    <asp:ListItem Text="Family/Friend" Value="Family/Friend"></asp:ListItem>
                    <asp:ListItem Text="Existing Customer" Value="Existing Customer"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvHearAboutUs" runat="server" ErrorMessage="*" ControlToValidate="ddlHearAboutUs"
                    Display="Dynamic" EnableClientScript="false" CssClass="failureNotification"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblDescribeHearOfUs" runat="server" Visible="false" Text="Describe how you heard of us:"
                    Width="325px"></asp:Label>
            </li>
            <li>
                <asp:TextBox ID="txtDescribeHearOfUs" TextMode="MultiLine" Rows="5" runat="server"
                    Visible="false" Width="500px" MaxLength="250"></asp:TextBox>
            </li>
        </ul>
    </fieldset>
    <br />
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
            <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" />
            </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div style="padding:15px">
    <link type="text/css" href="Styles\StyleSheet.css" rel="Stylesheet" />
    <div class="stepComplete">Step One:<br />Applicant</div>
    <div class="stepComplete" style="border-color:#ffffff; border-width:5px;">Step Two:<br />General Information</div>
    <div class="stepIncomplete">Step Three:<br />Policy</div>
    <div class="stepIncomplete">Step Four:<br />Assignments</div>
    <div class="stepIncomplete">Step Five:<br />Quote Result</div>
</div>
</asp:Content>