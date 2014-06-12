<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Quotes.aspx.cs" Inherits="Wepages_Quotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Quotes</h2>
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <ul class="ul">
                <li>
                    <asp:ImageButton ID="ImbAdd" runat="server" ImageUrl="~/Images/Add.png" Height="12px"
                        OnClick="ImbAdd_Click" />&nbsp; Create a Quote </li>
                <br />
                <li>To view or edit existing quotes, please sign in
                    <asp:HyperLink ID="hylLogin" runat="server" NavigateUrl="~/Wepages/Authentication/Login.aspx">here</asp:HyperLink>
                </li>
            </ul>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <ul class="ul">
                <li>
                    <asp:ImageButton ID="ImbAdd" runat="server" ImageUrl="~/Images/Add.png" Height="12px"
                        OnClick="ImbAdd_Click" />&nbsp; Create a Quote
                    <li>
                        <asp:ImageButton ID="imbViewQuote" runat="server" Height="12px" ImageUrl="~/Images/View.png"
                            OnClick="imbViewQuote_Click" />
                        &nbsp; View a Quote </li>
                </li>
                <br />
                </li>
            </ul>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
