<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="GettingStarted.aspx.cs" Inherits="Wepages_GettingStarted" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Get Started on a Free Quote</h2>
    <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification" />
    <fieldset class="fieldset">
        <legend>
            <h3>
                Insurance Type and Disclosure</h3>
        </legend>
        <ul class="ul">
            <li>
                <asp:Label ID="lblTypeOfInsurance" runat="server" Text="Type of Insurance:" Width="250px"></asp:Label>
                <asp:DropDownList ID="ddlTypeOfInsurance" runat="server">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="Home" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Auto" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvTypeOfInsurance" runat="server" Display="Dynamic"
                    ControlToValidate="ddlTypeOfInsurance" ErrorMessage="*" CssClass="failureNotification"
                    EnableClientScript="false"></asp:RequiredFieldValidator>
            </li>
            <br />
            <li><strong>Information Disclosure:</strong> To provide you with a quote, we work with
                third party consumer reporting agencies to obtain a credit based insurance score.
                We promise to keep your information private and secure. Please feel free to contact
                us at (800) 555-5555 with any questions about our
                <asp:HyperLink ID="hylPrivacyPolicy" runat="server" NavigateUrl="~/Wepages/Privacy Policy.aspx">Privacy Policy</asp:HyperLink>.
            </li>
            <br />
            <li><span style="margin-left: 10px; padding-left: 10px;">I have read the Information
                Disclosure statement above and wish to continue</span>
                <asp:DropDownList ID="ddlContinue" runat="server" OnSelectedIndexChanged="ddlContinue_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvContinue" runat="server" ControlToValidate="ddlContinue"
                    CssClass="failureNotification" ErrorMessage="*" EnableClientScript="false" />
           </li>
           <li>
                <asp:CustomValidator ID="cuvContinue" runat="server" ControlToValidate="ddlContinue"
                    CssClass="failureNotification" ErrorMessage="Sorry, but unless you consent to this information disclosure we will be unable to accurately quote you. Please contact an agent directly."
                    display = "Dynamic" onservervalidate="cuvContinue_ServerValidate" >
                </asp:CustomValidator>
            </li>
            <li>
                <asp:Label ID="lblUnableToContinue" runat="server" CssClass="failureNotification" />
            </li>
        </ul>
    </fieldset>
    <br />
    <asp:Button ID="btnContinue" runat="server" OnClick="btnContinue_Click" Text="Continue" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
