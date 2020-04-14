using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Drawing;

namespace Black_Red_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RB();
            var random = new Random();
            for (int i = 0; i < 26; i++)
            {
                var node = random.Next(1, 100);
                if (tree.FindKey(node) == null)
                    tree.Add(node);
            }
            PrintOfTree.Print(tree.Root);
            ReverceInput.Input(tree);
            Console.ReadLine();
        }
    }
}
