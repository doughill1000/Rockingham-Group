<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="Property.aspx.cs" Inherits="Wepages_Home_Property" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <a href="../Applicant.aspx">Applicant</a> » Property
    <h2>
        Property</h2>
    <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification" />
    <fieldset class="fieldset">
        <legend>
            <h3>
                Property Information</h3>
        </legend>
        <ul class="ul">
            <li>
                <asp:Label ID="lblYearHomeBuilt" runat="server" Text="Year home was built:" Width="300px"></asp:Label>
                <asp:TextBox ID="txtYearHomeBuilt" runat="server" MaxLength="4"></asp:TextBox> <i>(YYYY)</i>
                <asp:RequiredFieldValidator ID="rvYearHomeBuilt" runat="server" ErrorMessage="*"
                    CssClass="failureNotification" Display="Dynamic" ControlToValidate="txtYearHomeBuilt"
                    EnableClientScript="false"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revYearHomeBuilt" runat="server" ErrorMessage="Please enter a valid year"
                    CssClass="failureNotification" ValidationExpression="^(\d{4})$" Display="Dynamic"
                    ControlToValidate="txtYearHomeBuilt" EnableClientScript="false" ValidationGroup="vgProperty"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="cvYearHomeBuilt" runat="server" ControlToValidate="txtYearHomeBuilt"
                    ErrorMessage="Please enter a valid year" Operator="LessThanEqual" Type="Integer"
                    CssClass="failureNotification" EnableClientScript="false" Display="Dynamic" ValidationGroup="vgProperty"> 
                </asp:CompareValidator>
            </li>
            <li>
                <asp:Label ID="lblConstructionType" runat="server" Text="Construction type:" Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlConstructionType" runat="server">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="Brick/Block/Stone" Value="Brick/Block/Stone"></asp:ListItem>
                    <asp:ListItem Text="Wood/Vinyl Siding & Log" Value="Wood/Vinyl Siding & Log"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvConstructionType" runat="server" ErrorMessage="*"
                    CssClass="failureNotification" Display="Dynamic" ControlToValidate="ddlConstructionType"
                    EnableClientScript="false"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblNearestFireStation" runat="server" Text="Distance to nearest fire station:"
                    Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlNearestFireStation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNearestFireStation_SelectedIndexChanged">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="Within 5 miles" Value="Within 5 miles"></asp:ListItem>
                    <asp:ListItem Text="Over 5 miles" Value="Over 5 miles"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvNearestFireStation" runat="server" ErrorMessage="*"
                    CssClass="failureNotification" Display="Dynamic" ControlToValidate="ddlNearestFireStation"
                    EnableClientScript="false"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblFireHydrant" runat="server" Text="Fire hydrant within 1,000 feet of home:"
                    Width="300px" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddlFireHydrant" runat="server" Visible="false">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvFireHydrant" runat="server" ErrorMessage="*" CssClass="failureNotification"
                    Display="Dynamic" ControlToValidate="ddlFireHydrant" Visible="false" EnableClientScript="false"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblNewlyPurchased" runat="server" Text="Newly Purchased:" Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlNewlyPurchased" runat="server" OnSelectedIndexChanged="ddlNewlyPurchased_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvNewlyPurchased" runat="server" ErrorMessage="*"
                    CssClass="failureNotification" Display="Dynamic" ControlToValidate="ddlNewlyPurchased"
                    EnableClientScript="false"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblCurrentInsurance" runat="server" Text="Current insurance on home:"
                    Width="300px" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddlCurrentInsurance" runat="server" OnSelectedIndexChanged="ddlCurrentInsurance_SelectedIndexChanged"
                    AutoPostBack="true" Visible="false">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes with Rockingham Group" Value="Yes with Rockingham Group"></asp:ListItem>
                    <asp:ListItem Text="Yes with another insurance company" Value="Yes with another insurance company"></asp:ListItem>
                    <asp:ListItem Text="No prior home cancelled" Value="No prior home cancelled"></asp:ListItem>
                    <asp:ListItem Text="No insurance on current home" Value="No insurance on current home"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCurrentInsurance" runat="server" ErrorMessage="*"
                    CssClass="failureNotification" Display="Dynamic" ControlToValidate="ddlCurrentInsurance"
                    EnableClientScript="false" Visible="false"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblCurrentInsuranceCompany" runat="server" Text="Current insurance company:"
                    Width="300px" Visible="false"></asp:Label>
                <asp:TextBox ID="txtCurrentInsuranceCompany" runat="server" Visible="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCurrentInsuranceCompany" runat="server" ErrorMessage="*"
                    CssClass="failureNotification" Display="Dynamic" ControlToValidate="txtCurrentInsuranceCompany"
                    EnableClientScript="false" Visible="false"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblDesiredAmount" runat="server" Text="Home Value:" Width="300px"></asp:Label>
                <asp:TextBox ID="txtDesiredAmount" runat="server" AutoPostBack="true" 
                    MaxLength="9" ontextchanged="txtDesiredAmount_TextChanged"></asp:TextBox><i>(Numeric format)</i>
                <asp:RegularExpressionValidator ID="revDesiredInsuranceAmount" runat="server" ErrorMessage="Please enter a valid amount"
                    CssClass="failureNotification" ValidationExpression="\d+" Display="Dynamic" ControlToValidate="txtDesiredAmount"
                    EnableClientScript="false"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="cvDesiredAmount" runat="server" ErrorMessage="Minimum amount is 50000"
                    ControlToValidate="txtDesiredAmount" ValueToCompare="50000" Type="Double" CssClass="failureNotification"
                    EnableClientScript="false" Display="Dynamic" Operator="GreaterThanEqual"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="rfvDesiredAmount" runat="server" ErrorMessage="*"
                    CssClass="failureNotification" Display="Dynamic" ControlToValidate="txtDesiredAmount"
                    EnableClientScript="false"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblPaidLosses3Years" runat="server" Text="Number of paid losses in past three years:"
                    Width="300px"></asp:Label>
                <asp:DropDownList ID="ddlPaidLosses3Years" runat="server">
                    <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4 or more" Value="4 or more"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPaidLosses3Years" runat="server" ErrorMessage="*"
                    CssClass="failureNotification" Display="Dynamic" ControlToValidate="ddlPaidLosses3Years"
                    EnableClientScript="false"></asp:RequiredFieldValidator>
            </li>
        </ul>
    </fieldset>
    <br />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
    <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click"
        ValidationGroup="vgProperty" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div style="padding:15px">
    <link type="text/css" href="Styles\StyleSheet.css" rel="Stylesheet" />
    <div class="stepComplete">Step One:<br />Applicant</div>
    <div class="stepComplete" style="border-color:#ffffff; border-width:5px;">Step Two:<br />Property</div>
    <div class="stepIncomplete">Step Three:<br />Coverage</div>
    <div class="stepIncomplete">Step Four:<br />Discounts</div>
    
    <div class="stepIncomplete">Step Five:<br />Quote Result</div>
</div>
</asp:Content>
