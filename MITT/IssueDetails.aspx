<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="IssueDetails.aspx.cs" Inherits="MITT.IssueDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    <br />
    </p>
    <p>
        Requester
        <asp:TextBox ID="txtRequester" runat="server"></asp:TextBox>
    </p>
    <p>
    Title<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
    </p>
<p>
    DateLogged<asp:TextBox ID="txtDtLogged" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="User Urgency"></asp:Label>
        <asp:TextBox ID="txtUrgency" runat="server"></asp:TextBox>
    </p>
<p>
    Current Priority<asp:TextBox ID="txtPriority" runat="server"></asp:TextBox>
    </p>
<p>
    Category<asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
    </p>
    <p>
        Status&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
<p>
    <asp:Label ID="lblDetails" runat="server" Text="Label"></asp:Label>
</p>
    <p>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
</p>
    <p>
        &nbsp;</p>
    <p>
        Please type the message for the requester</p>
    <p>
        The issue # and title are inserted automatically in the email and you do not 
        need to mention that</p>
    <p>
        <asp:TextBox ID="TextBox5" runat="server" Height="60px" Width="332px"></asp:TextBox>
</p>
    <p>
        Email was sent to</p>
    <p>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
</p>
    <p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Send Email" />
    </p>
    <p>
        &nbsp;</p>
</asp:Content>
