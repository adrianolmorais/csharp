<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="uploadAW.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Usando FileUpload em um UpdatePanel</title>
    <script type="text/javascript">
        function MostrarProgresso() {
            document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "inline";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                Por favor, Aguarde....
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                <asp:Button ID="btnEnviarArquivo" runat="server" Text="Enviar Arquivo" OnClick="btnEnviarArquivo_Click" OnClientClick="MostrarProgresso();" />
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnEnviarArquivo" runat="server" />
            </Triggers>
        </asp:UpdatePanel>
 <%--       <asp:GridView ID="gvArquivos" runat="server" Height="131px" Width="787px">
            <Columns>
                <asp:TemplateField HeaderText="Cod.">
                    <ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Nome">
                    <ItemTemplate><%#Eval("nome") %>    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtnome" runat="server" Text='<%#Eval("nome") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Endereco">
                    <ItemTemplate><%#Eval("endereco") %>    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtendereco" runat="server" Text='<%#Eval("endereco") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate><%#Eval("email") %>    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtemail" runat="server" Text='<%#Eval("email") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowEditButton="true" ButtonType="Image"
                    EditImageUrl="Imagem/editar.jpg"
                    UpdateImageUrl="Imagem/aceitar.jpg"
                    CancelImageUrl="Imagem/erro.jpg" HeaderText="Editar" />
                <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="Imagem/erro.jpg" HeaderText="Deletar" />

            </Columns>
        </asp:GridView>--%>
    </form>
</body>
</html>
