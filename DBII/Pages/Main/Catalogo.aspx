<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="DBII.Pages.Main.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="h-100 w-100 text-center">
        <h5>Catálogo</h5><asp:Label runat="server" ID="lbError"></asp:Label>
        <div class="container">
            <main class="row">
                <div class="col-3">
                    <div class="list-group list-group-flush border-bottom scrollarea" style="height: 74vh; overflow: auto">
                        <asp:GridView DataKeyNames="id" AutoGenerateColumns="false" ID="gvList" runat="server" Class="table table-stripped" OnRowCommand="gvList_RowCommand">
                            <SelectedRowStyle Font-Bold="true" CssClass="table-secondary" />
                            <Columns>
                                <asp:BoundField DataField="descripcion" HeaderText="descripcion" />
                                <asp:TemplateField HeaderText="🔎">
                                    <ItemTemplate>
                                        <asp:Button ID="btBuscar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" class="btn-table" CommandName="Buscar" runat="server" Text="🔎"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-3">

                    <div><asp:Label ID="lbDescripcion" runat="server" Text="Descripcion"></asp:Label>
                    <asp:TextBox ID="tbDescripcion" runat="server">Descripcion</asp:TextBox></div>
                    <div><asp:Label ID="lbPrecio" runat="server" Text="Precio"></asp:Label>
                    <asp:TextBox ID="tbPrecio" runat="server">Precio</asp:TextBox></div>
                    <div><asp:Label ID="lbFoto" runat="server" Text="Foto"></asp:Label>
                    <asp:TextBox ID="tbFoto" runat="server">Foto</asp:TextBox></div>
                    <div><asp:Label ID="lbVideo" runat="server" Text="Video"></asp:Label>
                    <asp:TextBox ID="tbVideo" runat="server">Video</asp:TextBox></div>
                    <div><asp:Label ID="lbComentarios" runat="server" Text="Comentarios"></asp:Label>
                    <asp:TextBox ID="tbComentarios" runat="server">Comentarios</asp:TextBox></div>

                </div>
                <div class="col-3">
                    <asp:Button ID="btAgregar" runat="server" Text="Agregar" OnClick="btAgregar_Click" />
                </div>
            </main>
        </div>
    </div>
</asp:Content>
