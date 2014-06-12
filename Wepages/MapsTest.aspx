<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MapsTest.aspx.cs" Inherits="Wepages_MapsTest" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <style>
      #map_canvas {
        width: 500px;
        height: 400px;
        background-color: #CCC;
      }
    </style>
    <asp:Label ID="lblLat" runat="server"></asp:Label>
    <asp:Label ID="lblLong" runat="server"></asp:Label>
    <asp:Label ID="selected" runat="server" Text="TEST" ClientIDMode="Static"></asp:Label>
    <asp:Button ID="test" runat="server" ClientIDMode="Static" Visible="false"/>

    <div id="map_canvas"><asp:Literal ID="jsMap" runat="server"></asp:Literal></div>
    
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

