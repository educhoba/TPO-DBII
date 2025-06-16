<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="DBII.Pages.Main.Factura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="h-100 w-100 text-center">
        <h5>Factura</h5>
        <asp:Label runat="server" ID="lbError"></asp:Label>
        <div class="container">
            <main class="row">
                <div class="col-6">
                    <div class="list-group list-group-flush border-bottom scrollarea" style="height: 74vh; overflow: auto">
                        <asp:GridView DataKeyNames="id" AutoGenerateColumns="false" ID="gvList" runat="server" Class="table table-stripped">
                            <SelectedRowStyle Font-Bold="true" CssClass="table-secondary" />
                            <Columns>
                                
                                <asp:BoundField DataField="id" HeaderText="#" />
                                <asp:BoundField DataField="idPedido" HeaderText="#pedido" />
                                <asp:BoundField DataField="condicionPago" HeaderText="condición" />
                                <asp:BoundField DataField="total" HeaderText="$" />
                                <asp:TemplateField HeaderText="Pagar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>vacio</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-3">
                    <asp:Button ID="btnProcesarSeleccion" runat="server" Text="Marcar como pagado" OnClick="btnProcesarSeleccion_Click" />
                </div>
            </main>
        </div>
    </div>
</asp:Content>
