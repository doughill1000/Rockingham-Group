<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="Discounts.aspx.cs" Inherits="Wepages_Home_Discounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <a href="../Applicant.aspx">Applicant</a> » <a href ="Property.aspx">Property</a> » <a href ="Coverage.aspx">Coverage</a> » Discounts
    <h2>
        Discounts</h2>
    <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification" />
    <fieldset class="fieldset">
        <legend>
            <h3>
                Policy Discounts</h3>
        </legend>
        <ul class="ul">
            <li>
                <asp:Label ID="lblSmokeAlarms" runat="server" Text="Smoke alarms on all floors:"
                    Width="500px"></asp:Label>
                <asp:RadioButton ID="rbSmokeAlarmsYes" runat="server" Text="Yes" GroupName="smokeAlarms" />
                &nbsp;
                <asp:RadioButton ID="rbSmokeAlarmsNo" runat="server" Text="No" GroupName="smokeAlarms" />
            </li>
            <li>
                <asp:Label ID="lblFireExtinguishers" runat="server" Text="One or more fire extinguishers:"
                    Width="500px"></asp:Label>
                <asp:RadioButton ID="rbFireExtinguisherYes" runat="server" Text="Yes" GroupName="fireExtinguishers" />
                &nbsp;
                <asp:RadioButton ID="rbFireExtinguisherNo" runat="server" Text="No" GroupName="fireExtinguishers" />
            </li>
            <li>
                <asp:Label ID="lblDeadbolts" runat="server" Text="Deadbolts locks on all exterior doors:"
                    Width="500px"></asp:Label>
                <asp:RadioButton ID="rbDeadBoltsYes" runat="server" Text="Yes" GroupName="deadbolts" />
                &nbsp;
                <asp:RadioButton ID="rbDeadBoltsNo" runat="server" Text="No" GroupName="deadbolts" />
            </li>
            <li>
                <asp:Label ID="lblFireAlarmReport" runat="server" Text="Fire alarm reporting to a central monitoring center:"
                    Width="500px"></asp:Label>
                <asp:RadioButton ID="rbFireAlarmReportYes" runat="server" Text="Yes" GroupName="fireAlarmReport" />
                &nbsp;
                <asp:RadioButton ID="rbFireAlarmReportNo" runat="server" Text="No" GroupName="fireAlarmReport" />
            </li>
            <li>
                <asp:Label ID="lblBurglarReport" runat="server" Text="Burglar alarm reporting to a central monitoring system:"
                    Width="500px"></asp:Label>
                <asp:RadioButton ID="rbBuglarReportYes" runat="server" Text="Yes" GroupName="burglarAlarm" />
                &nbsp;
                <asp:RadioButton ID="rbBurglarReportNo" runat="server" Text="No" GroupName="burglarAlarm" />
            </li>
            <li>
                <asp:Label ID="lblSprinklers" runat="server" Text="Sprinkler system on all floors:"
                    Width="500px"></asp:Label>
                <asp:RadioButton ID="rbSprinklersYes" runat="server" Text="Yes" GroupName="sprinklers" />
                &nbsp;
                <asp:RadioButton ID="rbSprinklersNo" runat="server" Text="No" GroupName="sprinklers" />
            </li>
            <br />
            <li>
                <asp:Label ID="lblAuto" runat="server" Text="Auto policy with Rockingham: " Width="400px"></asp:Label>
                <asp:DropDownList ID="ddlAutoPolicy" runat="server">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                    <asp:ListItem Text="No, but planning to my coverage to Rockingham" Value="No, but planning to my coverage to Rockingham"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvAutoPolicy" runat="server" ErrorMessage="*" ControlToValidate="ddlAutoPolicy"
                    Display="Dynamic" EnableClientScript="false" CssClass="failureNotification"></asp:RequiredFieldValidator></li>
        </ul>
    </fieldset>
    <br />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
    <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div style="padding:15px">
    <link type="text/css" href="Styles\StyleSheet.css" rel="Stylesheet" />
    <div class="stepComplete">Step One:<br />Applicant</div>
    <div class="stepComplete">Step Two:<br />Property</div>
    <div class="stepComplete">Step Three:<br />Coverage</div>
    <div class="stepComplete" style="border-color:#ffffff; border-width:5px;">Step Four:<br />Discounts</div>
    <div class="stepIncomplete">Step Five:<br />Quote Result</div>
</div>
</asp:Content>
