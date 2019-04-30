using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreGeneric.classes
{
    class MoreArray<T> where T : IComparable
    {
        private MoreNode<T> root { get; set; }

        protected int size = 0;
        public int Size { get { return size;  } }


        public void Add(MoreNode<T> MoreNode)
        {
            if (root == null) root = MoreNode;
            else
            {
                _last().link = MoreNode;
            }
            size++;
        }

        public void Add(T valor)
        {
            Add(new MoreNode<T>(valor));
        }

        public override string ToString()
        {
            string result = "";
            MoreNode<T> current = root;

            for (int i = 0; i < size; i++)
            {
                result += $"{current.value}" + (current.link == null ? "" : ",");
                current = current.link;
            }

            return result;
        }

        public void insert(T value, int index)
        {
            if (index < 0 || index >= size) throw new System.ArgumentException("Index out of range");
            MoreNode<T> MoreNode = new MoreNode<T>(value);

            if (index == 0)
            {
                MoreNode.link = root;
                root = MoreNode;
            }
            else if (index == size - 1)
            {
                _last().link = MoreNode;
            }
            else
            {
                MoreNode<T> current = root;

                for (int i = 0; i < size; i++)
                {
                    if (i + 1 == index)
                    {
                        MoreNode.link = current.link;
                        current.link = MoreNode;
                        break;
                    }
                    current = current.link;
                }
            }

            size++;
        }

        public void pop(int index)
        {
            if (index < 0 || index >= size) throw new System.ArgumentException("Index out of range");
            MoreNode<T> current = root;

            if (index == 0) root = root.link;
            else
            {
                for (int i = 0; i < size; i++)
                {
                    if (i + 1 == index)
                    {
                        current.link = current.link.link;
                        break;
                    }
                    current = current.link;
                }
            }

            size--;
        }

        public void remove(T value)
        {
            if (root.value.Equals(value))
            {
                root = root.link;
                size--;
            }
            else
            {
                MoreNode<T> current = root;

                for (int i = 0; i < size; i++)
                {
                    if (current.link != null && current.link.value.Equals(value))
                    {
                        current.link = current.link.link;
                        size--;
                        break;
                    }
                    current = current.link;
                }
            }
        }

        public bool isEmpty()
        {
            return size == 0;
        }

        public int count(T valor)
        {
            int result = 0;
            MoreNode<T> current = root;

            for (int i = 0; i < size; i++)
            {
                if (current.value.Equals(valor)) result++;
                current = current.link;
            }

            return result;
        }

        public bool contains(T valor)
        {
            bool result = false;
            MoreNode<T> current = root;

            for (int i = 0; i < size; i++)
            {
                if (current.value.Equals(valor))
                {
                    result = true;
                    break;
                }
                current = current.link;
            }

            return result;
        }

        public void extends(T[] iterable)
        {
            foreach (var item in iterable) Add(item);
        }

        public void reverse()
        {
            if (size == 1) return;

            MoreNode<T> ref_1 = null, ref_2 = null, ref_3 = null;
            MoreNode<T> current = root;
            MoreNode<T> new_root = _last();

            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    ref_1 = current.link;
                    current.link = null;
                    current = ref_1;
                    continue;
                }
                else if (i == 1)
                {
                    ref_2 = current.link;
                    current.link = root;
                    current = ref_2;
                    continue;
                }
                else
                {
                    ref_3 = current.link;
                    current.link = ref_1;
                    ref_1 = ref_2;
                    ref_2 = ref_3;
                    current = ref_3;
                    continue;
                }
            }

            root = new_root;
        }

        private MoreNode<T> _last()
        {
            MoreNode<T> result = root;
            for (int i = 0; i < size - 1; i++) result = result.link;
            return result;
        }

        public T last()
        {
            return _last().value;
        }

        public void EveryOther()
        {
            for (int i = 0; i < size; i++) if (i % 2 == 0) pop(i);
        }

        public void swap(int index_1, int index_2)
        {
            if (index_1 < 0 || index_1 >= size) throw new System.ArgumentException("Index 1 out of range");
            else if (index_2 < 0 || index_2 >= size) throw new System.ArgumentException("Index 2 out of range");
            else if (index_1 == index_2) throw new System.ArgumentException("Indexes need to be diffrent");

            MoreNode<T> nodo_1 = null, nodo_2 = null, current = root;

            if (index_1 == 0) nodo_1 = root;
            else if (index_2 == 0) nodo_2 = root;

            for (int i = 0; i < size; i++)
            {
                if (i + 1 == index_1)
                {
                    nodo_1 = current;
                }
                else if (i + 1 == index_2)
                {
                    nodo_2 = current;
                }

                if (nodo_1 != null && nodo_2 != null) break;

                current = current.link;
            }

            MoreNode<T> ref_1 = nodo_1.link, ref_2 = nodo_2.link.link;

            nodo_2.link.link = nodo_1.link.link;
            nodo_1.link = nodo_2.link;

            nodo_2.link = ref_1;
            nodo_2.link.link = ref_2;

            if (index_1 == 0) root = nodo_1;
            else if (index_2 == 0) root = nodo_2;
        }

        private MoreNode<T> _get(int index)
        {
            if (index >= size || index < 0) throw new System.ArgumentException("Index out of range");

            MoreNode<T> result = root;

            for (int i = 0; i < size; i++)
            {
                if (i == index) break;

                result = result.link;
            }

            return result;
        }

        public T get(int index)
        {
            return _get(index).value;
        }

        public void clear()
        {
            root = null;
            size = 0;
        }
    }
}
