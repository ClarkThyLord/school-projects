using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericClasses
{
    class Lista<T>
    {
       public Nodo<T> raiz { get; set; }

        public int tamanio { get; private set; } = 0;
       
        
        public void AgregarNodo(Nodo<T> nuevonodo)
        {
            if (raiz is null)
            {
                raiz = nuevonodo;
            }
            else
            {
                Nodo<T> temp = raiz;
                while (!(temp.siguiente is null))
                {
                    temp = temp.siguiente;
                }
                temp.siguiente = nuevonodo;
                nuevonodo.anterior = temp;
            }
            tamanio++;
            
        }

        public void AgregaNodo(T valor )
        {
            AgregarNodo(new Nodo<T>(valor));
        }

        public override string ToString()
        {
            string resultado = "";
            Nodo<T> actual = raiz;
            for (int i = 0; i < tamanio; i++)
            {
                resultado += actual.valor + " ";
                actual = actual.siguiente;
            }
            return resultado;
        }
        public void insertar(T valor, int index)
        {
            Nodo<T> actual = raiz;
            Nodo<T> temp = new Nodo<T>(valor);

            for (int i = 0; i < tamanio; i++)
            {
                if (i == index)
                {
                    actual.anterior.siguiente = temp;
                    temp.siguiente = actual;
                    break;
                }
                actual = actual.siguiente;
            }
        }
        public void pop(int index)
        {
            Nodo<T> actual = raiz;


            for (int i = 0; i < tamanio; i++)
            {
                if (i == index)
                {
                    actual.anterior.siguiente = actual.siguiente;
                    actual.siguiente.anterior = actual.anterior;
                    break;
                }
                actual = actual.siguiente;
            }
        }

        public void remove(T valor )
        {
            Nodo<T> actual = raiz;
            for (int i = 0; i < tamanio; i++)
            {
                
                if (actual.Equals(valor))
                {
                    actual.anterior.siguiente = actual.siguiente;
                    actual.siguiente.anterior = actual.anterior;
                    break;
                }
                actual = actual.siguiente;
            }
        }

        public bool isEmpty()
        {
            return tamanio == 0;
        }
        
        public int count(T valor)
        {
            int result = 0;

            Nodo<T> actual = raiz;
            for (int i = 0; i < tamanio; i++)
            {
                if (actual.Equals(valor)) result++;
                actual = actual.siguiente;
            }

            return result;
        }

        public bool contains(T valor)
        {
            bool result = false;

            Nodo<T> actual = raiz;
            for (int i = 0; i < tamanio; i++)
            {
                if (actual.Equals(valor))
                {
                    result = true;
                    break;
                }
                actual = actual.siguiente;
            }

            return result;
        }

        public void extends(T[] iterable)
        {
            foreach (var item in iterable)
            {
                AgregaNodo(item);
            }
        }

        public void reverse()
        {
            raiz = last();
            Nodo<T> actual = raiz;
            Nodo<T> temp;

            for (int i = 0; i < tamanio; i++)
            {
                temp = actual.siguiente;
                actual.siguiente = actual.anterior;
                actual.anterior = temp;
            }
        }
        public Nodo<T> last()
        {
            Nodo<T> result = raiz;
            for (int i = 0; i < tamanio; i++)
            {
                result = result.siguiente;
            }
            return result;
        }
        

       
    }
}
