using System;

namespace GenericClasses
{
    class Nodo<T> where T : IComparable
    {
        public T value { get; set; }
        public Nodo<T> link { get; set; }
      
        public Nodo(T value)
        {
            this.value = value;
        }

        public Nodo(T value, Nodo<T> link)
        {
            this.value = value;
            this.link = link;
        } 
    }
}
