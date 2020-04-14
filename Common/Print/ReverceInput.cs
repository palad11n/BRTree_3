using Black_Red_tree.Common;
using System;

namespace Black_Red_tree
{
    class ReverceInput
    {
        public static void Input(ITree tree)
        {
            string help = "1 x - AddNode(x)\n2 x - DeleteNode(x)\n3 x - FindColor(x)\n4   - MinNode()\n5   - MaxNode()\n6 x - FindNext(x)\n7 x - FindPrevious(x)\nh - help";
            Console.WriteLine(help);
            var button = Console.ReadLine();
            while (true)
            {
                try
                {
                    switch (button[0])
                    {
                        case '\0':
                            Console.WriteLine("Line is Empty!!");
                            break;
                        case 'h':
                            Console.WriteLine('\n' + help);
                            break;
                        case '1':
                            tree.AddNode(Convert.ToDouble((button.Split(' ')[1])));
                            PrintOfTree.Print(tree.Root);
                            break;
                        case '2':
                            if (double.IsNaN(tree.RemoveNode(Convert.ToDouble(button.Split(' ')[1]))))
                                Console.WriteLine("Node does not exist.");
                            else PrintOfTree.Print(tree.Root);

                            break;
                        case '3':
                            int value = Convert.ToInt32(button.Split(' ')[1]);
                            if (tree.GetColorNodeByKey(value) == Color.NaN)
                                Console.WriteLine("Node does not exist.");
                            else Console.WriteLine("Color of node: " + tree.GetColorNodeByKey(value));
                            break;
                        case '4':
                            var min = tree.MinNode();
                            Console.WriteLine("Min node: " + ((min == null) ? "Tree is empty" : min.Value.ToString()));
                            break;
                        case '5':
                            var max = tree.MaxNode();
                            Console.WriteLine("Max node: " + ((max == null) ? "Tree is empty" : max.Value.ToString()));
                            break;
                        case '6':
                            value = Convert.ToInt32(button.Split(' ')[1]);
                            if (tree.FindNextNode(value) == null)
                                Console.WriteLine("FindNext: Node does not exist.");
                            else Console.WriteLine("Next node of {0}: {1}", value, tree.FindNextNode(value).Value);
                            break;
                        case '7':
                            value = Convert.ToInt32(button.Split(' ')[1]);
                            if (tree.FindPrevNode(value) == null)
                                Console.WriteLine("FindPrevious: Node does not exist.");
                            else Console.WriteLine("Previous node of {0}: {1}", value, tree.FindPrevNode(value).Value);
                            break;
                        default:
                            Console.WriteLine("Incorrect input. Please try again.");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect expression.");
                }
                button = Console.ReadLine();
            }
        }
    }
}