using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Black_Red_tree.RBTree;

namespace Black_Red_tree
{
    static class PrintOfTree
    {
        class NodeInfo
        {
            public Node Node;
            public string Value;
            public Color Color;
            public int StartPos;
            public int Size { get { return Value.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        public static void Print(this Node root, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            //хождение по дереву
            for (int lvl = 0; next != null; lvl++)
            {
                var item = new NodeInfo { Node = next, Value = next.Value.ToString() + next.Colour.ToString() };
                item.Color = next.Colour;
                if (lvl < last.Count)
                {
                    item.StartPos = last[lvl].EndPos + 1;
                    last[lvl] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (lvl > 0)
                {
                    item.Parent = last[lvl - 1];
                    if (next == item.Parent.Node.Left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                    }
                }
                next = next.Left ?? next.Right;
                for (; next == null; item = item.Parent)
                {
                    Print(item, rootTop + 2 * lvl);
                    if (--lvl < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos;
                        next = item.Parent.Node.Right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos;
                        else
                            item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(NodeInfo item, int top)
        {
            SwapColors();
            Print(item.Value, top, item.StartPos);
            SwapColors();
            if (item.Left != null)
                PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
            if (item.Right != null)
                PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
        }
        
        private static void PrintLink(int top, string start, string end, int startPos, int endPos)
        {
            Print(start, top, startPos);
            Print("─", top, startPos + 1, endPos);
            Print(end, top, endPos);
        }

        private static void Print(string node, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0)
                right = left + node.Length;
            while (Console.CursorLeft < right) Console.Write(node);
        }

        private static void SwapColors()
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;// цвет, которым выводятся символы
            Console.BackgroundColor = ConsoleColor.Cyan; // цвет, на фоне которого выводятся символы
        }
    }
}
