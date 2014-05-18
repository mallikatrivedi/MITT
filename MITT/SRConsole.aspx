<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="SRConsole.aspx.cs" Inherits="MITT.SRConsole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type = "text/javascript" language = "javascript">
    function validate_IssueNo() {

        var issueNo = document.getElementById('<%=txtIssueNo.ClientID%>').value;
        if (typeof issueNo == 'Number') {
            alert("Please enter a valid integer for issue number");
            document.getElementById('<%=txtIssueNo.ClientID%>').focus();
            return false;
        }
        
        return true;
    }

</script>
    <asp:Button ID="btnUpdateIssue" runat="server" Height="59px" Text="Update Issues" 
        Width="187px" onclick="btnTrackIssue_Click" style="margin-right: 47px" />
    <asp:Button ID="btnWorkload" runat="server" Height="57px" 
        Text="Check Queue wise Workload" Width="184px" 
        onclick="btnWorkload_Click" />
    &nbsp;
    <br />
    <asp:Label ID="lblErrMsg" runat="server" Font-Bold="True" Font-Size="Large" 
    ForeColor="#CC0000" Text="Error on page, please contact the Administrator" 
    Visible="False"></asp:Label>
    <br />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
        <asp:Panel ID="pnlUpdate" runat="server" DefaultButton="btnGetDetails">
            <asp:Label ID="lbIssueNo" runat="server" Text="Give issue No to be updated"></asp:Label>
            <asp:TextBox ID="txtIssueNo" runat="server" 
                ontextchanged="txtIssueNo_TextChanged"></asp:TextBox>
            <asp:Button ID="btnGetDetails" runat="server" onclick="btnGetDetails_Click" onClientClick="validate_IssueNo()"
                Text="GetDetails" />
            <br />
            <br />
            <asp:Label ID="lblIssueMsg" runat="server" Text="Please check the issue no, this issue no not found!" 
                Visible="False"></asp:Label>
           
                <asp:GridView ID="issueGrid0" runat="server" 
                    onselectedindexchanged="issueGrid_SelectedIndexChanged" 
                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                    CellPadding="2" ForeColor="Black" GridLines="None" Width="317px">
                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    <FooterStyle BackColor="Tan" />
                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                        HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                </asp:GridView> 
            
            <p>
                <asp:CheckBox ID="chkClose" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chkClose_CheckedChanged" Text="Close/Reopen" />
            </p>
            <p>
                <asp:TextBox ID="txtMemo" runat="server" Height="143px" TextMode="MultiLine" 
                    Width="611px"></asp:TextBox>
            </p>
            <p>
                <asp:Button ID="btnNewMemo" runat="server" onclick="btnNewMemo_Click" 
                    Text="New Memo" />
            </p>
            <p>
                &nbsp;&nbsp;<asp:TextBox ID="txtNewMemo" runat="server" Height="47px" 
                    TextMode="MultiLine" Width="609px"></asp:TextBox>
                <asp:Button ID="btnSaveMemo" runat="server" onclick="btnSaveMemo_Click" 
                    Text="Save Memo" />
            
            </asp:Panel>
        </asp:View>
       
        <asp:View ID="View2" runat="server">
            <br />
            <asp:Button ID="btnAssignedQueues" runat="server" 
                onclick="btnAssignedQueues_Click" 
                Text="Click here to list the queues assigned to you" />
            <br />
            <asp:Label ID="lblSelectQueue" runat="server" Text="Please select a queue" 
                Visible="False"></asp:Label>
            <br />
            <asp:DropDownList ID="drpQueueAssign" runat="server" Height="20px" 
                Width="124px" onselectedindexchanged="drpQueueAssign_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button ID="btnSRSearchIssue" runat="server" 
                onclick="btnSRSearchIssue_Click" Text="Go" />
            <asp:Label ID="lblNoQ" runat="server" Text="No Queues are assigned to you yet"></asp:Label>
            <br />
            <asp:Label ID="lblIssueCount" runat="server"></asp:Label>
            <br />

            <asp:Panel ID="pnlIssue" runat="server">
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
       <td style ="width:17px;text-align:center"></td>

    </tr>

</table>

</div>

     <div style ="height:200px; width:617px; overflow:auto;">
        <asp:GridView ID="issueGrid" runat="server" AutoGenerateColumns="False"
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
        </Columns>
       
        </asp:GridView>
       </div>
       </asp:Panel>
            <br />
            <br />
        </asp:View>
    </asp:MultiView>
    <br />
    <br />
    <br />
</asp:Content>
