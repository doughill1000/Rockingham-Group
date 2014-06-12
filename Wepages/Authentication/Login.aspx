<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Wepages_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false"
        DestinationPageUrl="~/Wepages/Home.aspx">
        <LayoutTemplate>
            <div class="accountInfo">
                <fieldset class="fieldset" style="width: 470px">
                    <legend>Account Information</legend>
                    <br />
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Width="325px">Username:</asp:Label>
                    <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator><br />
                    <br />
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Width="325px">Password:</asp:Label>
                    <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server" />
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                    </p>
                </fieldset>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <p class="submitButton">
                    <asp:Button ID="Button1" runat="server" CommandName="Login" Text="Log In" 
                        ValidationGroup="LoginUserValidationGroup" />
                </p>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="LoginUserValidationGroup" />
            </div>
        </LayoutTemplate>
    </asp:Login>
    <br />
    Don't have a login? Please click
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Wepages/Authentication/Register.aspx">here</asp:HyperLink>&nbsp;to
    register.
    <br />
    <br />
    Forgot your password? Please click
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Wepages/Authentication/Password/ResetPassword.aspx">here</asp:HyperLink>&nbsp;to
    reset it
    <br />
    Forgot your username? Please click     <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="RetrieveUserName.aspx">here</asp:HyperLink>&nbsp;to
    retrieve it
</asp:Content>
