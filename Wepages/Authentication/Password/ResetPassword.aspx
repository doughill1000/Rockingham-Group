<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="Wepages_ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" Height="76px" Width="547px">
        <QuestionTemplate>
            <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                <tr>
                    <td>
                        <table cellpadding="0" style="height: 76px; width: 547px;">
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>Please answer your security question to reset your password.</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    User Name:
                                </td>
                                <td>
                                    <asp:Literal ID="UserName" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Question:
                                </td>
                                <td>
                                    <asp:Literal ID="Question" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Answer:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                        ErrorMessage="Answer is required." ToolTip="Answer is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="color: Red;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </QuestionTemplate>
        <SuccessTemplate>
            <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                <tr>
                    <td>
                        <table cellpadding="0" style="height: 76px; width: 338px;">
                            <tr>
                                <td>
                                    <strong>Success!</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Your new password has been sent to you.<br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Return to
                                    <asp:HyperLink ID="hypLogin" NavigateUrl="~/Wepages/Authentication/Login.aspx" runat="server">Login</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </SuccessTemplate>
        <UserNameTemplate>
            <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                <tr>
                    <td>
                        <table cellpadding="0">
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>Enter your Username below:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <caption>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                    </td>
                                    <td style="width: 176px">
                                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                            ValidationGroup="PasswordRecovery1">*<br/></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="color: Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" />
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </td>
                </tr>
            </table>
        </UserNameTemplate>
    </asp:PasswordRecovery>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
