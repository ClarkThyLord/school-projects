using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericClasses
{
    class Lista<T> where T: IComparable
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
            if (index >= tamanio || index < 0) throw new System.ArgumentException("Index out of range");
            Nodo<T> anterior = null;
            Nodo<T> actual = raiz;
            Nodo<T> temp = new Nodo<T>(valor);

            for (int i = 0; i < tamanio; i++)
            {
                if (i == index)
                {
                    if (i != 0) anterior.siguiente = temp;
                    temp.siguiente = actual;
                    tamanio++;
                    break;
                }
                anterior = actual;
                actual = actual.siguiente;
            }
        }
        public void pop(int index)
        {
            Nodo<T> anterior = null;
            Nodo<T> actual = raiz;
            if (index >= tamanio || index < 0) throw new System.ArgumentException("Index out of range");

            for (int i = 0; i < tamanio; i++)
            {
                if (i == 0)
                {
                    raiz = actual.siguiente;
                }
                else if (i == index)
                {
                    anterior.siguiente = actual.siguiente;
                    tamanio--;
                    break;
                }
                anterior = actual;
                actual = actual.siguiente;
            }
        }

        public void remove(T valor )
        {
            Nodo<T> anterior = null;
            Nodo<T> actual = raiz;
            for (int i = 0; i < tamanio; i++)
            {
                if (actual.valor.Equals(valor))
                {
                    anterior.siguiente = actual.siguiente;
                    tamanio--;
                    break;
                }
                anterior = actual;
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
                if (actual.valor.Equals(valor)) result++;
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
                if (actual.valor.Equals(valor))
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
                tamanio++;
            }
        }

        public void reverse()
        {
            Nodo<T> new_raiz = _last();
            Nodo<T> actual = raiz;

            for (int i = 0; i < tamanio; i++)
            {
                actual.siguiente = new_raiz.siguiente;
                new_raiz.siguiente = actual;

                actual = actual.siguiente;
            }
        }

        private Nodo<T> _last()
        {
            Nodo<T> result = raiz;
            for (int i = 0; i < tamanio; i++)
            {
                result = result.siguiente;
            }
            return result;
        }

        public T last()
        {
            Nodo<T> result = raiz;
            for (int i = 0; i < tamanio; i++)
            {
                result = result.siguiente;
            }
            return result.valor;
        }

        public void EveryOther()
        {
            for (int i = 0; i < tamanio; i++)
            {
                if (i%2==-0)
                {
                    pop(i);
                }
            }
        }

        

        private void swap(int index_1, int index_2)
        {
            if (index_1 >= tamanio || index_1 < 0) throw new System.ArgumentException("Index 1 out of range");
            if (index_2 >= tamanio || index_2 < 0) throw new System.ArgumentException("Index 2 out of range");

            Nodo<T> nodoA = null;
            Nodo<T> nodoB = null;
            Nodo<T> nodoC = null;
            Nodo<T> nodoD = null;

            Nodo<T> current = raiz;
            Nodo<T> previous = raiz;
            Nodo<T> temp = null;


            for (int i = 0; i < tamanio; i++)
            {
                if (i == index_1)
                {
                    nodoA = previous;
                    nodoB = current;
                }
                if (i == index_2)
                {
                    nodoC = previous;
                    nodoD = current;
                }

                previous = current;
                current = current.siguiente;
            }

            nodoA.siguiente = nodoD;
            nodoC.siguiente = nodoB;

            temp = nodoD.siguiente;
            nodoD.siguiente = nodoB.siguiente;
            nodoB.siguiente = temp;
        }
    }
}
