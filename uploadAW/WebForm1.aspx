<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="uploadAW.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Usando FileUpload em um UpdatePanel</title>
    <script type="text/javascript">
        function ShowProgress() {
            document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "inline";
            setTimeout(function () {
                var modal = document.querySelectorAll('div')[2];
                modal.classList.add("modal");
                document.querySelector('body').append(modal);
                var loading = document.querySelector(".loading");
                loading.style.display = 'block';
                var top = Math.max(document.documentElement.clientHeight / 2 - loading.clientHeight / 2, 0);
                var left = Math.max(document.documentElement.clientHeight / 2 - loading.clientHeight / 2, 0);
                loading.style.top = 50;
                loading.style.left = 50;
            }, 200);
        }
    </script>
    <style>
        body.center-form {
            min-height: 100vh;
        }

        div.center-form {
            position: relative;
            min-height: 100vh;
        }

            div.center-form > form {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translateY(-50%) translateX(-50%);
            }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.3;
            filter: alpha(opacity=30);
            /*-moz-opacity: 0.3;*/
            min-height: 100%;
            width: 100%;
        }

        .loading {
            text-align: center;
            font-family: Arial;
            font-size: 10pt;
            width: 200px;
            height: 50px;
            display: none;
            position: absolute;
            top: 100%;
            left: 50%;
            transform: translateY(-50%) translateX(-50%);
            z-index: 999;
        }
    </style>
</head>
<body class="center-form">
    <div class="center-form">
        <form id="form1" runat="server">
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        Carregando. Por favor aguarde...
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <hr />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                        <asp:Button ID="btnEnviarArquivo" runat="server" Text="Enviar Arquivo" OnClick="btnEnviarArquivo_Click" OnClientClick="ShowProgress();" />
                        </br>
                        <hr />
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        <br />
                        </br>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnEnviarArquivo" runat="server" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="loading">                    
                    <br />
                    <img src="Assets/loading_new.gif" alt="Carregando. Por favor aguarde..." />
                </div>
            </div>
        </form>
    </div>
</body>
</html>
