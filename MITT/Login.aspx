<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MITT.Login" MasterPageFile="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <asp:Login ID="AppLogin" runat="server" Style="margin:auto"
                                
           TitleText="Please enter your credentials to access this application." 
           BackColor="#FFFBD6" Font-Bold="True" Font-Names="Verdana" ForeColor="#333333" 
           BorderStyle="Solid" onauthenticate="AppLogin_Authenticate" 
        BorderColor="#FFDFAD" BorderPadding="4" BorderWidth="1px" Font-Size="0.8em" 
        TextLayout="TextOnTop">
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <LoginButtonStyle BackColor="White" BorderColor="#CC9966" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#990000" />
        <TextBoxStyle Font-Size="0.8em" />
        <TitleTextStyle BackColor="#990000" Font-Bold="True" Font-Size="0.9em" 
            ForeColor="White" />
      </asp:Login>
    <br />
    <asp:Label ID="lblLockedMsg" runat="server"></asp:Label>
    </form>
</body>
</html>
