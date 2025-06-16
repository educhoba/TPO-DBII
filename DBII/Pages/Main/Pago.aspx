<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="DBII.Pages.Main.Pago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="h-100 w-100 text-center">
        <h5>Pago</h5>
        <asp:Label runat="server" ID="lbError"></asp:Label>
        <div class="container">
            <main class="row">
                <div class="col-9">
                    <div class="list-group list-group-flush border-bottom scrollarea" style="height: 74vh; overflow: auto">
                        <asp:GridView DataKeyNames="id" AutoGenerateColumns="false" ID="gvList" runat="server" Class="table table-stripped">
                            <SelectedRowStyle Font-Bold="true" CssClass="table-secondary" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="#" />
                                <asp:BoundField DataField="total" HeaderText="$" />
                            </Columns>
                            <EmptyDataTemplate>vacio</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </main>
        </div>
    </div>
</asp:Content>
