<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="UserConsole.aspx.cs" Inherits="MITT.UserConsole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type = "text/javascript" language = "javascript">
        function validate_pass() 
        {
            if (document.getElementById('<%=txtPh.ClientID%>').value == "") {
                alert("Phone cannot be blank");
                document.getElementById('<%=txtPh.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtcompany.ClientID%>').value == "") {
                alert("Company cannot be blank");
                document.getElementById('<%=txtcompany.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtPwd.ClientID%>').value == "") {
                alert("Password cannot be blank");
                document.getElementById('<%=txtPwd.ClientID%>').focus();
                return false;
            }
            
          }
            
            function nocopy() {
                alert('Copy/paste/cut not allowed');
                return false;
            }
            function validate_Issue() {
                if (document.getElementById('<%=txtTitle.ClientID%>').value == "") {
                    alert("Title cannot be blank");
                    document.getElementById('<%=txtTitle.ClientID%>').focus();
                    return false;
                }
                if (document.getElementById('<%=txtDetails.ClientID%>').value == "") {
                    alert("Please give some details");
                    document.getElementById('<%=txtDetails.ClientID%>').focus();
                    return false;
                }
                return true;
            }

</script>
    <asp:Button ID="btnNewIssue" runat="server" Height="96px" 
    Text="Create New Issue" Width="322px" onclick="btnNewIssue_Click" />
<asp:Button ID="btnIssueHistory" runat="server" Height="101px" 
    Text="View Issue History" Width="301px" onclick="btnIssueHistory_Click" />
<asp:Button ID="btnProfile" runat="server" Height="102px" 
    style="margin-left: 1px" Text="Update Profile" Width="287px" 
    onclick="btnProfile_Click" />
<br />
    <asp:Label ID="lblErrMsg" runat="server" Font-Bold="True" Font-Size="Large" 
        ForeColor="#CC0000" Text="Error on Page Please contact the Administrator" 
        Visible="False"></asp:Label>
<br />
<asp:Multiview ID="MultiView1" runat="server">
<asp:View ID="View0" runat="server">
     <asp:Panel ID="pnlLogNewIssue" runat="server" DefaultButton="btnLogIssue" Font-Bold="True" Font-Size="Large">
        Title
        <asp:TextBox ID="txtTitle" runat="server" Width="419px" 
            ontextchanged="txtTitle_TextChanged"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MITTConnectionString10 %>" 
            SelectCommand="SELECT * FROM [Queue]">
        </asp:SqlDataSource>
        <br />
         &nbsp;<asp:Label ID="lblUrgency" runat="server" ForeColor="#000099" Text="Urgency"></asp:Label>
&nbsp;
         <asp:DropDownList ID="drpUrgency" runat="server" 
             onselectedindexchanged="drpUrgency_SelectedIndexChanged">
             <asp:ListItem>Low</asp:ListItem>
             <asp:ListItem>Medium</asp:ListItem>
             <asp:ListItem>High</asp:ListItem>
             <asp:ListItem>Critical</asp:ListItem>
         </asp:DropDownList>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
         &nbsp;&nbsp;<asp:DropDownList ID="drpCat" runat="server" DataSourceID="SqlDataSource1" 
             DataTextField="Name" DataValueField="Name" 
             onselectedindexchanged="drpCat_SelectedIndexChanged" Height="24px" 
             Width="219px" AutoPostBack="True">
         </asp:DropDownList>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
         <br />
         <asp:Label ID="lblDetails" runat="server" Text="Give Details :"></asp:Label>
        <br />
        <asp:TextBox ID="txtDetails" runat="server" Height="101px" Width="351px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnLogIssue" runat="server" onclick="btnLogIssue_Click" OnClientClick = "return validate_Issue()"
             Text="Log this Issue" Width="104px" />
        <br />
        <br />
        </asp:Panel>
        <asp:Panel ID="pnlIssueNo" runat="server">
            &nbsp;<asp:Label ID="lblUrIssue" runat="server" Text="Your issue"></asp:Label>
&nbsp;<asp:Label ID="lblIssueNo" runat="server"></asp:Label>
            &nbsp;<asp:Label ID="lblLogged" runat="server" Text="has been logged"></asp:Label>
            <br />
        </asp:Panel>
    </asp:View>
    
    <asp:View ID="View1" runat="server">
    

<div style = "height:30px;width:600px; margin:0;padding:0">

<table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader"
bgcolor="#00FF00"
 style="font-family:Arial;font-size:10pt;width:617px;color:white;

 border-collapse:collapse;height:100%;">

    <tr>

       <td style ="width:75px;text-align:center">Issue No</td>

       <td style ="width:250px;text-align:center">Title</td>

       <td style ="width:175px;text-align:center">Time Logged</td>

       <td style ="width:100px;text-align:center">Status</td>
       <td style ="width:17px;text-align:center"></td>

    </tr>

</table>

</div>

     <div style ="height:300px; width:617px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        style="height:300px; overflow:scroll" ShowHeader = "false"
        onselectedindexchanged="GridView1_SelectedIndexChanged1" 
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
            CellPadding="2" ForeColor="Black" GridLines="None" 
            HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
         <Columns>
            <asp:BoundField DataField="id" ItemStyle-Width = "75px" ItemStyle-cssClass= "TACenter" />
            <asp:BoundField DataField="title" ItemStyle-Width = "250px" ItemStyle-cssClass= "TACenter" />
            <asp:BoundField DataField="timelogged" ItemStyle-Width = "175px" ItemStyle-cssClass= "TACenter" />
            <asp:BoundField DataField="Status" ItemStyle-Width = "100px" ItemStyle-cssClass= "TACenter" />
        </Columns>
       
        </asp:GridView>
       </div>
        <asp:Label ID="lblZeroRecords" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="blEmailMITT" runat="server" 
            
            Text="To contact MITT administrator, please send an email to the following address. Please include Issue#"></asp:Label>
        <p>
            <asp:Label ID="lblMITTEmail" runat="server" ForeColor="Maroon"></asp:Label>
        </p>
        <br />
    </asp:View>
        <asp:View ID="View2" runat="server">
        <asp:Panel ID="pnlProfile" runat="server" DefaultButton="btnProfileSave">
            <br />
        <br />
            <asp:Label ID="lblName_" runat="server" Text="Name"></asp:Label>
            <asp:Label ID="lblName" runat="server" Width="150px"></asp:Label>
            <br />
            <asp:Label ID="blEmail_" runat="server" Text="Email"></asp:Label>
            <asp:Label ID="lblEmail" runat="server" Width="250px"></asp:Label>
        <br />
            <asp:Label class= "labelClass" ID="lblPhone" runat="server" Text="Phone"></asp:Label>
            <asp:TextBox ID="txtPh" runat="server" Enabled="False" 
                ontextchanged="txtPh_TextChanged"></asp:TextBox>
            <asp:Button ID="btnEditPh" runat="server" onclick="btnEditPh_Click" 
                Text="Edit" />
        <br />
            <asp:Label class= "labelClass" ID="lblCompany" runat="server" Text="Company "></asp:Label>
            <asp:TextBox ID="txtcompany" runat="server" Enabled="False" 
                ontextchanged="txtcompany_TextChanged"></asp:TextBox>
            <asp:Button ID="btnEditComp" runat="server" onclick="btnEditComp_Click" 
                Text="Edit" />
            <br />
            <asp:Label class= "labelClass"  ID="lblPwd" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPwd" runat="server" ontextchanged="txtPwd_TextChanged" 
                Enabled="False"></asp:TextBox>
            <asp:Button ID="btnEditPwd" runat="server" onclick="btnEditPwd_Click" 
                Text="Edit" />
            <br />
            <asp:Label class= "labelClass" ID="lblConfirmPwd" runat="server" Text="Confirm Password" 
                Visible="False"></asp:Label>
            <asp:TextBox ID="txtConfirmPwd" runat="server" Visible="False" 
                TextMode="Password"></asp:TextBox>
                
        <br />
            <br />
        <br />
        <asp:Button ID="btnProfileSave" runat="server" OnClientClick = "return validate_pass()"
        onclick="btnProfileSave_Click" 
            Text="Update My Profile" Enabled="False" />
        <br />
        <asp:Label ID="lblChngProfile" runat="server"></asp:Label>
            <br />
        <br />
        </asp:Panel>
                    <br />
            <br />
    </asp:View>
    </asp:MultiView>
</asp:Content>
