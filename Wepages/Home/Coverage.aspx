<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="Coverage.aspx.cs" Inherits="Wepages_Home_Coverage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <a href="../Applicant.aspx">Applicant</a> » <a href ="Property.aspx">Property</a> » Coverage
    <h2>
        Coverage</h2>
    <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification" />
    <fieldset class="fieldset">
        <legend>
            <h3>
                Coverage Information</h3>
        </legend>
        <ul class="ul">
            <li>
                <asp:Label ID="lblCoverageA" runat="server" Text="Coverage A - Dwelling:" Width="300px"></asp:Label>
                <asp:Label ID="lblCoverageAAmount" runat="server" Width="110px"></asp:Label>
                <asp:Image ID="imCoverageAQ" runat="server" ToolTip="protection from total loss/partial loss"
                    Height="15px" ImageUrl="~/Images/Question.png" />
            </li>
            <li>
                <asp:Label ID="lblCoverageB" runat="server" Text="Coverage B - Other Structures:"
                    Width="300px"></asp:Label>
                <asp:Label ID="lblCoverageBAmount" runat="server" Width="110px"></asp:Label>
                <asp:Image ID="imCoverageBQ" runat="server" ToolTip="Coverage for detached buildings"
                    Height="15px" ImageUrl="~/Images/Question.png" />
            </li>
            <li>
                <asp:Label ID="lblCoverageC" runat="server" Text="Coverage C - Personal Property:"
                    Width="300px"></asp:Label>
                <asp:Label ID="lblCoverageCAmount" runat="server" Width="110px"></asp:Label>
                <asp:Image ID="imCoverageCQ" runat="server" ToolTip="Coverage for personal property (In house, not attached)"
                    Height="15px" ImageUrl="~/Images/Question.png" />
            </li>
            <li>
                <asp:Label ID="lblCoverageD" runat="server" Text="Coverage D - Loss of Use:" Width="300px"></asp:Label>
                <asp:Label ID="lblCoverageDAmount" runat="server" Width="110px"></asp:Label>
                <asp:Image ID="imCoverageDQ" runat="server" ToolTip="Coverage for loss of use" Height="15px"
                    ImageUrl="~/Images/Question.png" />
            </li>
            <li>
                <asp:Label ID="lblCoverageL" runat="server" Text="Coverage L - Personal Liability:"
                    Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlCoverageL" runat="server" AutoPostBack="true" Width="110px">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="None" Value="0"></asp:ListItem>
                    <asp:ListItem Text="$100,000" Value="100000"></asp:ListItem>
                    <asp:ListItem Text="$300,000" Value="300000"></asp:ListItem>
                    <asp:ListItem Text="$500,000" Value="500000"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCoverageL" runat="server" CssClass="failureNotification"
                    ErrorMessage="*" Display="Dynamic" EnableClientScript="false" ControlToValidate="ddlCoverageL"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblCoverageM" runat="server" Text="Coverage M - Medical Payments:"
                    Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlCoverageM" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="None" Value="0"></asp:ListItem>
                    <asp:ListItem Text="$1,000" Value="1000"></asp:ListItem>
                    <asp:ListItem Text="$2,000" Value="2000"></asp:ListItem>
                    <asp:ListItem Text="$3,000" Value="3000"></asp:ListItem>
                    <asp:ListItem Text="$4,000" Value="4000"></asp:ListItem>
                    <asp:ListItem Text="$5,000" Value="5000"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCoverageM" runat="server" CssClass="failureNotification"
                    ErrorMessage="*" Display="Dynamic" EnableClientScript="false" ControlToValidate="ddlCoverageM"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblPolicyDeductible" runat="server" Text="Policy Deductible:" Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlPolicyDeductible" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="None" Value="0"></asp:ListItem>
                    <asp:ListItem Text="$500" Value="500"></asp:ListItem>
                    <asp:ListItem Text="$1,000" Value="1000"></asp:ListItem>
                    <asp:ListItem Text="$2,500" Value="2500"></asp:ListItem>
                    <asp:ListItem Text="$5,000" Value="5000"></asp:ListItem>
                    <asp:ListItem Text="$10,000" Value="10000"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPolicyDeductible" runat="server" CssClass="failureNotification"
                    ErrorMessage="*" Display="Dynamic" EnableClientScript="false" ControlToValidate="ddlPolicyDeductible"></asp:RequiredFieldValidator>
            </li>
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
    <div class="stepComplete" style="border-color:#ffffff; border-width:5px;">Step Three:<br />Coverage</div>
    <div class="stepIncomplete">Step Four:<br />Discounts</div>
    <div class="stepIncomplete">Step Five:<br />Quote Result</div>
</div>
</asp:Content>
