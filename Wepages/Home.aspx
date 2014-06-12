<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Wepages_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <header>
    </header>
    <asp:LoginView ID="LoginView1" runat="server" >
        <AnonymousTemplate>
                <h2>Welcome to our site!</h2>
                <p>To get started on a new quote click <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Wepages/GettingStarted.aspx">here</asp:HyperLink>.</p>
                <p>
                Want to access your quote information later? Please click
                <asp:HyperLink ID="hyplnkLogin" runat="server" NavigateUrl="~/Wepages/Authentication/Login.aspx">here</asp:HyperLink>&nbsp;to
                login.
                <br />
                Don't have a login? Please click
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Wepages/Authentication/Register.aspx">here</asp:HyperLink>&nbsp;to
                register.</p>
        </AnonymousTemplate>
        <LoggedInTemplate>
        
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>

