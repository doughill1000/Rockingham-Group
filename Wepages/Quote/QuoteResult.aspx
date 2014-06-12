<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="QuoteResult.aspx.cs" Inherits="Wepages_QuoteResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <style>
        #map_canvas
        {
            width: 700px;
            height: 500px;
            background-color: #CCC;
        }
    </style>
    <asp:Label ID="lblBreadcrumbs" runat="server" Text=""></asp:Label>
    <h2>
        Quote Results</h2>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblmonthlyPrem" runat="server" Text="Monthly Premium" Width="325px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblmonthlyPremAmount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblannualPrem" runat="server" Text="Annual Premium" Width="325px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblannualPremAmount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQuoteRef" runat="server" Text="Quote Reference Number" Width="325px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblQuoteRefShow" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDate" runat="server" Text="Date" Width="325px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDateShow" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAgent" Text="Agent:" Width="325px" Visible="false" runat="server"></asp:Label>
            </td>
            <td rowspan="2">
                <asp:Label ID="lblAgentShow" Text="" Visible="false" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <asp:Panel runat="server" ID="pnlAgentStuff">
        <fieldset class="fieldset">
            <ul class="ul">
                <li>This quote is an estimate based on the information you provided utilizing our current
                    rates and is subject to change. </li>
                <li>You can print a copy of the detailed quote results by clicking the following link:
                </li>
                <li>Would you like one of our local agents to contact you to complete an application?
                    <asp:DropDownList ID="ddlContact" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlContact_SelectedIndexChanged">
                        <asp:ListItem Value="" Text="Select One"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
        </fieldset>
        <center>
            <asp:Panel ID="pnlContact" runat="server" Visible="false">
                <br />
                <asp:Label ID="lblMapInfo" runat="server" Text="<b><i>The map below shows the closest agencies to your location.  <br>Once you've decided what agency you want, select an agent belonging to the agency in the drop down list below.</i></b>"></asp:Label>
                <br /><br />
                <div id="map_canvas">
                    <asp:Literal ID="jsMap" runat="server"></asp:Literal></div>
                <br />
                <table>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblAgentinfo" Text="<h3>Agent Information</h3>" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Select an Agent in your Region:
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlAgentRegion" AutoPostBack="true" OnSelectedIndexChanged="ddlAgentRegion_SelectedIndexChanged"
                                OnDataBound="ddlAgentRegion_DataBound">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAgentRegion" runat="server" ErrorMessage="*" CssClass="failureNotification"
                                ControlToValidate="ddlAgentRegion" EnableClientScript="false" Display="dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Or, Find an Agent in your state:
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlAllAgents" AutoPostBack="true" OnSelectedIndexChanged="ddlAllAgents_SelectedIndexChanged"
                                OnDataBound="ddlAllAgents_DataBound">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAllAgents" runat="server" ErrorMessage="*" CssClass="failureNotification"
                                ControlToValidate="ddlAllAgents" EnableClientScript="false" Display="dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="lblContactInfo" Text="<h3>Additional Contact Information</h3>" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Method of Contact:
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlContactMethod" AutoPostBack="true" OnSelectedIndexChanged="ddlContactMethod_SelectedIndexChanged">
                                <asp:ListItem Value="" Text="Select One"></asp:ListItem>
                                <asp:ListItem Value="Email" Text="Email"></asp:ListItem>
                                <asp:ListItem Value="Phone" Text="Phone"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvContactMethod" runat="server" ErrorMessage="*"
                                CssClass="failureNotification" ControlToValidate="ddlContactMethod" EnableClientScript="false"
                                Display="dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlEmail" runat="server" Visible="false">
                <asp:Label runat="server" Text="Email:"></asp:Label>
                <asp:TextBox runat="server" ID="txtEmail" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" CssClass="failureNotification"
                    ControlToValidate="txtEmail" EnableClientScript="false" Display="dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="txtEmail" ErrorMessage=" Please enter a valid email" Display="Dynamic"
                    CssClass="failureNotification" ValidationGroup="agencyInformation" EnableClientScript="false"></asp:RegularExpressionValidator>
            </asp:Panel>
            <asp:Panel ID="pnlPhone" runat="server" Visible="false">
                <table>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" Text="Phone:"></asp:Label>
                            <asp:TextBox runat="server" ID="txtPhone"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ErrorMessage="*" CssClass="failureNotification"
                                ControlToValidate="txtPhone" EnableClientScript="false" Display="dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPhone" runat="server" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                                ControlToValidate="txtPhone" ErrorMessage=" Please enter a valid phone number"
                                ValidationGroup="agencyInformation" CssClass="failureNotification" EnableClientScript="false"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" Text="Preferred Call Time:"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlPreferredCallTime">
                                <asp:ListItem Value="" Text="Select One"></asp:ListItem>
                                <asp:ListItem Value="Morning" Text="Morning"></asp:ListItem>
                                <asp:ListItem Value="Afternoon" Text="Afternoon"></asp:ListItem>
                                <asp:ListItem Value="Evening" Text="Evening"></asp:ListItem>
                                <asp:ListItem Value="Anytime" Text="Anytime"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:Label ID="lblSubmission" runat="server"></asp:Label>
            <asp:HyperLink ID="hylReturnToQuotes" runat="server" Text="Return to Home" NavigateUrl="~/Wepages/Home.aspx"
                Visible="false" />
            <br />
        </center>
    </asp:Panel>
    <center>
        <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" />
        <asp:Button ID="btnSendToAgent" Text="Send Quote to Agent" runat="server" OnClick="btnSendToAgent_Click" />
        <asp:Button ID="btnQuotePDF" runat="server" Text="View PDF" Visible="false" OnClick="btnQuotePDF_Click" />
    </center>
    <center>
    <asp:HyperLink ID="hylHome" runat="server" Text="Return to Home" NavigateUrl="~/Wepages/Home.aspx"
        Visible="false" />
    </center>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Literal ID="htmlSteps" runat="server"></asp:Literal>
</asp:Content>

