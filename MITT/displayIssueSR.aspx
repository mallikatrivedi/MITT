<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="displayIssueSR.aspx.cs" Inherits="MITT.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <br />
    <asp:Label ID="lblIssueCount" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    <br />

    
    <asp:GridView ID="issuesGrid" runat="server" 
        onselectedindexchanged="issuesGrid_SelectedIndexChanged">
        <Columns>
            <asp:HyperLinkField 
                Text="Get Details" DataNavigateUrlFields="id" 
                DataNavigateUrlFormatString="~/IssueDetails.aspx?id={0}" />
        </Columns>
    </asp:GridView> 
    <br />
</asp:Content>
