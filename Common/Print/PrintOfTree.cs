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

        public static void Print(this Node root, int topMargin = 2, int leftMargin = 25)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            //хождение по дереву
            for (int lvl = 0; next != null; lvl++)
            {
                var nodeInfo = new NodeInfo { Node = next, Value = next.Value.ToString() + next.Colour.ToString() };
                nodeInfo.Color = next.Colour;
                if (lvl < last.Count)
                {
                    nodeInfo.StartPos = last[lvl].EndPos + 1;
                    last[lvl] = nodeInfo;
                }
                else
                {
                    nodeInfo.StartPos = leftMargin;
                    last.Add(nodeInfo);
                }
                if (lvl > 0)
                {
                    nodeInfo.Parent = last[lvl - 1];
                    if (next == nodeInfo.Parent.Node.Left)
                    {
                        nodeInfo.Parent.Left = nodeInfo;
                        nodeInfo.EndPos = Math.Max(nodeInfo.EndPos, nodeInfo.Parent.StartPos);
                    }
                    else
                    {
                        nodeInfo.Parent.Right = nodeInfo;
                        nodeInfo.StartPos = Math.Max(nodeInfo.StartPos, nodeInfo.Parent.EndPos);
                    }
                }
                next = next.Left ?? next.Right;
                for (; next == null; nodeInfo = nodeInfo.Parent)
                {
                    Print(nodeInfo, rootTop + 2 * lvl);
                    if (--lvl < 0) break;
                    if (nodeInfo == nodeInfo.Parent.Left)
                    {
                        nodeInfo.Parent.StartPos = nodeInfo.EndPos;
                        next = nodeInfo.Parent.Node.Right;
                    }
                    else
                    {
                        if (nodeInfo.Parent.Left == null)
                            nodeInfo.Parent.EndPos = nodeInfo.StartPos;
                        else
                            nodeInfo.Parent.StartPos += (nodeInfo.StartPos - nodeInfo.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(NodeInfo nodeInfo, int top)
        {
            SwapColors(nodeInfo.Color);
            Print(nodeInfo.Value, top, nodeInfo.StartPos);
            Console.ResetColor();
            if (nodeInfo.Left != null)
                PrintLink(top + 1, "┌", "┘", nodeInfo.Left.StartPos + nodeInfo.Left.Size / 2, nodeInfo.StartPos);
            if (nodeInfo.Right != null)
                PrintLink(top + 1, "└", "┐", nodeInfo.EndPos - 1, nodeInfo.Right.StartPos + nodeInfo.Right.Size / 2);
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

        private static void SwapColors(Color color)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (color == Color.R)
                Console.BackgroundColor = ConsoleColor.Red;
            else Console.BackgroundColor = ConsoleColor.Blue;
            // цвет, на фоне которого выводятся символы - Console.BackgroundColor
            // цвет, которым выводятся символы - Console.ForegroundColor
        }
    }
}
