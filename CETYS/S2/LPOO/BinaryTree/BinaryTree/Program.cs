using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTree.classes;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            classes.BinaryTree tree = new classes.BinaryTree();

            tree.Add(1);
            tree.Add(100);
            tree.Add(new Node(10));
            tree.Add(77);
            tree.Add(-37);
            tree.Add(new Node(-1));

            Console.WriteLine(tree.Contains(100));
            Console.WriteLine(tree.Contains(10));
            Console.WriteLine(tree.Contains(-100));
            Console.WriteLine(tree.Contains(-36));
            Console.WriteLine(tree.Contains(-37));

            Console.ReadKey();
        }
    }
}
