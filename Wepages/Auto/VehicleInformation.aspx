<%@ Page Title="" Language="C#" MasterPageFile="~/MasterQuote.master" AutoEventWireup="true"
    CodeFile="VehicleInformation.aspx.cs" Inherits="Wepages_Auto_VehicleInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pblVehicle" runat="server" DefaultButton="btnContinue">
        <h2>
            Vehicle</h2>
        <asp:Label ID="lblRequiredFieldError" runat="server" CssClass="failureNotification" />
        <fieldset class="fieldset">
            <legend>
                <h3>
                    Vehicle Information</h3>
            </legend>
            <ul class="ul">
                <li>
                    <asp:Label ID="lblVehicleType" runat="server" Text="Vehicle Type:" Width="325px" />
                    <asp:DropDownList ID="ddlVehicleType" runat="server">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="Car/Truck" Value="Car/Truck"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvVehicleType" runat="server" ErrorMessage="*" EnableClientScript="false"
                        CssClass="failureNotification" ControlToValidate="ddlVehicleType" Display="dynamic"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Label ID="lblVIN" runat="server" Text="Vin Number:" Width="325px" />
                    <asp:TextBox ID="txtVIN" runat="server" OnTextChanged="txtVIN_TextChanged" AutoPostBack="true" />
                </li>
                <li>
                    <asp:Label ID="lblYear" runat="server" Text="Year:" Width="325px" />
                    <asp:DropDownList ID="ddlYear" runat="server" DataSourceID="sdsYear" DataTextField="VYEAR"
                        DataValueField="VYEAR" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                        AutoPostBack="true" OnDataBound="ddlYear_DataBound">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="*" EnableClientScript="false"
                        CssClass="failureNotification" ControlToValidate="ddlYear" Display="dynamic"></asp:RequiredFieldValidator>
                    <asp:SqlDataSource ID="sdsYear" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="SELECT DISTINCT [VYEAR] FROM [VIN] ORDER BY [VYEAR] DESC"></asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label ID="lblMake" runat="server" Text="Make:" Width="325px" Visible="false" />
                    <asp:DropDownList ID="ddlMake" runat="server" DataSourceID="sdsMake" Visible="false"
                        OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" DataTextField="MAKE" DataValueField="MAKE"
                        AutoPostBack="true" OnDataBound="ddlMake_DataBound">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvMake" runat="server" ErrorMessage="*" EnableClientScript="false"
                        CssClass="failureNotification" ControlToValidate="ddlMake" Display="Dynamic"
                        Visible="false"></asp:RequiredFieldValidator>
                    <asp:SqlDataSource ID="sdsMake" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="SELECT DISTINCT [MAKE] FROM [VIN] WHERE ([VYEAR] = @VYEAR) ORDER BY [MAKE]">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="" Name="VYEAR" Type="Double" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label ID="lblModel" runat="server" Text="Model:" Width="325px" Visible="false" />
                    <asp:DropDownList ID="ddlModel" runat="server" DataSourceID="sdsModel" Visible="false"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" DataTextField="Model"
                        DataValueField="Model" OnDataBound="ddlModel_DataBound">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvModel" runat="server" ErrorMessage="*" EnableClientScript="false"
                        CssClass="failureNotification" ControlToValidate="ddlModel" Display="Dynamic"
                        Visible="false"></asp:RequiredFieldValidator>
                    <asp:SqlDataSource ID="sdsModel" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="SELECT DISTINCT [MODEL] FROM [VIN] WHERE (([VYEAR] = @VYEAR) AND ([MAKE] = @MAKE)) ORDER BY [MODEL] DESC">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="" Name="VYEAR" Type="Double" />
                            <asp:Parameter DefaultValue="" Name="MAKE" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label ID="lblBodyType" runat="server" Text="Body Type:" Width="325px" Visible="false" />
                    <asp:DropDownList ID="ddlBodyType" runat="server" DataSourceID="sdsBodyType" Visible="false"
                        OnSelectedIndexChanged="ddlBodyType_SelectedIndexChanged" DataTextField="Body"
                        AutoPostBack="true" DataValueField="Body" OnDataBound="ddlBodyType_DataBound">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsBodyType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="SELECT DISTINCT [BODY] FROM [VIN] WHERE (([VYEAR] = @VYEAR) AND ([MAKE] = @MAKE) AND ([MODEL] = @MODEL)) ORDER BY [BODY] DESC">
                        <SelectParameters>
                            <asp:Parameter Name="VYEAR" Type="Double" />
                            <asp:Parameter Name="MAKE" Type="String" />
                            <asp:Parameter Name="MODEL" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label ID="lblEngineCylPartialVIN" runat="server" Text="Engine Cyl/Partial VIN:"
                        Width="325px" Visible="false" />
                    <asp:DropDownList ID="ddlEngineCylPartialVIN" runat="server" DataSourceID="sdsEngineCylPartialVIN"
                        Visible="false" DataTextField="ENGINE_CYLINDERS" DataValueField="ENGINE_CYLINDERS"
                        OnDataBound="ddlEngineCylPartialVIN_DataBound">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsEngineCylPartialVIN" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="SELECT DISTINCT [ENGINE_CYLINDERS] FROM [VIN] WHERE (([VYEAR] = @VYEAR) AND ([MAKE] = @MAKE) AND ([MODEL] = @MODEL) AND ([BODY] = @BODY)) ORDER BY [ENGINE_CYLINDERS] DESC">
                        <SelectParameters>
                            <asp:Parameter Name="VYEAR" Type="Double" />
                            <asp:Parameter Name="MAKE" Type="String" />
                            <asp:Parameter Name="MODEL" Type="String" />
                            <asp:Parameter Name="BODY" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </li>
            </ul>
        </fieldset>
        <fieldset class="fieldset">
            <legend>
                <h3>
                    Garaging Information</h3>
            </legend>
            <ul class="ul">
                <li>
                    <asp:Label ID="lblAwayAtSchool" runat="server" Text="Vehicle Away at School:" Width="325px" />
                    <asp:DropDownList ID="ddlAwayAtSchool" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAwayAtSchool_SelectedIndexChanged"
                        Style="height: 25px">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        <asp:ListItem Text="In-State" Value="In-State"></asp:ListItem>
                        <asp:ListItem Text="Out-of State" Value="Out-of State"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="lblInStateZipCode" runat="server" Text="College Zip Code:" Width="325px"
                        Visible="false" />
                    <asp:TextBox ID="txtInStateZipCode" runat="server" Visible="false" OnTextChanged="txtInStateZipCode_TextChanged" />
                    <asp:RegularExpressionValidator ID="revInStateZipCode" runat="server" ErrorMessage="Please enter a valid zip code"
                        ValidationExpression="\d{5}-?(\d{4})?$" ControlToValidate="txtInStateZipCode"
                        CssClass="failureNotification" Visible="false"></asp:RegularExpressionValidator>
                </li>
                <li>
                    <asp:Label ID="lblCollegeName" runat="server" Text="College Name: " Width="325px"
                        Visible="false" />
                    <asp:TextBox ID="txtCollegeName" runat="server" Visible="false" />
                    <asp:RegularExpressionValidator ID="revCollegeName" runat="server" ErrorMessage="Please enter a valid college name"
                        ValidationExpression="^[a-zA-Z'.\s]{1,50}" ControlToValidate="txtCollegeName"
                        CssClass="failureNotification" Display="Dynamic" ValidationGroup="applicationInformation"
                        EnableClientScript="false"></asp:RegularExpressionValidator>
                </li>
                <li>
                    <asp:Label ID="lblCollegePop" runat="server" Text="College Population: " Width="325px"
                        Visible="false" />
                    <asp:DropDownList ID="ddlCollegePop" runat="server" Visible="false">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="less than 30,000" Value="less than 30,000"></asp:ListItem>
                        <asp:ListItem Text="30,000 - 249,999" Value="30,000 - 249,999"></asp:ListItem>
                        <asp:ListItem Text="325,000 or greater" Value="325,000 or greater"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="lblGaragedAtCurrentAddress" runat="server" Text="Garaged at Current Address:"
                        Width="325px" />
                    <asp:DropDownList ID="ddlGaragedAtCurrentAddress" runat="server" OnSelectedIndexChanged="ddlGaragedAtCurrentAddress_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="No" Value="false"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="lblGarageZipCode" runat="server" Text="Garage Zip Code:" Width="325px"
                        Visible="false" />
                    <asp:TextBox ID="txtGarageZipCode" runat="server" Visible="false" OnTextChanged="txtGarageZipCode_TextChanged" />
                    <asp:RegularExpressionValidator ID="revGarageZipCode" runat="server" ErrorMessage="Please enter a valid zip code"
                        ValidationExpression="\d{5}-?(\d{4})?$" ControlToValidate="txtGarageZipCode"
                        CssClass="failureNotification" Visible="false"></asp:RegularExpressionValidator>
                </li>
            </ul>
        </fieldset>
        <fieldset class="fieldset">
            <legend>
                <h3>
                    Safety Features</h3>
            </legend>
            <ul class="ul">
                <li>
                    <asp:Label ID="lblAntiTheftDevices" runat="server" Text="Anti-Theft Devices:" Width="325px" />
                    <asp:DropDownList ID="ddlAntiTheftDevices" runat="server">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        <asp:ListItem Text="Alarm Only" Value="Alarm Only"></asp:ListItem>
                        <asp:ListItem Text="Passive & Active Device" Value="Passive & Active Device"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvAntiTheftDevices" runat="server" ErrorMessage="*"
                        CssClass="failureNotification" Display="Dynamic" ControlToValidate="ddlAntiTheftDevices"
                        EnableClientScript="false"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Label ID="lblAntiLockBrakes" runat="server" Text="Anti-Lock Brakes:" Width="325px" />
                    <asp:DropDownList ID="ddlAntiLockBrakes" runat="server">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        <asp:ListItem Text="4-Wheel" Value="4-Wheel"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvAntiLockBrakes" runat="server" ErrorMessage="*"
                        CssClass="failureNotification" Display="Dynamic" ControlToValidate="ddlAntiLockBrakes"
                        EnableClientScript="false"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Label ID="lblAirbags" runat="server" Text="Airbags:" Width="325px" />
                    <asp:DropDownList ID="ddlAirbags" runat="server">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        <asp:ListItem Text="Driver Only Airbag" Value="Driver Only Airbag"></asp:ListItem>
                        <asp:ListItem Text="Passenger Airbag" Value="Passenger Airbag"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvAirbags" runat="server" ErrorMessage="*" CssClass="failureNotification"
                        EnableClientScript="false" Display="Dynamic" ControlToValidate="ddlAirbags"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </fieldset>
        <fieldset class="fieldset">
            <legend>
                <h3>
                    Vehicle Usage & Coverages</h3>
            </legend>
            <ul class="ul">
                <li>
                    <asp:Label ID="lblUse" runat="server" Text="Use:" Width="325px" />
                    <asp:DropDownList ID="ddlUse" runat="server">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="Pleasure (No Work Use)" Value="Pleasure (No Work Use)"></asp:ListItem>
                        <asp:ListItem Text="Work Use (Drive to work or school)" Value="Work Use (Drive to work or school)"></asp:ListItem>
                        <asp:ListItem Text="Business Use or Farm Use" Value="Business Use or Farm Use"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvUse" runat="server" ControlToValidate="ddlUse"
                        ErrorMessage="*" EnableClientScript="false" CssClass="failureNotification" Display="Dynamic" />
                </li>
                <li>
                    <asp:Label ID="lblOtherThanCollisionDeductable" runat="server" Text="Other Than Collision Deductible:"
                        Width="325px" />
                    <asp:DropDownList ID="ddlOtherThanCollisionDeductable" runat="server" AutoPostBack="true"
                        
                        OnSelectedIndexChanged="ddlOtherThanCollisionDeductable_SelectedIndexChanged1">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="No Cov" Value="0"></asp:ListItem>
                        <asp:ListItem Text="$100" Value="$100"></asp:ListItem>
                        <asp:ListItem Text="$325" Value="$325"></asp:ListItem>
                        <asp:ListItem Text="$500" Value="$500"></asp:ListItem>
                        <asp:ListItem Text="$1000" Value="$1000"></asp:ListItem>
                        <asp:ListItem Text="$2000" Value="$2000"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvOtherThanCollisionDeductable" runat="server" ControlToValidate="ddlOtherThanCollisionDeductable"
                        ErrorMessage="*" EnableClientScript="false" CssClass="failureNotification" Display="Dynamic" />
                </li>
                <li>
                    <asp:Label ID="lblCollisionDeductible" runat="server" Text="Collision Deductible:"
                        Width="325px" Visible="false" />
                    <asp:DropDownList ID="ddlCollisionDeductible" runat="server" Visible="false"  
                        >
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="No Cov" Value="0"></asp:ListItem>
                        <asp:ListItem Text="$100" Value="$100"></asp:ListItem>
                        <asp:ListItem Text="$325" Value="$325"></asp:ListItem>
                        <asp:ListItem Text="$500" Value="$500"></asp:ListItem>
                        <asp:ListItem Text="$1000" Value="$1000"></asp:ListItem>
                        <asp:ListItem Text="$2000" Value="$2000"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="lblRentalReimbursement" runat="server" Text="Rental Reimbursement:"
                        Width="325px" Visible="false" />
                    <asp:DropDownList ID="ddlRentalReimbursement" runat="server" AutoPostBack="true"
                        Visible="false" OnSelectedIndexChanged="ddlRentalReimbursement_SelectedIndexChanged1">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="No Cov" Value="No Cov"></asp:ListItem>
                        <asp:ListItem Text="Option 1" Value="Option 1"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="lblRentalReimbursementLimit" runat="server" Text="Rental Reimbursement Limit:"
                        Width="325px" Visible="false" />
                    <asp:DropDownList ID="ddlRentalReimbursementLimit" runat="server" Visible="false">
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="$600" Value="$600"></asp:ListItem>
                        <asp:ListItem Text="$900" Value="$900"></asp:ListItem>
                        <asp:ListItem Text="$1,200" Value="$1,200"></asp:ListItem>
                        <asp:ListItem Text="$1,500" Value="$1,500"></asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
        </fieldset>
        <br />
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
