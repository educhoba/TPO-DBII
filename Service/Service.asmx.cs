using Service.DAO;
using Service.DTO;
using Service.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using MongoDB.Bson;
using Service.Helper;
using static System.Collections.Specialized.BitVector32;

// Direct conversion
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
        [WebMethod]
        public void HelloMongo()
        {
            InsertarArticulos.Insertar();
        }

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
        public void InsertFactura(int idPedido, string condicionPago, decimal total, int idUsuario)
        {
            if (idPedido > 0 && condicionPago != null && total > 0)
            {
                var dto = new FacturaDTO
                {
                    condicionPago = condicionPago,
                    idPedido = idPedido,
                    total = total,
                    idUsuario = idUsuario
                };

                new DAOBase<FacturaDTO>().Insert(dto);

            }

        }
        [WebMethod]
        public void UpdateFactura(int idPedido, string condicionPago, decimal total, int idUsuario, int id)
        {
            if (idPedido > 0 && condicionPago != null && total > 0)
            {
                var dto = new FacturaDTO
                {
                    id = id,
                    condicionPago = condicionPago,
                    idPedido = idPedido,
                    total = total,
                    idUsuario = idUsuario
                };
                new DAOBase<FacturaDTO>().Update(dto);
            }

        }
        [WebMethod]
        public void DeleteFactura(int id)
        {
            if (id > 0)
            {
                new DAOBase<FacturaDTO>().Delete(id);
            }

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
            {
                var dto = new ItemDTO
                {
                    descripcion = desc,
                    stock = stock,
                    importe = precio
                };
                new DAOBase<ItemDTO>().Insert(dto);

            }


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

        }
        [WebMethod]
        public void DeleteItem(int id)
        {
            if (id > 0)
                new DAOBase<ItemDTO>().Delete(id);
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
        public int InsertPago(decimal total, int idUsuario)
        {
            if (total > 0)
            {
                return new DAOBase<PagoDTO>().Insert(new PagoDTO
                {
                    total = total,
                    idUsuario = idUsuario
                });
            }
            return -1;
        }
        [WebMethod]
        public void UpdatePago(decimal total, int idUsuario, int id)
        {
            if (total > 0)
                new DAOBase<PagoDTO>().Insert(new PagoDTO
                {
                    id = id,
                    total = total,
                    idUsuario = idUsuario
                });

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

        }
        [WebMethod]
        public void DeletePagoRow(int id)
        {
            if (id > 0)
                new DAOBase<PagoRowDTO>().Delete(id);

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
        public int InsertPedido(int idUsuario, string condicionIVA)
        {
            if (idUsuario > 0 && condicionIVA != null)
                return new DAOBase<PedidoDTO>().Insert(new PedidoDTO
                {
                    idUsuario = idUsuario,
                    condicionIVA = condicionIVA
                });
            return -1;
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

        }
        [WebMethod]
        public void DeletePedido(int id)
        {
            if (id > 0)
                new DAOBase<PedidoDTO>().Delete(id);

        }
        #endregion  
        #region PEDIDO ROW CRUDL

        [WebMethod]
        public PedidoRowDTO GetPedidoRow(int id)
        {
            return new DAOBase<PedidoRowDTO>().Read(id);
        }
        [WebMethod]
        public List<PedidoRowDTO> GetPedidoRows(int idPedido)
        {

            var rows = new DAOBase<PedidoRowDTO>().ReadAll($"{nameof(PedidoRowDTO.idPedido)} = {idPedido}");
            return rows;
        }
        [WebMethod]
        public void InsertPedidoRow(int idPedido, decimal importe, decimal IVA, decimal descuento, string descripcion, int cantidad)
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
                    cantidad = cantidad
                });

        }
        [WebMethod]
        public void UpdatePedidoRow(int idPedido, decimal importe, decimal IVA, decimal descuento, string descripcion, int cantidad, int id)
        {
            if (idPedido > 0 &&
                descripcion != null &&
                importe > 0 &&
                IVA >= 0 &&
                descuento >= 0)
                new DAOBase<PedidoRowDTO>().Insert(new PedidoRowDTO
                {
                    id = id,
                    descripcion = descripcion,
                    descuento = descuento,
                    idPedido = idPedido,
                    importe = importe,
                    IVA = IVA,
                    cantidad = cantidad
                });

        }
        [WebMethod]
        public void DeletePedidoRow(int id)
        {
            if (id > 0)
                new DAOBase<PedidoRowDTO>().Delete(id);

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
        public void InsertUsuario(string nombre, string direccion, string documento, string categoria)
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
                    categoria = categoria
                });

        }
        [WebMethod]
        public void UpdateUsuario(string nombre, string direccion, string documento, string categoria, int id)
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
                    categoria = categoria,
                    id = id
                });
        }
        [WebMethod]
        public void DeleteUsuario(int id)
        {
            if (id > 0)
                new DAOBase<UsuarioDTO>().Delete(id);

        }
        #endregion

        #region negocio

        [WebMethod]
        public UserSession Login(string nombre)
        {
            var dao = new DAOBase<UsuarioDTO>();

            var ret = dao.ReadAll($"{nameof(UsuarioDTO.nombre)} = '{nombre}'").FirstOrDefault();

            if (ret != null)
            {
                RedisService redis = new RedisService();

                var session = new UserSession
                {
                    UserId = ret.id,
                    Nombre = ret.nombre,
                    Direccion = ret.direccion,
                    Documento = ret.documento,
                    Login = DateTime.Now
                };

                redis.SaveUserSession(session);

                return session;
            }
            return null;
        }
        [WebMethod]
        public void LogOut(UserSession sesion)
        {
            var dao = new DAOBase<UsuarioDTO>();

            var ret = dao.ReadAll($"{nameof(UsuarioDTO.nombre)} = '{sesion.Nombre}'").FirstOrDefault();

            if (ret != null)
            {
                RedisService redis = new RedisService();

                redis.UpdateLogout();

                ret.categoria = redis.CategorizeUser();
                UpdateUsuario(ret.nombre, ret.direccion, ret.documento, ret.categoria, ret.id);
            }
        }
        [WebMethod]
        public UserSession GetUser()
        {

            RedisService redis = new RedisService();

            return redis.GetUserSession();
        }
        [WebMethod]
        public void AddItem(UserSession user, ItemDTO item)
        {
            var redis = new RedisService();
            var historial = new CarritoHistorialService();
            redis.AddOrUpdateItem(user.UserId, new CarritoItemSession
            {
                Cantidad = 1,
                Descripcion = item.descripcion,
                IdItem = item.id,
                Importe = item.importe
            });

            var carrito = redis.GetCarritoItems(user.UserId);
            historial.GuardarSnapshot(user.UserId, carrito);
        }
        [WebMethod]
        public void DeleteCarritoRow(UserSession user, int idItem)
        {
            var redis = new RedisService();
            var historial = new CarritoHistorialService();

            redis.RemoveItem(user.UserId, idItem);

            var carrito = redis.GetCarritoItems(user.UserId);
            historial.GuardarSnapshot(user.UserId, carrito);
        }
        [WebMethod]
        public void UpdateCarritoRow(UserSession user, int IdItem, int cantidad)
        {
            var redis = new RedisService();
            var historial = new CarritoHistorialService();

            redis.AddOrUpdateItem(user.UserId, new CarritoItemSession
            {
                IdItem = IdItem,
                Cantidad = cantidad
            });

            var carrito = redis.GetCarritoItems(user.UserId);
            historial.GuardarSnapshot(user.UserId, carrito);
        }

        [WebMethod]
        public List<CarritoItemSession> GetCarritoItems(UserSession user)
        {
            var redis = new RedisService();

            return redis.GetCarritoItems(user.UserId);
        }
        [WebMethod]
        public List<CarritoItemSession> RollBackCarrito(UserSession user)
        {
            var redis = new RedisService();
            var historial = new CarritoHistorialService();

            var anterior = historial.DeshacerUltimoCambio(user.UserId);
            redis.ClearCarrito(user.UserId);
            if (anterior != null)
            {
                foreach (var row in anterior.Estado)
                {
                    redis.AddOrUpdateItem(user.UserId, new CarritoItemSession
                    {
                        Cantidad = row.Cantidad,
                        IdItem = row.IdItem,
                        Descripcion = row.Descripcion,
                        Importe = row.Importe
                    });
                }
                return redis.GetCarritoItems(user.UserId);
            }
            else
                return new List<CarritoItemSession>();
        }

        [WebMethod]
        public void NuevoPedido(UserSession usuario, string condicionIVA)
        {
            var redis = new RedisService();
            var historial = new CarritoHistorialService();

            var items = GetCarritoItems(usuario);
            if (items != null && items.Count > 0)
            {
                int idPedido = InsertPedido(usuario.UserId, condicionIVA);

                foreach (var item in items)
                {
                    InsertPedidoRow(idPedido, item.Importe, 21, 0, item.Descripcion, item.Cantidad);
                }
                redis.ClearCarrito(usuario.UserId);
                historial.BorrarHistorial(usuario.UserId);
            }
        }
        [WebMethod]
        public List<PedidoDTO> GetPedidosSinFacturar(UserSession usuario)
        {
            string query = $@"SELECT t0.*
                                FROM Pedido T0
                                LEFT JOIN Factura T1 on T0.id = T1.{nameof(FacturaDTO.idPedido)}
                                WHERE T1.id IS NULL AND T0.{nameof(PedidoDTO.idUsuario)} = {usuario.UserId}";

            return new DAOBase<PedidoDTO>().ReadQuery(query);
        }

        [WebMethod]
        public void NuevaFactura(PedidoDTO pedido, UserSession usuario, string condicionPago)
        {
            var rows = GetPedidoRows(pedido.id);
            decimal total = 0;
            foreach (var r in rows)
            {
                total += r.cantidad * r.importe * (1 - r.descuento / 100) * (1 + r.IVA / 100);
            }

            InsertFactura(pedido.id, condicionPago, total, usuario.UserId);
            //todo actualizar stock de articulos
        }

        [WebMethod]
        public List<FacturaDTO> GetFacturasSinPagar(UserSession usuario)
        {
            string query = $@"SELECT t0.*
                                FROM Factura T0
                                LEFT JOIN PagoRow T1 on T0.id = T1.{nameof(PagoRowDTO.idFactura)}
                                WHERE T1.id IS NULL AND T0.{nameof(PedidoDTO.idUsuario)} = {usuario.UserId}";

            return new DAOBase<FacturaDTO>().ReadQuery(query);
        }

        [WebMethod]
        public void NuevoPago(List<FacturaDTO> facturas, UserSession usuario, decimal total)
        {

            int idPago = InsertPago(total, usuario.UserId);

            foreach (var f in facturas)
            {
                InsertPagoRow(idPago, f.id, f.total);
            }
        }

        [WebMethod]
        public List<PagoDTO> GetPagosDeUsuario(UserSession usuario)
        {
            return new DAOBase<PagoDTO>().ReadAll($"{nameof(PagoDTO.idUsuario)} = {usuario.UserId}");
        }

        [WebMethod]
        public ItemEnriquecido GetItemEnriquecido(int sqlID)
        {
            return DAOItemEnriquecido.GetItemEnriquecido(sqlID);
        }
        #endregion


    }
}
