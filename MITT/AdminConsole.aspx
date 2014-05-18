<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="AdminConsole.aspx.cs" Inherits="MITT.AdminConsole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
        <script type = "text/javascript" language = "javascript">
            function validate_passQ() {
                if (document.getElementById('<%=txtQName.ClientID%>').value == "") {
                    alert("Queue name cannot be blank");
                    document.getElementById('<%=txtQName.ClientID%>').focus();
                    return false;
                }
                  }
                  
        function validate_passU() {
            
            if (document.getElementById('<%=txtName.ClientID%>').value == ""){
                alert("Name cannot be blank");
                document.getElementById('<%=txtName.ClientID%>').focus();
                return false;
            }
            if (document.getElementById("<%=txtUserEmail.ClientID %>").value == "") {
                alert("Email id can not be blank");
                document.getElementById("<%=txtUserEmail.ClientID %>").focus();
                return false;
            }
            var emailPat = /^(\".*\"|[A-Za-z]\w*)@(\[\d{1,3}(\.\d{1,3}){3}]|[A-Za-z]\w*(\.[A-Za-z]\w*)+)$/;
            var emailid = document.getElementById("<%=txtUserEmail.ClientID %>").value;
            var matchArray = emailid.match(emailPat);
            if (matchArray == null) {
                alert("Your email address seems incorrect. Please try again.");
                document.getElementById("<%=txtUserEmail.ClientID %>").focus();
                return false;
            }

            if (document.getElementById('<%=txtPh.ClientID%>').value == "q") {
                alert("Phone Field cannot be blank");
                document.getElementById('<%=txtPh.ClientID%>').focus();
                return false;
            }
            
            if (document.getElementById('<%=txtCompany.ClientID%>').value == "") {
                alert("Company Field cannot be blank");
                document.getElementById('<%=txtCompany.ClientID%>').focus();
                return false;
            }
            
            return true;
        }

</script>
    <asp:Button ID="btnIssue" runat="server" Height="87px" Text="Track Issues" 
        Width="196px" onclick="btnIssue_Click" />
    <asp:Button ID="btnCategory" runat="server" Height="86px" 
        Text="Manage Categories and Queues" Width="206px" 
    onclick="btnCategory_Click" />
    <asp:Button ID="btnRoles" runat="server" Height="89px" Text="Assign Roles" 
        Width="173px" onclick="btnRoles_Click" />
    <asp:Button ID="btnEmail" runat="server" Height="89px" onclick="btnEmail_Click" 
        Text="Mange email account" Width="165px" />
    <asp:Button ID="btnUser" runat="server" onclick="btnUser_Click" 
        Text="Manage User Accounts" Height="89px" Width="182px" />
    <br />
    <br />
    <asp:Label ID="lblErrMsg" runat="server" Font-Bold="True" Font-Size="Large" 
    ForeColor="#CC0000" Text="Error on Page, please check the Logs " 
    Visible="False"></asp:Label>
    <br />
    <br />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <asp:Button ID="btnNewUser" runat="server" onclick="btnNewUser_Click" 
                Text="Create a new user" />
            <asp:Button ID="btnFindUser" runat="server" Text="Find/Delete/Update User" 
                Width="237px" onclick="btnFindUser_Click" />
            <br />
            <br />
            <asp:Panel ID="pnlFindUser" runat="server" DefaultButton = "Go">
            
            <asp:DropDownList ID="drpListUser" runat="server" 
                onselectedindexchanged="drpListUser_SelectedIndexChanged">
                <asp:ListItem Value="List All"></asp:ListItem>
                <asp:ListItem>Contains</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Go" runat="server" onclick="Go_Click" style="width: 31px" 
                Text="Go" />
                </asp:Panel>
            <br />
            <asp:Panel ID="pnlCreateUser" runat="server" Visible="False" Height="264px" DefaultButton = "btnAddUser">
                &nbsp;<br />
                <asp:Label class= "labelClass" ID="lblName" runat="server" Text="Name"></asp:Label>

                <asp:TextBox ID="txtName" runat="server" ontextchanged="TextBox1_TextChanged"></asp:TextBox>
                <br />
                <asp:Label class= "labelClass" ID="lblMail" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="txtUserEmail" runat="server"></asp:TextBox>
                <br />
                <asp:Label class= "labelClass" ID="lblPh" runat="server" Text="PhoneNo"></asp:Label>
                <asp:TextBox ID="txtPh" runat="server"></asp:TextBox>
                <br />
                <asp:Label class= "labelClass" ID="lblCompany" runat="server" Text="Company Name"></asp:Label>
                <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                <br />
                <asp:Label class= "labelClass" ID="lblPswd" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <asp:Label class= "labelClass" ID="lblConfrmPwd" runat="server" Text="Confirm Password"></asp:Label>
                <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password"></asp:TextBox><br />
                <asp:Label ID="lblUserType" runat="server" Text="User Type"></asp:Label>
                <asp:RadioButtonList ID="rdUserType" runat="server" 
                    RepeatDirection="Horizontal" 
                    onselectedindexchanged="rdUserType_SelectedIndexChanged">
                    <asp:ListItem Value="2">End User</asp:ListItem>
                    <asp:ListItem Value="1">Support Rep</asp:ListItem>
                    <asp:ListItem Value="0">Administrator</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Button ID="btnAddUser" runat="server" OnClientClick= "validate_passU()" onclick="btnAddUser_Click" 
                    Text="ADD USER " />
                    <br />
                <asp:Label ID="lblAssignRoles" runat="server" 
                    Text=" Click on Assign Roles if you want to assign roles now"></asp:Label>
                    <br />
                <asp:Label ID="lblUserName" runat="server" Visible="False"></asp:Label>
            </asp:Panel>
            <br />
            <br />
            <br />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:Button ID="btnNewCat" runat="server" Text="New Queue/Category" 
                onclick="btnNewCat_Click" />
            <asp:Button ID="btnFindCat" runat="server" 
                Text="Find/Update/Delete Queue/Category" onclick="btnFindCat_Click" />
                <br>
            <br>
            <br></br>
            <asp:DropDownList ID="drpCatCriteria" runat="server" 
                onselectedindexchanged="drpCatCriteria_SelectedIndexChanged" Visible="False">
                <asp:ListItem Value="List All"></asp:ListItem>
                <asp:ListItem Value="Name Contains"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtCatName" runat="server" 
                ontextchanged="txtCatName_TextChanged" Visible="False"></asp:TextBox>
            <br></br>
            <asp:Button ID="btnFindCatQueue" runat="server" 
                onclick="btnFindCatQueue_Click1" Text="Find" Visible="False" />
            <br />
            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblQName" runat="server" Text="Name" Visible="False"></asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox ID="txtQName" runat="server" ontextchanged="txtQName_TextChanged" 
                style="margin-left: 55px" Visible="False" Width="174px"></asp:TextBox>
            &nbsp;<asp:Label ID="lblMsgQAdded" runat="server" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnAddQueue" runat="server" onclick="btnAddQueue_Click" 
                OnClientClick="validate_passQ()" Text="Add " Visible="False" />
            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnTryAgain" runat="server" onclick="btnTryAgain_Click" 
                Text="Try again" Visible="False" />
            
            <br />
           
        </asp:View>
        <asp:View ID="View3" runat="server">
            Select User and then assign queues:<br />
            <asp:Button ID="btnSelectUser" runat="server" onclick="btnSelectUser_Click" 
                Text="Select User" Width="158px" />
            <br />
            <asp:DropDownList ID="drpUser" runat="server" Height="16px" AutoPostBack="True"
                onselectedindexchanged="drpUser_SelectedIndexChanged" Width="165px" 
                Visible="False">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnAssign" runat="server" onclick="btnAssign_Click" 
                Text="CLICK TO SEE QUEUE ASSIGNMENT" />
            <br />
            <br />
             <div style ="height:200px; width:400px; overflow:auto;">
            <asp:CheckBoxList ID="chkQueue" runat="server">
            </asp:CheckBoxList>
            </div>
            
            <asp:Button ID="btnUpdAssign" runat="server" onclick="btnUpdAssign_Click" 
                Text="UPDATE ASSIGNMENT" />
            <br />
            <br />
        </asp:View>
        <asp:View ID="View4" runat="server">

            <br />
            <asp:Panel ID="pnlTrack" runat="server" DefaultButton="btnSearchIssue">
                Track issue based on<br />&nbsp;<asp:Button ID="btnUserIssue" runat="server" 
                    onclick="Button1_Click" Text="User" Width="112px" />
                <asp:Button ID="btnTimeIssue" runat="server" onclick="btnTimeIssue_Click" 
                    Text="Time logged" Width="101px" />
                <asp:Button ID="btnPriority" runat="server" onclick="btnPriority_Click" 
                    Text="Priority" Width="133px" />
                <asp:Button ID="btnCatg" runat="server" onclick="btnCatg_Click" Text="Category" 
                    Width="136px" />
                <br />
                <br />
                &nbsp;<asp:DropDownList ID="drpCriteria" runat="server" Height="30px" 
                    onselectedindexchanged="drpCriteria_SelectedIndexChanged" Visible="False" 
                    Width="170px">
                </asp:DropDownList>
                <br />
                <asp:Calendar ID="Calendar1" runat="server" 
                    onselectionchanged="Calendar1_SelectionChanged" Visible="False">
                </asp:Calendar>
                <asp:Button ID="btnSearchIssue" runat="server" onclick="btnSearchIssue_Click" 
                    Text="GO" />
            </asp:Panel>
            <br />
        </asp:View>
        <asp:View ID="View5" runat="server">
            <asp:Panel ID="pnlEmail" runat="server" DefaultButton="btnSubmit">
            
            <asp:Label ID="ConfigEmail" runat="server" 
                Text="Configure System Email Account"></asp:Label>
            <br />
            <asp:Label ID="lblEmail" runat="server" Text="Email Address"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" Width="233px"></asp:TextBox>
            <asp:Button ID="btnEditEmail" runat="server" Text="Edit" 
                    onclick="btnEditEmail_Click" />
            <br />
            &nbsp;<asp:Label ID="d" runat="server" Text="Password"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
&nbsp;
            <asp:Button ID="btnEditPwd" runat="server" Text="Edit" onclick="btnEditPwd_Click" />
            <br />
            <asp:Label ID="lblConfirmPwd" runat="server" Text=" Confirm Password"></asp:Label>
            <asp:TextBox ID="txtConfPwd" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnEditConfPwd" runat="server" Text="Edit" />
            <br />
            &nbsp;<br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                Text="Submit Changes" Width="145px" />
            <br />
            <asp:Label ID="lblSubmit" runat="server"></asp:Label>
            </asp:Panel>
        </asp:View>
        <br />
        <br />
        <br />
        <br />
    </asp:MultiView>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

