<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="DBII.Pages.Main.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="h-100 w-100 text-center">
        <h5>Catálogo</h5>
        <asp:Label runat="server" ID="lbError"></asp:Label>
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
                <div class="col-5">

                    <div>
                        <asp:Label ID="lbDescripcion" runat="server" Text="Descripcion"></asp:Label>
                        <asp:TextBox ID="tbDescripcion" runat="server">Descripcion</asp:TextBox>
                    </div>
                    <div>
                        <asp:Label ID="lbPrecio" runat="server" Text="Precio"></asp:Label>
                        <asp:TextBox ID="tbPrecio" runat="server">Precio</asp:TextBox>
                    </div>
                    <h2>Detalle del Item en Mongo</h2>

                    <!-- Nombre -->
                    <div>
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre: " />
                        <asp:TextBox ID="txtNombre" runat="server" />
                    </div>

                    <!-- Marca -->
                    <div>
                        <asp:Label ID="lblMarca" runat="server" Text="Marca: " />
                        <asp:TextBox ID="txtMarca" runat="server" />
                    </div>

                    <!-- SqlId -->
                    <div>
                        <asp:Label ID="lblSqlId" runat="server" Text="SQL ID: " />
                        <asp:TextBox ID="txtSqlId" runat="server" ReadOnly="true" />
                    </div>

                    <!-- Imágenes -->
                    <h4>Imágenes</h4>
                    <asp:Repeater ID="rptImagenes" runat="server">
                        <ItemTemplate>
                            <%# Container.DataItem?.ToString() ?? "" %><br />
                        </ItemTemplate>
                    </asp:Repeater>

                    <!-- Videos -->
                    <h4>Videos</h4>
                    <asp:Repeater ID="rptVideos" runat="server">
                        <ItemTemplate>
                            <%# Container.DataItem?.ToString() ?? "" %><br />
                        </ItemTemplate>
                    </asp:Repeater>

                    <!-- Comentarios -->
                    <h4>Comentarios</h4>
                    <asp:Repeater ID="rptComentarios" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <strong><%# Eval("Usuario") %></strong> - <%# Eval("Fecha", "{0:dd/MM/yyyy}") %> - 
                        <%# Eval("Calificacion") %>⭐<br />
                                <%# Eval("ComentarioTexto") %>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate></ul></FooterTemplate>
                    </asp:Repeater>

                    <!-- Especificaciones -->
                    <h4>Especificaciones</h4>
                    <asp:Repeater ID="rptEspecificaciones" runat="server">
                        <ItemTemplate>
                            <strong><%# Eval("Clave") %>:</strong> <%# Eval("Valor") %><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-1">
                    <asp:Button ID="btAgregar" runat="server" Text="Agregar" OnClick="btAgregar_Click" />
                </div>
            </main>
        </div>
    </div>
</asp:Content>
