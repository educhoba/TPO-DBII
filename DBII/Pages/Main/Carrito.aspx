<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="DBII.Pages.Main.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="h-100 w-100 text-center">
        <h5>Carrito</h5><asp:Label runat="server" ID="lbError"></asp:Label>
        <div class="container">
            <main class="row">
                <div class="col-5">
                    <div class="list-group list-group-flush border-bottom scrollarea" style="height: 74vh; overflow: auto">
                        <asp:GridView DataKeyNames="IdItem" AutoGenerateColumns="false" ID="gvList" runat="server" Class="table table-stripped" OnRowCommand="gvList_RowCommand">
                            <SelectedRowStyle Font-Bold="true" CssClass="table-secondary" />
                            <Columns>
                                <asp:BoundField DataField="descripcion" HeaderText="descripción" />
                                <asp:BoundField DataField="cantidad" HeaderText="cantidad" />
                                <asp:BoundField DataField="importe" HeaderText="importe" />
                                <asp:TemplateField HeaderText="Agregar">
                                    <ItemTemplate>
                                        <asp:Button ID="btAgregar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" class="btn-table" CommandName="Agregar" runat="server" Text="+"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Restar">
                                    <ItemTemplate>
                                        <asp:Button ID="btRestar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" class="btn-table" CommandName="Restar" runat="server" Text="-"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:Button ID="btEliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" class="btn-table" CommandName="Eliminar" runat="server" Text="x"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>vacio</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-1">
                    <asp:Button ID="btVolver" runat="server" Text="↺" OnClick="btVolver_Click" />
                </div>
                <div class="col-3">
                    <asp:DropDownList ID="ddlIVA" runat="server">
                        <asp:ListItem>Responsable Inscripto</asp:ListItem>
                        <asp:ListItem>Sujeto Extento</asp:ListItem>
                        <asp:ListItem>Consumidor Final</asp:ListItem>
                        <asp:ListItem>Responsable Monotributo</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btPedido" runat="server" Text="Generar Pedido" OnClick="btPedido_Click" />
                </div>
            </main>
        </div>
    </div>
</asp:Content>
