<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pedido.aspx.cs" Inherits="DBII.Pages.Main.Pedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="h-100 w-100 text-center">
        <h5>Pedido</h5><asp:Label runat="server" ID="lbError"></asp:Label>
        <div class="container">
            <main class="row">
                <div class="col-1">
                    <div class="list-group list-group-flush border-bottom scrollarea" style="height: 74vh; overflow: auto">
                        <asp:GridView DataKeyNames="id" AutoGenerateColumns="false" ID="gvList" runat="server" Class="table table-stripped" OnRowCommand="gvList_RowCommand">
                            <SelectedRowStyle Font-Bold="true" CssClass="table-secondary" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="#" />
                                <asp:TemplateField HeaderText="🔎">
                                    <ItemTemplate>
                                        <asp:Button ID="btBuscar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" class="btn-table" CommandName="Buscar" runat="server" Text="🔎"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>vacio</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-5">
                    <div class="list-group list-group-flush border-bottom scrollarea" style="height: 74vh; overflow: auto">
                        <asp:GridView DataKeyNames="id" AutoGenerateColumns="false" ID="gv" runat="server" Class="table table-stripped">
                            <SelectedRowStyle Font-Bold="true" CssClass="table-secondary" />
                            <Columns>
                                <asp:BoundField DataField="descripcion" HeaderText="descripción" />
                                <asp:BoundField DataField="cantidad" HeaderText="cantidad" />
                                <asp:BoundField DataField="importe" HeaderText="importe" />
                                <asp:BoundField DataField="IVA" HeaderText="I.V.A." />
                                <asp:BoundField DataField="descuento" HeaderText="descuento" />
                            </Columns>
                            <EmptyDataTemplate>vacio</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-3">
                    <asp:Label ID="lbTotal" runat="server" Text="Total"></asp:Label>
                    <asp:TextBox ID="tbTotal" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddlPago" runat="server">
                        <asp:ListItem>Contado</asp:ListItem>
                        <asp:ListItem>Débito</asp:ListItem>
                        <asp:ListItem>Crédito</asp:ListItem>
                        <asp:ListItem>Cuenta Corriente</asp:ListItem>
                        <asp:ListItem>Transferencia</asp:ListItem>
                        <asp:ListItem>Otra</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btFactura" runat="server" Text="Facturar" OnClick="btFactura_Click" />
                </div>
            </main>
        </div>
    </div>
</asp:Content>
