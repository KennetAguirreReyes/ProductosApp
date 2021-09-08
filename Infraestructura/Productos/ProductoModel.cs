using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Productos
{
    public class ProductoModel
    {
        private Producto[] productos;

        #region CRUD
        private void Add(Producto p)
        { 
            Add(p, ref productos );
        }
        public int Update(Producto p)//sirve para actualizar la existencia del producto
        {
            if(p == null)
            {
                throw new ArgumentException("El Producto no puede ser null.");
            }

            int index = GetIndexById(p.Id);
            if(index < 0)
            {
                throw new Exception($"El Producto con id {p.Id} no se encuentra");
            }
            productos[index] = p;
            return index;
        }

        public bool Delete(Producto p)// consiste en agg un temporal y eliminar la ultima posicion
        {
            if (p == null)
            {
                throw new ArgumentException("El Producto no puede ser null.");
            }

            int index = GetIndexById(p.Id);
            if (index < 0)
            {
                throw new Exception($"El Producto con id {p.Id} no se encuentra");
            }
            //métodos para eliminar la posición
            if(index != productos.Length - 1){
                productos[index] = productos[productos.Length - 1];
            }

            Producto[] tmp = new Producto[productos.Length - 1];
            Array.Copy(productos, tmp, productos.Length - 1);
            productos = tmp;

            return productos.Length == tmp.Length;
        }

        public Producto[] GetAll()
        {
            return productos;
        }

        #endregion

        #region Queries

        public Producto GetProductoById(int id)//muestra la posición del id
        {
            if(id <= 0)
            {
                throw new ArgumentException($"El Id: {id} no es válido.");
            }

            int index = GetIndexById(id);

            return index <= 0 ? null : productos[index];
        }



        #endregion 

        #region Métodos Privados 
        private void Add(Producto p, ref Producto[] pds)// el cambioque sufra el método tambie va a sufrir donde se está pasando, solo si está la referencia
        {
            if(pds == null)
            {
                pds = new Producto[1];
                pds[pds.Length - 1] = p;
                return;
            }
            Producto[] tmp = new Producto[pds.Length + 1];
            Array.Copy(pds, tmp, pds.Length);
            tmp[tmp.Length - 1] = p;
            pds = tmp;
        }

        private int GetIndexById(int id)// sirve para encontrar el ID y muestra(retorna) su posición
        {
            if(id <= 0)
            {
                throw new ArgumentException("El Id no puede ser negativo o cero.");
            }

            int index = int.MinValue, i = 0;
            if(productos == null)
            {
                return index;
            }
            foreach(Producto p in productos)
            {
                if(p.Id == id)
                {
                    index = i;
                    break;
                }
                i++;
            }

            return index;
        }
        #endregion
    }
}
