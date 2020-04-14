using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Red_tree
{
    class ReverceInput
    {
        public static void Input(RB tree)
        {
            string help = "1 x - Add(x)\n2 x - Delete(x)\n3 x - Find(x)\n4   - Min()\n5   - Max()\n6 x - FindNext(x)\n7 x - FindPrevious(x)";
            Console.WriteLine(help);
            var button = Console.ReadLine();
            while (true)
            {
                if (button == "") Console.WriteLine("Line is Empty");
                else
                    if (button == "help") Console.WriteLine('\n' + help);
                    else
                        switch (button[0])
                        {
                            case '1':
                                try 
                                {
                                    tree.Add(Convert.ToDouble((button.Split(' ')[1])));
                                    PrintOfTree.Print(tree.Root);
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Incorrect expression with Add(x)");
                                }

                                break;
                            case '2':
                                try
                                {
                                    if (double.IsNaN(tree.Delete1(Convert.ToDouble(button.Split(' ')[1]))))
                                        Console.WriteLine("Node does not exist");
                                   else PrintOfTree.Print(tree.Root);
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Incorrect expression with Delete(x)");
                                }
                                break;
                            case '3':
                                Console.Write("Color of node: ");
                                try
                                {
                                    if (tree.Find(Convert.ToInt32(button.Split(' ')[1])) == Color.NaN)
                                    {
                                        Console.WriteLine("Node does not exist");
                                    }
                                    else Console.WriteLine(tree.Find(Convert.ToInt32(button.Split(' ')[1])));
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Incorrect expression with Find(x)");
                                }                        
                                break;
                            case '4':
                                Console.Write("Min node: ");
                                if (button == "4") Console.WriteLine(tree.Min().Value);
                                else Console.WriteLine("Incorrect expression with Min()");
                                break;
                            case '5':
                                Console.Write("Max node: ");
                                if (button == "5") Console.WriteLine(tree.Max().Value);
                                else Console.WriteLine("Incorrect expression with Max()");
                                break;
                            case '6':
                                try
                                {
                                    if (tree.FindNext(Convert.ToInt32(button.Split(' ')[1])) == null)
                                        Console.WriteLine("FindNext: Node does not exist");
                                    else
                                    {
                                        Console.Write("Next node of {0}: ", button.Split(' ')[1]);
                                        Console.WriteLine(tree.FindNext(Convert.ToInt32(button.Split(' ')[1])).Value);
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Incorrect expression with FindNext(x)");
                                }
                                break;
                            case '7':
                                try
                                {
                                    if (tree.FindPrev(Convert.ToInt32(button.Split(' ')[1])) == null)
                                        Console.WriteLine("FindPrevious: Node does not exist");
                                    else
                                    {
                                        Console.Write("Previous node of {0}: ", button.Split(' ')[1]);
                                        Console.WriteLine(tree.FindPrev(Convert.ToInt32(button.Split(' ')[1])).Value);
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Incorrect expression with FindPrevious(x)");
                                }
                                break;
                            default:
                                Console.WriteLine("Incorrect input. Please try again");
                                break;
                        }
                button = Console.ReadLine();
            }
        }
    }
}
