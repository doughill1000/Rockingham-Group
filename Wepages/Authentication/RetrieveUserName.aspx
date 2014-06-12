<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RetrieveUserName.aspx.cs" Inherits="Wepages_Authentication_Password_RetrieveUserName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Username Retrieval</h3>
    <ul class="ul">
        <li>Please enter your email address: &nbsp
            <asp:TextBox ID="txtEmailAddress" runat="server" />
        </li>
        <li>
            <asp:Label ID="lblResponse" runat="server"></asp:Label></li>
        <li>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
