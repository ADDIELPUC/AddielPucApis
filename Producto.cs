using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Parcial2Listas
{
    public class Producto
    {
        public string name;
        public int valor;
        public int capacidad;
        public int retirado;
        public Producto next;
        public Producto(string nombre, int costo, int inv, Producto sig)
        {
            name = nombre;
            valor = costo;
            capacidad = inv;
            retirado = 0;
            next = sig;
        }
        public Producto(string nombre, int costo) 
        {
            name = nombre;
            valor = costo;
        }
    }
}
