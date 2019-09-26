<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="ThreadedMessaging.MainPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS.css"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="modal fade" id="newThread" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="form-group">
                            <textarea class="form-control" rows="10"></textarea >
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Button1" class="btn btn-success btn-lg" runat="server" Text="Save"/>
                        <asp:Button ID="Button2" class="btn btn-danger btn-lg" runat="server" Text="Discard"/>
                    </div>
                </div>
      
            </div>
        </div>

        <div id="toolbar">
            <asp:Button ID="btnNewThread" runat="server" class="btn" data-toggle="modal" data-target="#newThread" Text="New Thread" OnClientClick="return false;" />
        </div>
        <br/>
        <div id="Msg" runat="server">
            
        </div>
    </form>
</body>
</html>
