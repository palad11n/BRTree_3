using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Drawing;
using Black_Red_tree.Common;

namespace Black_Red_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nodes1 = { 10, 42, 20, 36, 92, 18 };
            int[] nodes = { 25, 8, 52, 46, 22, 78 };
            ITree tree = new RBTree();
            var random = new Random();//random.Next(1, 100);
            for (int i = 0; i < nodes.Length; i++)
            {
                var node = nodes[i];
                if (tree.FindNodeByKey(node) == null)
                    tree.AddNode(node);
            }
            PrintOfTree.Print(tree.Root);
            ReverceInput.Input(tree);

            Console.ReadLine();
        }
    }
}
