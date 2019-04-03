using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericClasses
{
    class Nodo<T>
    {
        public T valor { get; set; }
        public Nodo<T> siguiente { get; set; }
        public Nodo<T> anterior { get; set; }
        public Nodo(T valor)
        {
            this.valor = valor;
        }
        public Nodo(T valor, Nodo<T> siguiente)
        {
            this.valor = valor;
            this.siguiente = siguiente;
        }
        public Nodo(T valor, Nodo<T> siguiente, Nodo<T> anterior)
        {
            this.valor = valor;
            this.siguiente = siguiente;
            this.anterior = anterior;
        }

        public override bool Equals(object obj)
        {
            return valor.Equals(obj);
        }
    }
}
