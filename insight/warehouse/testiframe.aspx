<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testiframe.aspx.vb" Inherits="insight.testiframe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <asp:Timer ID="Timer1" runat="server" Interval="3300" ontick="Timer1_Tick"></asp:Timer>

        <div>

<asp:UpdatePanel ID="uPnlCreatedToday" runat="server">
                        <ContentTemplate>


<iframe src="convertjavatocodebehind.aspx" title="W3Schools Free Online Web Tutorials" width="100%" height="300" ></iframe>
                            
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>
