using Service.DAO;
using Service.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;

namespace Service
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        #region CARRITO CRUDL

        [WebMethod]
        public CarritoDTO GetCarrito(int id)
        {
            return new DAOBase<CarritoDTO>().Read(id);
        }
        [WebMethod]
        public List<CarritoDTO> GetCarritos()
        {
            return new DAOBase<CarritoDTO>().ReadAll();
        }
        [WebMethod]
        public void InsertCarrito(int idUsuario)
        {
            if (idUsuario > 0)
                new DAOBase<CarritoDTO>().Insert(new CarritoDTO
                {
                    idUsuario = idUsuario
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void UpdateCarrito(int idUsuario, int id)
        {
            if (idUsuario > 0)
                new DAOBase<CarritoDTO>().Update(new CarritoDTO
                {
                    id = id,
                    idUsuario = idUsuario
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void DeleteCarrito(int id)
        {
            if (id > 0)
                new DAOBase<CarritoDTO>().Delete(id);
            //todo log en otra DB
        }
        #endregion  
        #region CARRITO ROW CRUDL

        [WebMethod]
        public CarritoRowDTO GetCarritoRow(int id)
        {
            return new DAOBase<CarritoRowDTO>().Read(id);
        }
        [WebMethod]
        public List<CarritoRowDTO> GetCarritoRows()
        {
            return new DAOBase<CarritoRowDTO>().ReadAll();
        }
        [WebMethod]
        public void InsertCarritoRow(int idCarrito, int idItem)
        {
            if (idCarrito > 0 && idItem > 0)
                new DAOBase<CarritoRowDTO>().Insert(new CarritoRowDTO
                {
                    idCarrito = idCarrito,
                    idItem = idItem
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void UpdateCarritoRow(int idCarrito, int idItem, int id)
        {
            if (idCarrito > 0 && idItem > 0) 
                new DAOBase<CarritoRowDTO>().Update(new CarritoRowDTO
                {
                    id = id,
                    idCarrito = idCarrito,
                    idItem = idItem
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void DeleteCarritoRow(int id)
        {
            if (id > 0)
                new DAOBase<CarritoRowDTO>().Delete(id);
            //todo log en otra DB
        }
        #endregion  
        #region FACTURA CRUDL

        [WebMethod]
        public FacturaDTO GetFactura(int id)
        {
            return new DAOBase<FacturaDTO>().Read(id);
        }
        [WebMethod]
        public List<FacturaDTO> GetFacturas()
        {
            return new DAOBase<FacturaDTO>().ReadAll();
        }
        [WebMethod]
        public void InsertFactura(int idPedido, string condicionPago, decimal total)
        {
            if (idPedido > 0 && condicionPago != null && total > 0)
                new DAOBase<FacturaDTO>().Insert(new FacturaDTO
                {
                    condicionPago = condicionPago,
                    idPedido = idPedido,
                    total = total
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void UpdateFactura(int idPedido, string condicionPago, decimal total, int id)
        {
            if (idPedido > 0 && condicionPago != null && total > 0)
                new DAOBase<FacturaDTO>().Update(new FacturaDTO
                {
                    id = id,
                    condicionPago = condicionPago,
                    idPedido = idPedido,
                    total = total
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void DeleteFactura(int id)
        {
            if (id > 0)
                new DAOBase<FacturaDTO>().Delete(id);

            //todo log en otra DB
        }
        #endregion  
        #region ITEMS CRUDL

        [WebMethod]
        public ItemDTO GetItem(int id)
        {
            return new DAOBase<ItemDTO>().Read(id);
        }
        [WebMethod]
        public List<ItemDTO> GetItems()
        {
            return new DAOBase<ItemDTO>().ReadAll();
        }
        [WebMethod]
        public void InsertItem(string desc, int stock, decimal precio)
        {
            if (stock >= 0 && precio > 0)
                new DAOBase<ItemDTO>().Insert(new ItemDTO
                {
                    descripcion = desc,
                    stock = stock,
                    importe = precio
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void UpdateItem(string desc, int stock, decimal precio, int id)
        {
            if (stock >= 0 && precio > 0 && id > 0)
                new DAOBase<ItemDTO>().Update(new ItemDTO
                {
                    id = id,
                    descripcion = desc,
                    stock = stock,
                    importe = precio
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void DeleteItem(int id)
        {
            if (id > 0)
                new DAOBase<ItemDTO>().Delete(id);
            //todo log en otra DB
        }
        #endregion
        #region PAGO CRUDL

        [WebMethod]
        public PagoDTO GetPago(int id)
        {
            return new DAOBase<PagoDTO>().Read(id);
        }
        [WebMethod]
        public List<PagoDTO> GetPagos()
        {
            return new DAOBase<PagoDTO>().ReadAll();
        }
        [WebMethod]
        public void InsertPago(decimal total)
        {
            if (total > 0)
                new DAOBase<PagoDTO>().Insert(new PagoDTO
                {
                    total = total
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void UpdatePago(decimal total, int id)
        {
            if (total > 0)
                new DAOBase<PagoDTO>().Insert(new PagoDTO
                {
                    id =id,
                    total = total
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void DeletePago(int id)
        {
            if (id > 0)
                new DAOBase<PagoDTO>().Delete(id);

            //todo log en otra DB
        }
        #endregion  
        #region PAGO CRUDL

        [WebMethod]
        public PagoRowDTO GetPagoRow(int id)
        {
            return new DAOBase<PagoRowDTO>().Read(id);
        }
        [WebMethod]
        public List<PagoRowDTO> GetPagoRows()
        {
            return new DAOBase<PagoRowDTO>().ReadAll();
        }
        [WebMethod]
        public void InsertPagoRow(int idPago, int idFactura, decimal monto)
        {
            if (idPago > 0 && idFactura > 0 && monto > 0)
                new DAOBase<PagoRowDTO>().Insert(new PagoRowDTO
                {
                    idFactura = idFactura,
                    idPago = idPago,
                    monto = monto
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void UpdatePagoRow(int idPago, int idFactura, decimal monto, int id)
        {
            if (idPago > 0 && idFactura > 0 && monto > 0)
                new DAOBase<PagoRowDTO>().Insert(new PagoRowDTO
                {
                    idFactura = idFactura,
                    idPago = idPago,
                    monto = monto,
                    id = id
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void DeletePagoRow(int id)
        {
            if (id > 0)
                new DAOBase<PagoRowDTO>().Delete(id);

            //todo log en otra DB
        }
        #endregion  
        #region PEDIDO CRUDL

        [WebMethod]
        public PedidoDTO GetPedido(int id)
        {
            return new DAOBase<PedidoDTO>().Read(id);
        }
        [WebMethod]
        public List<PedidoDTO> GetPedidos()
        {
            return new DAOBase<PedidoDTO>().ReadAll();
        }
        [WebMethod]
        public void InsertPedido(int idUsuario, string condicionIVA)
        {
            if (idUsuario > 0 && condicionIVA != null)
                new DAOBase<PedidoDTO>().Insert(new PedidoDTO
                {
                    idUsuario = idUsuario,
                    condicionIVA = condicionIVA
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void UpdatePedido(int idUsuario, string condicionIVA, int id)
        {
            if (idUsuario > 0 && condicionIVA != null)
                new DAOBase<PedidoDTO>().Insert(new PedidoDTO
                {
                    idUsuario = idUsuario,
                    condicionIVA = condicionIVA,
                    id = id
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void DeletePedido(int id)
        {
            if (id > 0)
                new DAOBase<PedidoDTO>().Delete(id);

            //todo log en otra DB
        }
        #endregion  
        #region PEDIDO ROW CRUDL

        [WebMethod]
        public PedidoRowDTO GetPedidoRow(int id)
        {
            return new DAOBase<PedidoRowDTO>().Read(id);
        }
        [WebMethod]
        public List<PedidoRowDTO> GetPedidoRows()
        {
            return new DAOBase<PedidoRowDTO>().ReadAll();
        }
        [WebMethod]
        public void InsertPedidoRow(int idPedido, decimal importe, decimal IVA, decimal descuento, string descripcion)
        {
            if (idPedido > 0 &&
                descripcion != null &&
                importe > 0 &&
                IVA >= 0 &&
                descuento >= 0)
                new DAOBase<PedidoRowDTO>().Insert(new PedidoRowDTO
                {
                    descripcion = descripcion,
                    descuento = descuento,
                    idPedido = idPedido,
                    importe = importe,
                    IVA = IVA
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void UpdatePedidoRow(int idPedido, decimal importe, decimal IVA, decimal descuento, string descripcion, int id)
        {
            if (idPedido > 0 &&
                descripcion != null &&
                importe > 0 &&
                IVA >= 0 &&
                descuento >= 0)
                new DAOBase<PedidoRowDTO>().Insert(new PedidoRowDTO
                {
                    descripcion = descripcion,
                    descuento = descuento,
                    idPedido = idPedido,
                    importe = importe,
                    IVA = IVA,
                    id = id
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void DeletePedidoRow(int id)
        {
            if (id > 0)
                new DAOBase<PedidoRowDTO>().Delete(id);

            //todo log en otra DB
        }
        #endregion 
        #region USUARIO CRUDL

        [WebMethod]
        public UsuarioDTO GetUsuario(int id)
        {
            return new DAOBase<UsuarioDTO>().Read(id);
        }
        [WebMethod]
        public List<UsuarioDTO> GetUsuarios()
        {
            return new DAOBase<UsuarioDTO>().ReadAll();
        }
        [WebMethod]
        public void InsertUsuario(string nombre, string direccion, string documento)
        {
            if (nombre != null &&
                direccion != null &&
                documento != null
                )
                new DAOBase<UsuarioDTO>().Insert(new UsuarioDTO
                {
                    direccion = direccion,
                    documento = documento,
                    nombre =    nombre
                });

            //todo log en otra DB
        }
        [WebMethod]
        public void UpdateUsuario(string nombre, string direccion, string documento, int id)
        {
            if (nombre != null &&
                direccion != null &&
                documento != null
                )
                new DAOBase<UsuarioDTO>().Insert(new UsuarioDTO
                {
                    direccion = direccion,
                    documento = documento,
                    nombre = nombre,
                    id = id
                });
            //todo log en otra DB
        }
        [WebMethod]
        public void DeleteUsuario(int id)
        {
            if (id > 0)
                new DAOBase<UsuarioDTO>().Delete(id);

            //todo log en otra DB
        }
        #endregion 
    }
}
