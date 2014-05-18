<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="IssueDetailsForSR _notused.aspx.cs" Inherits="MITT.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <p>
    <br />
    </p>
<p>
    <asp:GridView ID="issueGrid" runat="server" 
        onselectedindexchanged="issueGrid_SelectedIndexChanged">
    </asp:GridView>
    </p>
<p>
    <asp:CheckBox ID="chkClose" runat="server" />
    </p>
    <p>
        <asp:TextBox ID="txtMemo" runat="server" Height="169px" Width="551px" 
            TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnNewMemo" runat="server" onclick="btnNewMemo_Click" 
            Text="New Memo" />
    </p>
    <p>
        &nbsp;&nbsp;<asp:TextBox ID="txtNewMemo" runat="server" Height="47px" 
            TextMode="MultiLine" Width="547px"></asp:TextBox>
        <asp:Button ID="btnSaveMemo" runat="server" onclick="btnSaveMemo_Click" 
            Text="Save Memo" />
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
