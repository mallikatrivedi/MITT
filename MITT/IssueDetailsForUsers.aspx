<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="MITT.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <p>
    <br />
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
        &nbsp;&nbsp;</p>
    You can send a message to MITT administrtor on to the following email address (Do not forget issue # and title)
        <p>
            &nbsp;</p>
    <p>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
   
</asp:Content>
