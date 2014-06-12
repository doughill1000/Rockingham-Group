<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="Policy.aspx.cs" Inherits="Wepages_Auto_Policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <a href="../Applicant.aspx">Applicant</a> » <a href="GeneralInformation.aspx">General Information</a> » Policy
    <h2>
        Policy</h2>
    <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification" />
    <fieldset class="fieldset">
        <legend>
            <h3>
                Coverages & Limits</h3>
        </legend>
        <asp:Label ID="lblBodilyInjuryLimit" runat="server" Text="Bodily Injury Limit:" Width="400px" />
        <asp:DropDownList ID="ddlBodilyInjuryLimit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SetUninsuredMotoristBodyLimit">
            <asp:ListItem Text="Select One" Value=""></asp:ListItem>
            <asp:ListItem Text="$25,000/$50,000" Value="$25,000/$50,000"></asp:ListItem>
            <asp:ListItem Text="$50,000/$100,000" Value="$50,000/$100,000"></asp:ListItem>
            <asp:ListItem Text="$100,000/$200,000" Value="$100,000/$200,000"></asp:ListItem>
            <asp:ListItem Text="$100,000/$300,000" Value="$100,000/$300,000"></asp:ListItem>
            <asp:ListItem Text="$300,000/$300,000" Value="$300,000/$300,000"></asp:ListItem>
            <asp:ListItem Text="$250,000/$500,000" Value="$250,000/$500,000"></asp:ListItem>
            <asp:ListItem Text="$500,000/$500,000" Value="$500,000/$500,000"></asp:ListItem>
            <asp:ListItem Text="$70,000" Value="$70,000"></asp:ListItem>
            <asp:ListItem Text="$75,000" Value="$75,000"></asp:ListItem>
            <asp:ListItem Text="$100,000" Value="$100,000"></asp:ListItem>
            <asp:ListItem Text="$200,000" Value="$200,000"></asp:ListItem>
            <asp:ListItem Text="$300,000" Value="$300,000"></asp:ListItem>

        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvBodilyInjuryLimit" runat="server" ErrorMessage="*"
            Display="Dynamic" ControlToValidate="ddlBodilyInjuryLimit" CssClass="failureNotification"
            EnableClientScript="false"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblPropertyDamageLimit" runat="server" Text="Property Damage Limit: "
            Width="400px" />
        <asp:DropDownList ID="ddlPropertyDamageLimit" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="SetUninsuredPropertyLimit">
            <asp:ListItem Text="Select One" Value=""></asp:ListItem>
            <asp:ListItem Text = "$20,000" Value = "$20,000"></asp:ListItem>
            <asp:ListItem Text="$25,000" Value="$25,000"></asp:ListItem>
            <asp:ListItem Text="$40,000" Value="$40,000"></asp:ListItem>
            <asp:ListItem Text="$50,000" Value="$50,000"></asp:ListItem>
            <asp:ListItem Text="$100,000" Value="$100,000"></asp:ListItem>
            <asp:ListItem Text="$150,000" Value="$150,000"></asp:ListItem>
            <asp:ListItem Text="$200,000" Value="$200,000"></asp:ListItem>
            <asp:ListItem Text="$250,000" Value="$250,000"></asp:ListItem>
            <asp:ListItem Text="$500,000" Value="$500,000"></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvPropertyDamageLimit" runat="server" ErrorMessage="*"
            Display="Dynamic" ControlToValidate="ddlPropertyDamageLimit" CssClass="failureNotification"
            EnableClientScript="false"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblUninsuredMotoristBodyLimit" runat="server" Text="Uninsured Motorist Bodily Injury Limit: "
            Width="400px" />
        <asp:Label ID="lblUninsuredMotoristBodyAmount" runat="server" />
        <br />
        <asp:Label ID="lblUninsuredPropertyDamageLimit" runat="server" Text="Uninsured Motorist Property Damage Limit: "
            Width="400px" />
        <asp:Label ID="lblUninsuredPropertyDamageAmount" runat="server" />
        <br />
        <asp:Label ID="lblMedicalExpense" runat="server" Text="Medical Expense: " Width="400px" />
        <asp:DropDownList ID="ddlMedicalExpense" runat="server" AutoPostBack = "true">
            <asp:ListItem Text="Select One" Value=""></asp:ListItem>
            <asp:ListItem Text="None" Value="None"></asp:ListItem>
            <asp:ListItem Text="$1,000" Value="$1,000"></asp:ListItem>
            <asp:ListItem Text="$2,000" Value="$2,000"></asp:ListItem>
            <asp:ListItem Text="$5,000" Value="$5,000"></asp:ListItem>
            <asp:ListItem Text="$10,000" Value="$10,000"></asp:ListItem>
            <asp:ListItem Text="$25,000" Value="$25,000"></asp:ListItem>
            <asp:ListItem Text="$50,000" Value="$50,000"></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvMedicalExpense" runat="server" ErrorMessage="*"
            Display="Dynamic" ControlToValidate="ddlMedicalExpense" CssClass="failureNotification"
            EnableClientScript="false"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblIncomeLoss" runat="server" Text="Income Loss: " Width="400px" />
        <asp:DropDownList ID="ddlIncomeLoss" runat="server">
            <asp:ListItem Text="Select One" Value=""></asp:ListItem>
            <asp:ListItem Text="No" Value="false"></asp:ListItem>
            <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvIncomeLoss" runat="server" ErrorMessage="*"
            Display="Dynamic" ControlToValidate="ddlIncomeLoss" CssClass="failureNotification"
            EnableClientScript="false"></asp:RequiredFieldValidator>
    </fieldset>
    <br />
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
            <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click"
                 />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
<div style="padding:15px">
    <link type="text/css" href="Styles\StyleSheet.css" rel="Stylesheet" />
    <div class="stepComplete">Step One:<br />Applicant</div>
    <div class="stepComplete">Step Two:<br />General Information</div>
    <div class="stepComplete" style="border-color:#ffffff; border-width:5px;">Step Three:<br />Policy</div>
    <div class="stepIncomplete">Step Four:<br />Assignments</div>
    <div class="stepIncomplete">Step Five:<br />Quote Result</div>
</div>
</asp:Content>

