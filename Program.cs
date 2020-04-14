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
            int[] nodes = { 5,23,32,19,39,69,96,29,76,67};
            int[] nodes1= { 25, 8, 52, 46, 22, 78 };
            ITree tree = new RBTree();
            var random = new Random();// nodes[i];
            for (int i = 0; i < nodes.Length; i++)
            {
                var node = random.Next(1, 100);
                if (tree.FindNodeByKey(node) == null)
                    tree.AddNode(node);
            }
            PrintOfTree.Print(tree.Root);
            ReverceInput.Input(tree);

            Console.ReadLine();
        }
    }
}
