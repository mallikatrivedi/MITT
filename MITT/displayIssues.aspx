<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="displayIssues.aspx.cs" Inherits="MITT.displayIssues" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="lblIssueCount" runat="server" Text="Label"></asp:Label>
<br />
    <asp:panel ID="pnlHeadings" runat="server">
    <div style = "height:30px;width:600px; margin:0;padding:0">
    <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader"
    bgcolor="#00FF00"
    style="font-family:Arial;font-size:10pt;width:617px;color:white;
    border-collapse:collapse;height:100%;">
    <tr>
       <td style ="width:75px;text-align:center">Issue No</td>
       <td style ="width:250px;text-align:center">Title</td>
       <td style ="width:175px;text-align:center">User Name</td>
       <td style ="width:175px;text-align:center">Time Logged</td>
       <td style ="width:100px;text-align:center">Priority</td>
       <td style ="width:100px;text-align:center">Status</td>
       <td style ="width:150px;text-align:center">Issue Details</td>
       <td style ="width:17px;text-align:center"></td>
    </tr>
</table>
</div>
</asp:panel>
    
   <div style ="height:200px; width:617px; overflow:auto;">
   
    <asp:GridView ID="issuesGrid" runat="server" AutoGenerateColumns="False"
    style="height:200px; overflow:scroll" ShowHeader = "false"
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
            CellPadding="2" ForeColor="Black" GridLines="None" 
            HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
        <asp:BoundField DataField="id" ItemStyle-Width = "75px" ItemStyle-cssClass= "TACenter" />
            <asp:BoundField DataField="title" ItemStyle-Width = "250px" ItemStyle-cssClass= "TACenter" />
            <asp:BoundField DataField="Requester" ItemStyle-Width = "175px" ItemStyle-cssClass= "TACenter" />
            <asp:BoundField DataField="timelogged" ItemStyle-Width = "175px" ItemStyle-cssClass= "TACenter" />
            <asp:BoundField DataField="Priority" ItemStyle-Width = "100px" ItemStyle-cssClass= "TACenter" />
            <asp:BoundField DataField="Status" ItemStyle-Width = "100px" ItemStyle-cssClass= "TACenter" />
            <asp:HyperLinkField 
                Text="Get Details" ItemStyle-Width = "150px" DataNavigateUrlFields="id" 
                DataNavigateUrlFormatString="~/IssueDetails.aspx?id={0}" />
        </Columns>
    </asp:GridView>  
   </div> 
    <br />
</asp:Content>
