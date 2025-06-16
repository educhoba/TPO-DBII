using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Model
{
    public class CarritoItem : CarritoRowDTO
    {
        public string descripcion { get; set; }
        public decimal importe { get; set; }


        public CarritoItem()
        {

        }

        public static CarritoItem dto2Model(CarritoRowDTO row, ItemDTO item)
        {
            return new CarritoItem
            {
                cantidad = row.cantidad,
                descripcion = item.descripcion,
                idCarrito = row.idCarrito,
                id = row.id,
                idItem = item.id,
                importe = item.importe
            };
        }
    }
}